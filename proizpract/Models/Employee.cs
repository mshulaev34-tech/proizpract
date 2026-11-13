namespace proizpract.Models
{
    /// <summary>
    /// Represents an employee with basic HR attributes.
    /// </summary>
    public class Employee
    {
        public string Name { get; }
        public int Age { get; }
        public decimal Salary { get; }
        public string Department { get; }

        public Employee(string name, int age, decimal salary, string department)
        {
            Name = name;
            Age = age;
            Salary = salary;
            Department = department;
        }

        public override string ToString()
        {
            return $"{Name,-15} | возраст: {Age,2} | зарплата: {Salary,8:F0} | отдел: {Department}";
        }
    }
}