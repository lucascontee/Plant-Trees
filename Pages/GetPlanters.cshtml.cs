using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PlantTrees.Context;
using PlantTrees.Entities;



namespace PlantTrees.Pages
{

    public class GetPlanters : PageModel
    {
        private readonly PlantTreesContext _context;

        public GetPlanters(PlantTreesContext context)
        {
            _context = context;
        }

        public IList<Planter> Planters { get; set; } 

        public async Task OnGetAsync()
        {
            Planters = await _context.Planters.OrderByDescending(p => p.PlantedTrees).ToListAsync();
        }
    }
    
}