using ContosoUniversity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Interfaces
{
    public interface ICourseServices
    {
        Task<IEnumerable<Course>> GetAllCourses();
        Task<Course> GetCourseById(int? id);
        Task CreateCourse(Course courseEntity);
        Task UpdateStudent(Course courseEntity);
        Task DeletCourse(int? id);
        Task<List<int>> GetAllCourseIds();
    }
}
