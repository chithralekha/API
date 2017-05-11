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
    public class UserTaskFilterRepository : IRepository<UserTaskFilter>
    {
        public UserTaskFilterRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<UserTaskFilter> GetItems(string FilterOwnerUserId)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var userTaskFilters = UserTaskFilterDataAccess.Instance.GetUserTaskFilters(connectionString, null, FilterOwnerUserId);

            var userTaskFilterResultCounts = UserTaskFilterDataAccess.Instance.GetUserTaskFilterResultCounts(connectionString);

            foreach (var item in userTaskFilters)
            {
                item.UserTaskFilterResultCounts = userTaskFilterResultCounts.Where(x => x.FilterId == item.FilterId);
            }

            return userTaskFilters;
        }

        public IEnumerable<UserTaskFilter> GetItems()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            return GetItems(null);
        }

        public UserTaskFilter GetItem(int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var userTaskFilters = UserTaskFilterDataAccess.Instance.GetUserTaskFilters(connectionString, Id);

            var userTaskFilterResultCounts = UserTaskFilterDataAccess.Instance.GetUserTaskFilterResultCounts(connectionString);

            foreach (var item in userTaskFilters)
            {
                item.UserTaskFilterResultCounts = userTaskFilterResultCounts.Where(x => x.FilterId == item.FilterId);
            }

            if (userTaskFilters.Count() != 1)
                throw new Exception();

            return userTaskFilters.First();
        }

        public int? Add(UserTaskFilter item)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (item == null)
                throw new ArgumentNullException();

            #endregion

            var newId = UserTaskFilterDataAccess.Instance.Create(connectionString, item);
            return newId;
        }

        public bool Update(UserTaskFilter item)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (item == null)
                throw new ArgumentNullException();

            #endregion

            var result = UserTaskFilterDataAccess.Instance.Update(connectionString, item);
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

            UserTaskFilterDataAccess.Instance.Delete(connectionString, Id);
        }
    }
}
