using System.ComponentModel.DataAnnotations;

namespace session03_MVC_.PL.ViewModels.DepartmentsVms
{
    public class DepartmentVm
    {


        public int Id { get; set; }
        [Required(ErrorMessage ="the name is requierd")]
        public string Name { get; set; }
        public string? description { get; set; }
        [Required(ErrorMessage = "The Code is required")]
        public string code { get; set; }


    }
}
