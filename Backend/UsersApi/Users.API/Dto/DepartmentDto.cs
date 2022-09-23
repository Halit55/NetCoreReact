using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Users.API.Dto
{
    public class DepartmentDto
    {
        [Key]
        public int DepartmentId { get; set; }

        [DisplayName("Departman Adı")]
        [Required(ErrorMessage = "Departman adı zorunludur!")]
        public string DepartmentName { get; set; }
    }
}
