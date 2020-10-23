using StarWars.Shared.DDD;
using System.Collections.Generic;

namespace StarWars.Characters.Domain
{
    public class Friendship : ValueObject
    {
        public long Friend1Id { get; private set; }
        public long Friend2Id { get; private set; }
        public Character Friend1 { get; private set; }
        public Character Friend2 { get; private set; }

        //For EF Core
        private Friendship()
        { }

        public Friendship(Character friend1, Character friend2)
        {
            Friend1 = friend1;
            Friend1Id = friend1.Id;
            Friend2 = friend2;
            Friend2Id = friend2.Id;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            if (Friend1Id < Friend2Id)
                yield return $"{Friend1Id}-{Friend2Id}";
            else
                yield return $"{Friend2Id}-{Friend1Id}";
        }
    }
}