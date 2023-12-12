using ContosoUniversity.DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Interfaces
{
    public interface IAdminServices
    {
        Task addAdmin(Admin adminEntity);
        Task<Admin> GetAdminByEmail(String emailAddress);
    }
}
