using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Exceptions
{
    public class MyException : Exception
    {
        public string Code { get; }

        public MyException()
        {
        }

        public MyException(string code)
        {
            Code = code;
        }

        public MyException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public MyException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public MyException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public MyException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}
