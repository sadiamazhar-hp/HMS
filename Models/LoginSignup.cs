using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Contracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace V._3._0.Models
{
    public enum UserRoles
    {
        Admin,
        Patient,
        Doctor,
        Staff

    }
    public class Signup
    {
        [BindProperty]
        public int SignupId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set;}

        [Required]
        [Column(TypeName = "nvarchar(max)")]
        public string Roles { get; set; }
        //on return from DB
        //RolesEnum userRole = Enum.Parse<RolesEnum>(user.Roles);



        public ICollection<Patients> Patients { get; set; }
    }


    public class Payment : Signup
    {
        [Required]
        public int Package { get; set; }

        [Required]
        public int CardType { get; set; }

        [Required]
        public string CardName { get; set; }

        [Required]
        public int Exp { get; set; }

        [Required]
        public int Cvc { get; set; }
    }


    
}
