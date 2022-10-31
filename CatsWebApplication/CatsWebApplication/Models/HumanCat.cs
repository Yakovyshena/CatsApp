namespace CatsWebApplication.Models
{
    public class HumanCat
    {
        public int Id { get; set; }
        public int HumanId { get; set; }
        public int CatId { get; set; }
        public virtual Human Human { get; set; }
        public virtual Cat Cat { get; set; }

    }
}
