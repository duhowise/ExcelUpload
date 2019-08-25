using Microsoft.EntityFrameworkCore;

namespace DataUploader.Models
{
	public class DalexDbContext:DbContext
	{
		//public DalexDbContext(DbContextOptions<DalexDbContext> options) : base(options)
		//{

		//}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=RaffleServiceDb;Persist Security Info=True;MultipleActiveResultSets=True;");
		}

		public DbSet<Paddie> Paddies { get; set; }
		public DbSet<Branch> Branches { get; set; }
		public DbSet<Ticket> Tickets { get; set; }
		public DbSet<Attendant> Attendants { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Paddie>().HasIndex(o => o.Name).IsUnique();
			modelBuilder.Entity<Paddie>().HasIndex(o => o.Name).IsUnique();
		}

	}
}