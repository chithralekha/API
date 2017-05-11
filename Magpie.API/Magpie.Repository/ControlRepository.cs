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
    public class ControlRepository : IRepository<Control>
    {
        public ControlRepository(string ConnectionString)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(ConnectionString))
                throw new ArgumentNullException();

            #endregion

            connectionString = ConnectionString;
        }

        protected string connectionString;

        public IEnumerable<Control> GetItems()
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            #endregion

            var controls = ControlDataAccess.Instance.GetControls(connectionString);

            return controls;
        }

        public Control GetItem(int Id)
        {
            #region Preconditions

            if (string.IsNullOrWhiteSpace(connectionString))
                throw new InvalidOperationException();

            if (Id <= 0)
                throw new ArgumentOutOfRangeException();

            #endregion

            var controls = ControlDataAccess.Instance.GetControls(connectionString, Id);

            if (controls.Count() != 1)
                throw new Exception();

            return controls.First();
        }

        public int? Add(Control item)
        {
            throw new NotImplementedException();
        }

        public bool Update(Control item)
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
