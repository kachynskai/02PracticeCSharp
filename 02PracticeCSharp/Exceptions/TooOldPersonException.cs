using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Practice2.Exceptions
{
    internal class TooOldPersonException: Exception
    {
        public TooOldPersonException() : base("An invalid date of birth entered! This person is unrealistically old!!")
        {
        }

        public TooOldPersonException(string message) : base(message)
        {
        }

        public TooOldPersonException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
