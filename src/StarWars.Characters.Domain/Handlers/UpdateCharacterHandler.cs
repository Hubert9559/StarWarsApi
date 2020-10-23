using MediatR;
using StarWars.Characters.Domain.Commands;
using StarWars.Characters.Domain.Repositories;
using StarWars.Characters.Domain.Responses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarWars.Characters.Domain.Handlers
{
    public class UpdateCharacterHandler : IRequestHandler<UpdateCharacterCommand, CharacterModuleResponse>
    {
        private readonly ICharactersRepository _repository;

        public UpdateCharacterHandler(ICharactersRepository repository)
        {
            _repository = repository;
        }

        public async Task<CharacterModuleResponse> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _repository.Get(request.Id);

            if (character == null)
                return CharacterModuleResponse.NotFound;

            if (character.Name != request.Name && await _repository.IsNameTaken(request.Name))
                return CharacterModuleResponse.Duplicate;

            if (request.FriendsIds.Contains(character.Id))
                return CharacterModuleResponse.SelfFriend;

            var friends = await _repository.GetRange(request.FriendsIds);
            var friendships = friends.Select(x => new Friendship(character, x)).ToList();

            character.ChangeName(request.Name);
            character.ChangeEpisodes(request.Episodes);
            character.ChangeFriends(friendships);

            await _repository.Update(character);
            return CharacterModuleResponse.Success;
        }
    }
}