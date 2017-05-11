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
    public sealed class ConfigurationDataAccess
    {
        private static volatile ConfigurationDataAccess instance;
        private static object syncRoot = new Object();

        private ConfigurationDataAccess() { }

        public static ConfigurationDataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ConfigurationDataAccess();
                    }
                }

                return instance;
            }
        }

        #region Enums

        private enum ConfigurationIndices
        {
            Key,
            Value
        }

        #endregion

        public Configuration GetConfiguration(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new InvalidOperationException();

            #endregion

            try
            {
                string storedProcedureName = "usp_AppSettingsGet";

                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        Configuration configuration = new Configuration
                        {
                            ConfigurationEntries = new Dictionary<string, string>()
                        };

                        command.CommandType = CommandType.StoredProcedure;
                        command.Connection = connection;
                        command.CommandText = storedProcedureName;

                        connection.Open();

                        var reader = command.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string key = reader.GetString((int)ConfigurationIndices.Key);

                                string value = null;

                                if (!reader.IsDBNull((int)ConfigurationIndices.Value))
                                    value = reader.GetString((int)ConfigurationIndices.Value);

                                configuration.ConfigurationEntries.Add(key, value);
                            }
                        }

                        reader.Close();

                        return configuration;
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
