﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebFrameworksGroupCA2Project.Models
{
    public class Rating
    {

        [Key]

        public int Id { get; set; }

        [Required]
        [Range(0, 10, ErrorMessage = "Rating must be between 0 and 10.")]
        public double UserRating { get; set; }
        public string? Comment { get; set; }
        public string? UserId { get; set; }
        [ForeignKey("UserId")]
        public AppUser? User { get; set; }

        public int? SongId { get; set; }
        [ForeignKey("SongId")]
        public Song? Song { get; set; }

    }
}
