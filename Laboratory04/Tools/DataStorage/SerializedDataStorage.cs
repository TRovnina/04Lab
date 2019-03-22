using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Laboratory04.Models;
using Laboratory04.Tools.Manager;

namespace Laboratory04.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private List<Person> _people;

        internal SerializedDataStorage()
        {
            try
            {
                _people = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                _people = new List<Person>();
            }
        }
       

        public void AddPerson(Person person)
        {
            PeopleList.Add(person);
            SaveChanges();
        }

        public void DeletePerson(Person person)
        {
            PeopleList.Remove(person);
            SaveChanges();
        }

        public void SortList(string parameter)
        {
            if (parameter.Equals("Name"))
                PeopleList = new List<Person>(_people.OrderBy(p => p.Name).ToList());
            else if (parameter.Equals("Surname"))
                PeopleList = new List<Person>(_people.OrderBy(p => p.Surname).ToList());
            else if (parameter.Equals("Email"))
                PeopleList = new List<Person>(_people.OrderBy(p => p.Email).ToList());
            else if (parameter.Equals("Birthday"))
                PeopleList = new List<Person>(_people.OrderBy(p => p.Birthday).ToList());
            else if (parameter.Equals("Age"))
                PeopleList = new List<Person>(_people.OrderBy(p => p.Age).ToList());
            else if (parameter.Equals("ChineseSign"))
                PeopleList = new List<Person>(_people.OrderBy(p => p.ChineseSign).ToList());
            else if (parameter.Equals("SunSign"))
                PeopleList = new List<Person>(_people.OrderBy(p => p.SunSign).ToList());
        }

        public void FilterList(string name, string surname, string email, DateTime? birthdayFrom, DateTime? birthdayTo,
            string chineseSign, string sunSign, bool adult, bool notAdult, bool birthday, bool notBirthday)
        {
            var res = from p in PeopleList
                where p.Name.Contains(name) && p.Surname.Contains(surname) && p.Email.Contains(email) &&
                      p.ChineseSign.Contains(chineseSign) && p.SunSign.Contains(sunSign) &&
                      p.Birthday >= birthdayFrom &&
                      p.Birthday <= birthdayTo
                select p;

            if (!(adult && notAdult))
            {
                if (adult)
                {
                    var r = from p in res
                        where p.IsAdult
                        select p;
                    res = r;
                }
                else
                {
                    var r = from p in res
                        where !p.IsAdult
                        select p;
                    res = r;
                }
            }


            if (!(birthday && notBirthday))
            {
                if (birthday)
                {
                    var r = from p in res
                        where p.IsBirthday
                        select p;
                    res = r;
                }
                else
                {
                    var r = from p in res
                        where !p.IsBirthday
                        select p;
                    res = r;
                }
            }

            PeopleList = res.ToList();
        }

        public List<Person> PeopleList
        {
            get { return _people; }
            set
            {
                _people = value;
            }
        }

        public void SaveChanges()
        {
            SerializationManager.Serialize(_people, FileFolderHelper.StorageFilePath);
        }
    }
}
