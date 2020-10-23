using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using StarWars.Characters.Domain;
using System.Collections.Generic;

namespace StarWars.Characters.Infrastructure.EntityTypeConfigurations
{
    public class CharacterEntityTypeConfiguration : IEntityTypeConfiguration<Character>
    {
        public void Configure(EntityTypeBuilder<Character> builder)
        {
            builder.ToTable("Characters", "Characters");
            
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseHiLo("charactersseq", "Characters");

            builder.HasIndex(x => x.Name).IsUnique();

            builder.Ignore(x => x.Friendships);
            builder.Ignore(x => x.FriendshipsOf);
            builder.Ignore(x => x.Friends);

            builder.Property(x => x.Episodes)
                .HasConversion(x => JsonConvert.SerializeObject(x), y => JsonConvert.DeserializeObject<List<string>>(y));
        }
    }
}