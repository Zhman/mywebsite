using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace JewelryStore.Models
{
    public class Customer
    {
        [DisplayName("Имя")]
        public string FirstName { get; set; }

        [DisplayName("Фамилия")]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        ErrorMessage = "Не корректный номер телефона")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        ErrorMessage = "Не корректный Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Доставка")]
        public string Delivery { get; set; }

        [DisplayName("Адрес доставки")]
        public string Shipping { get; set; }

        public string Comment { get; set; }
        public int? Discount { get; set; }

        [Key]
        public int? CustomerId { get; set; }
        public List<Order> Orders { get; set; }
        public Guid? UserId { get; set; }
    }
}



