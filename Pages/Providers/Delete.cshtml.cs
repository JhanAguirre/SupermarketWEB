using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Providers
{
    public class DeleteModel : PageModel
    {
        private readonly SupermarketContext _context;
        private readonly ILogger<DeleteModel> _logger;

        public DeleteModel(SupermarketContext context, ILogger<DeleteModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Provider Provider { get; set; } = default!;

        public bool HasRelatedRecords { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var provider = await _context.Providers.FirstOrDefaultAsync(m => m.Id == id);
            if (provider == null)
            {
                return NotFound();
            }

            Provider = provider;
            

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var provider = await _context.Providers.FindAsync(id);
                if (provider != null)
                {
                    Provider = provider;
                    _context.Providers.Remove(Provider);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Proveedor eliminado exitosamente.";
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el proveedor {ProviderId}", id);
                TempData["ErrorMessage"] = "Ha ocurrido un error al eliminar el proveedor.";
                return RedirectToPage("./Index");
            }
        }
    }
}