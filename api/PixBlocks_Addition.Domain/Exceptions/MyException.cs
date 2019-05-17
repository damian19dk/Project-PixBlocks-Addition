using System;
using System.Collections.Generic;
using System.Text;

namespace PixBlocks_Addition.Domain.Exceptions
{
    public class MyException : Exception
    {
        private string code;
        private string message1;

        public MyException() { }

        public MyException(string message) :base(message)
        {

        }

        public MyException(string code, string message) :this(null, code, message)
        {

        }

        public MyException(string message, string code, string message1) : this(message)
        {
            this.code = code;
            this.message1 = message1;
        }
    }
}
