using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Practice2.Exceptions
{
    internal class UnbornPersonException: Exception
    {
        public UnbornPersonException() : base("An invalid date of birth entered! This person was not even born!")
        {
        }

        public UnbornPersonException(string message) : base(message)
        {
        }

        public UnbornPersonException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
