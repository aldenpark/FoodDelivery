using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.FoodType
{
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [BindProperty]
        public Models.FoodType FoodTypeObj { get; set; }

        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new Models.FoodType();

            if(id != null)
            {
                FoodTypeObj = _unitOfWork.FoodType.GetFirstorDefault(u => u.Id == id);
                if(FoodTypeObj == null)
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

            if(FoodTypeObj.Id == 0) //means a brand new FoodType
            {
                _unitOfWork.FoodType.Add(FoodTypeObj);
                _unitOfWork.Save(); //Saved on the Update
            }
            else
            {
                _unitOfWork.FoodType.Update(FoodTypeObj);
            }

            return RedirectToPage("./Index");
        }


    }
}
