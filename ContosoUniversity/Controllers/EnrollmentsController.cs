using AutoMapper;
using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.Comon.Servises;
using ContosoUniversity.Data;
using ContosoUniversity.Entity;
using ContosoUniversity.Models;
using ContosoUniversity.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.Controllers
{
   
    public class EnrollmentsController : Controller
    {
        private IEnrollmentServices enrollmentServices;
        private IStudentServices studentServices;
        private ICourseServices courseServices;
        private Mapping _map;
        public EnrollmentsController(
            IEnrollmentServices enrollmentServices,
            ICourseServices courseServices,
            IStudentServices studentServices,
            Mapping map )
        {
            this.enrollmentServices = enrollmentServices ;
            this.studentServices = studentServices;
            this.courseServices = courseServices;
            this._map = map;
            
        }
        //get Enrollments
        public async  Task<IActionResult> Index()
        {
            var enrollmentEntity = await enrollmentServices.GetAllEnrollments();
                ;
            var enrollmentModel = _map.EntollmentToListEnrollment(enrollmentEntity);
            return View(enrollmentModel);
        }
        // get deatiles

        public async Task<IActionResult> Details(int? id)
        {
            var enrollmentEntity = await enrollmentServices.GetEnrollmentById(id);
            ;
            var enrollmentModel = _map.enrollmentToEnrollmentModel(enrollmentEntity);
            return View(enrollmentModel);
        }

        // get create
        public async Task<IActionResult> Create()
        {
            var studentIds = await studentServices.GetAllStudentIds();
            var courseIds = await courseServices.GetAllCourseIds();

           

            ViewBag.StudentList = new SelectList(studentIds);
            ViewBag.CourseList = new SelectList(courseIds);



            return View();
        }

        // post create

        [HttpPost]
       
        public async Task<JsonResult> Create([Bind("StudentID,CourseID,Grade")] EnrollmentModel Emodel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var enrollmentEntity = _map.enrollmentModelToEnrollment(Emodel);
                    await enrollmentServices.CreateEnrollment(enrollmentEntity);
                    return Json(new { success = true });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "unable to save ");
            }

            return Json(new { success = false });
        }
        //  get enrollment/Edit

        public async Task<IActionResult> Edit(int? id)
        {
            var studentIds = await studentServices.GetAllStudentIds();
            var courseIds = await courseServices.GetAllCourseIds();



            ViewBag.StudentList = new SelectList(studentIds);
            ViewBag.CourseList = new SelectList(courseIds);
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentEntity = await enrollmentServices.GetEnrollmentById(id);
            if (enrollmentEntity == null)
            {
                return NotFound();
            }
            var courseModel = _map.enrollmentToEnrollmentModel(enrollmentEntity);



            return View(courseModel);
        }
        // post : enrollment/edit
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id, EnrollmentModel enrollmentModel)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentEntity = _map.enrollmentModelToEnrollment(enrollmentModel);
            if (await TryUpdateModelAsync(enrollmentModel, "",
                c => c.StudentID, c => c.CourseID,c => c.Grade
                ))
            {
                try
                {
                    await enrollmentServices.UpdateEnrollment(enrollmentEntity);
                    return base.RedirectToAction(nameof(Index));

                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "unable to save ");
                }

            }
            return View(enrollmentModel);
        }

        // GET: enrollment/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollmentEntity = await enrollmentServices.GetEnrollmentById(id) ;

            if (enrollmentEntity == null)
            {
                return NotFound();
            }

            var enrollmentModel = _map.enrollmentToEnrollmentModel(enrollmentEntity);

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(enrollmentModel);
        }

        // post:enrollment/delete

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            await enrollmentServices.DeleteEnrollment(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
