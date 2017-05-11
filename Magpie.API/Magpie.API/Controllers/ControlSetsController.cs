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
    public class ControlSetsController : ApiController
    {
        IRepository<ControlSet> controlSetRepository;

        public ControlSetsController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            controlSetRepository = new ControlSetRepository(connectionString);
        }

        public ControlSetsController(IRepository<ControlSet> Repository)
        {
            #region Preconditions

            if (controlSetRepository == null)
                throw new ArgumentNullException();

            #endregion

            controlSetRepository = Repository;
        }

        [Authorize]
        [Route("api/ControlSets")]
        public IHttpActionResult Get()
        {
            #region Preconditions

            if (controlSetRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var controlSets = controlSetRepository.GetItems();

                var dtoControlSets = controlSets.Select(cs => ControlSetMapper.TranslateModelControlSetToDTOControlSet(cs));

                return Ok(dtoControlSets);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/ControlSets/{id}")]
        public IHttpActionResult Get(int id, int? DefinitionSourceId = null, int? ControlSetClassificationId = null)
        {
            #region Preconditions

            if (controlSetRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var controlSet = ((ControlSetRepository)controlSetRepository).GetItem(id, DefinitionSourceId, ControlSetClassificationId);

                var dtoControlSet = ControlSetMapper.TranslateModelControlSetToDTOControlSet(controlSet);

                return Ok(dtoControlSet);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
