using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Users.Entities.Entities.Concreate;

namespace Users.API.Dto
{
    public class UserDto
    {
        [Key]
        public int UserId { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı zorunludur!")]
        public string UserName { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "Şifre zorunludur!")]
        public string Password { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public string? Token { get; set; }

    }
}
