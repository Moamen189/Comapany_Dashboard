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
    public class EmployeeRepository : GenericRepository<Employee> , IEmployeeRepository
    {

       

        public EmployeeRepository(MvcContext context):base(context)
        {
            
        }

        public IEnumerable<Employee> GetEmployeeByDepartmentName(string DepartmentName)
        {
            throw new NotImplementedException();
        }
    }
}
