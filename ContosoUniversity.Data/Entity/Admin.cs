using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccess.Entity
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

       
        public string EmailAddress { get; set; }

        
        public string FirstName { get; set; }


        
        public string LastName { get; set; }

        
        public string Password { get; set; }
    }
}
