using AutoMapper;
using BussniessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Models;
using System;

namespace PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository departmentRepository;
        private readonly IMapper mapper;

        public DepartmentController(IDepartmentRepository departmentRepository , IMapper Mapper)
        {
            this.departmentRepository = departmentRepository;
            this.mapper = Mapper;
        }
        public IActionResult Index()
        {
            //ViewData["Message"] = "Hellp View Data";
            //ViewBag.Messages = "Hello View Bag";
        
            return View(departmentRepository.GetAll());
        }

        public IActionResult Details(int? id , string ViewName= "Details")
        {

           if(id == null )
                return NotFound();

           var Department = departmentRepository.Get(id);

            if (Department== null)
                return NotFound();
            return View(ViewName ,Department);
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
                departmentRepository.Add(DeptModel);
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

                    departmentRepository.Update(DeptModel);
                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
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


                     departmentRepository.Delete(DeptModel);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {

                    return View(department);
                }
            
            
        }
    }
}
