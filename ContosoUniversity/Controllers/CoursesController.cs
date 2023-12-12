using AutoMapper;
using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.Comon.Repositories;
using ContosoUniversity.Comon.Servises;
using ContosoUniversity.Data;
using ContosoUniversity.Entity;
using ContosoUniversity.Models;
using ContosoUniversity.Web.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
    public class CoursesController : Controller
    {
       
        private ICourseServices courseServices;
        private Mapping _map;

        public CoursesController(ICourseServices courseServices,Mapping map)
        {
            this.courseServices = courseServices;
            _map = map;
            
        }
        //get : Courses
        public   ViewResult Index()
        {
            
;            return View();
        }
        [Authorize]
        public async Task<IActionResult> getCourseList()
        {
            var CourseEntities = await courseServices.GetAllCourses();
            var courses = _map.coursessToListCourses(CourseEntities);
             
            return PartialView("~/Views/Courses/PartialViews/courseIndexPartial.cshtml", courses);
        }

        // get Deatils
        public ViewResult Details(int? id)
        {
            return View(id);
        }
        public async Task<IActionResult> getCourseDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEntity = await courseServices.GetCourseById(id);

            if (courseEntity == null)
            {
                return NotFound();
            }


            var courseModel = _map.courseToCourseModel(courseEntity);
            return PartialView("~/Views/Courses/PartialViews/courseDetailsPartial.cshtml",courseModel);
        }
        // get : Courses/Create
        public IActionResult create()
        {
            return View();
        }

        // POST: Courses/Create
        [Authorize]
        [HttpPost,ActionName("Create")]
      
        public async Task<JsonResult> Create([Bind("CourseID,Title,Credits")] CourseModel courseModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var courseEntity = _map.courseModelToCourse(courseModel);
                    await courseServices.CreateCourse(courseEntity);

                    return Json(new { success = true });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "unable to save ");
            }

            return Json(new { success = false });
        }


        //  get Course/Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEntity = await courseServices.GetCourseById(id);
            if (courseEntity == null)
            {
                return NotFound();
            }
            var courseModel = _map.courseToCourseModel(courseEntity);



            return View(courseModel);
        }
        // post : Course/edit
        [HttpPost,ActionName("Edit")]
        
        public async Task<JsonResult> EditPost(int? id)
        {


            var courseEntity =await courseServices.GetCourseById(id);
            var courseModel = _map.courseToCourseModel(courseEntity);
            if (await TryUpdateModelAsync(courseModel, "",c => c.CourseID,
                c => c.Title, c => c.Credits
                ))
            {
                try
                {
                    var courseNewEntity = _map.courseModelToCourse(courseModel);
                    await courseServices.UpdateStudent(courseNewEntity);
                    return Json(new { success = true });

                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "unable to save ");
                }

            }
            return Json(new { success = false });
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseEntity = await courseServices.GetCourseById(id);

            if (courseEntity == null)
            {
                return NotFound();
            }

            var courseModel = _map.courseToCourseModel(courseEntity);

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(courseModel);
        }

        // post:Courses/delete

        [HttpPost, ActionName("Delete")]
        
        public async Task<JsonResult> DeleteConfirm(int id)
        {
            await courseServices.DeletCourse(id);

            return Json(new { success = true });
        }
    }
}
