using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SupermarketWEB.Data;
using SupermarketWEB.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SupermarketWEB.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SupermarketContext _context;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(SupermarketContext context, ILogger<RegisterModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public string Email { get; set; } = string.Empty;

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;

        [BindProperty]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
        [Display(Name = "Confirmar Contraseña")]
        public string ConfirmPassword { get; set; } = string.Empty;

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var existingUser = await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == Email);

                if (existingUser != null)
                {
                    ModelState.AddModelError(string.Empty,
                        "Este correo electrónico ya está registrado.");
                    return Page();
                }

                var newUser = new User
                {
                    Email = Email,
                    Password = Password
                };

                await _context.Users.AddAsync(newUser);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Usuario registrado exitosamente.";
                return RedirectToPage("/Account/Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al registrar el usuario");
                ModelState.AddModelError(string.Empty,
                    "Ha ocurrido un error al registrar el usuario. Por favor, intente nuevamente.");
                return Page();
            }
        }
    }
}