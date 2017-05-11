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
    public sealed class UserTaskFilterDataAccess
    {
        private static volatile UserTaskFilterDataAccess instance;
        private static object syncRoot = new Object();

        private UserTaskFilterDataAccess() { }

        public static UserTaskFilterDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new UserTaskFilterDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum UserTaskFiltersIndices
        {
            FilterId,
            FilterTypeId,
            FilterType,
            FilterOwnerUserId,
            FilterName,
            AssignedStatusId,
            ControlId,
            ControlSetId,
            DueEndDate,
            DueStartDate,
            DueStatusId,
            IncludeRelations,
            ResponsibleUserId,
            TaskStateId,
            UserTaskCode,
            UserTaskId,
            WorkingSetId
        }

        private enum UserTaskFilterResultCountsIndices
        {
            FilterId,
            WorkingSetId,
            Count
        }

        #endregion

        public IEnumerable<UserTaskFilter> GetUserTaskFilters(string ConnectionString, int? Id = null, string FilterOwnerUserId = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id != null && Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_FiltersGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var filters = new List<UserTaskFilter>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Id != null)
                            command.Parameters.AddWithValue("@FilterId", Id);

                        if (!string.IsNullOrWhiteSpace(FilterOwnerUserId))
                            command.Parameters.AddWithValue("@FilterOwnerUserId", FilterOwnerUserId);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var f = new UserTaskFilter();

                                f.FilterId = reader.GetInt32((int)UserTaskFiltersIndices.FilterId);
                                f.FilterTypeId = reader.GetInt32((int)UserTaskFiltersIndices.FilterTypeId);
                                f.FilterType = reader.GetString((int)UserTaskFiltersIndices.FilterType);
                                f.FilterOwnerUserId = reader.GetString((int)UserTaskFiltersIndices.FilterOwnerUserId);
                                f.FilterName = reader.GetString((int)UserTaskFiltersIndices.FilterName);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.AssignedStatusId))
                                    f.AssignedStatusId = reader.GetInt32((int)UserTaskFiltersIndices.AssignedStatusId);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.ControlId))
                                    f.ControlId = reader.GetInt32((int)UserTaskFiltersIndices.ControlId);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.ControlSetId))
                                    f.ControlSetId = reader.GetInt32((int)UserTaskFiltersIndices.ControlSetId);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.DueEndDate))
                                    f.DueEndDate = reader.GetDateTime((int)UserTaskFiltersIndices.DueEndDate);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.DueStartDate))
                                    f.DueStartDate = reader.GetDateTime((int)UserTaskFiltersIndices.DueStartDate);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.DueStatusId))
                                    f.DueStatusId = reader.GetInt32((int)UserTaskFiltersIndices.DueStatusId);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.IncludeRelations))
                                    f.IncludeRelations = reader.GetBoolean((int)UserTaskFiltersIndices.IncludeRelations);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.ResponsibleUserId))
                                    f.ResponsibleUserId = reader.GetString((int)UserTaskFiltersIndices.ResponsibleUserId);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.TaskStateId))
                                    f.TaskStateId = reader.GetInt32((int)UserTaskFiltersIndices.TaskStateId);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.UserTaskCode))
                                    f.UserTaskCode = reader.GetString((int)UserTaskFiltersIndices.UserTaskCode);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.UserTaskId))
                                    f.UserTaskId = reader.GetInt32((int)UserTaskFiltersIndices.UserTaskId);

                                if (!reader.IsDBNull((int)UserTaskFiltersIndices.WorkingSetId))
                                    f.WorkingSetId = reader.GetInt32((int)UserTaskFiltersIndices.WorkingSetId);

                                filters.Add(f);
                            }
                        }

                        reader.Close();

                        return filters;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public IEnumerable<UserTaskFilterResultCount> GetUserTaskFilterResultCounts(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            #endregion

            try
            {
                string storedProcedureName = "usp_GetUserTaskFilterResultCounts";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var userTaskFilterResultCounts = new List<UserTaskFilterResultCount>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var utfrc = new UserTaskFilterResultCount();

                                utfrc.FilterId = reader.GetInt32((int)UserTaskFilterResultCountsIndices.FilterId);
                                utfrc.WorkingSetId = reader.GetInt32((int)UserTaskFilterResultCountsIndices.WorkingSetId);
                                utfrc.Count = reader.GetInt32((int)UserTaskFilterResultCountsIndices.Count);

                                userTaskFilterResultCounts.Add(utfrc);
                            }
                        }

                        reader.Close();

                        return userTaskFilterResultCounts;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public int? Create(string ConnectionString, UserTaskFilter UserTaskFilter)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (UserTaskFilter == null)
                throw new ArgumentNullException();

            #endregion

            try
            {
                string storedProcedureName = "usp_FilterCreate";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        int? newId = null;

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@FilterOwnerUserId", UserTaskFilter.FilterOwnerUserId);
                        command.Parameters.AddWithValue("@FilterName", UserTaskFilter.FilterName);
                        command.Parameters.AddWithValue("@AssignedStatusId", UserTaskFilter.AssignedStatusId);
                        command.Parameters.AddWithValue("@ControlId", UserTaskFilter.ControlId);
                        command.Parameters.AddWithValue("@ControlSetId", UserTaskFilter.ControlSetId);
                        command.Parameters.AddWithValue("@DueEndDate", UserTaskFilter.DueEndDate);
                        command.Parameters.AddWithValue("@DueStartDate", UserTaskFilter.DueStartDate);
                        command.Parameters.AddWithValue("@DueStatusId", UserTaskFilter.DueStatusId);
                        command.Parameters.AddWithValue("@IncludeRelations", UserTaskFilter.IncludeRelations);
                        command.Parameters.AddWithValue("@ResponsibleUserId", UserTaskFilter.ResponsibleUserId);
                        command.Parameters.AddWithValue("@TaskStateId", UserTaskFilter.TaskStateId);
                        command.Parameters.AddWithValue("@UserTaskCode", UserTaskFilter.UserTaskCode);
                        command.Parameters.AddWithValue("@UserTaskId", UserTaskFilter.UserTaskId);
                        command.Parameters.AddWithValue("@WorkingSetId", UserTaskFilter.WorkingSetId);

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

        public bool Update(string ConnectionString, UserTaskFilter UserTaskFilter)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (UserTaskFilter == null)
                throw new ArgumentNullException();

            #endregion

            try
            {
                string storedProcedureName = "usp_FilterUpdate";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@FilterId", UserTaskFilter.FilterId);
                        command.Parameters.AddWithValue("@FilterOwnerUserId", UserTaskFilter.FilterOwnerUserId);
                        command.Parameters.AddWithValue("@FilterName", UserTaskFilter.FilterName);
                        command.Parameters.AddWithValue("@AssignedStatusId", UserTaskFilter.AssignedStatusId);
                        command.Parameters.AddWithValue("@ControlId", UserTaskFilter.ControlId);
                        command.Parameters.AddWithValue("@ControlSetId", UserTaskFilter.ControlSetId);
                        command.Parameters.AddWithValue("@DueEndDate", UserTaskFilter.DueEndDate);
                        command.Parameters.AddWithValue("@DueStartDate", UserTaskFilter.DueStartDate);
                        command.Parameters.AddWithValue("@DueStatusId", UserTaskFilter.DueStatusId);
                        command.Parameters.AddWithValue("@IncludeRelations", UserTaskFilter.IncludeRelations);
                        command.Parameters.AddWithValue("@ResponsibleUserId", UserTaskFilter.ResponsibleUserId);
                        command.Parameters.AddWithValue("@TaskStateId", UserTaskFilter.TaskStateId);
                        command.Parameters.AddWithValue("@UserTaskCode", UserTaskFilter.UserTaskCode);
                        command.Parameters.AddWithValue("@UserTaskId", UserTaskFilter.UserTaskId);
                        command.Parameters.AddWithValue("@WorkingSetId", UserTaskFilter.WorkingSetId);

                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();

                        return true;
                    }
                }
            }
            catch (Exception ex)
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
                string storedProcedureName = "usp_FilterDelete";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@FilterId", Id);

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
