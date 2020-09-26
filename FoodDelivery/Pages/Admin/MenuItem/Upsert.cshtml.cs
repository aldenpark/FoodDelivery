using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.MenuItem
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public UpsertModel(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public MenuItemVM MenuItemObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            
            // Map other models to new bind Object (Could do it long hand and bind each model to different objects)
            MenuItemObj = new MenuItemVM
            {
                MenuItem = new Models.MenuItem(),
                CategoryList = _unitOfWork.Category.GetCategoryListForDropDown(),
                FoodTypeList = _unitOfWork.FoodType.GetFoodTypeListForDropDown()
            };

            if (id != null)
            {
                MenuItemObj.MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(u => u.Id == id);
                if (MenuItemObj == null)
                {
                    return NotFound(); // returns a 404 page
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {

            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files; // get, post, put, etc....
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (MenuItemObj.MenuItem.Id == 0) //means a brand new MenuItem
            {

                string fileName = Guid.NewGuid().ToString();
                var uploads = Path.Combine(webRootPath, @"images\menuitems");
                var extension = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                }

                MenuItemObj.MenuItem.Image = @"\images\menuitems\" + fileName + extension;

                _unitOfWork.MenuItem.Add(MenuItemObj.MenuItem);
                _unitOfWork.Save(); //Saved on the Update
            }
            else
            {
                var objFromDb = _unitOfWork.MenuItem.Get(MenuItemObj.MenuItem.Id);
                if (files.Count > 0)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuitems");
                    var extension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }

                    MenuItemObj.MenuItem.Image = @"\images\menuitems\" + fileName + extension;
                }
                else
                {
                    MenuItemObj.MenuItem.Image = objFromDb.Image;
                }

                _unitOfWork.MenuItem.Update(MenuItemObj.MenuItem);
            }

            return RedirectToPage("./Index");
        }
    }
}
