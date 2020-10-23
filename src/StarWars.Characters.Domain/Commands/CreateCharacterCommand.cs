using MediatR;
using StarWars.Characters.Domain.Responses;
using System.Collections.Generic;

namespace StarWars.Characters.Domain.Commands
{
    public class CreateCharacterCommand : IRequest<CharacterModuleResponse>
    {
        public string Name { get; }
        public List<string> Episodes { get; }
        public List<long> FriendsIds { get; }

        public CreateCharacterCommand(string name, List<string> episodes, List<long> friendsIds)
        {
            Name = name;
            Episodes = episodes;
            FriendsIds = friendsIds;
        }
    }
}