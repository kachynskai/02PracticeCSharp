using KMA.ProgrammingInCSharp.Practice2.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KMA.ProgrammingInCSharp.Practice2.Model
{
    internal class Person
    {
        #region Fields
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime _birthday;
        private bool _isAdult;
        private string _sunSign;
        private string _chineseSign;
        private bool _isBirthday;
        #endregion


        #region Constructors
        public Person(string firstName, string lastName, string email, DateTime birthday)
        {
            FirstName = firstName;
            LastName = lastName;    
            Email = email;  
            Birthday = birthday;
            CalculateProperties();
            Thread.Sleep(1000);
        }

        public Person(string firstName, string lastName, string email) : this(firstName, lastName, email, DateTime.Today){}
        public Person(string firstName, string lastName, DateTime birthday) : this(firstName, lastName, string.Empty, birthday) { }

        #endregion

        #region Properties
        public string FirstName { get { return _firstName; } set { _firstName = value; } }
        public string LastName { get { return _lastName; } set { _lastName = value; } }
        public string Email 
        { 
            get { return _email; } 
            set 
            {
                Regex validateEmailRegex = new Regex(@"^[A-Za-z0-9]+@(?:[A-Za-z]+\.)+[A-Za-z]+$");

                if (!validateEmailRegex.IsMatch(value))
                {
                    throw new InvalidEmailException();
                }
                _email = value; 
            } 
        } 
        public DateTime Birthday 
        { 
            get { return _birthday; }
            set 
            {
                DateTime today = DateTime.Now;

                if (value > today)
                    throw new UnbornPersonException();

                int age = today.Year - value.Year;
                if (value.Date > today.AddYears(-age)) age--;

                if(age > 135)
                    throw new TooOldPersonException();

                _birthday = value; 
                CalculateProperties(); 
            } 
        }
        public bool IsAdult => _isAdult;
        public string SunSign => _sunSign;
        public string ChineseSign => _chineseSign;
        public bool IsBirthday => _isBirthday;


        #endregion

        private void CalculateProperties()
        {
            _isAdult = GetIsAdult();
            _sunSign = CalculateWesternSign(); 
            _chineseSign = CalculateChineseSign();
            _isBirthday = IsBirthdayToday();
        }

        private string CalculateWesternSign()
        {
            string[] zodiacSigns = {
            "Capricorn", "Aquarius", "Pisces", "Aries", "Taurus", "Gemini",
            "Cancer", "Leo", "Virgo", "Libra", "Scorpio", "Sagittarius"
            };


            int[] zodiacStartDays = { 20, 19, 21, 20, 21, 21, 23, 23, 23, 23, 22, 22 };

            int month = Birthday.Month;
            int day = Birthday.Day;

            return (day < zodiacStartDays[month - 1]) ? zodiacSigns[month - 1] : zodiacSigns[month % 12];
        }
        private string CalculateChineseSign()
        {
            int year = Birthday.Year;
            string[] chineseZodiacSigns = { "Rat", "Ox", "Tiger", "Rabbit", "Dragon", "Snake", "Horse", "Goat", "Monkey", "Rooster", "Dog", "Pig" };
            return chineseZodiacSigns[(year - 4) % 12];
        }
        private bool IsBirthdayToday()
        {
            DateTime today = DateTime.Today;
            return Birthday.Day == today.Day && Birthday.Month == today.Month;
        }

        private bool GetIsAdult()
        {
            DateTime today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            if (Birthday.Date > today.AddYears(-age))
            {
                age--;
            }
            return age >= 18;
        }
    }
}
