using CBS.Data.TenantDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.TenantDB
{
    public class Document
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }

        public DateTime CreatedDate { get; set; }

        //public int UserId { get; set; }
        public int CreatedBy { get; set; }

        public Client Client { get; set; }

        //public User User { get; set; }
        public User CreatedByUser { get; set; }

    }
}
