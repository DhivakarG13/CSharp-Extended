using CSVFilePractice.Constants;
using CSVFilePractice.Models;
using CSVFilePractice.Repositories;

namespace CSVFilePractice
{
    public class Program
    {
        static void Main(string[] args)
        {
            PeopleRepository peopleRepository = new PeopleRepository("Data.csv");
            List<Person> people = peopleRepository.People;
            people.Add(new Person("Dhivakar", 65.5, GenderOptions.Male));
            people.Add(new Person("Arav", 64.5, GenderOptions.Male));
            people.Add(new Person("Hemanth", 65.5, GenderOptions.Female));
            peopleRepository.SaveToDatabaseUsingCSVHelper();
            people = peopleRepository.LoadFromDatabaseUsingCSVHelper();
            DisplayPeopleData(people);
            Console.ReadKey();
        }

        public static void DisplayPeopleData(List<Person> people)
        {
            foreach (Person person in people)
            {
                Console.WriteLine($"{person.Name},{person.Weight},{person.Gender}");
            }
        }
    }
}


