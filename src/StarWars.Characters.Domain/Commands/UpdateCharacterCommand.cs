using MediatR;
using StarWars.Characters.Domain.Responses;
using System.Collections.Generic;

namespace StarWars.Characters.Domain.Commands
{
    public class UpdateCharacterCommand : IRequest<CharacterModuleResponse>
    {
        public long Id { get; }
        public string Name { get; }
        public List<long> FriendsIds { get; }
        public List<string> Episodes { get; }

        public UpdateCharacterCommand(long id, string name, List<long> friendsIds, List<string> episodes)
        {
            Id = id;
            Name = name;
            FriendsIds = friendsIds;
            Episodes = episodes;
        }
    }
}