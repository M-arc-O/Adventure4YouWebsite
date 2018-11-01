﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adventure4You.Models
{
    [Table("Points")]
    public class Point
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }

        [MaxLength(255)]
        public string Coordinates { get; set; }

        public Point()
        {
        }
    }
}
