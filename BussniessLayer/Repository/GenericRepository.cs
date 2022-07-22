using BussniessLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
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

        public  int Add(T item)
        {
            context.Set<T>().Add(item);
            return  context.SaveChanges();
        }

        public  int Delete(T item)
        {
            context.Set<T>().Remove(item);
            return  context.SaveChanges();

        }

        public  T Get(int? id)
        =>  context.Set<T>().Find(id);

        public   IEnumerable<T> GetAll()
        {
            if(typeof(T) == typeof(Employee))
                return (IEnumerable<T>) context.Set<Employee>().Include(E => E.Department).ToList();
            return  context.Set<T>().ToList();
        }
        public  int Update(T item)
        {
            context.Set<T>().Update(item);
            return  context.SaveChanges();

        }
    }
}
