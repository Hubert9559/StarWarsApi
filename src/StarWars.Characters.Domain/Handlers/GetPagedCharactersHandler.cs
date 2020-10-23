using MediatR;
using StarWars.Characters.Domain.Commands;
using StarWars.Characters.Domain.ReadModels;
using StarWars.Characters.Domain.Repositories;
using StarWars.Characters.Domain.Responses;
using System.Threading;
using System.Threading.Tasks;
using X.PagedList;

namespace StarWars.Characters.Domain.Handlers
{
    public class GetPagedCharactersHandler : IRequestHandler<GetPagedCharactersCommand, CharacterModuleResponse<IPagedList<CharacterReadModel>>>
    {
        private readonly ICharactersRepository _repository;

        public GetPagedCharactersHandler(ICharactersRepository repository)
        {
            _repository = repository;
        }

        public async Task<CharacterModuleResponse<IPagedList<CharacterReadModel>>> Handle(GetPagedCharactersCommand request, CancellationToken cancellationToken)
        {
            var characters = await _repository.GetPaged(request.Page, request.Size);

            return CharacterModuleResponse<IPagedList<CharacterReadModel>>.Success(characters.Select(x => CharacterReadModel.From(x)));
        }
    }
}