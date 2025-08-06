using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data.DTO
{
    public class UserDetail
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // Consider using a secure hash instead of plain text
        public string ConfirmPassword { get; set; } // Consider using a secure hash instead of plain text
        
        public string Remarks { get; set; }
        public string Phone { get; set; }
        //public string Title { get; set; }
        public string FullName { get; set; }
        public string CompanyName { get; set; }
        public string BranchName { get; set; }
        public int Branchid { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }

        //public string Address { get; set; }
    }
}