using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.Data.DTO
{
    public class DocumentDto
    {
        public int Id { get; set; } 
        public int ClientId { get; set; }
        public string DocumentType { get; set; }
        public string FileName { get; set; }
        public int CreatedBy { get; set; }
    }
}
