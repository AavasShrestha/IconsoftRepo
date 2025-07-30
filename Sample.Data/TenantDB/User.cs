using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data.TenantDB
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? ModifiedDate { get; set; }
        public string? Remarks { get; set; }
        public string? Phone { get; set; }
        public int Branchid { get; set; }
        public bool IsActive { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string ConfirmPassword { get; set; }

        //Add Address
    }
}