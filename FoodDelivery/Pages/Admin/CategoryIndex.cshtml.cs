using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.DataAccess.Data;

namespace FoodDelivery.Pages.Admin
{
    public class CategoryIndexModel : PageModel
    {
        private readonly FoodDelivery.DataAccess.Data.ApplicationDbContext _context;

        public CategoryIndexModel(FoodDelivery.DataAccess.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FoodDelivery.Models.Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
