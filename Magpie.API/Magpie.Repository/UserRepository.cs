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
    public class UserRepository : IRepository<User>
    {
        public UserRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<User> GetItems()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var users = UserDataAccess.Instance.GetUsers(connectionString);

            return users;
        }

        public User GetItem(int Id)
        {
            throw new NotImplementedException("uh maybe we should not implement IRepository because this repo does not provide GetItem(int Id)...");
        }

        public User GetItem(string Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (string.IsNullOrWhiteSpace(Id))
                throw new ArgumentOutOfRangeException();

            #endregion

            var users = UserDataAccess.Instance.GetUsers(connectionString, Id);

            if (users.Count() != 1)
                throw new Exception();

            return users.First();
        }

        public int? Add(User item)
        {
            throw new NotImplementedException();
        }

        public bool Update(User item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
