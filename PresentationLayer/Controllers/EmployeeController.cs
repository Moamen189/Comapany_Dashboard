using AutoMapper;
using BussniessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Helper;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
        [Authorize]
    public class EmployeeController : Controller
    {
        public IUnitOfWork UnitOfWork { get; }

        //public IEmployeeRepository EmployeeRepository { get; }
        //public IDepartmentRepository DepartmentRepository { get; }
        public IMapper Map { get; }

        public EmployeeController(IUnitOfWork unitOfWork, IMapper Map)
        {
            UnitOfWork = unitOfWork;
            //this.EmployeeRepository = EmployeeRepository;
            //DepartmentRepository = departmentRepository;
            this.Map = Map;
        }

        public IActionResult Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
            var mappedEmployee = Map.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel> >( UnitOfWork.EmployeeRepository.GetAll());
            return  View(mappedEmployee);

            }else
            {
                var mappedEmployee = Map.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(UnitOfWork.EmployeeRepository.SearchEmployee(SearchValue));
                return View(mappedEmployee);

            }
        }

        public IActionResult Details(int? id, string ViewName = "Details" )
        {

            if (id == null)
                return NotFound();

            var Employee = UnitOfWork.EmployeeRepository.Get(id);

            if (Employee == null)
                return NotFound();
            var vm = new EmployeeViewModel()
            {
                Id = Employee.Id,
                Name = Employee.Name,
                Age = Employee.Age,
                Address = Employee.Address,
                Salary = Employee.Salary,
                IsActive = Employee.IsActive,
                Email = Employee.Email,
                PhoneNumber = Employee.PhoneNumber,
                HireDate = Employee.HireDate,
                DepartmentId = Employee.DepartmentId,
                Department = Employee.Department,
                ImageName = Employee.ImageName,

            };

            return View(ViewName ,vm);
        }


        public IActionResult Create()
        {
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();

            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel Employee)
        {
            if (ModelState.IsValid)
            {
                //Manual Mapping
                //var mappedEmployee = new Employee()
                //{
                //    Id = Employee.Id,
                //    Name = Employee.Name,
                //    Age = Employee.Age,
                //    DepartmentId = Employee.DepartmentId,
                //    Email = Employee.Email,
                //    HireDate = Employee.HireDate,
                //    IsActive = Employee.IsActive,
                //    PhoneNumber = Employee.PhoneNumber
                //    Salary = Employee.Salary,

                //};
               Employee.ImageName = DocumentSettings.Upload(Employee.Image, "Images");
                var mappedEmployee = Map.Map<EmployeeViewModel , Employee>(Employee);
                UnitOfWork.EmployeeRepository.Add(mappedEmployee);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();

            return View(Employee);

        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();

            //if (id == null)
            //    return NotFound();

            //var Employee = UnitOfWork.EmployeeRepository.Get(id);

            //if (Employee == null)
            //    return NotFound();
            //return View(Employee);
            return Details(id, "Edit");
        }



        [HttpPost]
        [ValidateAntiForgeryToken] //oppisite any outside Tool 
        public IActionResult Edit([FromRoute] int? id, EmployeeViewModel Employee)
        {

            if (id != Employee.Id)
                return BadRequest();
            if (ModelState.IsValid) //serverside validation
            {
                try
                {
                    Employee.ImageName = DocumentSettings.Upload(Employee.Image, "Images");

                    var mappedEmployee = Map.Map<EmployeeViewModel, Employee>(Employee);

                    UnitOfWork.EmployeeRepository.Update(mappedEmployee);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    return View(Employee);
                }
            }
            ViewBag.Departments = UnitOfWork.DepartmentRepository.GetAll();

            return View(Employee);
        }


        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]

        public IActionResult Delete([FromRoute] int? id, EmployeeViewModel Employee)
        {

            if (id != Employee.Id)
                return BadRequest();

            try
            {
                var mappedEmployee = Map.Map<EmployeeViewModel, Employee>(Employee);
                DocumentSettings.Delete(Employee.ImageName, "Images");
                UnitOfWork.EmployeeRepository.Delete(mappedEmployee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(Employee);
            }

        }
    }
}
