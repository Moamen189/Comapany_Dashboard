using AutoMapper;
using BussniessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }
        public IMapper Map { get; }

        public EmployeeController(IEmployeeRepository EmployeeRepository , IDepartmentRepository departmentRepository , IMapper Map)
        {
            this.EmployeeRepository = EmployeeRepository;
            DepartmentRepository = departmentRepository;
            this.Map = Map;
        }

        public IActionResult Index()
        {
            var mappedEmployee = Map.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel> >(EmployeeRepository.GetAll());
            return View(mappedEmployee);
        }

        public IActionResult Details(int? id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();

            var Employee = EmployeeRepository.Get(id);

            if (Employee == null)
                return NotFound();
            return View(ViewName, Employee);
        }


        public IActionResult Create()
        {
            ViewBag.Departments = DepartmentRepository.GetAll();

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

                var mappedEmployee = Map.Map<EmployeeViewModel , Employee>(Employee);
                EmployeeRepository.Add(mappedEmployee);
                return RedirectToAction("Index");
            }
            ViewBag.Departments = DepartmentRepository.GetAll();

            return View(Employee);

        }

        public IActionResult Edit(int? id)
        {
            ViewBag.Departments = DepartmentRepository.GetAll();

            //if (id == null)
            //    return NotFound();

            //var Employee = EmployeeRepository.Get(id);

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
                    var mappedEmployee = Map.Map<EmployeeViewModel, Employee>(Employee);

                    EmployeeRepository.Update(mappedEmployee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    return View(Employee);
                }
            }
            ViewBag.Departments = DepartmentRepository.GetAll();

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

                EmployeeRepository.Delete(mappedEmployee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(Employee);
            }

        }
    }
}
