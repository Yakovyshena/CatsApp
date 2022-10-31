namespace CatsWebApplication.Models
{
    public class Human
    {
        public Human()
        {
            HumanCats = new List<HumanCat>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }
        public virtual ICollection<HumanCat> HumanCats { get; set; }
    }
}
