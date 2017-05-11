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
    public class ControlPriorityDataAccess
    {
        private static volatile ControlPriorityDataAccess instance;
        private static object syncRoot = new Object();

        private ControlPriorityDataAccess() { }

        public static ControlPriorityDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ControlPriorityDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum ControlPrioritiesndices
        {
            ControlPriorityId,
            Name,
            Description,
            DefinitionSourceId,
            DefinitionSourceCode,
            MagpieCoreDefinitionSourceGuid,
            DefinitionSource
        }

        #endregion

        public IEnumerable<ControlPriority> GetControlPriorities(string ConnectionString, int? Id = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            #endregion

            try
            {
                string storedProcedureName = "usp_ControlPrioritiesGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var controlPriorities = new List<ControlPriority>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Id != null)
                            command.Parameters.AddWithValue("@ControlPriorityId", Id);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var cp = new ControlPriority();

                                cp.Id = reader.GetInt32((int)ControlPrioritiesndices.ControlPriorityId);

                                cp.Name = reader.GetString((int)ControlPrioritiesndices.Name);

                                if (!reader.IsDBNull((int)ControlPrioritiesndices.Description))
                                    cp.Description = reader.GetString((int)ControlPrioritiesndices.Description);

                                cp.DefinitionSource = new DefinitionSource
                                {
                                    Id = reader.GetInt32((int)ControlPrioritiesndices.DefinitionSourceId),
                                    Code = reader.GetString((int)ControlPrioritiesndices.DefinitionSourceCode),
                                    MagpieCoreDefinitionSourceGuid = (!reader.IsDBNull((int)ControlPrioritiesndices.MagpieCoreDefinitionSourceGuid)) ? reader.GetGuid((int)ControlPrioritiesndices.MagpieCoreDefinitionSourceGuid) : (Guid?)null,
                                    Source = reader.GetString((int)ControlPrioritiesndices.DefinitionSource)
                                };

                                controlPriorities.Add(cp);
                            }
                        }

                        reader.Close();

                        return controlPriorities;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public int? Create(string ConnectionString, ControlPriority ControlPriority)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (ControlPriority == null)
                throw new ArgumentNullException();

            #endregion

            try
            {
                string storedProcedureName = "usp_ControlPriorityCreate";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        int? newId = null;

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@Name", ControlPriority.Name);

                        if (ControlPriority.Description != null)
                            command.Parameters.AddWithValue("@Description", ControlPriority.Description);

                        if (ControlPriority.DefinitionSource != null)
                            command.Parameters.AddWithValue("@DefinitionSourceId", ControlPriority.DefinitionSource.Id);

                        SqlParameter outPutVal = new SqlParameter("@NewId", SqlDbType.Int);
                        outPutVal.Direction = ParameterDirection.Output;
                        command.Parameters.Add(outPutVal);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        if (outPutVal.Value != DBNull.Value)
                            newId = Convert.ToInt32(outPutVal.Value);

                        return newId;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool Update(string ConnectionString, ControlPriority ControlPriority)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (ControlPriority == null)
                throw new ArgumentNullException();

            #endregion

            try
            {
                string storedProcedureName = "usp_ControlPriorityUpdate";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@ControlPriorityId", ControlPriority.Id);
                        command.Parameters.AddWithValue("@Name", ControlPriority.Name);
                        command.Parameters.AddWithValue("@Description", ControlPriority.Description);
                        
                        if (ControlPriority.DefinitionSource != null)
                            command.Parameters.AddWithValue("@DefinitionSourceId", ControlPriority.DefinitionSource.Id);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(string ConnectionString, int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_ControlPriorityDelete";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@ControlPriorityId", Id);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
