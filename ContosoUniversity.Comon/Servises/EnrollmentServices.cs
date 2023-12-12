using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.Comon.Repositories;
using ContosoUniversity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Servises
{
    public class EnrollmentServices : IEnrollmentServices
    {
        private IEnrollmentRepository enrollmentRepository;
        public EnrollmentServices(IEnrollmentRepository enrollmentRepository)
        {
            this.enrollmentRepository = enrollmentRepository;
        }
        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            return await enrollmentRepository.GetAllEnrollments();
        }

        public async Task CreateEnrollment(Enrollment enrollmentEntity)
        {
            await enrollmentRepository.CreateEnrollment(enrollmentEntity);

        }
        public async Task<Enrollment> GetEnrollmentById(int? id)
        {
            var enrollmentEntity = await enrollmentRepository.GetEnrollmentById(id);


            return enrollmentEntity;

        }
        public async Task UpdateEnrollment(Enrollment enrollmentEntity)
        {
            await enrollmentRepository.UpdateEnrollment(enrollmentEntity);
        }

        public async Task DeleteEnrollment(int? id)
        {
            await enrollmentRepository.DeleteEnrollment(id);
        }

    }
}
