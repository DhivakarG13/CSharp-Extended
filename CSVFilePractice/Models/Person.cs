using CSVFilePractice.Constants;
namespace CSVFilePractice.Models
{
    public class Person
    {
        public string Name { get; set; }

        public double Weight { get; set; }

        public GenderOptions Gender { get; set; }

        public Person(string name, double weight, GenderOptions gender)
        {
            Name = name;
            Weight = weight;
            Gender = gender;
        }
    }
}


