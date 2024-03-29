﻿using System.ComponentModel.DataAnnotations;
using Milles_Project1Library.Interfaces.ModelsInterface;

namespace Milles_Project1Library.Models
{
    public class Shape : IShape
    {
        [Key]
        public int ShapeId { get; set; }

        public string ShapeType { get; set; }

        public decimal Base { get; set; }

        public decimal Height { get; set; }

        public decimal? SideLength { get; set; }

        public decimal Area { get; set; }

        public decimal Perimeter { get; set; }

        public DateTime CalculationDate { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
