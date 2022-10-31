using System.ComponentModel.DataAnnotations;

namespace CatsWebApplication.Models
{
    public class CatBreed
    {
        public CatBreed()
        {
            Cats = new List<Cat>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "THIS FIELD SHOULD NOT BE EMPTY")]
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Cat> Cats { get; set; }
    }
}
