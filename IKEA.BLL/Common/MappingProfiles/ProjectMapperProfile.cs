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
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest=>dest.DepartmentName,options=>options.MapFrom(src => src.Department !=null? src.Department.Name : "N/A"))
                //3shan yhwl mn  employeedto l  employee
                .ReverseMap();
            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department != null ? src.Department.Name : "N/A"))
                //3shan yhwl mn  EmployeeDetailsDto l  employee

                .ReverseMap();
            CreateMap<CreateEmployeeDto, Employee>()
                .ForMember(dest=>dest.EmployeeType, options=>options.MapFrom(src=>src.EmployeeType)); //twgih manual (a5tlaf bink w bin el teammate)
            CreateMap<UpdateEmployeeDto, Employee>().ReverseMap();


           
        }
    }
}
