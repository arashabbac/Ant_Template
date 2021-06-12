namespace Data.Tools
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.User>().HasData(
                new Models.User
                {
                    Id = System.Guid.NewGuid(),
                    IsActive = true,
                    FullName = "آرش عباسی",
                    CellPhoneNumber = "09351008895",
                    Username = "admin",
                    Password = "password",
                    Type = Models.Enums.UserType.Programmer,
                }
            );
        }
    }
}
