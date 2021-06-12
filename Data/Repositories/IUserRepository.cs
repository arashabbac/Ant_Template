namespace Data.Repositories
{
	public interface IUserRepository : Base.IRepository<Models.User>
	{
		Models.User GetByUsername(string username);

		System.Threading.Tasks.Task<Models.User> GetByUsernameAsync(string username);

        System.Threading.Tasks.Task<ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>
			GellAllWithPagingAsync(ViewModels.Users.GetAllRequestViewModel request);
    }
}
