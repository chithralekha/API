using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magpie.DataAccess;
using Magpie.Model;

namespace Magpie.Repository
{
    public class UserTaskRepository : IFilterableRepository<UserTask, UserTaskFilter>
    {
        public UserTaskRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<UserTask> GetItems()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var userTasks = UserTaskDataAccess.Instance.GetItems(connectionString);
            return userTasks;
        }

        public UserTask GetItem(int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var userTask = UserTaskDataAccess.Instance.GetItem(connectionString, Id);

            //ControlSet
            var controlSets = ControlSetDataAccess.Instance.GetControlSets(connectionString, userTask.ControlSetId, null, null);

            if (controlSets.Count() != 1)
                throw new Exception();

            userTask.ControlSet = controlSets.First();

            //Control
            var controls = ControlDataAccess.Instance.GetControls(connectionString, userTask.ControlId);

            if (controls.Count() != 1)
                throw new Exception();

            userTask.Control = controls.First();

            //WorkingSet
            var workingSets = WorkingSetDataAccess.Instance.GetWorkingSets(connectionString, userTask.WorkingSetId);

            if (workingSets.Count() != 1)
                throw new Exception();

            userTask.WorkingSet = workingSets.First();

            //TaskState
            //DueStatus
            //RaciTeam
            //Comments
            //Event

            return userTask;
        }

        public IEnumerable<UserTask> GetItems(int FilterId, int? OverrideWorkingSetId)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (FilterId <= 0)
                throw new ArgumentOutOfRangeException();

            if (OverrideWorkingSetId != null && OverrideWorkingSetId <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var userTasks = UserTaskDataAccess.Instance.GetItems(connectionString, FilterId, OverrideWorkingSetId);
            return userTasks;
        }

        public IEnumerable<UserTask> GetItems(int FilterId)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (FilterId <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var userTasks = UserTaskDataAccess.Instance.GetItems(connectionString, FilterId);
            return userTasks;
        }

        public IEnumerable<UserTask> GetItems(UserTaskFilter Filter)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            throw new NotImplementedException();
        }

        public int? Add(UserTask item)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (item == null)
                throw new ArgumentNullException();

            #endregion

            var newId = UserTaskDataAccess.Instance.Create(connectionString, item);
            return newId;
        }

        public bool Update(UserTask item)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (item == null)
                throw new ArgumentNullException();

            #endregion

            var result = UserTaskDataAccess.Instance.Update(connectionString, item);
            return result;
        }

        public void Remove(int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            UserTaskDataAccess.Instance.Delete(connectionString, Id);
        }
    }
}
