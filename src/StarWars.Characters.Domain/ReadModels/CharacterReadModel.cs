using System.Collections.Generic;
using System.Linq;

namespace StarWars.Characters.Domain.ReadModels
{
    public class CharacterReadModel
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<string> Friends { get; private set; }
        public IEnumerable<string> Episodes { get; private set; }

        private CharacterReadModel(Character character)
        {
            Id = character.Id;
            Name = character.Name;
            Friends = character.Friends?.Select(x => x.Name);
            Episodes = character.Episodes;
        }

        public static CharacterReadModel From(Character character) => new CharacterReadModel(character);
    }
}