﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace adad.Controllers
{
    public class ContactDataModel

    {
        [Required]
        [StringLength(50, MinimumLength = 1)]
        [DisplayName("Full Name:")]
        [BindProperty(SupportsGet = true, Name = "Name")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(255, MinimumLength = 1)]
        [DisplayName("Email Address:")]
        [BindProperty(SupportsGet = true, Name = "Email")]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 1)]
        [DisplayName("Message:")]
        [BindProperty(SupportsGet = true, Name = "Message")]
        public string? Message { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(1000, MinimumLength = 1)]
        [DisplayName("Subject:")]
        [BindProperty(SupportsGet = true, Name = "Subject")]
        public string? Subject { get; set; }

        
    }

}
