using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodDelivery.DataAccess.Data.Repository;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using FoodDelivery.Utility;

namespace FoodDelivery.Pages.Customer.Home
{
    [Authorize] // locks down page so user must be logged in to view it
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public DetailsModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }

        public void OnGet(int id)
        {
            ShoppingCartObj = new ShoppingCart()
            {
                MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(includeProperties: "Category,FoodType", filter: c => c.Id == id),
                MenuItemId = id
            };
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                // get guid of user  add default token providers in startup
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ShoppingCartObj.ApplicationUserId = claim.Value;

                // get shopping cart that might exist in db or save one
                ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstorDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.MenuItemId == ShoppingCartObj.MenuItemId);
                if(cartFromDb == null)
                {
                    _unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                }
                else
                {
                    _unitOfWork.ShoppingCart.IncrementCount(cartFromDb, ShoppingCartObj.Count);
                }
                _unitOfWork.Save();

                var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count;
                // Establish the session
                HttpContext.Session.SetInt32(SD.ShoppingCart,count); // (string)name and (int)value
                
                return RedirectToPage("Index");
            }
            else
            {
                ShoppingCartObj.MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(includeProperties: "Category,FoodType", filter: c => c.Id == ShoppingCartObj.MenuItem.Id);
                return Page();
            }
        }
    }
}