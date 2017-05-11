using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Magpie.Mapping;
using Magpie.Model;
using Magpie.Repository;

namespace Magpie.API.Controllers
{
    public class ConfigurationController : ApiController
    {
        // THIS IS NOT YET AN INTERFACE!!!
        ConfigurationRepository configurationRepository;
        // THIS IS NOT YET AN INTERFACE!!!

        public ConfigurationController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            configurationRepository = new ConfigurationRepository(connectionString);
        }

        public ConfigurationController(ConfigurationRepository Repository)
        {
            #region Preconditions

            if (configurationRepository == null)
                throw new ArgumentNullException();

            #endregion

            configurationRepository = Repository;
        }

        [Authorize]
        [Route("api/Configuration")]
        public IHttpActionResult Get()
        {
            #region Preconditions

            if (configurationRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var configuration = configurationRepository.GetConfiguration();

                var dtoConfiguration = ConfigurationMapper.TranslateModelConfigurationToDTOConfiguration(configuration);

                return Ok(dtoConfiguration);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
