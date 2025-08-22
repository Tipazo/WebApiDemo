namespace WebApiDemo.Models
{
    public class SqlServerParams
    {
        public string host { get; set; } = "localhost";
        public int port { get; set; } = 1430;
        public string? database { get; set; } = null;
        public string? user { get; set; } = null;
        public string? password { get; set; } = null;
        public bool trusted_connection { get; set; } = false;

        public string GetConnectionString()
        {
            if (trusted_connection)
            {
                return $"Server={host},{port};Database={database};Trusted_Connection=True;";
            }
            else
            {
                return $"Server={host},{port};Database={database};User Id={user};Password={password};Encrypt=true;TrustServerCertificate=true;";
            }
        }
    }
}