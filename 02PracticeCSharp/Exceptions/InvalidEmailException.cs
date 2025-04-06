using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Practice2.Exceptions
{
    internal class InvalidEmailException: Exception
    {
        public InvalidEmailException() : base ("An invalid email format entered!") 
        { 
        }

        public InvalidEmailException(string message) : base(message)
        { 
        }

        public InvalidEmailException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
