namespace WebApiDemo.Models
{
    public class Employee
    {
        public required string position { get; set; }
        public required string fullName { get; set; }
        public int? managerId { get; set; }
        public bool? isEnabled { get; set; }
    }
}
