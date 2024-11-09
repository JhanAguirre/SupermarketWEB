using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;
using SupermarketWEB.Models;

namespace SupermarketWEB.Pages.PayModes
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(SupermarketContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IList<PayMode> PayModes { get; set; } = default!;

        public async Task OnGetAsync()
        {
            try
            {
                PayModes = await _context.PayModes.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar los métodos de pago");
                PayModes = new List<PayMode>();
            }
        }
    }
}