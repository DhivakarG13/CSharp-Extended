using Models;
using Repository;

namespace CustomConverterPractice
{
    public class Program
    {
        static void Main(string[] args)
        {
            ProjectRepository projectRepository = new ProjectRepository("Data.json");
            List<Person> people = projectRepository.PeopleData;
            people.AddRange(new List<Person>()
            {
                new Employee("Dhivakar", 20, 25000),
                new Student("Amar", 19, 7.9)
            });

            foreach (Person person in people)
            {
                Console.WriteLine($"Type:{person.GetType()}, Name: {person.Name}");
            }

            projectRepository.WriteToDatabase();
            List<Person> newPeoples = projectRepository.FetchFromDatabase();

            foreach (Person person in newPeoples)
            {
                Console.WriteLine($"Type:{person.GetType()}, Name: {person.Name}");
            }
        }

        public static void PrintPeopleData(List<Person> peopleDataToPrint)
        {
            foreach (Person person in peopleDataToPrint)
            {
                Console.WriteLine($"Type:{person.GetType()}, Name: {person.Name}");
            }
        }
    }
}
