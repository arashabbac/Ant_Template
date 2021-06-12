namespace ViewModels.Users
{
    public class GetAllViewModel
    {
        // **********
        public System.Guid Id { get; set; }
		// **********

		// **********
		public bool IsActive { get; set; }
		// **********

		// **********
		public string FullName { get; set; }
		// **********

		// **********
		public string Username { get; set; }
        // **********

        // **********
        public Enums.UserType UserType { get; set; }
        // **********

        // **********
        public string UserTypeValue 
		{
			get => Extensions.EnumExtension.GetDisplayName(UserType);
		}
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
	}
}
