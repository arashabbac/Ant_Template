using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class UserRepository : Base.Repository<Models.User>, IUserRepository
    {
        internal UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public Models.User GetByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            var result =
                DbSet
                .Where(current => current.Username.ToLower() == username.ToLower())
                .FirstOrDefault();

            return result;
        }

        public async System.Threading.Tasks.Task<Models.User> GetByUsernameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }


            var result =
                await DbSet
                .Where(current => current.Username.ToLower() == username.ToLower())
                .FirstOrDefaultAsync();

            return result;

        }

        public async System.Threading.Tasks.Task<ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>
            GellAllWithPagingAsync(ViewModels.Users.GetAllRequestViewModel request)
        {
            var user =
                DbSet.AsQueryable();

            if(string.IsNullOrWhiteSpace(request.FullName) == false)
            {
                user =
                    user
                    .Where(current => current.FullName.Contains(request.FullName));
            }

            if(string.IsNullOrWhiteSpace(request.Username) == false)
            {
                user =
                    user
                    .Where(current => current.Username.Contains(request.Username));
            }

            if(string.IsNullOrWhiteSpace(request.CellPhoneNumber) == false)
            {
                user =
                    user
                    .Where(current => current.CellPhoneNumber.Contains(request.CellPhoneNumber));
            }

            if(string.IsNullOrWhiteSpace(request.NationalCode) == false)
            {
                user =
                    user
                    .Where(current => current.NationalCode.Contains(request.NationalCode));
            }

            var result = new ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalCount =
                    request.TotalCount != 0 ?
                    request.TotalCount :
                    await DbSet.CountAsync(),

                Result =
                    await user
                    .Skip(request.PageSize * request.PageIndex)
                    .Take(request.PageSize)
                    .OrderByDescending(current => current.InsertDateTime)
                    .Select(current => new ViewModels.Users.GetAllViewModel
                    {
                        Id = current.Id,
                        CellPhoneNumber = current.CellPhoneNumber,
                        UserType = (ViewModels.Enums.UserType)current.Type,
                        FullName = current.FullName,
                        NationalCode = current.NationalCode,
                        Username = current.Username,
                        IsActive = current.IsActive,
                    })
                    .ToListAsync(),
            };

            return result;
        }


    }
}
