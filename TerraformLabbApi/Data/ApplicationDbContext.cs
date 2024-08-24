using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TerraformLabbApi.Models;

namespace TerraformLabbApi.Data
{
	public class ApplicationDbContext(DbContextOptions options) : DbContext (options)
	{
		public DbSet<Character> Characters { get; set; }
	}
}
