using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LightingSystem.Data.Models
{
    public class Bulb
    {
        [Key]
        public Guid Id { get; set; }
        public int Number { get; set; }
        public bool Status { get; set; }
    }
}
