using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Servises
{
    public class CourseServices : ICourseServices 
    {
        private ICourseRepository courseRepository;

        public CourseServices(ICourseRepository courseRepository)
        {
            this.courseRepository = courseRepository;
        }

        public async Task<IEnumerable<Course>> GetAllCourses()
        {
            return await courseRepository.GetAllCourses();
        }
        public async Task<Course> GetCourseById(int? id)
        {
            return await courseRepository.GetCourseById(id);

        }
        public async Task CreateCourse(Course courseEntity)
        {
            await courseRepository.CreateCourse(courseEntity);
        }
        public async Task UpdateStudent(Course courseEntity)
        {
            await courseRepository.UpdateStudent(courseEntity);
        }

        public async Task DeletCourse(int? id)
        {
            await courseRepository.DeletCourse(id);
        }
        public async Task<List<int>> GetAllCourseIds()
        {

            return await courseRepository.GetAllCourseIds();
        }
    }
}
