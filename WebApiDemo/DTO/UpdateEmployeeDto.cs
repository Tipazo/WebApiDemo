namespace WebApiDemo.DTO
{
    public class UpdateEmployeeDto
    {
        public required int EmployeeId { get; set; }
        public required string Position { get; set; } = "";
        public required string FullName { get; set; } = "";
        public int? ManagerId { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
