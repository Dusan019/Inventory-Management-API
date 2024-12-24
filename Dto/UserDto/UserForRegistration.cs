using System.ComponentModel.DataAnnotations;

namespace InventoryManagementAPI.Dto.UserDto
{
    public record UserForRegistrationDto
    {


        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }

        [Required]
        public string Username { get; init; }


        [Required]
        public string Email { get; init; }
        [Required]
        public string Password { get; init; }
      
    }
}
