using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JewelryStore.Models;

namespace JewelryStore.UI.Models
{
    public class CartItemListModel
    {
        public Jewelry Jewelry { get; set; }

        public CartItem CartItem { get; set; }

        public string CategoryName { get; set; }

        public Order Order { get; set; }

        public Customer Customer { get; set; }

    }
}