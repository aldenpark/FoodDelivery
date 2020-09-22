using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodDelivery.DataAccess.Data.Repository
{
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository
    {
        private readonly ApplicationDbContext _db;

        public MenuItemRepository(ApplicationDbContext db) : base(db)  // ctor tabtab
        {
            _db = db;
        }

        public void Update(MenuItem MenuItem)
        {
            var objFromDb = _db.MenuItem.FirstOrDefault(s => s.Id == MenuItem.Id);

            //var Id = objFromDb.Id;
            //objFromDb = MenuItem;
            //objFromDb.Id = Id;
            objFromDb.Name = MenuItem.Name;
            objFromDb.CategoryId = MenuItem.CategoryId;
            objFromDb.Description = MenuItem.Description;
            objFromDb.Price = MenuItem.Price;

            if (MenuItem.Image != null)
            {
                objFromDb.Image = MenuItem.Image;
            }

            _db.SaveChanges();
        }
    }
}
