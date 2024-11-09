using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using Microsoft.AspNetCore.Authorization;

namespace SupermarketWEB.Pages.Providers
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly SupermarketContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(SupermarketContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Provider Provider { get; set; } = default!;

        public IActionResult OnGet()
        {
            ViewData["MaxDate"] = DateTime.Today.AddYears(-18).ToString("yyyy-MM-dd");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                // Log para verificar que el método se está llamando
                _logger.LogInformation("Iniciando creación de proveedor");

                if (!ModelState.IsValid)
                {
                    _logger.LogError("ModelState no es válido");
                    foreach (var modelState in ModelState.Values)
                    {
                        foreach (var error in modelState.Errors)
                        {
                            _logger.LogError($"Error de validación: {error.ErrorMessage}");
                        }
                    }
                    return Page();
                }

                // Log de los datos del proveedor
                _logger.LogInformation($"Datos del proveedor: Nombre={Provider.Name}, Email={Provider.Email}");

                await _context.Providers.AddAsync(Provider);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Proveedor creado exitosamente");
                TempData["SuccessMessage"] = "Proveedor creado exitosamente.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el proveedor");
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al crear el proveedor. Por favor, intente nuevamente.");
                return Page();
            }
        }
    }
}