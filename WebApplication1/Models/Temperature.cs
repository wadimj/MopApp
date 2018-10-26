namespace WebApplication1.Models
{
    public class Temperature
    {
        public int Id { get; set; }
        public double Temp { get; set; }
        public int Timestamp { get; set; }

        public virtual Device Device{ get; set; }
    }
}