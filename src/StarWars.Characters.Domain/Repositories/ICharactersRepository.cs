using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace StarWars.Characters.Domain.Repositories
{
    public interface ICharactersRepository
    {
        public Task Add(Character character);
        public Task Update(Character character);
        public Task<Character> Get(long id);
        public Task<IPagedList<Character>> GetPaged(int page, int size);
        public Task Delete(Character character);
        public Task<List<Character>> GetRange(List<long> ids);
        public Task<bool> IsNameTaken(string name);
    }
}