using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Users.API.Dto
{
    public class LoginDto
    {
        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "Kullanıcı adı zorunludur!")]
        public string UserName { get; set; }

        [DisplayName("Şifre")]
        [Required(ErrorMessage = "Şifre zorunludur!")]
        public string Password { get; set; }
    }
}
