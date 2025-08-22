namespace WebApiDemo.Models
{
    public class APIresponse
    {
        public Object? Data { get; set; }
        public Metrics metrics { get; set; } = new Metrics();
        public Error error { get; set; } = new Error();
    }

    public class Metrics
    {
        public DateTime startDate { get; set; } = DateTime.Now;
        public DateTime endDate { get; set; } = DateTime.Now;
        public string resultTime => (endDate - startDate).ToString();
    }

    public class Error
    {
        public int code { get; set; } = 0;
        public string message { get; set; } = "Proceso finalizado con exito";
    }
}
