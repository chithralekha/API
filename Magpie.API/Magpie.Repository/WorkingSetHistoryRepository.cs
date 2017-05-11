using Magpie.DataAccess;
using Magpie.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Repository
{
    public class WorkingSetHistoryRepository : IRepository<WorkingSetDataPoint> 
    {
        public WorkingSetHistoryRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<WorkingSetDataPoint> GetItems()
        {
            throw new InvalidOperationException();
        }


        public IEnumerable<WorkingSetDataPoint> GetItems(int Id, DateTime? startDate, DateTime? endDate)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var workingSetHistory = WorkingSetHistoryDataAccess.Instance.GetWorkingSetHistory(connectionString, Id, startDate, endDate);

            return workingSetHistory;
        }
        
        public WorkingSetDataPoint GetItem(int Id)
        {
            throw new NotImplementedException();
        }
        
        public bool Deploy(int Id)
        {
            throw new NotImplementedException();
        }

        public int? Add(WorkingSetDataPoint item)
        {
            throw new NotImplementedException();
        }

        public bool Update(WorkingSetDataPoint item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}


