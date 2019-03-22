using System;
using Laboratory04.Models;
using Laboratory04.Tools.DataStorage;

namespace Laboratory04.Tools.Manager
{
    internal static class StationManager
    {
        public static event Action StopThreads;
        private static IDataStorage _dataStorage;
        private static readonly string[] Names = {"Arthur", "Mia", "Ben", "Oscar", "Alisa", "Marry", "Ann", "Peter", "Lucas", "Isabella", "Erica", "Jack", "Kate", "Sophie", "Iva", "Hugo", "Olivia", "Zoe", "Amelia", "Harry", "Robin", "Fill", "Leah", "Megan", "Drake", "Logan", "Tony", "Poppy", "Nancy", "Lily", "Ash", "Neil", "Scarlet", "Tom", "Chloe", "Set", "Alex", "Victoria", "Ethan", "Julia", "Stephen", "Owen" };
        private static readonly string[] Surnames = { "Black", "Morin", "White", "Stark", "Ponce", "Aries", "Wolf", "Stone", "Wood", "Rou", "Lo", "Grey", "Jones", "Oliver", "Sun", "Ink", "Raven", "Breeze", "Ocean", "Ram", "Filch", "Rain", "Salas", "Lopez", "Valdez", "Pena", "Pir", "Bush", "Hunt", "Thomas", "Tree", "Lava", "Benet", "Sky", "Heel", "Silent", "Cube", "Land", "Rise", "Deep", "Moor"};
        private static readonly string[] Emails = { "bk123@gmail.com", "skyWar@mail.ru", "free.mail@ukma.edu.ua", "student.mohyla@ukma.edu.ua", "s.t.moris@gmail.com", "sleepwalker11@ukr.net", "stark@gmail.com", "best@ukma.edu.ua", "f.n.drake@mail.ru", "black.stone@gmail.com", "nightMare@gmail.com", "robinHood@ukr.net", "magic.lord@gmail.com", "n.lava@ukma.edu.ua", "d.b.lava@gmail.com", "myPost@gmail.com" };


        internal static Person CurrentPerson { get; set; }

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
            set
            {
                _dataStorage = value;

            }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
            if (DataStorage.PeopleList.Count == 0)
                CreatePeople();
            DataStorage.SaveChanges();
        }

        private static void CreatePeople()
        {
            Random rand = new Random();
            for (int i = 0; i < 50; i++)
            {
                var name = Names[rand.Next(Names.Length)];
                var surname = Surnames[rand.Next(Surnames.Length)];
                var email = Emails[rand.Next(Emails.Length)];
                var year = rand.Next(DateTime.Today.Year - 135, DateTime.Today.Year + 1);
                var month = (year == 2019? rand.Next(1, DateTime.Today.Month + 1) : rand.Next(1, 13));
                var day = rand.Next(1, DateTime.DaysInMonth(year, month) +1);
                var birthday = new DateTime(year, month, day);

                DataStorage.AddPerson(new Person(name, surname, birthday, email));
            }
        }

        internal static void CloseApp()
        {
            StopThreads?.Invoke();
            Environment.Exit(1);
        }
    }
}
