using StarWars.Shared.DDD;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Characters.Domain
{
    public class Character : Entity
    {
        public string Name { get; private set; }
        public List<string> Episodes { get; private set; }
        public List<Friendship> Friendships { get; private set; }
        public List<Friendship> FriendshipsOf { get; private set; }
        public List<Character> Friends
        {
            get
            {
                var friends = new List<Character>();
                friends.AddRange(Friendships?.Select(x => x.Friend2) ?? new List<Character>());
                friends.AddRange(FriendshipsOf?.Select(x => x.Friend1) ?? new List<Character>());
                return friends;
            }
        }

        //For EF Core
        private Character()
        { }
        
        public Character(string name, List<string> episodes)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            if (episodes == null)
                throw new ArgumentNullException(nameof(episodes));
            if (episodes.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(episodes));
            if (episodes.Any(x => !Shared.Episodes.IsValid(x)))
                throw new ArgumentOutOfRangeException(nameof(episodes));

            Name = name;
            Episodes = episodes;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void ChangeEpisodes(List<string> episodes)
        {
            if (episodes is null || episodes.Count == 0)
                throw new ArgumentOutOfRangeException(nameof(episodes));

            Episodes = episodes;
        }

        public void ChangeFriends(List<Friendship> friendships)
        {
            Friendships = friendships;
        }
    }
}