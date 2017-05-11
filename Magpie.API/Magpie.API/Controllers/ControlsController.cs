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
    public class ControlsController : ApiController
    {
        IRepository<Control> controlRepository;

        public ControlsController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            controlRepository = new ControlRepository(connectionString);
        }

        public ControlsController(IRepository<Control> Repository)
        {
            #region Preconditions

            if (controlRepository == null)
                throw new ArgumentNullException();

            #endregion

            controlRepository = Repository;
        }

        [Authorize]
        [Route("api/Controls")]
        public IHttpActionResult Get()
        {
            #region Preconditions

            if (controlRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var controls = controlRepository.GetItems();

                var dtoControls = controls.Select(cs => ControlMapper.TranslateModelControlToDTOControl(cs));

                return Ok(dtoControls);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/Controls/{id}")]
        public IHttpActionResult Get(int id)
        {
            #region Preconditions

            if (controlRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var control = controlRepository.GetItem(id);

                var dtoControl = ControlMapper.TranslateModelControlToDTOControl(control);

                return Ok(dtoControl);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
