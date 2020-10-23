using MediatR;
using StarWars.Characters.Domain.Commands;
using StarWars.Characters.Domain.Repositories;
using StarWars.Characters.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;

namespace StarWars.Characters.Domain.Handlers
{
    public class DeleteCharacterHandler : IRequestHandler<DeleteCharacterCommand, CharacterModuleResponse>
    {
        private readonly ICharactersRepository _repository;

        public DeleteCharacterHandler(ICharactersRepository repository)
        {
            _repository = repository;
        }

        public async Task<CharacterModuleResponse> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            var character = await _repository.Get(request.Id);

            if (character == null)
                return CharacterModuleResponse.NotFound;
            await _repository.Delete(character);
            return CharacterModuleResponse.Success;
        }
    }
}