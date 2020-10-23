using MediatR;
using StarWars.Characters.Domain.Responses;

namespace StarWars.Characters.Domain.Commands
{
    public class DeleteCharacterCommand : IRequest<CharacterModuleResponse>
    {
        public long Id { get; }

        public DeleteCharacterCommand(long id)
        {
            Id = id;
        }
    }
}