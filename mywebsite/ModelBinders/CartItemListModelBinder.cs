using JewelryStore.Models;
using JewelryStore.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;



namespace JewelryStore.UI.Binders
{
    public class CartItemListModelBinder : IModelBinder
    {
        #region IModelBinder Members

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var cartItemList = new List<CartItem>();

            var ids = controllerContext.HttpContext.Request["item.CartItem.CartItemId"];
            var quantities = controllerContext.HttpContext.Request["item.CartItem.Quantity"];

            string[] splitted_ids = ids.Split(',');
            string[] splitted_quantites = quantities.Split(',');
            
            for (int i = 0; i < splitted_ids.Length; i++)
            {                
                var cartItem = new CartItem();
                cartItem.CartItemId = int.Parse(splitted_ids[i]);
                cartItem.Quantity = int.Parse(splitted_quantites[i]);

                cartItemList.Add(cartItem);
            }

            return cartItemList;
        }

        #endregion
    }
}