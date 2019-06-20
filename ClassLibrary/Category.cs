using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JewelryStore.Models
{
    public class Category
    {
        [Key]
        public int? CategoryId { get; set; }

        [Display(Name="Название", Order = 0)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Название категории обязательно для заполнения!")]
        [MaxLength(20, ErrorMessage = "Длина описания не должна превышать 100 символов!")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [MaxLength(100, ErrorMessage = "Длина описания не должна превышать 100 символов!")]
        public string Description { get; set; }


        public List<Jewelry> JewelryList { get; set; }
    }
}
