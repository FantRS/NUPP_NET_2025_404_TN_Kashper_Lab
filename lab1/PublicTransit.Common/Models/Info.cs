namespace PublicTransit.Common.Models
{
    public struct Info(string firstName, string lastName, float salary)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
        public float Salary { get; set; } = salary;
    }
}
