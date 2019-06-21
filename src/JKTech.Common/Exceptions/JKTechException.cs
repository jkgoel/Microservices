using System;
using System.Collections.Generic;
using System.Text;

namespace JKTech.Common.Exceptions
{
    public class JKTechException : Exception
    {
        public string Code { get; set; }

        public JKTechException()
        {
            
        }

        public JKTechException(string code)
        {
            Code = code;
        }

        public JKTechException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        public JKTechException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        public JKTechException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public JKTechException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }        

    }
}
