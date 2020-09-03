using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
    public class Category
    {
        [Key] // Data Annotation (optional, but highly recommended)
        public int Id { get; set; }

        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }

        [Display(Name="Display Order")]
        public int DisplayOrder { get; set; }
    }
}
