using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.Providers
{
    public class EditModel : PageModel
    {
        private readonly SupermarketContext _context;
        private readonly ILogger<EditModel> _logger;

        public EditModel(SupermarketContext context, ILogger<EditModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Provider Provider { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                if (Provider.Age < 18)
                {
                    ModelState.AddModelError("Provider.DateOfBirth", "El proveedor debe ser mayor de edad.");
                    return Page();
                }

                _context.Attach(Provider).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Proveedor actualizado exitosamente.";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ProviderExistsAsync(Provider.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el proveedor {@Provider}", Provider);
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al actualizar el proveedor. Por favor, intente nuevamente.");
                return Page();
            }
        }

        private async Task<bool> ProviderExistsAsync(int id)
        {
            return await _context.Providers.AnyAsync(e => e.Id == id);
        }
    }
}