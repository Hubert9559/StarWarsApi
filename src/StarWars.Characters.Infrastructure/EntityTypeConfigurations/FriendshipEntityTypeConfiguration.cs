using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StarWars.Characters.Domain;

namespace StarWars.Characters.Infrastructure.EntityTypeConfigurations
{
    public class FriendshipEntityTypeConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendships", "Characters");

            builder.HasKey(x => new { x.Friend1Id, x.Friend2Id });

            builder.HasOne(x => x.Friend1)
                .WithMany(x => x.Friendships)
                .HasForeignKey(x => x.Friend1Id)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Friend2)
                .WithMany(x => x.FriendshipsOf)
                .HasForeignKey(x => x.Friend2Id)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}