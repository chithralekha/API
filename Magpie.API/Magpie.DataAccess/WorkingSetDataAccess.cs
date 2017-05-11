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
    public sealed class WorkingSetDataAccess
    {
        private static volatile WorkingSetDataAccess instance;
        private static object syncRoot = new Object();

        private WorkingSetDataAccess() { }

        public static WorkingSetDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new WorkingSetDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum WorkingSetsIndices
        {
            WorkingSetId,
            WorkingSetGuid,
            WorkingSetTemplateId,
            Name,
            Description,
            DeployedByUserId,
            DeployedByUsername,
            Deployed,
            City,
            StateId,
            State,
            ZipCode,
            Compliance

        }

        private enum WorkingSetTemplatesIndices
        {
            WorkingSetTemplateId,
            WorkingSetTemplateGuid,
            Name,
            CreatedByUserId,
            CreatedByUserName,
            Created
        }



        private enum WorkingSetsDataPointIndices
        {
            WorkingSetId,
            Name,
            TotalTasks,
            TotalNew,
            TotalInProgress,
            TotalCompleted,
            TotalOnTime,
            TotalOverdue,
            TotalInJeopardy,
            TotalAssigned,
            TotalUnAssigned
        }

        #endregion

        public IEnumerable<WorkingSet> GetWorkingSets(string ConnectionString, int? Id = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id != null && Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_WorkingSetsGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var workingSets = new List<WorkingSet>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Id != null)
                            command.Parameters.AddWithValue("@WorkingSetId", Id);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var ws = new WorkingSet();

                                ws.WorkingSetId = reader.GetInt32((int)WorkingSetsIndices.WorkingSetId);

                                ws.WorkingSetGuid = reader.GetGuid((int)WorkingSetsIndices.WorkingSetGuid);
                                ws.WorkingSetTemplateId = reader.GetInt32((int)WorkingSetsIndices.WorkingSetTemplateId);
                                ws.Name = reader.GetString((int)WorkingSetsIndices.Name);

                                if (!reader.IsDBNull((int)WorkingSetsIndices.Description))
                                    ws.Description = reader.GetString((int)WorkingSetsIndices.Description);

                                ws.DeployedByUserId = reader.GetString((int)WorkingSetsIndices.DeployedByUserId);
                                ws.DeployedByUsername = reader.GetString((int)WorkingSetsIndices.DeployedByUsername);
                                ws.Deployed = reader.GetDateTime((int)WorkingSetsIndices.Deployed);

                                if (!reader.IsDBNull((int)WorkingSetsIndices.City))
                                    ws.City = reader.GetString((int)WorkingSetsIndices.City);

                                if (!reader.IsDBNull((int)WorkingSetsIndices.StateId))
                                    ws.StateId = reader.GetInt32((int)WorkingSetsIndices.StateId);

                                if (!reader.IsDBNull((int)WorkingSetsIndices.State))
                                    ws.State = reader.GetString((int)WorkingSetsIndices.State);

                                if (!reader.IsDBNull((int)WorkingSetsIndices.ZipCode))
                                    ws.ZipCode = reader.GetString((int)WorkingSetsIndices.ZipCode);

                                if (!reader.IsDBNull((int)WorkingSetsIndices.Compliance))
                                    ws.Compliance = reader.GetInt32((int)WorkingSetsIndices.Compliance);

                                workingSets.Add(ws);
                            }
                        }

                        reader.Close();

                        foreach (var ws in workingSets)
                        {
                            ws.WorkingSetDataPoint = GetWorkingSetDataPoint(ConnectionString, ws.WorkingSetId, ws.Compliance);
                        }

                        return workingSets;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }


        private WorkingSetDataPoint GetWorkingSetDataPoint(string ConnectionString, int id, int? compliancePercent)
        {
            try
            {
                WorkingSetDataPoint workingSetDataPoint = new WorkingSetDataPoint();

                string storedProcedureName = "uspGetTaskStatisticsDeprecated";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        command.Parameters.AddWithValue("@WorkingSetId", id);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                workingSetDataPoint.WorkingSetId = reader.GetInt32((int)WorkingSetsIndices.WorkingSetId);

                                if (compliancePercent != null)
                                    workingSetDataPoint.CompliancePercent = compliancePercent.Value;

                                if (!reader.IsDBNull((int)WorkingSetsDataPointIndices.TotalTasks))
                                    workingSetDataPoint.TotalTasks = reader.GetInt32((int)WorkingSetsDataPointIndices.TotalTasks);
                                if (!reader.IsDBNull((int)WorkingSetsDataPointIndices.TotalInProgress))
                                    workingSetDataPoint.TotalInProgress = reader.GetInt32((int)WorkingSetsDataPointIndices.TotalInProgress);
                                if (!reader.IsDBNull((int)WorkingSetsDataPointIndices.TotalCompleted))
                                    workingSetDataPoint.TotalCompleted = reader.GetInt32((int)WorkingSetsDataPointIndices.TotalCompleted);
                                if (!reader.IsDBNull((int)WorkingSetsDataPointIndices.TotalOnTime))
                                    workingSetDataPoint.TotalOnTime = reader.GetInt32((int)WorkingSetsDataPointIndices.TotalOnTime);
                                if (!reader.IsDBNull((int)WorkingSetsDataPointIndices.TotalOverdue))
                                    workingSetDataPoint.TotalOverdue = reader.GetInt32((int)WorkingSetsDataPointIndices.TotalOverdue);
                                if (!reader.IsDBNull((int)WorkingSetsDataPointIndices.TotalInJeopardy))
                                    workingSetDataPoint.TotalInJeopardy = reader.GetInt32((int)WorkingSetsDataPointIndices.TotalInJeopardy);
                            }
                        }
                        reader.Close();
                    }
                }

                return workingSetDataPoint;
            }

            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }
          

        public int? Create(string ConnectionString, WorkingSet WorkingSet)
        {
            throw new NotImplementedException();
        }

        public bool Update(string ConnectionString, WorkingSet WorkingSet)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ConnectionString, int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<WorkingSetTemplate> GetWorkingSetTemplates(string ConnectionString, int? Id = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id != null && Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var workingSetTemplates = new List<WorkingSetTemplate>();

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        string storedProcedureName = "usp_WorkingSetTemplatesGet";

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Id != null)
                            command.Parameters.AddWithValue("@WorkingSetTemplateId", Id);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var wst = new WorkingSetTemplate();

                                wst.WorkingSetTemplateId = reader.GetInt32((int)WorkingSetTemplatesIndices.WorkingSetTemplateId);

                                wst.WorkingSetTemplateGuid = reader.GetGuid((int)WorkingSetTemplatesIndices.WorkingSetTemplateGuid);
                                wst.Name = reader.GetString((int)WorkingSetTemplatesIndices.Name);

                                wst.CreatedByUserId = reader.GetString((int)WorkingSetTemplatesIndices.CreatedByUserId);
                                wst.CreatedByUserName = reader.GetString((int)WorkingSetTemplatesIndices.CreatedByUserName);
                                wst.Created = reader.GetDateTime((int)WorkingSetTemplatesIndices.Created);

                                workingSetTemplates.Add(wst);
                            }
                        }

                        reader.Close();
                    }
                }

                foreach (var wst in workingSetTemplates)
                {
                    wst.ControlSets = ControlSetDataAccess.Instance.GetControlSets(ConnectionString, wst.WorkingSetTemplateId);
                }

                return workingSetTemplates;
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }
    }
}
