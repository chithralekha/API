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
    public sealed class ___RETHINK_RaciTeamDataAccess
    {
        private static volatile ___RETHINK_RaciTeamDataAccess instance;
        private static object syncRoot = new Object();

        private ___RETHINK_RaciTeamDataAccess() { }

        public static ___RETHINK_RaciTeamDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ___RETHINK_RaciTeamDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum RaciTeamsIndices
        {
            RaciTeamId,
            Name,
            Description,
            RaciRoleId,
            RaciRole,
            UserId,
            UserName,
            FirstName,
            LastName,
            Email
        }

        #endregion

        public IEnumerable<RaciTeam> GetRaciTeams(string ConnectionString, int? Id = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            if (Id != null && Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                string storedProcedureName = "usp_RaciTeamsGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        var raciTeams = new List<RaciTeam>();

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        if (Id != null)
                            command.Parameters.AddWithValue("@RaciTeamId", Id);

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var rt = new RaciTeam();
                                rt.ConsultedUsers = new List<User>();
                                rt.InformedUsers = new List<User>();

                                rt.Id = reader.GetInt32((int)RaciTeamsIndices.RaciTeamId);

                                rt.Name = reader.GetString((int)RaciTeamsIndices.Name);

                                if (!reader.IsDBNull((int)RaciTeamsIndices.Description))
                                    rt.Description = reader.GetString((int)RaciTeamsIndices.Description);

                                int raciRoleId = reader.GetInt32((int)RaciTeamsIndices.RaciRoleId);
                                string raciRole = reader.GetString((int)RaciTeamsIndices.RaciRole);

                                var user = new User
                                {
                                    Id = reader.GetString((int)RaciTeamsIndices.UserId),
                                    UserName = reader.GetString((int)RaciTeamsIndices.UserName),
                                    FirstName = reader.GetString((int)RaciTeamsIndices.FirstName),
                                    LastName = reader.GetString((int)RaciTeamsIndices.LastName),
                                    Email = reader.GetString((int)RaciTeamsIndices.Email)
                                };

                                switch (raciRole)
                                {
                                    case "Responsible":
                                        {
                                            rt.ResponsibleUser = user;
                                        }
                                        break;
                                    case "Accountable":
                                        {
                                            rt.AccountableUser = user;
                                        }
                                        break;
                                    case "Consulted":
                                        {
                                            ((List<User>)rt.ConsultedUsers).Add(user);
                                        }
                                        break;
                                    case "Informed":
                                        {
                                            ((List<User>)rt.InformedUsers).Add(user);
                                        }
                                        break;
                                    default:
                                        throw new Exception();
                                }

                                raciTeams.Add(rt);
                            }
                        }

                        reader.Close();

                        return raciTeams;
                    }
                }
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                throw;
            }
        }

        public int? Create(string ConnectionString, RaciTeam RaciTeam)
        {
            throw new NotImplementedException();
        }

        public bool Update(string ConnectionString, RaciTeam RaciTeam)
        {
            throw new NotImplementedException();
        }

        public void Delete(string ConnectionString, int Id)
        {
            throw new NotImplementedException();
        }
    }
}
