using ContosoUniversity.Data;
using ContosoUniversity.DataAccess.Entity;
using ContosoUniversity.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccess.Repositories
{
    
    public class AdminRepository : IAdiminRepository
    {
        private SchoolContext _context;
        public AdminRepository(SchoolContext context)
        {
            _context = context;
        }

        public async Task AddAdmin(Admin adminEntity)
        {
            _context.Add(adminEntity);
            await _context.SaveChangesAsync();
        }

        public  Task<Admin> GetAdminByEmail(String emailAddress)
        {
            var admin =  _context.Admins.FirstOrDefaultAsync(a => a.EmailAddress == emailAddress);
            return admin;
        }
    }
}
