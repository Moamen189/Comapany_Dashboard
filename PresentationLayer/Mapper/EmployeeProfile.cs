using AutoMapper;
using DataAccessLayer.Entities;
using PresentationLayer.Models;

namespace PresentationLayer.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
