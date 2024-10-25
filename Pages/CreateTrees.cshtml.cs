using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlantTrees.Context;
using PlantTrees.Entities;
using System.Security.Cryptography;
using System.Text;

namespace PlantTrees.Pages
{
    public class CreateTreesModel : PageModel
    {
        private readonly PlantTreesContext _context;

        public CreateTreesModel(PlantTreesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Tree Tree { get; set; }

        [BindProperty]
        public Planter Planter { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var planter = await _context.Planters.FirstOrDefaultAsync(p => p.Username == Planter.Username);

            if (!VerifyPassword(Planter.Password, planter.Password))
            {
                ModelState.AddModelError("Planter.Password", "Invalid password.");
                return Page();
            }

            Tree.IdPlanter = planter.Id;

            _context.Trees.Add(Tree);
            planter.PlantedTrees += Tree.AmountPlanted;

            _context.SaveChanges();
            return RedirectToPage("./GetPlanters");
        }

        private bool VerifyPassword(string password, string storedPassword)
        {
            var parts = storedPassword.Split(':');
            if (parts.Length != 2) return false;

            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            using (var hmac = new HMACSHA512(salt))
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var computedHash = hmac.ComputeHash(passwordBytes);

                return hash.SequenceEqual(computedHash);
            }
        }
    }
}
