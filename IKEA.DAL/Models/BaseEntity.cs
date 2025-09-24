using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Models
{
    public class BaseEntity
    {
        //table per concrete class 

        public int id { get; set; }
        public DateTime CreatedOn { get; set; } 
        public int createdBy { get; set; }

        public DateTime UpdatedOn { get; set; }

        public int updatedBy { get; set; }

        public bool isDeleted { get; set; }



    }
}
