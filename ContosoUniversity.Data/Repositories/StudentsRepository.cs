
using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.Data;
using ContosoUniversity.Entity;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Repositories
{
    public class StudentsRepository : IStudentsRepository
    {
        private SchoolContext _context;
        
        public StudentsRepository(SchoolContext context)
        {
            
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var studentEntity = await _context.Students.ToArrayAsync();
            return studentEntity ;
        }
        public async Task<Student> GetStudentById(int? id)
        {
           var studentEntity =await  _context.Students
                .Include(s => s.Enrollments)
            .ThenInclude(e => e.Course)
        .FirstOrDefaultAsync(m => m.ID == id);
                

            return studentEntity;

        }
        public async Task CreateStudent(Student studentEntity)
        {
            _context.Add(studentEntity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudent(Student studentEntity,int id)
        {
            var stuentOldEntity = await GetStudentById(id);
            if (stuentOldEntity == null)
            {
                throw new Exception("Student not found");
            }

            UpdateStudentProperties(stuentOldEntity, studentEntity);
            _context.Entry(stuentOldEntity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

             void UpdateStudentProperties(Student student_old, Student student_new)
            {
                if (!string.IsNullOrEmpty(student_new.LastName) && student_old.LastName != student_new.LastName)
                {
                    student_old.LastName = student_new.LastName;
                    Console.WriteLine("ok");
                }

                if (!string.IsNullOrEmpty(student_new.FirstMidName) && student_old.FirstMidName != student_new.FirstMidName)
                {
                    student_old.FirstMidName = student_new.FirstMidName;
                }

                if (  student_old.EnrollmentDate != student_new.EnrollmentDate)
                {
                    student_old.EnrollmentDate = student_new.EnrollmentDate;
                }
            }
        }
        


        public async Task DeleteStudent(int? id)
        {
            var studentEntity = await _context.Students.FirstOrDefaultAsync(m => m.ID == id);
            _context.Students.Remove(studentEntity);
            await _context.SaveChangesAsync();
        }
        public async Task<List<int>> GetAllStudentIds()
        {
            var studentIds = await _context.Students.Select(s => s.ID).ToListAsync();
            return studentIds;
        }
    }
}
