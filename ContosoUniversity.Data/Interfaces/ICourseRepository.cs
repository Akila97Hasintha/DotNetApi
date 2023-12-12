using ContosoUniversity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<List<int>> GetAllCourseIds();
        Task CreateCourse(Course courseEntity);
        Task<Course> GetCourseById(int? id);
        Task UpdateStudent(Course courseEntity);
        Task DeletCourse(int? id);
    }
}
