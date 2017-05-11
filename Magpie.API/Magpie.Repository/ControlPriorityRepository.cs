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
    public class ControlPriorityRepository : IRepository<ControlPriority>
    {
        public ControlPriorityRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<ControlPriority> GetItems()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var controlPrioritys = ControlPriorityDataAccess.Instance.GetControlPriorities(connectionString);

            return controlPrioritys;
        }

        public ControlPriority GetItem(int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var controlPrioritys = ControlPriorityDataAccess.Instance.GetControlPriorities(connectionString, Id);

            if (controlPrioritys.Count() != 1)
                throw new Exception();

            return controlPrioritys.First();
        }

        public int? Add(ControlPriority item)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (item == null)
                throw new ArgumentNullException();

            #endregion

            var newId = ControlPriorityDataAccess.Instance.Create(connectionString, item);
            return newId;
        }

        public bool Update(ControlPriority item)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (item == null)
                throw new ArgumentNullException();

            #endregion

            var result = ControlPriorityDataAccess.Instance.Update(connectionString, item);
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

            ControlPriorityDataAccess.Instance.Delete(connectionString, Id);
        }
    }
}
