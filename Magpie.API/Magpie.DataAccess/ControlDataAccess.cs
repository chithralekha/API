using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magpie.Model;

namespace Magpie.DataAccess
{
    public sealed class ControlDataAccess
    {
        private static volatile ControlDataAccess instance;
        private static object syncRoot = new Object();

        private ControlDataAccess() { }

        public static ControlDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ControlDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum ControlsIndices
        {
            ControlId,
            MagpieCoreControlGuid,
            DefinitionSourceId,
            DefinitionSourceCode,
            MagpieCoreDefinitionSourceGuid,
            DefinitionSource,
            Code,
            Title,
            ControlPriorityId,
            ControlPriorityDefinitionSourceId,
            ControlPriorityDescription,
            ControlPriorityName,
            Weight
        }

        #endregion

        public IEnumerable<Control> GetControls(string ConnectionString, int? Id = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id != null && Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_ControlsGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var controls = new List<Control>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Id != null)
                            command.Parameters.AddWithValue("@ControlId", Id);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var c = new Control();

                                c.Id = reader.GetInt32((int)ControlsIndices.ControlId);
                                c.MagpieCoreControlGuid = reader.GetGuid((int)ControlsIndices.MagpieCoreControlGuid);

                                c.DefinitionSource = new DefinitionSource
                                {
                                    Id = reader.GetInt32((int)ControlsIndices.DefinitionSourceId),
                                    Code = reader.GetString((int)ControlsIndices.DefinitionSourceCode),
                                    MagpieCoreDefinitionSourceGuid = reader.GetGuid((int)ControlsIndices.MagpieCoreDefinitionSourceGuid),
                                    Source = reader.GetString((int)ControlsIndices.DefinitionSource)
                                };

                                c.Code = reader.GetString((int)ControlsIndices.Code);
                                c.Title = reader.GetString((int)ControlsIndices.Title);

                                if (!reader.IsDBNull((int)ControlsIndices.ControlPriorityId))
                                {
                                    c.ControlPriority = new ControlPriority
                                    {
                                        Id = reader.GetInt32((int)ControlsIndices.ControlPriorityId),
                                        DefinitionSource = new DefinitionSource
                                        {
                                            Id = reader.GetInt32((int)ControlsIndices.ControlPriorityDefinitionSourceId)
                                        },
                                        Description = reader.GetString((int)ControlsIndices.ControlPriorityDescription),
                                        Name = reader.GetString((int)ControlsIndices.ControlPriorityName)
                                    };
                                }

                                c.Weight = reader.GetInt32((int)ControlsIndices.Weight);

                                controls.Add(c);
                            }
                        }

                        reader.Close();

                        return controls;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public int? Create(string ConnectionString, Control Control)
        {
            throw new NotImplementedException();
        }

        public bool Update(string ConnectionString, Control Control)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ConnectionString, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
