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
    public class StudentServices : IStudentServices
    {
        private IStudentsRepository studentsRepository;
        public StudentServices(IStudentsRepository studentsRepository)
        {
            this.studentsRepository = studentsRepository;
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await studentsRepository.GetAllStudents();
        }
        public async Task<Student> GetStudent(int? id)
        {
            return await studentsRepository.GetStudentById(id);

        }
        public async Task CreateStudent(Student studentEntity)
        {
            await studentsRepository.CreateStudent(studentEntity);
        }
        public async Task UpdateStudent(Student studentEntity)
        {
            await studentsRepository.UpdateStudent(studentEntity);
        }

        public async Task DeleteStudent(int? id)
        {
            await studentsRepository.DeleteStudent(id);
        }
        public async Task<List<int>> GetAllStudentIds()
        {
            return await studentsRepository.GetAllStudentIds();
        }

    }
}
