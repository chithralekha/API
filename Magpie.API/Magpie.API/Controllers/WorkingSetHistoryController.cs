using Magpie.Mapping;
using Magpie.Model;
using Magpie.Repository;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Magpie.API.Controllers
{
    public class WorkingSetHistoryController : ApiController
    {
        IRepository<WorkingSetDataPoint> workingSetHistoryRepository;

        public WorkingSetHistoryController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            workingSetHistoryRepository = new WorkingSetHistoryRepository(connectionString);
        }

        public WorkingSetHistoryController(IRepository<WorkingSetDataPoint> workingSetHistoryRepository)
        {
            #region Preconditions

            if (this.workingSetHistoryRepository == null)
                throw new ArgumentNullException();

            #endregion

            this.workingSetHistoryRepository = workingSetHistoryRepository;
        }

        [Authorize]
        [Route("api/WorkingSetHistory/{id}")]
        public IHttpActionResult Get(int id, DateTime? startDate = null, DateTime? endDate = null)
        {
            #region Preconditions

            if (workingSetHistoryRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var workingSetHistory = ((WorkingSetHistoryRepository)workingSetHistoryRepository).GetItems(id, startDate, endDate);

                var dtoWorkingSetHistory = workingSetHistory.Select(utf => WorkingSetHistoryMapper.TranslateModelWorkingSetHistoryToDTOWorkingSet(utf));

                return Ok(dtoWorkingSetHistory);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }
    }
}

