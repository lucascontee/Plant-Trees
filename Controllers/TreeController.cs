using Microsoft.AspNetCore.Mvc;
using PlantTrees.Context;
using PlantTrees.Entities;

namespace PlantTrees.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class TreeController : Controller
    {
        private readonly PlantTreesContext _context;
        public TreeController(PlantTreesContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(Tree tree)
        {

            var planter = await _context.Planters.FindAsync(tree.IdPlanter);

            if (planter == null)
            {
                return NotFound();
            }

            _context.Add(tree);
            planter.PlantedTrees += tree.AmountPlanted;

            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Create), new { id = tree.Id }, tree);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var tree = _context.Trees.Find(id);

            if (tree == null)
            {
                return NotFound();
            }

            return Ok(tree);
        }

        [HttpPut("{id}")]
        public IActionResult Upddate(int id, Tree tree)
        {
            var treeDB = _context.Trees.Find(id);

            if (treeDB == null)
            {
                return NotFound();
            }

            treeDB.TreeName = tree.TreeName;
            treeDB.IdPlanter = tree.IdPlanter;
            treeDB.AmountPlanted = tree.AmountPlanted;
            treeDB.PlantationCity = tree.PlantationCity;

            _context.Trees.Update(treeDB);
            _context.SaveChanges();

            return Ok(treeDB);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var treeDB = _context.Trees.Find(id);

            if (treeDB == null)
            {
                return NotFound();
            }

            var planter = _context.Planters.Find(treeDB.IdPlanter);

            if(planter == null)
            {
                throw new Exception();
            }

            planter.PlantedTrees -= treeDB.AmountPlanted;

            _context.Trees.Remove(treeDB);
            _context.SaveChanges();
            return NoContent();
        }


    }
}
