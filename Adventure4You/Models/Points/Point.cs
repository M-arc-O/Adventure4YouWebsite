﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adventure4You.Models.Points
{
    [Table("Points")]
    public class Point
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid StageId { get; set; }

        [Required]
        public PointType Type { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public int Value { get; set; }

        [MaxLength(255)]
        public string Coordinates { get; set; }

        public string Answer { get; set; }

        public string Message { get; set; }

        public Point()
        {
        }
    }
}
