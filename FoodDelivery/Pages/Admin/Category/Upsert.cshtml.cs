using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.Category
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.Category CategoryObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            CategoryObj = new Models.Category();

            if(id != null)
            {
                CategoryObj = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
                if(CategoryObj == null)
                {
                    return NotFound(); // returns a 404 page
                }
            }

            return Page();
        }

        public IActionResult OnPost()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if(CategoryObj.Id == 0) //means a brand new category
            {
                _unitOfWork.Category.Add(CategoryObj);
                _unitOfWork.Save(); //Saved on the Update
            }
            else
            {
                _unitOfWork.Category.Update(CategoryObj);
            }

            return RedirectToPage("./Index");
        }


    }
}
