using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Magpie.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetItems();
        T GetItem(int Id);
        int? Add(T item);
        bool Update(T item);
        void Remove(int Id);
    }
}
