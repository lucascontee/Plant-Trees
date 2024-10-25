using System.ComponentModel.DataAnnotations;

namespace PlantTrees.Entities
{
    public class Planter
    {
        public int Id { get; set; }

        [Required]
        public string? Username { get; set; }

        [Required]
        public string? Password { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public DateTime Birth { get; set; }

        [Required]
        public string Country { get; set; }
        public int PlantedTrees { get; set; } = 0;

    }
}
