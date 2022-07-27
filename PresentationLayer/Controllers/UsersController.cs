using AutoMapper;
using BussniessLayer.Interfaces;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Helper;
using PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if (string.IsNullOrEmpty(SearchValue))
            {
              
                return View(userManager.Users);

            }
            else
            {
                var user = await userManager.FindByEmailAsync(SearchValue);
                return View(new List<ApplicationUser>() { user });

            }
        }

        public IActionResult Details(string id, string ViewName = "Details")
        {

            if (id == null)
                return NotFound();

            var user = userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound();
            

            return View(ViewName, user);
        }


        

        public IActionResult Edit(string id)
        {
     

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
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser UpdatedUser)
        {

            if (id != UpdatedUser.Id)
                return BadRequest();
            if (ModelState.IsValid) //serverside validation
            {
                try
                {
                       var user = await userManager.FindByIdAsync(id);
                    user.UserName = UpdatedUser.UserName;
                    user.NormalizedUserName = UpdatedUser.UserName.ToUpper();
                    user.PhoneNumber = UpdatedUser.PhoneNumber;

                    var result =await userManager.UpdateAsync(user);




                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
          

            return View(UpdatedUser);
        }


        public IActionResult Delete(string id)
        {

            return Details(id, "Delete");
        }

        [HttpPost]

        public async Task<IActionResult> Delete([FromRoute] string id, ApplicationUser deletedUser)
        {

            if (id != deletedUser.Id)
                return BadRequest();

            try
            {
                var result = await userManager.DeleteAsync(deletedUser);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));



                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(deletedUser);



            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
