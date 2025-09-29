using IKEA.BLL.Dto_s.DepartmentsDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Services.Department
{
    public  interface IDepartmentService
    {
        
        public IEnumerable<DepartmentDto> GetAllDepartments();
        public DepartmentDetailsDto GetDepartmentById(int id);
        public int AddDepartment(CreateDepartmentDto department);
        public int UpdateDepartment(UpdatedDepartmentDto department);
        public int DeleteDepartment(int id);


    }
}
