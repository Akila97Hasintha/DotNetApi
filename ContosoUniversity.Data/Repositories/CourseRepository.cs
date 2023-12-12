using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.Data;
using ContosoUniversity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private SchoolContext _context;
        public CourseRepository(SchoolContext context )
        {
            _context = context;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            var courseEntity = await _context.Courses.ToArrayAsync();
            return courseEntity;
        }
        public async Task<Course> GetCourseById(int? id)
        {
            var courseEntity = await _context.Courses
         .FirstOrDefaultAsync(m => m.CourseID == id);


            return courseEntity;

        }
        public async Task CreateCourse(Course courseEntity)
        {
            _context.Add(courseEntity);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateStudent(Course courseEntity)
        {
            _context.Update(courseEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeletCourse(int? id)
        {
            var courseEntity = await _context.Courses.FirstOrDefaultAsync(m => m.CourseID == id);
            _context.Courses.Remove(courseEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetAllCourseIds()
        {
            
            return await _context.Courses.Select(s => s.CourseID).ToListAsync();
        }

    }
}
