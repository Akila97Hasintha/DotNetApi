
using ContosoUniversity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Interfaces
{
    public interface IStudentsRepository 
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<List<int>> GetAllStudentIds();
        Task<Student> GetStudentById(int? id);
        Task UpdateStudent(Student studentEntity);
        Task CreateStudent(Student studentEntity);
        Task DeleteStudent(int? id);
    }
}
