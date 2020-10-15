using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Models.ViewModels
{
    public class OrderDetailsCartVM
    {
        public OrderHeader OrderHeader { get; set; }
        public List<ShoppingCart> ListCart { get; set; }
    }
}
