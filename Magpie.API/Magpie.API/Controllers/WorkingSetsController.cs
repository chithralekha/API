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
    public class WorkingSetsController : ApiController
    {
        IRepository<WorkingSet> workingSetRepository;
        IRepository<UserTaskFilter> userTaskFilterRepository;

        public WorkingSetsController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            workingSetRepository = new WorkingSetRepository(connectionString);
            userTaskFilterRepository = new UserTaskFilterRepository(connectionString);
        }

        public WorkingSetsController(IRepository<WorkingSet> WorkingSetRepository, IRepository<UserTaskFilter> UserTaskFilterRepository)
        {
            #region Preconditions

            if (this.workingSetRepository == null)
                throw new ArgumentNullException();

            #endregion

            this.workingSetRepository = WorkingSetRepository;
            this.userTaskFilterRepository = UserTaskFilterRepository;
        }

        [Authorize]
        [Route("api/WorkingSets")]
        public IHttpActionResult Get()
        {
            #region Preconditions

            if (workingSetRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var workingSets = workingSetRepository.GetItems();

                var dtoWorkingSets = workingSets.Select(utf => WorkingSetMapper.TranslateModelWorkingSetToDTOWorkingSet(utf));

                return Ok(dtoWorkingSets);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/WorkingSets/{id}")]
        public IHttpActionResult Get(int id)
        {
            #region Preconditions

            if (workingSetRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var workingSet = workingSetRepository.GetItem(id);

                var dtoTaskWorkingSet = WorkingSetMapper.TranslateModelWorkingSetToDTOWorkingSet(workingSet);

                return Ok(dtoTaskWorkingSet);
            }
            catch (Exception ex)
            {
                string res = ex.ToString();
                return InternalServerError();
            }
        }
    }
}

