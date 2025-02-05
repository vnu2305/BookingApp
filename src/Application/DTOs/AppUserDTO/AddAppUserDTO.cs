﻿using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.AppUserDTO
{
    public class AddAppUserDTO
    {
        [Required(ErrorMessage = "This field {0} is required")]
        public long TelegramId { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [StringLength(150, ErrorMessage = "The field {0} must be between {2} and {1} characters", MinimumLength = 2)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [Phone]
        public string TelephoneNumber { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        [EmailAddress(ErrorMessage = "Not correct addres")]
        public string Email { get; set; }
        public Roles Role { get; set; }

        [Required(ErrorMessage = "This field {0} is required")]
        public DateTimeOffset EmploymentStart { get; set; }
        public DateTimeOffset? EmploymentEnd { get; set; }
        public Guid? PrefferdWorkPlaceId { get; set; }
    }
}
