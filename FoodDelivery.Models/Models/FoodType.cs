using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FoodDelivery.Models
{
    public class FoodType
    {
        [Key] // Data Annotation (optional, but highly recommended)
        public int Id { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Name { get; set; }

    }
}
