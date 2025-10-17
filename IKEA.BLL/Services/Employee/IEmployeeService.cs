using IKEA.BLL.Dto_s.DepartmentsDto_s;
using IKEA.BLL.Dto_s.EmployeeDto_s;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IKEA.BLL.Services.Employee
{
    public interface IEmployeeService
    {
        public IEnumerable<EmployeeDto> GetAllEmployees();

        public IEnumerable<EmployeeDto> GetEmployees(string ?searchValue);
        public EmployeeDetailsDto GetEmployeeById(int id);
        public int AddEmployee(CreateEmployeeDto dto);
        public int UpdateEmployee(UpdateEmployeeDto dto);
        public int DeleteEmployee(int?id);


    }
}
