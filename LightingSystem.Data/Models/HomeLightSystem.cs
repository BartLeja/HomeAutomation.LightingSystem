using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LightingSystem.Data.Models
{
    public class HomeLightSystem
    {
        [Key]
        public Guid LocalLightingSystemId { get; set; }
        public string UserName { get; set; }

        public List<LightPoint> LightPoints { get; set; }
    }
}
