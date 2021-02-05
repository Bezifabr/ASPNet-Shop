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
        [DataType(DataType.Text)]
        public string description { get; set; }

        [Display(Name = "Miniaturka")]
        [DataType(DataType.Text)]
        public string pictureUrl { get; set; }

    }
}
