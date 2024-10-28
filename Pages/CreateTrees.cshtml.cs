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

            if (planter == null)
            {
                ModelState.AddModelError("Planter.Username", "Incorrect Username.");
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

            Tree.IdPlanter = planter.Id;

            _context.Trees.Add(Tree);
            planter.PlantedTrees += Tree.AmountPlanted;

            _context.SaveChanges();
            return RedirectToPage("./GetPlanters");
        }
    }
}
