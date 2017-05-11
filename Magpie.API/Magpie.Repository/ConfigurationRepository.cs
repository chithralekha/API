using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magpie.DataAccess;
using Magpie.Model;

namespace Magpie.Repository
{
    public class ConfigurationRepository
    {
        public ConfigurationRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public Configuration GetConfiguration()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var configuration = ConfigurationDataAccess.Instance.GetConfiguration(connectionString);

            return configuration;
        }
    }
}
