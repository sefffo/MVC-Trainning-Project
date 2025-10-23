using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models.Users
{
    public class Email
    {
        public int id { set; get; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

    }
}
