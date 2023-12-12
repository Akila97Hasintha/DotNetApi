
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
        public async Task UpdateStudent(Student studentEntity)
        {
            _context.Update(studentEntity);
            await _context.SaveChangesAsync();
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
