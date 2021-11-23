using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Web.Models
{
    public class Territories
    {
        public int id { get; set; }
        public string name { get; set; }
        public int? parent { get; set; }
    }

}
