namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly Data.IUnitOfWork _unitOfWork;

        public AccountService
            (Data.IUnitOfWork unitOfWork,
            Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor,
            Microsoft.Extensions.Options.IOptions<Infrastructure.ApplicationSettings.Main> options) : base()
        {
            _unitOfWork = unitOfWork;
            MainSettings = options.Value;
        }

        protected Infrastructure.ApplicationSettings.Main MainSettings { get; }

        public async System.Threading.Tasks.Task<FluentResults.Result>
            RegisterAsync(ViewModels.Users.RegisterViewModel viewModel)
        {
            var result = new FluentResults.Result();

            if (viewModel == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var exsitsUser =
                await _unitOfWork.UserRepository
                .GetByUsernameAsync(viewModel.Username);

            if (exsitsUser != null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.IsDuplicateUsername);
            }
            else
            {
                var user = new Models.User
                {
                    FullName = viewModel.FullName,
                    CellPhoneNumber = viewModel.CellPhoneNumber,
                    NationalCode = viewModel.NationalCode,
                    Password = viewModel.Password,
                    Type = Models.Enums.UserType.User,
                    Username = viewModel.Username,
                };

                await _unitOfWork.UserRepository.InsertAsync(user);
                await _unitOfWork.SaveAsync();

                result.WithSuccess(successMessage: Resources.DataDictionary.SuccessMessage);
            }

            return result;
        }

        public async System.Threading.Tasks.Task
            <FluentResults.Result<ViewModels.Users.LoginResponseViewModel>>
            LoginAsync(ViewModels.Users.LoginRequestViewModel viewModel)
        {
            var result = new FluentResults.Result<ViewModels.Users.LoginResponseViewModel>();

            if (viewModel == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var foundedUser =
                await _unitOfWork.UserRepository
                .GetByUsernameAsync(viewModel.Username);

            if (foundedUser == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.ErrorMessage3020_3030);
                return result;
            }

            if (string.Compare(foundedUser.Password,
                Dtx.Security.Hashing.GetSha1(viewModel.Password),
                ignoreCase: false) != 0)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.ErrorMessage3020_3030);
                return result;
            }

            if (foundedUser.IsActive == false)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.ErrorMessage3050);
                return result;
            }

            // Authentication successful so generate jwt token

            string token =
                Infrastructure.JwtUtility.GenerateJwtToken
                (user: foundedUser, secretKey: MainSettings.SecretKey);

            var response =
                new ViewModels.Users.LoginResponseViewModel
                    (id: foundedUser.Id,
                    username: foundedUser.Username,
                    fullName: foundedUser.FullName,
                    type: (int)foundedUser.Type,
                    token: token);

            result.WithValue(value: response);

            return result;
        }


        public async System.Threading.Tasks.Task<FluentResults.Result>
             ChangePasswordAsync(ViewModels.Account.ChangePasswordViewModel user)
        {
            var loginResult =
                await LoginAsync(new ViewModels.Users.LoginRequestViewModel
                {
                    Username = user.Username,
                    Password = user.Password,
                });

            var result = new FluentResults.Result();

            if (loginResult.IsFailed == true)
            {
                return loginResult.ToResult();
            }

            if (user == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var model = await _unitOfWork.UserRepository.GetByIdAsync(user.Id.Value);
            model.Password = user.NewPassword;

            await _unitOfWork.UserRepository.UpdateAsync(model);
            await _unitOfWork.SaveAsync();

            result.WithSuccess(successMessage: Resources.DataDictionary.SuccessMessage);
            return result;
        }

        public async System.Threading.Tasks.Task<FluentResults.Result>
            UpdateUserInfoAsync(ViewModels.Account.AccountInformationViewModel viewModel)
        {
            var result = new FluentResults.Result();

            if (viewModel == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            var user =
                await _unitOfWork.UserRepository
                .GetByIdAsync(viewModel.Id.Value);

            if (user == null)
            {
                result.WithError(errorMessage: Resources.ErrorMessages.BadRequestMessage);
                return result;
            }

            user.FullName = viewModel.FullName;
            user.NationalCode = viewModel.NationalCode;
            user.CellPhoneNumber = viewModel.CellPhoneNumber;

            await _unitOfWork.UserRepository.UpdateAsync(user);
            await _unitOfWork.SaveAsync();

            result.WithSuccess(successMessage: Resources.DataDictionary.SuccessMessage);
            return result;
        }
    }
}
