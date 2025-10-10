using AutoMapper;
using IKEA.BLL.Dto_s.EmployeeDto_s;
using IKEA.DAL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.BLL.Common.MappingProfiles
{
    public class ProjectMapperProfile:Profile
    {
        //object
        public ProjectMapperProfile()
        {
            CreateMap<Employee, EmployeeDto>().ReverseMap();
            CreateMap<Employee, EmployeeDetailsDto>().ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest=>dest.EmployeeType, options=>options.MapFrom(src=>src.EmployeeType)); //twgih manual (a5tlaf bink w bin el teammate)
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();


           
        }
    }
}
