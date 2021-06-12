namespace ViewModels.Users
{
    public class LoginResponseViewModel : object
    {
        public LoginResponseViewModel()
        {
        }

        public LoginResponseViewModel
            (System.Guid id, string username, string fullName, int type, string token)
        {
            Token = token;

            Id = id;
            Username = username;
            FullName = fullName;
            Type = type;
        }

        public System.Guid Id { get; set; }

        public string Token { get; set; }

        public string Username { get; set; }

        public string FullName { get; set; }

        public int Type { get; set; }

        public string Message { get; set; }
    }
}
