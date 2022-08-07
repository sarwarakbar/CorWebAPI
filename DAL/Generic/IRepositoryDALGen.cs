using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Generic
{
    public interface IRepositoryDALGen<T>
    {
        public Task<T> Create(T _object);

        //public T Update(T id, T  _object);

        public IEnumerable<T> GetAll();

        public T GetById(int Id);

        //public void Delete(T _object);
    }
}
