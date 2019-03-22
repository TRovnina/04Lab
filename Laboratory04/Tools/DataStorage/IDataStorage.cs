using System;
using System.Collections.Generic;
using Laboratory04.Models;

namespace Laboratory04.Tools.DataStorage
{
    internal interface IDataStorage
    {
        void AddPerson(Person person);
        void DeletePerson(Person person);
        void SaveChanges();
        List<Person> PeopleList { get; set; }
        void SortList(string parameter);
        void FilterList(string name, string surname, string email, DateTime? birthdayFrom, DateTime? birthdayTo,
            string chineseSign, string sunSign, bool adult, bool notAdult, bool birthday, bool notBirthday);
    }
}
