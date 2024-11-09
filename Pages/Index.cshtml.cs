using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SupermarketWEB.Data;

namespace SupermarketWEB.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SupermarketContext _context;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(SupermarketContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            try
            {
                ViewData["ProductCount"] = await _context.Products.CountAsync();
                ViewData["ProviderCount"] = await _context.Providers.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar estadísticas del dashboard");
                ViewData["ProductCount"] = 0;
                ViewData["ProviderCount"] = 0;
            }
        }
    }
}