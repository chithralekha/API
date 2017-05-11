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
    public sealed class UserDataAccess
    {
        private static volatile UserDataAccess instance;
        private static object syncRoot = new Object();

        private UserDataAccess() { }

        public static UserDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new UserDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum UsersIndices
        {
            UserId,
            Email,
            FirstName,
            LastName,
            UserName
        }

        #endregion

        public IEnumerable<User> GetUsers(string ConnectionString, string Id = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            #endregion

            try
            {
                string storedProcedureName = "usp_UsersGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var users = new List<User>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Id != null)
                            command.Parameters.AddWithValue("@UserId", Id);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var u = new User();

                                u.Id = reader.GetString((int)UsersIndices.UserId);

                                if (!reader.IsDBNull((int)UsersIndices.Email))
                                    u.Email = reader.GetString((int)UsersIndices.Email);

                                if (!reader.IsDBNull((int)UsersIndices.FirstName))
                                    u.FirstName = reader.GetString((int)UsersIndices.FirstName);

                                if (!reader.IsDBNull((int)UsersIndices.LastName))
                                    u.LastName = reader.GetString((int)UsersIndices.LastName);

                                u.UserName = reader.GetString((int)UsersIndices.UserName);

                                users.Add(u);
                            }
                        }

                        reader.Close();

                        return users;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public IEnumerable<User> GetWorkingSetUsers(string ConnectionString, int WorkingSetId)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            #endregion

            try
            {
                string storedProcedureName = "usp_GetUsersWithWorkingSet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var users = new List<User>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (WorkingSetId != 0)
                            command.Parameters.AddWithValue("@WorkingSetId", WorkingSetId);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var u = new User();

                                u.Id = reader.GetString((int)UsersIndices.UserId);

                                if (!reader.IsDBNull((int)UsersIndices.Email))
                                    u.Email = reader.GetString((int)UsersIndices.Email);

                                if (!reader.IsDBNull((int)UsersIndices.FirstName))
                                    u.FirstName = reader.GetString((int)UsersIndices.FirstName);

                                if (!reader.IsDBNull((int)UsersIndices.LastName))
                                    u.LastName = reader.GetString((int)UsersIndices.LastName);

                                u.UserName = reader.GetString((int)UsersIndices.UserName);

                                users.Add(u);
                            }
                        }

                        reader.Close();

                        return users;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public int? Create(string ConnectionString, User User)
        {
            throw new NotImplementedException();
        }

        public bool Update(string ConnectionString, User User)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ConnectionString, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
