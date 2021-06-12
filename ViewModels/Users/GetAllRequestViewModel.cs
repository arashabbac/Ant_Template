namespace ViewModels.Users
{
    public class GetAllRequestViewModel : ViewDataRequest
    {
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
		public string NationalCode { get; set; }
		// **********

		// **********
		public string CellPhoneNumber { get; set; }
		// **********

	}
}
