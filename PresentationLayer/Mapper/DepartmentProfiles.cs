using AutoMapper;
using DataAccessLayer.Entities;
using PresentationLayer.Models;

namespace PresentationLayer.Mapper
{
    public class DepartmentProfiles:Profile
    {
        public DepartmentProfiles()
        {
            CreateMap<Department , DepartmentViewModel>().ReverseMap();
        }
    }
}
