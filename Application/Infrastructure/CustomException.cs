using System;

namespace Infrastructure
{
    public class CustomException : Exception
    {
        public string Code { get; set; }

        public CustomException()
        {

        }

        public CustomException(string code)
        {
            this.Code = code;
        }

        public CustomException(string message, params object[] args) : this(string.Empty, message, args)
        {

        }
        public CustomException(string code, string message, params object[] args) : this(null, code, message, args)
        {

        }
        public CustomException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {

        }
        public CustomException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
