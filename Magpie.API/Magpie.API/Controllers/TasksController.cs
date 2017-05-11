using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Magpie.Mapping;
using Magpie.Model;
using Magpie.Repository;
using Magpie.Logging;

namespace Magpie.API.Controllers
{
    public class TasksController : ApiController
    {
        IFilterableRepository<UserTask, UserTaskFilter> userTaskRepository;
        protected ILog logger = new Logging.Logger.Log(typeof(TasksController));

        public TasksController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            userTaskRepository = new UserTaskRepository(connectionString);
        }

        public TasksController(IFilterableRepository<UserTask, UserTaskFilter> Repository)
        {
            #region Preconditions

            if (userTaskRepository == null)
                throw new ArgumentNullException();

            #endregion

            userTaskRepository = Repository;
        }


        [Authorize]
        [Route("api/Tasks")]
        public IHttpActionResult Get(int? filterId = null, string sortBy = null, string sortOrder = null)
        {
            #region Preconditions

            if (userTaskRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                logger.Info($"TasksController Get: {filterId}, sortBy: {sortBy}, sortOrder: {sortOrder}");

                IEnumerable<UserTask> userTasks = null;

                if (filterId != null)
                {
                    // This cast is appropriate, we need the specific UserTaskRepository behaviour here.
                    userTasks = ((UserTaskRepository)userTaskRepository).GetItems(filterId.Value);
                }
                else
                {
                    userTasks = userTaskRepository.GetItems();
                }

                var dtoTaskInfos = userTasks.Select(ut => UserTaskMapper.TranslateModelUserTaskToDTOTaskInfo(ut));

                var taskInfoList = new DTO.TaskInfoList
                {
                    Metadata = new DTO.TaskInfoListMetadata
                    {
                        Count = dtoTaskInfos.Count(),
                        FilterId = filterId,
                        SortBy = sortBy,
                        SortOrder = sortOrder
                    },
                    TaskInfos = dtoTaskInfos
                };

                logger.Info($"TasksController Get taskInfoList Count: {taskInfoList.TaskInfos.Count()}");

                return Ok(taskInfoList);
            }
            catch (Exception ex)
            {
                logger.Error($"TasksController Get Error: {ex.Message}");
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/Tasks/{id}")]
        public IHttpActionResult Get(int id)
        {
            #region Preconditions

            if (userTaskRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var userTask = userTaskRepository.GetItem(id);

                var dtoTask = UserTaskMapper.TranslateModelUserTaskToDTOTask(userTask);

                return Ok(dtoTask);
            }
            catch (Exception ex)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/WorkingSets/{workingSetId}/Tasks")]
        public IHttpActionResult GetWorkingSetTasks(int workingSetId, int? filterId = null, string sortBy = null, string sortOrder = null)
        {
            try
            {
                IEnumerable<UserTask> userTasks = null;

                if (filterId != null)
                {
                    // This cast is appropriate, we need the specific UserTaskRepository behaviour here.
                    userTasks = ((UserTaskRepository)userTaskRepository).GetItems(filterId.Value, workingSetId);
                }
                else
                {
                    userTasks = userTaskRepository.GetItems(new Model.UserTaskFilter { WorkingSetId = workingSetId });
                }

                var dtoTaskInfos = userTasks.Select(ut => UserTaskMapper.TranslateModelUserTaskToDTOTaskInfo(ut));

                var taskInfoList = new DTO.TaskInfoList
                {
                    Metadata = new DTO.TaskInfoListMetadata
                    {
                        Count = dtoTaskInfos.Count(),
                        FilterId = filterId,
                        SortBy = sortBy,
                        SortOrder = sortOrder
                    },
                    TaskInfos = dtoTaskInfos
                };

                return Ok(taskInfoList);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/Tasks", Name = "AddUserTask")]
        public HttpResponseMessage Post([FromBody]DTO.Task task)
        {
            #region Preconditions

            if (userTaskRepository == null)
                throw new InvalidOperationException();

            if (task == null)
                throw new ArgumentNullException();

            #endregion

            var userTask = UserTaskMapper.TranslateDTOTaskToModelUserTask(task);

            int? newId = userTaskRepository.Add(userTask);

            if (newId == null)
                throw new HttpResponseException(HttpStatusCode.InternalServerError);

            task.Id = newId.Value;

            var response = Request.CreateResponse<DTO.Task>(HttpStatusCode.Created, task);

            string uri = Url.Link("AddUserTask", new { id = task.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/Tasks/{id}")]
        public void Put(int id, DTO.Task task)
        {
            #region Preconditions

            if (userTaskRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            if (task == null)
                throw new ArgumentNullException();

            #endregion

            UserTask userTask = null;

            task.Id = id;
            userTask = UserTaskMapper.TranslateDTOTaskToModelUserTask(task);

            if (!userTaskRepository.Update(userTask))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }

        [Authorize(Roles = "Staff,Security Officer")]
        [Route("api/Tasks/{id}")]
        public void Delete(int id)
        {
            #region Preconditions

            if (userTaskRepository == null)
                throw new InvalidOperationException();

            if (id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var userTask = userTaskRepository.GetItem(id);

            if (userTask == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            userTaskRepository.Remove(id);
        }
    }
}
