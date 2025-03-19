namespace Models
{
    public class Employee : Person
    {
        public double Salary { get; set; }
        public Employee(string name, int age, double salary) : base(name, age)
        {
            Salary = salary;
        }
    }
}
