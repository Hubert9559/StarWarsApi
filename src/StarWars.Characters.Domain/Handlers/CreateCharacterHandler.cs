using MediatR;
using StarWars.Characters.Domain.Commands;
using StarWars.Characters.Domain.Repositories;
using StarWars.Characters.Domain.Responses;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace StarWars.Characters.Domain.Handlers
{
    public class CreateCharacterHandler : IRequestHandler<CreateCharacterCommand, CharacterModuleResponse>
    {
        private readonly ICharactersRepository _repository;

        public CreateCharacterHandler(ICharactersRepository repository)
        {
            _repository = repository;
        }

        public async Task<CharacterModuleResponse> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            if (await _repository.IsNameTaken(request.Name))
                return CharacterModuleResponse.Duplicate;
            
            var character = new Character(request.Name, request.Episodes);

            var friends = await _repository.GetRange(request.FriendsIds);
            var friendships = friends.Select(x => new Friendship(character, x)).ToList();
            character.ChangeFriends(friendships);
            
            await _repository.Add(character);
            
            return CharacterModuleResponse.Success;
        }
    }
}