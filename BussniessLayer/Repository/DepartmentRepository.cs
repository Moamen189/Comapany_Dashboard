using BussniessLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussniessLayer.Repository
{
    public class DepartmentRepository : GenericRepository<Department> , IDepartmentRepository
    {
        

        public DepartmentRepository(MvcContext context) : base(context)
        {
            
        }
        
    }
}
