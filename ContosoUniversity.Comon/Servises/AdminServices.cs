using ContosoUniversity.Comon.Interfaces;
using ContosoUniversity.DataAccess.Entity;
using ContosoUniversity.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.Comon.Servises
{
    public  class AdminServices : IAdminServices
    {
        private IAdiminRepository _adminRepository;
        public AdminServices(IAdiminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }
        public async Task addAdmin(Admin adminEntity)
        {
            await _adminRepository.AddAdmin(adminEntity);
        }

        public async Task<Admin> GetAdminByEmail(String emailAddress)
        {
            return await _adminRepository.GetAdminByEmail(emailAddress);
            
        }
    }
}
