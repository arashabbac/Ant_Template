namespace ViewModels.Users
{
    public class UserViewModel
    {
		// **********
		public System.Guid Id { get; set; }
		// **********

		// **********
		public string FullName { get; set; }
		// **********

		// **********
		public string Username { get; set; }
		// **********

		// **********
		public string Password { get; set; }
		// **********

		// **********
		public string NationalCode { get; set; }
		// **********

		// **********
		public string CellPhoneNumber { get; set; }
		// **********

		// **********
		public string Description { get; set; }
		// **********

		// **********
		public bool IsActive { get; set; }
		// **********

		// **********
		public Enums.UserType Type { get; set; }
        // **********

    }
}
