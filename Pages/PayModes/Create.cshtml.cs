using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
    public class CreateModel : PageModel
    {
        private readonly SupermarketContext _context;
        private readonly ILogger<CreateModel> _logger;

        public CreateModel(SupermarketContext context, ILogger<CreateModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PayMode PayMode { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }

                _context.PayModes.Add(PayMode);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el método de pago {@PayMode}", PayMode);
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al crear el método de pago. Por favor, intente nuevamente.");
                return Page();
            }
        }
    }
}