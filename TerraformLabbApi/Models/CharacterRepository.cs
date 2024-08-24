using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TerraformLabbApi.Data;

namespace TerraformLabbApi.Models
{
	public class CharacterRepository(ApplicationDbContext context)
	{
		private readonly ApplicationDbContext _context = context;

		public async Task<List<Character>> GetAllCharactersAsync()
		{
			return await _context.Characters.ToListAsync();
		}

		public async Task AddCharacterAsync(Character character)
		{
			_context.Characters.Add(character);
			await _context.SaveChangesAsync();
		}
	}
}
