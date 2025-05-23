﻿using System.ComponentModel.DataAnnotations;

namespace WebFrameworksGroupCA2Project.Models
{
    public class RegisterVM
    {

        [Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Passwords dont match")]
        [Display(Name = "Confirm Password")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.MultilineText)]
        public string? Address { get; set; }
    }
}
