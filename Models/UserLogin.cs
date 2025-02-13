using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace V._3._0.Models
{
    public class UserLogin
    {
        [BindProperty]
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Emailid { get; set; }= string.Empty;
        [Required]
        public string Password { get; set; }= string.Empty;

        public string ReturnUrl { get; set; }= string.Empty;
        public void SetReturnUrl(string role)
        {
            ReturnUrl = role switch
            {
                "Admin" => "/Admin/Dashboard",
                "Doctor" => "/Doctor/Portal",
                "Patient" => "/Patient/Profile",
                "Staff" => "/Staff/Management",
                _ => "/Home/Index" // Default
            };
        }
    }
}