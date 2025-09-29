using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Dto_s.DepartmentsDto_s
{
    public class CreateDepartmentDto
    {
        [Required (ErrorMessage ="Name is reqierd")]
        public string Name { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "Code is reqierd")]
        public string code { get; set; }

    }
}
