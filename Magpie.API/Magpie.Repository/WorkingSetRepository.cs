using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Magpie.DataAccess;
using Magpie.Model;

namespace Magpie.Repository
{
    public class WorkingSetRepository : IRepository<WorkingSet>
    {
        public WorkingSetRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<WorkingSet> GetItems()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var workingSets = WorkingSetDataAccess.Instance.GetWorkingSets(connectionString);

            var workingSetTemplates = WorkingSetDataAccess.Instance.GetWorkingSetTemplates(connectionString);


            var controlSets = ControlSetDataAccess.Instance.GetControlSets(connectionString);

            foreach (var workingSet in workingSets)
            {
                var wsts = workingSetTemplates.Where(x => x.WorkingSetTemplateId == workingSet.WorkingSetTemplateId);

                if (wsts.Count() != 1)
                    throw new Exception();
                var users = UserDataAccess.Instance.GetWorkingSetUsers(connectionString, workingSet.WorkingSetId);
                workingSet.WorkingSetTemplate = wsts.First();
                workingSet.Users = users;

            }

            return workingSets;
        }

        public WorkingSet GetItem(int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var workingSets = WorkingSetDataAccess.Instance.GetWorkingSets(connectionString, Id);

            if (workingSets.Count() != 1)
                throw new Exception();

            var ws = workingSets.First();

            var wsts = WorkingSetDataAccess.Instance.GetWorkingSetTemplates(connectionString, ws.WorkingSetTemplateId);

            if (wsts.Count() != 1)
                throw new Exception();

            ws.WorkingSetTemplate = wsts.First();

            return ws;
        }

        public bool Deploy(int Id)
        {
            throw new NotImplementedException();
        }

        public int? Add(WorkingSet item)
        {
            throw new NotImplementedException();
        }

        public bool Update(WorkingSet item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
