using MediatR;
using StarWars.Characters.Domain.ReadModels;
using StarWars.Characters.Domain.Responses;
using X.PagedList;

namespace StarWars.Characters.Domain.Commands
{
    public class GetPagedCharactersCommand : IRequest<CharacterModuleResponse<IPagedList<CharacterReadModel>>>
    {
        public int Page { get; }
        public int Size { get; }

        public GetPagedCharactersCommand(int page, int size)
        {
            Page = page;
            Size = size;
        }
    }
}