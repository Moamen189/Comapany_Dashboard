using BussniessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        public IEmployeeRepository EmployeeRepository { get; }
        public IDepartmentRepository DepartmentRepository { get; }

        public EmployeeController(IEmployeeRepository EmployeeRepository , IDepartmentRepository departmentRepository)
        {
            this.EmployeeRepository = EmployeeRepository;
            DepartmentRepository = departmentRepository;
        }

        public IActionResult Index()
        {
            return View(EmployeeRepository.GetAll());
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
        public IActionResult Create(Employee Employee)
        {
            if (ModelState.IsValid)
            {
                EmployeeRepository.Add(Employee);
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
        public IActionResult Edit([FromRoute] int? id, Employee Employee)
        {

            if (id != Employee.Id)
                return BadRequest();
            if (ModelState.IsValid) //serverside validation
            {
                try
                {
                    EmployeeRepository.Update(Employee);
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

        public IActionResult Delete([FromRoute] int? id, Employee Employee)
        {

            if (id != Employee.Id)
                return BadRequest();

            try
            {
                EmployeeRepository.Delete(Employee);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return View(Employee);
            }

        }
    }
}
