using AutoMapper;
using BussniessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;

namespace PresentationLayer.Controllers
{
    [Authorize]

    public class DepartmentController : Controller
    {
        //private readonly IDepartmentRepository departmentRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper Mapper)
        {
            //this.departmentRepository = departmentRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = Mapper;
        }
        public  IActionResult Index()
        {
            //ViewData["Message"] = "Hellp View Data";
            //ViewBag.Messages = "Hello View Bag";
            var DeptModel = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(unitOfWork.DepartmentRepository.GetAll());


            return  View(DeptModel);
        }

        public IActionResult Details(int? id , string ViewName= "Details")
        {

           if(id == null )
                return NotFound();

           var Department = unitOfWork.DepartmentRepository.Get(id);

            if (Department== null)
                return NotFound();

            var Vm = new DepartmentViewModel()
            {
                Id = Department.Id,
                Name = Department.Name,
                Code = Department.Code,
                DateOfCreation = Department.DateOfCreation,
                Departments = Department.Departments,


            };
            return View(ViewName , Vm);
        }


        public IActionResult Create()
        {
            TempData["Message"] = "Department Created Successfully";

            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                var DeptModel = mapper.Map<DepartmentViewModel, Department>(department);
                unitOfWork.DepartmentRepository.Add(DeptModel);
                return RedirectToAction("Index");
            }
            return View(department);
            
        }
        
        public IActionResult Edit(int? id)
        {

            //if (id == null)
            //    return NotFound();

            //var Department = departmentRepository.Get(id);

            //if (Department == null)
            //    return NotFound();
            //return View(Department);
            return Details(id, "Edit");
        }
          


        [HttpPost]
        [ValidateAntiForgeryToken] //oppisite any outside Tool 
        public IActionResult Edit([FromRoute] int? id , DepartmentViewModel department)
        {

            if(id!= department.Id)
                return BadRequest();
            if (ModelState.IsValid) //serverside validation
            {
                try
                {
                    var DeptModel = mapper.Map<DepartmentViewModel, Department>(department);

                    unitOfWork.DepartmentRepository.Update(DeptModel);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception)
                {
                      
                    return View(department);
                }
            }
            return View(department);
        }


        public IActionResult Delete(int? id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]

        public IActionResult Delete([FromRoute] int? id, DepartmentViewModel department)
        {

            if (id != department.Id)
                return BadRequest();
           
                try
                {
                    var DeptModel = mapper.Map<DepartmentViewModel, Department>(department);


                unitOfWork.DepartmentRepository.Delete(DeptModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    return View(department);
                }
            
            
        }
    }
}
