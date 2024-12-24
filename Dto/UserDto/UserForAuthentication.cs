// In UserForAuthenticationDto
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public record UserForAuthenticationDto(
    [Required] string Email,   // Use Email instead of UserName
    [Required] string Password
);

