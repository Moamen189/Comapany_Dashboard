using BussniessLayer.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniessLayer.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MvcContext context;

        public GenericRepository(MvcContext context)
        {
            this.context = context;
        }

        public async Task<int> Add(T item)
        {
            context.Set<T>().Add(item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(T item)
        {
            context.Set<T>().Remove(item);
            return await context.SaveChangesAsync();

        }

        public async Task<T> Get(int? id)
        => await context.Set<T>().FindAsync(id);

        public async  Task<IEnumerable<T>> GetAll()
        => await context.Set<T>().ToListAsync();
        public async Task<int> Update(T item)
        {
            context.Set<T>().Update(item);
            return await context.SaveChangesAsync();

        }
    }
}
