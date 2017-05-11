using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Magpie.Mapping;
using Magpie.Model;
using Magpie.Repository;

namespace Magpie.API.Userlers
{
    public class UsersController : ApiController
    {
        IRepository<User> userRepository;

        public UsersController()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MagpieClient"].ConnectionString;
            userRepository = new UserRepository(connectionString);
        }

        public UsersController(IRepository<User> Repository)
        {
            #region Preconditions

            if (userRepository == null)
                throw new ArgumentNullException();

            #endregion

            userRepository = Repository;
        }

        [Authorize]
        [Route("api/Users")]
        public IHttpActionResult Get()
        {
            #region Preconditions

            if (userRepository == null)
                throw new InvalidOperationException();

            #endregion

            try
            {
                var users = userRepository.GetItems();

                var dtoUsers = users.Select(cs => UserMapper.TranslateModelUserToDTOUser(cs));

                return Ok(dtoUsers);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }

        [Authorize]
        [Route("api/Users/{id}")]
        public IHttpActionResult Get(string id)
        {
            #region Preconditions

            if (userRepository == null)
                throw new InvalidOperationException();

            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentOutOfRangeException();

            #endregion

            try
            {
                var user = ((UserRepository)userRepository).GetItem(id);

                var dtoUser = UserMapper.TranslateModelUserToDTOUser(user);

                return Ok(dtoUser);
            }
            catch (Exception)
            {
                return InternalServerError();
            }
        }
    }
}
