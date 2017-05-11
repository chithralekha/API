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
    public class ControlSetRepository : IRepository<ControlSet>
    {
        public ControlSetRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<ControlSet> GetItems()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var controlSets = ControlSetDataAccess.Instance.GetControlSets(connectionString);

            return controlSets;
        }

        public ControlSet GetItem(int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var controlSets = ControlSetDataAccess.Instance.GetControlSets(connectionString, Id);

            if (controlSets.Count() != 1)
                throw new Exception();

            return controlSets.First();
        }

        public ControlSet GetItem(int Id, int? DefinitionSourceId = null, int? ControlSetClassificationId = null)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var controlSets = ControlSetDataAccess.Instance.GetControlSets(connectionString, Id, DefinitionSourceId, ControlSetClassificationId);

            if (controlSets.Count() != 1)
                throw new Exception();

            return controlSets.First();
        }

        public int? Add(ControlSet item)
        {
            throw new NotImplementedException();
        }

        public bool Update(ControlSet item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
