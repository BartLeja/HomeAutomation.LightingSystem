using System;

namespace LightingSystem.Data.Dtos
{
    public class BulbDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public bool Status { get; set; }
    }
}
