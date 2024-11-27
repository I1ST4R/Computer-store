using System.ComponentModel.DataAnnotations;

namespace Computer_store.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Данное поле обязательно")]
        public string Password { get; set; }
    }
}
