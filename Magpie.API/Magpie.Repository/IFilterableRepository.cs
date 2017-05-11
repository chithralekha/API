using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Repository
{
    public interface IFilterableRepository<T, FT> : IRepository<T>
    {
        IEnumerable<T> GetItems(FT Filter);
        IEnumerable<T> GetItems(int FilterId);
    }
}
