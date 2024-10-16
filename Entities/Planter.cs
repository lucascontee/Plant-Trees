﻿using System.ComponentModel.DataAnnotations;

namespace PlantTrees.Entities
{
    public class Planter
    {
        public int Id { get; set; }

        [Required]
        public string Cpf { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        public string Nationality { get; set; }
        public int PlantedTrees { get; set; } = 0;

    }
}
