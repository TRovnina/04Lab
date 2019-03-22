using System;
using System.ComponentModel.DataAnnotations;
using Laboratory04.Exceptions;

namespace Laboratory04.Models
{
    [Serializable]
    internal class Person
    {
        #region Fields
        private string _name;
        private string _surname;
        private int _age;
        private DateTime _birthday;
        private string _email;
        private readonly string[] _chineseZodiacs = { "Monkey", "Cock", "Dog", "Pig/Boar", "Rat", "Bull", "Tiger", "Cat/Rabbit", "Dragon", "Snake", "Horse", "Goat/Sheep" };
        private readonly string _chineseSign;
        private readonly string _sunSign;

        #endregion

        #region Properties
       public string Name
        {
            get { return _name; }
            private set
            {
                _name = value;

            }
        }

        public string Surname
        {
            get { return _surname; }
            private set
            {
                _surname = value;
            }
        }


        public DateTime Birthday
        {
            get { return _birthday; }
            private set
            {
                _birthday = value;
            }
        }

        public String BirthdayDate
        {
            get { return Birthday.ToShortDateString(); }
        }

        public int Age
        {
            get { return _age; }
            private set
            {
                if (!IsValidAge(value))
                    throw new AgeException("Wrong age! This person should not exist!");
                _age = value;
            }
        }

        public string Email
        {
            get { return _email; }
            private set
            {
                if (!IsValidEmail(value))
                   throw new EmailException("Something wrong with email!");
                _email = value;
            }
        }


        #region Read-only Properties

        public bool IsAdult
        {
            get
            {
                return Age >= 18;
            }
        }

        public string SunSign
        {
            get { return _sunSign; }
        }

        public string ChineseSign
        {
            get { return _chineseSign; }
        }

        public bool IsBirthday
        {
            get { return DateTime.Today.Month == Birthday.Month && DateTime.Today.Day == Birthday.Day; }
        }

        #endregion
        #endregion


        #region Constructors

        public Person(string name, string surname, DateTime birthday, string email)
        {
            Name = name;
            Surname = surname;
            Birthday = birthday;
            Email = email;
            Age = CountAge();
            _chineseSign = _chineseZodiacs[Birthday.Year % 12];
            _sunSign = GetWesternZodiac();
        }


        public Person(string name, string surname, string email)
            : this(name, surname, DateTime.Today, email)
        {

        }

        public Person(string name, string surname, DateTime birthday)
            : this(name, surname, birthday, "")
        {
        }

        #endregion

        //calculate person age
        private int CountAge()
        {
            var today = DateTime.Today;
            if (today.Month == Birthday.Month && today.Day >= Birthday.Day || today.Month > Birthday.Month)
                return today.Year - Birthday.Year;

            return today.Year - Birthday.Year - 1;
        }

        //define western zodiac
        private string GetWesternZodiac()
        {
            if (Birthday.Month == 3 && Birthday.Day >= 21 || Birthday.Month == 4 && Birthday.Day <= 20)
                return "Aries";
            if (Birthday.Month == 4 && Birthday.Day >= 21 || Birthday.Month == 5 && Birthday.Day <= 20)
                return "Taurus";
            if (Birthday.Month == 5 && Birthday.Day >= 21 || Birthday.Month == 6 && Birthday.Day <= 21)
                return "Gemini";
            if (Birthday.Month == 6 && Birthday.Day >= 22 || Birthday.Month == 7 && Birthday.Day <= 22)
                return "Cancer";
            if (Birthday.Month == 7 && Birthday.Day >= 23 || Birthday.Month == 8 && Birthday.Day <= 23)
                return "Lion";
            if (Birthday.Month == 8 && Birthday.Day >= 24 || Birthday.Month == 9 && Birthday.Day <= 22)
                return "Virgo";
            if (Birthday.Month == 9 && Birthday.Day >= 23 || Birthday.Month == 10 && Birthday.Day <= 23)
                return "Libra";
            if (Birthday.Month == 10 && Birthday.Day >= 24 || Birthday.Month == 11 && Birthday.Day <= 22)
                return "Scorpio";
            if (Birthday.Month == 11 && Birthday.Day >= 23 || Birthday.Month == 12 && Birthday.Day <= 21)
                return "Sagittarius";
            if (Birthday.Month == 12 && Birthday.Day >= 22 || Birthday.Month == 1 && Birthday.Day <= 20)
                return "Capricorn";
            if (Birthday.Month == 1 && Birthday.Day >= 21 || Birthday.Month == 2 && Birthday.Day <= 20)
                return "Aquarius";

            return "Pisces";
        }

        private bool IsValidEmail(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }

        private bool IsValidAge(int age)
        {
            return age >= 0 && age <= 135;
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }

}
