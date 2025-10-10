using IKEA.DAL.Models.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA.DAL.Configrations
{
    public class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {

         

            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,3)");


            //for gender and EmployeType

                                                    //Direction: From your C# application to the database.
            builder.Property(e => e.Gender).HasConversion((empGender) => empGender.ToString()

            //Direction: From the database to your C# application.
            , (gender) => (Gender)Enum.Parse(typeof(Gender), gender));


            builder.Property(e => e.EmployeeType).HasConversion((empType) => empType.ToString()
            , (emp) => (EmployeeType)Enum.Parse(typeof(EmployeeType), emp));

        }
    }
}
