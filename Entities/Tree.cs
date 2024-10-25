namespace PlantTrees.Entities
{
    public class Tree
    {
        public int Id { get; set; }
        public string? TreeName { get; set; }
        public int IdPlanter { get; set; }
        public string? PlantationCity { get; set; }
        public int AmountPlanted { get; set; }
    }
}
