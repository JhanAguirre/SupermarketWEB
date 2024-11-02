using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly SupermarketContext _context;

        public CreateModel(SupermarketContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            LoadCategoryList();
            return Page();
        }

        [BindProperty]
        public Product Product { get; set; } = default!;
        public SelectList CategoryList { get; set; } = default!;

        private void LoadCategoryList()
        {
            CategoryList = new SelectList(_context.Categories, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                LoadCategoryList();
                return Page();
            }

            try
            {
                _context.Products.Add(Product);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al crear el producto. " + ex.Message);
                LoadCategoryList();
                return Page();
            }
        }
    }
}