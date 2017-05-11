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
    public class FiltersController : ApiController
    {
        IRepository<UserTaskFilter> userTaskFilterRepository;

        public FiltersController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            userTaskFilterRepository = new UserTaskFilterRepository(connectionString);
        }

        public FiltersController(IRepository<UserTaskFilter> Repository)
        {
            #region Preconditions

            if (userTaskFilterRepository == null)
                throw new ArgumentNullException();

            #endregion

            userTaskFilterRepository = Repository;
        }

        [Authorize]
        [Route("api/Filters")]
        public IHttpActionResult Get(string filterOwnerUserId = null)
        {
            #region Preconditions

            if (userTaskFilterRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var userTaskFilters = ((UserTaskFilterRepository)userTaskFilterRepository).GetItems(filterOwnerUserId);

                var dtoTaskFilters = userTaskFilters.Select(utf => UserTaskFilterMapper.TranslateModelUserTaskFilterToDTOTaskFilter(utf));

                return Ok(dtoTaskFilters);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/Filters/{id}")]
        public IHttpActionResult Get(int id)
        {
            #region Preconditions

            if (userTaskFilterRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var userTaskFilter = userTaskFilterRepository.GetItem(id);

                var dtoTaskFilter = UserTaskFilterMapper.TranslateModelUserTaskFilterToDTOTaskFilter(userTaskFilter);

                return Ok(dtoTaskFilter);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/Filters", Name = "AddFilter")]
        public HttpResponseMessage Post(DTO.TaskFilter taskFilter)
        {
            #region Preconditions

            if (userTaskFilterRepository == null)
                throw new InvalidOperationException();

            if (taskFilter == null)
                throw new ArgumentNullException();

            #endregion

            var userTaskFilter = UserTaskFilterMapper.TranslateDTOTaskFilterToModelUserTaskFilter(taskFilter);

            int? newId = userTaskFilterRepository.Add(userTaskFilter);

            if (newId == null)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            taskFilter.FilterId = newId.Value;

            var response = Request.CreateResponse<DTO.TaskFilter>(HttpStatusCode.Created, taskFilter);

            string uri = Url.Link("AddFilter", new { id = taskFilter.FilterId });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/Filters/{id}")]
        public void Put(int id, DTO.TaskFilter taskFilter)
        {
            #region Preconditions

            if (userTaskFilterRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            if (taskFilter == null)
                throw new ArgumentNullException();

            #endregion

            taskFilter.FilterId = id;

            var userTaskFilter = UserTaskFilterMapper.TranslateDTOTaskFilterToModelUserTaskFilter(taskFilter);

            if (!userTaskFilterRepository.Update(userTaskFilter))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/Filters/{id}")]
        public void Delete(int id)
        {
            #region Preconditions

            if (userTaskFilterRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var userTaskFilter = userTaskFilterRepository.GetItem(id);

            if (userTaskFilter == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            userTaskFilterRepository.Remove(id);
        }
    }
}
