using Microsoft.EntityFrameworkCore;

namespace CatsWebApplication.Models
{
    public class CatsAPIContext : DbContext
    {
        public virtual DbSet<Human> Humans { get; set; }
        public virtual DbSet<HumanCat> HumanCats { get; set; }
        public virtual DbSet<Cat> Cats { get; set; }

        public virtual DbSet<CatBreed> CatBreeds { get; set; }

        public CatsAPIContext(DbContextOptions<CatsAPIContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
