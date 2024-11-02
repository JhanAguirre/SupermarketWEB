using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
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
        public PayMode PayMode { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymode = await _context.PayModes.FirstOrDefaultAsync(m => m.Id == id);

            if (paymode == null)
            {
                return NotFound();
            }
            else
            {
                PayMode = paymode;
            }
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

                var paymode = await _context.PayModes.FindAsync(id);

                if (paymode != null)
                {
                    PayMode = paymode;
                    _context.PayModes.Remove(PayMode);
                    await _context.SaveChangesAsync();
                }

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al eliminar el método de pago {@PayMode}", PayMode);
                ModelState.AddModelError(string.Empty, "Ha ocurrido un error al eliminar el método de pago. Por favor, intente nuevamente.");
                return Page();
            }
        }
    }
}