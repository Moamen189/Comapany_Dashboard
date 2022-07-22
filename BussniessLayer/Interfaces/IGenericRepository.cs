using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniessLayer.Interfaces
{
    public interface IGenericRepository<T>
    {
        T Get(int? id);
       IEnumerable<T> GetAll();

        int Add(T T);
        int Update(T T);
        int Delete(T T);
    }
}
