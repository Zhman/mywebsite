using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStore.Models
{
    public class Registration
    {
        [Key]
        public int CustomerId { get; set; }

        public Guid? UserId { get; set; }

        [Required(ErrorMessage = "Введите имя пользователя.", AllowEmptyStrings = true)]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Введите фамилию пользователя.", AllowEmptyStrings = true)]
        public string LastName { get; set; }

        [Required(ErrorMessage= "Введите пароль.", AllowEmptyStrings= false)]
        [DataType(DataType.Password)]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "Пароль должен содержать не менее 8 символов.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароль не совпадает.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [RegularExpression(@"^([0-9a-zA-Z]([\+\-_\.][0-9a-zA-Z]+)*)+@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,3})$",
        ErrorMessage = "Введите правильный Email")]
        public string Email { get; set; }
    }

    public class Login
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
