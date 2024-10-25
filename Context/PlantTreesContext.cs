using Microsoft.EntityFrameworkCore;
using PlantTrees.Entities;

namespace PlantTrees.Context
{
    public class PlantTreesContext : DbContext
    {

        public PlantTreesContext(DbContextOptions<PlantTreesContext> options) : base(options){}

        public DbSet<Planter> Planters { get; set; }
        public DbSet<Tree> Trees { get; set; }

    }
}
