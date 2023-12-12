using ContosoUniversity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Interfaces
{
    public  interface IStudentServices
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudent(int? id);
        Task  CreateStudent(Student studentEntity);
        Task UpdateStudent(Student studentEntity);
        Task DeleteStudent(int? id);
        Task<List<int>> GetAllStudentIds();
    }
}
