using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [NotMapped()]
    public class ChartData
    {
        [Key]
        public int Week { get; private set; }
        public double AvgResult { get; private set; }
    }
}