﻿namespace Dtx.Text.RegularExpressions
{
	/// <summary>
	/// Version: 1.0.9
	/// Update Date: 1393/10/02
	/// Developer: Mr. Dariush Tasdighi
	/// </summary>
	public static class Patterns
	{
		// |     ==> Or
		// \d    ==> Digit

		// {n}   ==> n times
		// {n,m} ==> n to m times

		// a+    ==> One or more a
		// a*    ==> Zero or more a

		// $     ==> End of string
		// ^     ==> Start of string

		public const string Integer = @"(\+|-)?\d+";

		public const string NationalCode = @"\d{10}";

		public const string Money = @"\d+(.\d{0,2})?";

		public const string CellPhoneNumber = @"^(09)\d{9}";

		public const string ZeroOrUnsignedInteger = @"\d+";

		public const string Password = @"[a-zA-Z0-9_#?!@$%^&*-]{8,40}";

		public const string Username = @"[a-zA-Z0-9_.]{6,20}";

		public const string FileName = @"[a-zA-Z0-9_]{1,100}";

		public const string Percentage = @"100(.0{0,2})?|\d{1,2}(.\d{1,2})?";

		public const string Email = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";

		//public const string Double = @"[0-9.]{0,9}";

		public const string Url =
			@"^(https?)://(((([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]))|((([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])))(:[1-9][0-9]+)?(/)?([/?].+)?$";
	}
}
