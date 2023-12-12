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

    public class EnrollmentRepository : IEnrollmentRepository
    {
        private SchoolContext _context;

        public EnrollmentRepository(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Enrollment>> GetAllEnrollments()
        {
            var enrollmentEntity = await _context.Enrollments.ToArrayAsync();
            return enrollmentEntity;
        }
        public async Task<Enrollment> GetEnrollmentById(int? id)
        {
            var enrollmentEntity = await _context.Enrollments
         .FirstOrDefaultAsync(m => m.EnrollmentID == id);


            return enrollmentEntity;

        }
        public async Task CreateEnrollment(Enrollment enrollmentEntity)
        {
            _context.Add(enrollmentEntity);
            await _context.SaveChangesAsync();
            
        }
        public async Task UpdateEnrollment(Enrollment enrollmentEntity)
        {
            _context.Update(enrollmentEntity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEnrollment(int? id)
        {
            var enrollmentEntity = await _context.Enrollments.FirstOrDefaultAsync(m => m.EnrollmentID == id);
            _context.Enrollments.Remove(enrollmentEntity);
            await _context.SaveChangesAsync();
        }
    }
}
