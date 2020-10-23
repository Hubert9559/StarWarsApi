using Microsoft.EntityFrameworkCore;
using StarWars.Characters.Domain;
using StarWars.Characters.Infrastructure.EntityTypeConfigurations;

namespace StarWars.Characters.Infrastructure
{
    public class CharactersContext : DbContext
    {
        public CharactersContext(DbContextOptions<CharactersContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CharacterEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new FriendshipEntityTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}