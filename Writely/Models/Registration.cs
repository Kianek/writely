using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Writely.Models
{
    public class Registration
    {
        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        
        [Required(ErrorMessage = "Confirmation password is required")]
        public string? ConfirmPassword { get; set; }

        public bool IsComplete()
        {
            var properties = new Dictionary<string, bool>
            {
                [nameof(FirstName)] = string.IsNullOrEmpty(FirstName),
                [nameof(LastName)] = string.IsNullOrEmpty(LastName),
                [nameof(Email)] = string.IsNullOrEmpty(Email),
                [nameof(Username)] = string.IsNullOrEmpty(Username),
                [nameof(Password)] = string.IsNullOrEmpty(Password),
                [nameof(ConfirmPassword)] = string.IsNullOrEmpty(ConfirmPassword),
            };
            return properties.Values.All(value => !value);
        }
    }
}