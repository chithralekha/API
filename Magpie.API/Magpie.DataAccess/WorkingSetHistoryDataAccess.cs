using Magpie.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Magpie.DataAccess
{
    public sealed class WorkingSetHistoryDataAccess
    {
        private static volatile WorkingSetHistoryDataAccess instance;
        private static object syncRoot = new Object();

        private WorkingSetHistoryDataAccess() { }

        public static WorkingSetHistoryDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new WorkingSetHistoryDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum WorkingSetsHistoryIndices
        {
            id,
            ControlSetId,
            WorkingSetId,
            Timestamp,
            TotalTasks,
            TotalNew,
            TotalInProgress,
            TotalInJeopardy,
            TotalOverdue,
            TotalCompleted,
            CompliancePercent,
            TotalOnTime
        }

        #endregion

        public IEnumerable<WorkingSetDataPoint> GetWorkingSetHistory(string ConnectionString, int Id, DateTime? startDate, DateTime? endDate)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            if ((startDate == null) && (endDate != null))
                throw new ArgumentOutOfRangeException();

            if ((startDate != null) && (endDate != null) && (startDate > endDate))
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_HistoricStatisticsGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var dataPoints = new List<WorkingSetDataPoint>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@WorkingSetId", Id);
                        command.Parameters.AddWithValue("@startDate", startDate);
                        command.Parameters.AddWithValue("@endDate", endDate);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var dp = new WorkingSetDataPoint();

                                dp.WorkingSetId = reader.GetInt32((int)WorkingSetsHistoryIndices.WorkingSetId);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.ControlSetId))
                                    dp.ControlSetId = reader.GetInt32((int)WorkingSetsHistoryIndices.ControlSetId);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.Timestamp))
                                    dp.Timestamp = reader.GetDateTime((int)WorkingSetsHistoryIndices.Timestamp);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.TotalTasks))
                                    dp.TotalTasks = reader.GetInt32((int)WorkingSetsHistoryIndices.TotalTasks);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.TotalNew))
                                    dp.TotalNew = reader.GetInt32((int)WorkingSetsHistoryIndices.TotalNew);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.TotalInProgress))
                                    dp.TotalInProgress = reader.GetInt32((int)WorkingSetsHistoryIndices.TotalInProgress);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.TotalInJeopardy))
                                    dp.TotalInJeopardy = reader.GetInt32((int)WorkingSetsHistoryIndices.TotalInJeopardy);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.TotalOverdue))
                                    dp.TotalOverdue = reader.GetInt32((int)WorkingSetsHistoryIndices.TotalOverdue);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.TotalCompleted))
                                    dp.TotalCompleted = reader.GetInt32((int)WorkingSetsHistoryIndices.TotalCompleted);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.TotalOnTime))
                                    dp.TotalOnTime = reader.GetInt32((int)WorkingSetsHistoryIndices.TotalOnTime);

                                if (!reader.IsDBNull((int)WorkingSetsHistoryIndices.CompliancePercent))
                                    dp.CompliancePercent = reader.GetInt32((int)WorkingSetsHistoryIndices.CompliancePercent);

                                dataPoints.Add(dp);
                            }
                        }

                        reader.Close();

                        return dataPoints;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }
    }
}
