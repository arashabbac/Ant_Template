using System.Linq;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly Data.IUnitOfWork _unitOfWork;

        public UserService
            (Data.IUnitOfWork unitOfWork) : base()
        {
            _unitOfWork = unitOfWork;
        }

        public async System.Threading.Tasks.Task<FluentResults.Result<ViewModels.Users.UserViewModel>>
            GetByIdAsync(System.Guid? id)
        {
            var result =
                new FluentResults.Result
                <ViewModels.Users.UserViewModel>();

            if (id.HasValue == false)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var user =
                (await _unitOfWork.UserRepository
                .GetByIdAsync(id.Value));

            if (user != null)
            {
                var userViewModel = new ViewModels.Users.UserViewModel
                {
                    Id = user.Id,
                    Username = user.Username,
                    NationalCode = user.NationalCode,
                    FullName = user.FullName,
                    Password = user.Password,
                    CellPhoneNumber = user.CellPhoneNumber,
                    Type = (ViewModels.Enums.UserType)user.Type,
                    Description = user.Description,
                    IsActive = user.IsActive,
                };

                result.WithValue(value: userViewModel);
            }
            else
            {
                result.WithError(errorMessage: Resources.ErrorMessages.RecordNotFound);
            }

            return result;
        }

        public async System.Threading.Tasks.Task<ViewModels.Users.UserViewModel>
            GetByUsernameAsync(string username)
        {
            var user =
                await _unitOfWork.UserRepository.GetByUsernameAsync(username);

            if (user != null)
            {
                return new ViewModels.Users.UserViewModel
                {
                    Id = user.Id,
                    CellPhoneNumber = user.CellPhoneNumber,
                    FullName = user.FullName,
                    Username = user.Username,
                    NationalCode = user.NationalCode,
                    Type = (ViewModels.Enums.UserType)user.Type,
                };
            }
            else
            {
                return null;
            }
        }

        public async System.Threading.Tasks.Task<FluentResults.Result>
            InsertAsync(ViewModels.Users.CreateViewModel viewModel)
        {
            var result = new FluentResults.Result();

            if (viewModel == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var exsitsUser =
                await GetByUsernameAsync(viewModel.Username);

            if (exsitsUser != null)
            {
                result.WithError(Resources.ErrorMessages.IsDuplicateUsername);
                return result;
            }


            var user = new Models.User
            {
                IsActive = viewModel.IsActive,
                FullName = viewModel.FullName,
                CellPhoneNumber = viewModel.CellPhoneNumber,
                Description = viewModel.Description,
                NationalCode = viewModel.NationalCode,
                Password = viewModel.Password,
                Type = (Models.Enums.UserType)viewModel.Type,
                Username = viewModel.Username,
            };

            await _unitOfWork.UserRepository.InsertAsync(user);
            await _unitOfWork.SaveAsync();

            result.WithSuccess(successMessage: Resources.DataDictionary.SuccessMessage);

            return result;
        }

        public async System.Threading.Tasks.Task<FluentResults.Result>
            UpdateAsync(ViewModels.Users.CreateViewModel viewModel)
        {
            var result =
                new FluentResults.Result();

            if (viewModel == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var exsitsUser = await GetByUsernameAsync(viewModel.Username);

            if (exsitsUser != null && exsitsUser.Id != viewModel.Id)
            {
                result.WithError(Resources.ErrorMessages.IsDuplicateUsername);
                return result;
            }

            var user =
                await _unitOfWork.UserRepository
                .GetByIdAsync(viewModel.Id.Value);

            if (user == null)
            {
                result.WithError(Resources.ErrorMessages.IsDuplicateUsername);
                return result;
            }

            user.CellPhoneNumber = viewModel.CellPhoneNumber;
            user.FullName = viewModel.FullName;
            user.NationalCode = viewModel.NationalCode;
            user.Type = (Models.Enums.UserType)viewModel.Type;
            user.Username = viewModel.Username;
            user.IsActive = viewModel.IsActive;
            user.Description = viewModel.Description;

            if (string.IsNullOrWhiteSpace(viewModel.Password) == false)
            {
                user.Password = viewModel.Password;
            }

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            result.WithSuccess(successMessage: Resources.DataDictionary.SuccessMessage);

            return result;
        }

        public async System.Threading.Tasks.Task
            <FluentResults.Result<ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>>
            GetAllAsync(ViewModels.Users.GetAllRequestViewModel request)
        {
            var result =
                new FluentResults.Result
                <ViewModels.ViewPagingDataResult<ViewModels.Users.GetAllViewModel>>();

            if (request == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var users =
                await _unitOfWork.UserRepository.GellAllWithPagingAsync(request);

            result.WithValue(value: users);

            return result;
        }

        public async System.Threading.Tasks.Task<FluentResults.Result>
            DeleteByIdAsync(System.Guid? id)
        {
            var result = new FluentResults.Result();

            if (id.HasValue == false)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var user =
                await _unitOfWork.UserRepository.GetByIdAsync(id.Value);

            if(user == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            user.IsDeleted = true;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            result.WithSuccess(successMessage: Resources.DataDictionary.SuccessMessage);

            return result;
        }

    }
}
