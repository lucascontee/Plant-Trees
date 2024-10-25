using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PlantTrees.Context;
using PlantTrees.Entities;
using System.Security.Cryptography;
using System.Text;

namespace PlantTrees.Pages
{
    public class DeletePlanterModel : PageModel
    {

        private readonly PlantTreesContext _context;

        public DeletePlanterModel(PlantTreesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Planter Planter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (string.IsNullOrEmpty(Planter.Email) || string.IsNullOrEmpty(Planter.Password))
            {
                ModelState.AddModelError(string.Empty, "Email and password are required.");
            }

            var planter = _context.Planters.FirstOrDefault(p => p.Email == Planter.Email);

            if (planter == null)
            {
                ModelState.AddModelError("Planter.Email", "No planter found, rewiew your Email");
                return Page();
            }

            var storedPassword = planter.Password.Split(':');
            var salt = Convert.FromBase64String(storedPassword[0]);
            var storedHash = Convert.FromBase64String(storedPassword[1]);

            using (var hmac = new HMACSHA512(salt))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(Planter.Password);
                var computedHash = hmac.ComputeHash(passwordBytes);

                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                    {
                        ModelState.AddModelError("Planter.Password", "Incorrect Password.");
                        return Page();
                    }
                }
            }

            _context.Planters.Remove(planter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./GetPlanters");

        }
    }
}
