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
    public class ControlPrioritiesController : ApiController
    {
        IRepository<ControlPriority> controlPriorityRepository;

        public ControlPrioritiesController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            controlPriorityRepository = new ControlPriorityRepository(connectionString);
        }

        public ControlPrioritiesController(IRepository<ControlPriority> Repository)
        {
            #region Preconditions

            if (controlPriorityRepository == null)
                throw new ArgumentNullException();

            #endregion

            controlPriorityRepository = Repository;
        }

        [Authorize]
        [Route("api/ControlPriorities")]
        public IHttpActionResult Get()
        {
            #region Preconditions

            if (controlPriorityRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var controlPriorities = controlPriorityRepository.GetItems();

                var dtoControlPriorities = controlPriorities.Select(cs => ControlPriorityMapper.TranslateModelControlPriorityToDTOControlPriority(cs));

                return Ok(dtoControlPriorities);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/ControlPriorities/{id}")]
        public IHttpActionResult Get(int id, int? DefinitionSourceId = null, int? ControlPriorityClassificationId = null)
        {
            #region Preconditions

            if (controlPriorityRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var controlPriority = controlPriorityRepository.GetItem(id);

                var dtoControlPriority = ControlPriorityMapper.TranslateModelControlPriorityToDTOControlPriority(controlPriority);

                return Ok(dtoControlPriority);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/ControlPriorities", Name = "AddControlPriority")]
        public HttpResponseMessage Post(DTO.ControlPriority controlPriority)
        {
            #region Preconditions

            if (controlPriorityRepository == null)
                throw new InvalidOperationException();

            if (controlPriority == null)
                throw new ArgumentNullException();

            #endregion

            #region Validation

            if (controlPriority.Id != null)
                throw new InvalidOperationException();

            if (string.IsNullOrWhiteSpace(controlPriority.Name))
                throw new InvalidOperationException();

            if (controlPriority.Name.Length > 50)
                throw new InvalidOperationException();

            #endregion

            var modelControlPriority = ControlPriorityMapper.TranslateDTOControlPriorityToModelControlPriority(controlPriority);

            int? newId = controlPriorityRepository.Add(modelControlPriority);

            if (newId == null)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            controlPriority.Id = newId.Value;

            var response = Request.CreateResponse<DTO.ControlPriority>(HttpStatusCode.Created, controlPriority);

            string uri = Url.Link("AddControlPriority", new { id = controlPriority.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/ControlPriorities/{id}")]
        public void Put(int id, DTO.ControlPriority controlPriority)
        {
            #region Preconditions

            if (controlPriorityRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            if (controlPriority == null)
                throw new ArgumentNullException();

            #endregion

            #region Validation

            if (controlPriority.Id == null)
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

            if (string.IsNullOrWhiteSpace(controlPriority.Name))
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

            if (controlPriority.Name.Length > 50)
                throw new HttpResponseException(HttpStatusCode.NotAcceptable);

            #endregion

            controlPriority.Id = id;

            var modelControlPriority = ControlPriorityMapper.TranslateDTOControlPriorityToModelControlPriority(controlPriority);

            if (!controlPriorityRepository.Update(modelControlPriority))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/ControlPriorities/{id}")]
        public void Delete(int id)
        {
            #region Preconditions

            if (controlPriorityRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var modelControlPriority = controlPriorityRepository.GetItem(id);

            if (modelControlPriority == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            controlPriorityRepository.Remove(id);
        }
    }
}
