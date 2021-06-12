namespace Models
{
    public class User : Base.Entity
    {
        public User() : base()
        {
        }

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.IsActive))]
        public bool IsActive { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Type))]
        public Enums.UserType Type { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.FullName))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Constant.Length.FULL_NAME,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.MaxLength))]
        public string FullName { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Username))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Constant.Length.USERNAME,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.MaxLength))]
        public string Username { get; set; }
        // **********

        // **********
        public string _password;

        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Password))]

        [System.ComponentModel.DataAnnotations.Required
            (AllowEmptyStrings = false,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.Required))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Constant.Length.PASSWORD,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.MaxLength))]
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = Dtx.Security.Hashing.GetSha1(value);
            }
        }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.NationalCode))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Constant.Length.NATIONAL_CODE,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.MaxLength))]
        public string NationalCode { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.CellPhoneNumber))]

        [System.ComponentModel.DataAnnotations.MinLength
            (length: Constant.Length.MIN_CELL_PHONE_NUMBER,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.MinLength))]

        [System.ComponentModel.DataAnnotations.MaxLength
            (length: Constant.Length.MAX_CELL_PHONE_NUMBER,
            ErrorMessageResourceType = typeof(Resources.ErrorMessages),
            ErrorMessageResourceName = nameof(Resources.ErrorMessages.MaxLength))]
        public string CellPhoneNumber { get; set; }
        // **********

        // **********
        [System.ComponentModel.DataAnnotations.Display
            (ResourceType = typeof(Resources.DataDictionary),
            Name = nameof(Resources.DataDictionary.Description))]
        public string Description { get; set; }
        // **********

        // **********
        public bool IsDeleted { get; set; }
        // **********

    }
}
