using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace JewelryStore.Models
{
    public class CartItem
    {
        public int? CartItemId { get; set; }
        //public string ShoppingCartId { get; set; }
        public string CartId { get; set; }
        public string ItemId { get; set; }
        public int? OrderId { get; set; }
        public string SessionId { get; set; }//Session.SessionID
        public int? JewelryId { get; set; }
        //[IntegerValidator(MinValue = 1, MaxValue = 100)] 

        //[Required]
        [Range(1, 100, ErrorMessage ="Кол-во не может быть меньше 1 и больше 100")]
        public int? Quantity { get; set; }//1        
        public decimal? Amount { get; set; }//Quantity*Price
        public DateTime? Date { get; set; }//DateTime.Now
        public string Comment { get; set; }
        public Guid? UserId { get; set; }
        public int? CustomerId { get; set; }    
        
        public virtual Order Order { get; set; }
        public virtual Jewelry Jewelry { get; set; }

    }
}
