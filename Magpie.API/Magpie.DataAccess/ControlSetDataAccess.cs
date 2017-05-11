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
    public sealed class ControlSetDataAccess
    {
        private static volatile ControlSetDataAccess instance;
        private static object syncRoot = new Object();

        private ControlSetDataAccess() { }

        public static ControlSetDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ControlSetDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum ControlSetsIndices
        {
            ControlSetId,
            MagpieCoreControlSetGuid,
            DefinitionSourceId,
            DefinitionSourceCode,
            MagpieCoreDefinitionSourceGuid,
            DefinitionSource,
            Version,
            ControlSetClassificationId,
            ControlSetClassification,
            Code,
            Title
        }

        #endregion

        public IEnumerable<ControlSet> GetControlSets(string ConnectionString, int? Id = null, int? DefinitionSourceId = null, int? ControlSetClassificationId = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id != null && Id <= 0)
                throw new ArgumentOutOfRangeException();

            if (DefinitionSourceId != null && DefinitionSourceId <= 0)
                throw new ArgumentOutOfRangeException();

            if (ControlSetClassificationId != null && ControlSetClassificationId <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            List<int> idsList = null;

            if (Id != null)
            {
                idsList = new List<int>();
                idsList.Add(Id.Value);
            }

            return GetControlSets(ConnectionString, idsList, DefinitionSourceId, ControlSetClassificationId);
        }

        public IEnumerable<ControlSet> GetControlSets(string ConnectionString, IEnumerable<int> Ids, int? DefinitionSourceId = null, int? ControlSetClassificationId = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Ids != null && Ids.Count() == 0)
                throw new ArgumentOutOfRangeException();

            if (DefinitionSourceId != null && DefinitionSourceId <= 0)
                throw new ArgumentOutOfRangeException();

            if (ControlSetClassificationId != null && ControlSetClassificationId <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_ControlSetsGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var controlSets = new List<ControlSet>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Ids != null)
                        {
                            var dataTable = new DataTable();
                            dataTable.Columns.Add("Id", typeof(long));

                            foreach (var item in Ids)
                            {
                                dataTable.Rows.Add(item);
                            }

                            SqlParameter controlSetIdsParameter;
                            controlSetIdsParameter = command.Parameters.AddWithValue("@ControlSetIds", dataTable);
                            controlSetIdsParameter.SqlDbType = SqlDbType.Structured;
                            controlSetIdsParameter.TypeName = "dbo.IdTVPType";
                        }

                        if (DefinitionSourceId != null)
                            command.Parameters.AddWithValue("@DefinitionSourceId", DefinitionSourceId);

                        if (ControlSetClassificationId != null)
                            command.Parameters.AddWithValue("@ControlSetClassificationId", ControlSetClassificationId);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var cs = new ControlSet();

                                cs.Id = reader.GetInt32((int)ControlSetsIndices.ControlSetId);

                                if (!reader.IsDBNull((int)ControlSetsIndices.MagpieCoreControlSetGuid))
                                    cs.MagpieCoreControlSetGuid = reader.GetGuid((int)ControlSetsIndices.MagpieCoreControlSetGuid);

                                cs.DefinitionSource = new DefinitionSource
                                {
                                    Id = reader.GetInt32((int)ControlSetsIndices.DefinitionSourceId),
                                    Code = reader.GetString((int)ControlSetsIndices.DefinitionSourceCode),
                                    Source = reader.GetString((int)ControlSetsIndices.DefinitionSource)
                                };

                                if (!reader.IsDBNull((int)ControlSetsIndices.MagpieCoreDefinitionSourceGuid))
                                    cs.DefinitionSource.MagpieCoreDefinitionSourceGuid = reader.GetGuid((int)ControlSetsIndices.MagpieCoreDefinitionSourceGuid);

                                cs.Version = reader.GetString((int)ControlSetsIndices.Version);

                                cs.ControlSetClassification = new ControlSetClassification
                                {
                                    Id = reader.GetInt32((int)ControlSetsIndices.ControlSetClassificationId),
                                    Name = reader.GetString((int)ControlSetsIndices.ControlSetClassification)
                                };

                                cs.Code = reader.GetString((int)ControlSetsIndices.Code);
                                cs.Title = reader.GetString((int)ControlSetsIndices.Title);

                                controlSets.Add(cs);
                            }
                        }

                        reader.Close();

                        return controlSets;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public IEnumerable<ControlSet> GetControlSets(string ConnectionString, int WorkingSetTemplateId)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (WorkingSetTemplateId <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_GetWorkingSetTemplateControlSets";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var controlSets = new List<ControlSet>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@WorkingSetTemplateId", WorkingSetTemplateId);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var cs = new ControlSet();

                                cs.Id = reader.GetInt32((int)ControlSetsIndices.ControlSetId);

                                if (!reader.IsDBNull((int)ControlSetsIndices.MagpieCoreControlSetGuid))
                                    cs.MagpieCoreControlSetGuid = reader.GetGuid((int)ControlSetsIndices.MagpieCoreControlSetGuid);

                                cs.DefinitionSource = new DefinitionSource
                                {
                                    Id = reader.GetInt32((int)ControlSetsIndices.DefinitionSourceId),
                                    Code = reader.GetString((int)ControlSetsIndices.DefinitionSourceCode),
                                    Source = reader.GetString((int)ControlSetsIndices.DefinitionSource)
                                };

                                if (!reader.IsDBNull((int)ControlSetsIndices.MagpieCoreDefinitionSourceGuid))
                                    cs.DefinitionSource.MagpieCoreDefinitionSourceGuid = reader.GetGuid((int)ControlSetsIndices.MagpieCoreDefinitionSourceGuid);

                                cs.Version = reader.GetString((int)ControlSetsIndices.Version);

                                cs.ControlSetClassification = new ControlSetClassification
                                {
                                    Id = reader.GetInt32((int)ControlSetsIndices.ControlSetClassificationId),
                                    Name = reader.GetString((int)ControlSetsIndices.ControlSetClassification)
                                };

                                cs.Code = reader.GetString((int)ControlSetsIndices.Code);
                                cs.Title = reader.GetString((int)ControlSetsIndices.Title);

                                controlSets.Add(cs);
                            }
                        }

                        reader.Close();

                        return controlSets;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public int? Create(string ConnectionString, ControlSet ControlSet)
        {
            throw new NotImplementedException();
        }

        public bool Update(string ConnectionString, ControlSet ControlSet)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ConnectionString, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
