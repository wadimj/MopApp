using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WebApplication1.Models
{
    public class Device
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public double Average { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Temperature> Temperatures { get; set; }
    }
}