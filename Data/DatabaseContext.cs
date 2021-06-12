using Data.Tools;

namespace Data
{
	public class DatabaseContext : Microsoft.EntityFrameworkCore.DbContext
	{
        public DatabaseContext
            (Microsoft.EntityFrameworkCore.DbContextOptions<DatabaseContext> options) : base(options)
        {
			Database.EnsureCreated();
        }

        // **********
        public Microsoft.EntityFrameworkCore.DbSet<Models.User> Users { get; set; }
		// **********

		protected override void OnModelCreating
			(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Seed();

			#region Model: User
			modelBuilder.Entity<Models.User>()
				.HasIndex(user => new { user.Username })
				.IsUnique(unique: true)
				;
			#endregion /Model: User

			#region HasQueryFilter
			modelBuilder.Entity<Models.User>().HasQueryFilter(current => current.IsDeleted == false);
			#endregion

			modelBuilder.ApplyConfigurationsFromAssembly
				(System.Reflection.Assembly.GetExecutingAssembly());

		}
	}
}
