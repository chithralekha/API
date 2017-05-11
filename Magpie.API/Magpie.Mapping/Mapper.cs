using System;
using System.Collections.Generic;
using Magpie.DTO;
using Magpie.Model;

namespace Magpie.Mapping
{
    public static partial class Mapper
    {
        #region DTO to Model

        public static IEnumerable<Model.Comment> TranslateDTOCommentListToModelCommentList(IEnumerable<DTO.Comment> comments)
        {
            if (comments == null)
                return null;

            var modelComments = new List<Model.Comment>();

            foreach (var item in comments)
            {
                modelComments.Add(TranslateDTOCommentToModelComment(item));
            }

            return modelComments;
        }

        public static Model.Comment TranslateDTOCommentToModelComment(DTO.Comment c)
        {
            if (c == null)
                return null;

            return new Model.Comment
            {
                Id = c.Id,
                LastModified = c.LastModified,
                LastModifiedByUser = UserMapper.TranslateDTOUserToModelUser(c.LastModifiedByUser),
                Text = c.Text
            };
        }

        public static Model.DueStatus TranslateDTODueStatusToModelDueStatus(DTO.DueStatus ds)
        {
            if (ds == null)
                return null;

            return new Model.DueStatus
            {
                Id = ds.Id,
                Status = ds.Status
            };
        }

        public static IEnumerable<Model.Event> TranslateDTOEventListToModelEventList(IEnumerable<DTO.Event> events)
        {
            if (events == null)
                return null;

            var modelevents = new List<Model.Event>();

            foreach (var item in events)
            {
                modelevents.Add(TranslateDTOEventToModelEvent(item));
            }

            return modelevents;
        }

        public static Model.Event TranslateDTOEventToModelEvent(DTO.Event e)
        {
            if (e == null)
                return null;

            return new Model.Event
            {
                Id = e.Id,
                InstigatorUser = UserMapper.TranslateDTOUserToModelUser(e.InstigatorUser),
                Name = e.Name,
                Timestamp = e.Timestamp
            };
        }

        public static Model.RaciTeam TranslateDTORaciTeamToModelRaciTeam(DTO.RaciTeam rt)
        {
            if (rt == null)
                return null;

            return new Model.RaciTeam
            {
                AccountableUser = UserMapper.TranslateDTOUserToModelUser(rt.AccountableUser),
                ConsultedUsers = UserMapper.TranslateDTOUserListToModelUserList(rt.ConsultedUsers),
                //Description = rt.Description,
                InformedUsers = UserMapper.TranslateDTOUserListToModelUserList(rt.InformedUsers),
                //Id = rt.Id,
                //Name = rt.Name,
                ResponsibleUser = UserMapper.TranslateDTOUserToModelUser(rt.ResponsibleUser)
            };
        }

        public static Model.TaskState TranslateDTOTaskStateToModelTaskState(DTO.TaskState ts)
        {
            if (ts == null)
                return null;

            return new Model.TaskState
            {
                Id = ts.Id,
                Name = ts.Name
            };
        }

        

        #endregion

        #region Model to DTO

        public static IEnumerable<DTO.Comment> TranslateModelCommentListToDTOCommentList(IEnumerable<Model.Comment> ModelComments)
        {
            if (ModelComments == null)
                return null;

            var dtoComments = new List<DTO.Comment>();

            foreach (var item in ModelComments)
            {
                dtoComments.Add(TranslateModelCommentToDTOComment(item));
            }

            return dtoComments;
        }

        public static DTO.Comment TranslateModelCommentToDTOComment(Model.Comment c)
        {
            if (c == null)
                return null;

            return new DTO.Comment
            {
                Id = c.Id,
                Text = c.Text,
                LastModified = c.LastModified,
                LastModifiedByUser = UserMapper.TranslateModelUserToDTOUser(c.LastModifiedByUser)
            };
        }

        public static DTO.DueStatus TranslateModelDueStatusToDTODueStatus(Model.DueStatus ds)
        {
            if (ds == null)
                return null;

            return new DTO.DueStatus
            {
                Id = ds.Id,
                Status = ds.Status
            };
        }

        public static IEnumerable<DTO.Event> TranslateModelEventListToDTOEventList(IEnumerable<Model.Event> ModelEvents)
        {
            if (ModelEvents == null)
                return null;

            var dtoEvents = new List<DTO.Event>();

            foreach (var item in ModelEvents)
            {
                dtoEvents.Add(TranslateModelEventToDTOEvent(item));
            }

            return dtoEvents;
        }

        public static DTO.Event TranslateModelEventToDTOEvent(Model.Event e)
        {
            if (e == null)
                return null;

            return new DTO.Event
            {
                Id = e.Id,
                Name = e.Name,
                InstigatorUser = UserMapper.TranslateModelUserToDTOUser(e.InstigatorUser),
                Timestamp = e.Timestamp
            };
        }

        public static DTO.RaciTeam TranslateModelRaciTeamToDTORaciTeam(Model.RaciTeam rt)
        {
            if (rt == null)
                return null;

            return new DTO.RaciTeam
            {
                //Id = rt.Id,
                //Name = rt.Name,
                //Description = rt.Description,
                ResponsibleUser = UserMapper.TranslateModelUserToDTOUser(rt.ResponsibleUser),
                AccountableUser = UserMapper.TranslateModelUserToDTOUser(rt.AccountableUser),
                ConsultedUsers = UserMapper.TranslateModelUserListToDTOUserList(rt.ConsultedUsers),
                InformedUsers = UserMapper.TranslateModelUserListToDTOUserList(rt.InformedUsers)
            };
        }

        public static DTO.TaskState TranslateModelTaskStateToDTOTaskState(Model.TaskState ts)
        {
            if (ts == null)
                return null;

            return new DTO.TaskState
            {
                Id = ts.Id,
                Name = ts.Name
            };
        }

        

        #endregion
    }
}
