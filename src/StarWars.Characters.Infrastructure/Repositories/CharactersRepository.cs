using Microsoft.EntityFrameworkCore;
using StarWars.Characters.Domain;
using StarWars.Characters.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace StarWars.Characters.Infrastructure.Repositories
{
    public class CharactersRepository : ICharactersRepository
    {
        private readonly CharactersContext _context;

        public CharactersRepository(CharactersContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task Add(Character character)
        {
            _context.Characters.Add(character);
            return _context.SaveChangesAsync();
        }

        public async Task Delete(Character character)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM [Characters].[Friendships] WHERE Friend1Id = {character.Id} OR Friend2Id = {character.Id}");
            _context.Characters.Remove(character);
            await _context.SaveChangesAsync();
        }

        public Task<Character> Get(long id)
        {
            return _context.Characters.FindAsync(id).AsTask();
        }

        public Task<IPagedList<Character>> GetPaged(int page, int size)
        {
            return _context.Characters.AsQueryable()
                .Include(x => x.Friendships)
                .ThenInclude(x => x.Friend2)
                .Include(x => x.FriendshipsOf)
                .ThenInclude(x => x.Friend1)
                .ToPagedListAsync(page, size);
        }

        public Task<List<Character>> GetRange(List<long> ids)
        {
            return _context.Characters.AsQueryable().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public Task<bool> IsNameTaken(string name)
        {
            return _context.Characters.AsQueryable().AnyAsync(x => x.Name == name);
        }

        public async Task Update(Character character)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"DELETE FROM [Characters].[Friendships] WHERE Friend1Id = {character.Id} OR Friend2Id = {character.Id}");
            _context.Characters.Update(character);
            await _context.SaveChangesAsync();
        }
    }
}