using System.Collections.Generic;

namespace Magpie.Mapping
{
    public static class UserMapper
    {
        public static IEnumerable<Model.User> TranslateDTOUserListToModelUserList(IEnumerable<DTO.User> DTOUsers)
        {
            if (DTOUsers == null)
                return null;

            var modelUsers = new List<Model.User>();

            foreach (var item in DTOUsers)
            {
                modelUsers.Add(TranslateDTOUserToModelUser(item));
            }

            return modelUsers;
        }

        public static Model.User TranslateDTOUserToModelUser(DTO.User u)
        {
            if (u == null)
                return null;

            return new Model.User
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName
            };
        }

        public static IEnumerable<DTO.User> TranslateModelUserListToDTOUserList(IEnumerable<Model.User> ModelUsers)
        {
            if (ModelUsers == null)
                return null;

            var dtoUsers = new List<DTO.User>();

            foreach (var item in ModelUsers)
            {
                dtoUsers.Add(TranslateModelUserToDTOUser(item));
            }

            return dtoUsers;
        }

        public static DTO.User TranslateModelUserToDTOUser(Model.User u)
        {
            if (u == null)
                return null;

            return new DTO.User
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                UserName = u.UserName
            };
        }
    }
}
