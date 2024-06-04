using FirstTask2023.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FirstTask2023.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LinkedGatesDBContext _context;

        public GenericRepository(LinkedGatesDBContext context)
        {
            _context = context;
        }

        public async Task<T> Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
            await Save();
            return Entity;
        }

        public async Task<T> Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            await Save();
            return Entity;
        }

        public async void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
            await Save();
        }

        public T GetByIdAsync(int id)
            => _context.Set<T>().Find(id);


        public IReadOnlyList<T> ListAll()
            => _context.Set<T>().ToList();



        public IEnumerable<T> FindByQuery(Expression<Func<T, bool>> expression)
           => _context.Set<T>().Where(expression);

        private async Task Save()
        {
            await _context.SaveChangesAsync();
            //Dispose();
        }

    }
}
