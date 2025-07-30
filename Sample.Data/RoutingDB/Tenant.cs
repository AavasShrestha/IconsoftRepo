using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CBS.Data.RoutingDB
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ConnectionString { get; set; }
    }
}