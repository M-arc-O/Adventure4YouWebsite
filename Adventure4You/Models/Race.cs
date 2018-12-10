﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Adventure4You.Models
{
    [Table("Races")]
    public class Race
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required]
        public bool CoordinatesCheckEnabled { get; set; }

        [Required]
        public bool SpecialTasksAreStage { get; set; }

        [Required]
        public int MaximumTeamSize { get; internal set; }

        [Required]
        public int MimimumPointsToCompleteStage { get; internal set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public Race()
        {
        }
    }
}
