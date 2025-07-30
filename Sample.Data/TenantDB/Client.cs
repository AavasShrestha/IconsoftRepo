using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.TenantDB
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientType { get; set; }
        public string OrganizationName { get; set; }
        public string Email  { get; set; }
        public string Country { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string City { get; set; }
        public string Description { get; set; }
    }
}
