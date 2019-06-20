using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace JewelryStore.Models
{
    public class Order
    {
        public int? OrderId { get; set; }
       
        public DateTime? Date { get; set; }
        public OrderStatus Status { get; set; }

        
        public virtual List<CartItem> CartItems { get; set; }
        public decimal? Price { get; set; }
        public string Comment { get; set; }
        public int? Discount { get; set; }
        public Guid? UserId { get; set; }
        public string SessionId { get; set; }

        //[Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DisplayName("Имя")]        
        public string FirstName { get; set; }

        
        [DisplayName("Фамилия")]        
        public string LastName { get; set; }

        //[Required(ErrorMessage = "Поле обязательно для заполнения")]
        [DisplayName("Email")]
        //[RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}",
        //ErrorMessage = "Не корректный Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Доставка")]
        public string Delivery { get; set; }

        public enum Payment
        {
            CoD = 0,
            Card = 1,
            Pickup = 2,
        }

        //[Required(ErrorMessage = "Address is required")]
        [DisplayName("Адрес доставки")]        
        public string Shipping { get; set; }

        //[Required(ErrorMessage = "Поле обязательно для заполнения")]
        //[DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$",
        //ErrorMessage = "Не корректный номер телефона")]
        public string Phone { get; set; }

        
    }
    

}
