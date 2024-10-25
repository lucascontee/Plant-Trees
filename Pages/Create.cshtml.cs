using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlantTrees.Context;
using PlantTrees.Entities;
using System.Security.Cryptography;
using System.Text;


namespace PlantTrees.Pages
{
    public class CreateModel : PageModel
    {
        private readonly PlantTreesContext _context;

        public CreateModel(PlantTreesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Planter Planter { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            DateTime date = DateTime.Now;
            Planter.Age = date.Year - Planter.Birth.Year;
            var existsUser = await _context.Planters.AnyAsync(p => p.Username == Planter.Username);
            var existsEmail = await _context.Planters.AnyAsync(p => p.Email == Planter.Email);


            if (existsUser)
            {
                ModelState.AddModelError("Planter.Username", "Existing Username.");
                return Page();
            }

            if(existsEmail) 
            {
                ModelState.AddModelError("Planter.Email", "Existing Email.");
                return Page();
            }

            Planter.Password = HashPassword(Planter.Password);

            _context.Planters.Add(Planter);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private string HashPassword(string password)
        {
            using (var hmac = new HMACSHA512())
            {

                var salt = hmac.Key;

                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var hash = hmac.ComputeHash(passwordBytes);

                return $"{Convert.ToBase64String(salt)}:{Convert.ToBase64String(hash)}";
            }
        }
    }
}
