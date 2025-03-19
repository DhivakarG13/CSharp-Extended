namespace Models
{
    public class Student : Person
    {
        public double Grade { get; set; }

        public Student(string name, int age, double grade) : base(name, age)
        {
            Grade = grade;
        }
    }
}
