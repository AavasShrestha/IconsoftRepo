using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data.RoutingDB
{
    public class tblClientDetails
    {
        [Key]
        public int client_id { get; set; }

        public string client_name { get; set; }
        public string server_name { get; set; }
        public string db_name { get; set; }
        public string db_username { get; set; }
        public string db_pwd { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
        public int CID { get; set; }
    }
}