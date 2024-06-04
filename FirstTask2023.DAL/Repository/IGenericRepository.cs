using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask2023.DAL.Repository
{
    public interface IGenericRepository<T>
    {
        T GetByIdAsync(int id);
        IReadOnlyList<T> ListAll();
        IEnumerable<T> FindByQuery(Expression<Func<T, bool>> predicate);
        void Delete(T Entity);
        Task<T> Add(T Entity);
        Task<T> Update(T Entity);
        //Task Save();
    }
}
