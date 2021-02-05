using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ShopItem
    {
        public int id { get; set; }

        [Display(Name = "Nazwa")]
        [Required]
        [DataType(DataType.Text)]
        [StringLength(100, MinimumLength = 5)]
        public string name { get; set; }

        [Display(Name = "Opis")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; }

        [Display(Name = "Zdjęcie")]
        [DataType(DataType.ImageUrl)]
        public string pictureUrl { get; set; }

        [Display(Name = "Cena")]
        [DataType(DataType.Currency)]
        public decimal price { get; set; }

    }
}
