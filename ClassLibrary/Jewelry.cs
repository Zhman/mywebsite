using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace JewelryStore.Models
{
    public class Jewelry
    {
        public int? JewelryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [Display(Name = "Категория", Order = 0)]
        public int? CategoryId { get; set; }

        //Цена
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }        
        //Размер скидки
        [DataType(DataType.Currency)]
        public decimal? Offer { get; set; }
        //Цена со скидкой
        [DataType(DataType.Currency)]
        public decimal? OfferPrice { get; set; }        

        
        public bool IsAvailable { get; set; } 
        public bool Novelty { get; set; } 
        public bool Discounted { get; set; }

        public byte[] Image { get; set; }
        [HiddenInput(DisplayValue = false)]
        public string ImageMimeType { get; set; }

    }
}
