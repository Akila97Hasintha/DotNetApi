using ContosoUniversity.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccess.Interfaces
{
    public interface IAdiminRepository
    {
        Task AddAdmin(Admin adminEntity);
        Task<Admin> GetAdminByEmail(String emailAddress);

    }
}
