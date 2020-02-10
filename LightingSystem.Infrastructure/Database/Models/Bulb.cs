using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LightingSystem.Infrastructure.Database.Models
{
    public class Bulb
    {
        [Key]
        public Guid Id { get; set; }
        public bool Status { get; set; }
    }
}
