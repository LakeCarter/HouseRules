using System.ComponentModel.DataAnnotations;

namespace HouseRules.Models.DTOs;

public class RegistrationDTO
{
    [EmailAddress(ErrorMessage = "Please enter a valid email address")]
    public string Email { get; set; }
    public string Password { get; set; }
    [Required]
    [MaxLength(50)]
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }

}