using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleApp.Web.Models
{
    public class ResponseBody
    {
        public string userName { get; set; }
        public string displayName { get; set; }
        public List<string> roles { get; set; }
    }
}
