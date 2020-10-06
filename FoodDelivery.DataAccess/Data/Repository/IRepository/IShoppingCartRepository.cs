using FoodDelivery.DataAccess.IRepository;
using FoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.DataAccess.Data.Repository.IRepository
{
    public interface IShoppingCartRepository: IRepository<ShoppingCart>
    {
        public int IncrementCount(ShoppingCart shoppingCart, int count);
        public int DecrementCount(ShoppingCart shoppingCart, int count);
    }
}
