using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BussniessLayer.Interfaces
{
    public interface IEmployeeRepository :   IGenericRepository<Employee>
    {
        IEnumerable<Employee> GetEmployeeByDepartmentName(string DepartmentName);
        //Employee Get(int? id);
        //IEnumerable<Employee> GetAll();

        //int Add(Employee Employee);
        //int Update(Employee Employee);
        //int Delete(Employee Employee);

        IEnumerable<Employee> SearchEmployee(string Name);
    }

}
