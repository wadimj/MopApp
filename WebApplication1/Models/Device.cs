using System.Collections;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Temperature> Temperatures { get; set; }
    }
}