using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PlantTrees.Context;
using PlantTrees.Entities;

namespace PlantTrees.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanterController : Controller
    {
        private readonly PlantTreesContext _context;
        public PlanterController(PlantTreesContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Create(Planter planter)
        {
            _context.Add(planter);
            _context.SaveChanges();
            return Ok(planter);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var planter = _context.Planters.Find(id);

            if (planter == null)
            {
                return NotFound();
            }

            return Ok(planter);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Planter planter)
        {
            var planterDB = _context.Planters.Find(id);

            if (planterDB == null)
            {
                return NotFound();
            }

            planterDB.Username = planter.Username;
            planterDB.Name = planter.Name;
            planterDB.Password = planter.Password;
            planterDB.LastName = planter.LastName;
            planterDB.Age = planter.Age;
            planterDB.Country = planter.Country;
            planterDB.Email = planter.Email;


            _context.Planters.Update(planterDB);
            _context.SaveChanges();

            return Ok(planterDB);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var planterDB = _context.Planters.Find(id);

            if (planterDB == null)
            {
                return NotFound();
            }

            _context.Planters.Remove(planterDB);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
