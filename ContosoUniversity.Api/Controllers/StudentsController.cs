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
using Microsoft.AspNetCore.Http.HttpResults;


namespace ContosoUniversity.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    

    public class StudentsController : Controller
    {
       
        private IStudentServices studentServices;
        private  Mapping _map;
       

        public StudentsController(IStudentServices studentServices,Mapping map)
        {
            
            this.studentServices = studentServices;
            _map = map;

            
        }
      
        
        [HttpGet("Index")]
        public async Task<IActionResult> getStudentList(
 )
        {
            var studentEntity = await studentServices.GetStudents();
            var student = _map.studentsToListStudent(studentEntity);


            return Ok(student);
        }


        // Get: Students/Details


        [HttpGet("Details/{id}")]
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
            return Ok(studentModel);
        }
        // Get: Students/Create
        
        
        // POST: Students/Create
        
        [HttpPost("Create")]
        
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

        

        // post:student/delete
       

        [HttpPost("Delete")]
       
        public async Task<JsonResult> DeleteConfirm(int id)
        {
            await studentServices.DeleteStudent(id);

            return Json(new { success = true });
        }

        // get: Students/Edit

       


        //Post : Student/edit
        
        [HttpPost("Edit")]
       
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
