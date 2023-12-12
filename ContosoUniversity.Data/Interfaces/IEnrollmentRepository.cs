using ContosoUniversity.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task<IEnumerable<Enrollment>> GetAllEnrollments();
       Task CreateEnrollment(Enrollment enrollmentEntity);
        Task<Enrollment> GetEnrollmentById(int? id);
        Task UpdateEnrollment(Enrollment enrollmentEntity);
        Task DeleteEnrollment(int? id);


    }
}
