using CSVFilePractice.Constants;

namespace CSVFilePractice.Models
{
    public class Person
    {
        public string Name { get; set; }

        public double Weight { get; set; }

        public GenderOptions Gender { get; set; }

        public Person(string Name, double Weight, GenderOptions Gender)
        {
            this.Name = Name;
            this.Weight = Weight;
            this.Gender = Gender;
        }
    }
}


