namespace CatsWebApplication.Models
{
    public class Cat
    {
        public Cat()
        {
            HumanCats = new List<HumanCat>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int CatBreedId { get; set; }
        public virtual CatBreed CatBreed { get; set; }
        public virtual ICollection<HumanCat> HumanCats { get; set; }
    }
}
