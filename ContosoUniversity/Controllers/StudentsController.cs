using AutoMapper;
using ContosoUniversity.Comon.Interfaces;
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
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;


namespace ContosoUniversity.Controllers
{
   
    [EnableCors]
    public class StudentsController : Controller
    {
       
        private IStudentServices studentServices;
        private  Mapping _map;
       

        public StudentsController(IStudentServices studentServices,Mapping map)
        {
            
            this.studentServices = studentServices;
            _map = map;

            
        }
        [Authorize]
        public async Task<IActionResult> Index(
  )
        {
            var studentEntity = await studentServices.GetStudents();
            var student = _map.studentsToListStudent(studentEntity);
            
            
            return View(student);
        }
        [Authorize]
        [HttpGet,ActionName("getStudentList")]
        public async Task<IActionResult> getStudentList(
 )
        {
            var studentEntity = await studentServices.GetStudents();
            var student = _map.studentsToListStudent(studentEntity);


            return PartialView("~/Views/Students/PartialViews/studentIndexPartial.cshtml", student);
        }


        // Get: Students/Details
        
        public async Task<IActionResult> Details(int id)
        {
            return View(id);
        }
        [Authorize]
        public async Task<IActionResult> GetStudentDetails(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var studentEntity = await studentServices.GetStudent(id);

            if (studentEntity == null)
            {
                return NotFound();
            }


            var studentModel = _map.studentToStudentModel(studentEntity);
            return PartialView("~/Views/Students/PartialViews/StudentDetailsPartial.cshtml", studentModel);
        }
        // Get: Students/Create
        public IActionResult create()
        {
            return View();
        }
        // POST: Students/Create
        [Authorize]
        [HttpPost,ActionName("Create")]
        
        public async Task<JsonResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate")] StudentModel studentmodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var studentEntity = _map.studentModelToStudent(studentmodel);
                    Console.WriteLine($"LastName: {studentEntity.EnrollmentDate}, FirstMidName: {studentEntity.FirstMidName}");
                    await studentServices.CreateStudent(studentEntity);
                    return Json(new { success = true });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "unable to save ");
            }

            return Json(new { success = false, error = "Model validation failed" });
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEntity = await studentServices.GetStudent(id);
            if (studentEntity == null)
            {
                return NotFound();
            }
            var studentModel = _map.studentToStudentModel(studentEntity);

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(studentModel);
        }

        // post:student/delete
        [Authorize]

        [HttpPost,ActionName("Delete")]
       
        public async Task<JsonResult> DeleteConfirm(int id)
        {
            await studentServices.DeleteStudent(id);

            return Json(new { success = true });
        }

        // get: Students/Edit

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentEntity = await studentServices.GetStudent(id);

            if (studentEntity == null)
            {
                return NotFound();
            }

            var studentModel = _map.studentToStudentModel(studentEntity);

            return View(studentModel);
        }


        //Post : Student/edit
        [Authorize]
        [HttpPost,ActionName("Edit")]
       
        public async Task<JsonResult> EditPost(int? id,StudentModel studentModel)
        {

            var studentEntity = _map.studentModelToStudent(studentModel);


            if (await TryUpdateModelAsync(studentModel, "",
                c => c.FirstMidName, c => c.LastName,c => c.EnrollmentDate
                ))
            {
                
                try
                {
                    await studentServices.UpdateStudent(studentEntity);
                    return Json(new {success = true});

                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "unable to save ");
                }

            }
            return Json(new { success = false });
        }




    }
}
