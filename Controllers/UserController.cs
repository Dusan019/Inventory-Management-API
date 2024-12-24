using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data;
using InventoryManagementAPI.Dto.UserDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


namespace InventoryManagementAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public UserController(UserManager<User> userManager, IConfiguration configuration, RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto authDto)
        {
            var user = await _userManager.FindByEmailAsync(authDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, authDto.Password))
            {
                return Unauthorized(new { Message = "Invalid credentials" });
            }

            var token = await CreateToken(user);
            return Ok(new { Token = token });
        }

        [AllowAnonymous]
        [HttpPost("register")]
       
        public async Task<IActionResult> Register([FromBody] UserForRegistrationDto registerDto)
        {
            var userToCreate = new User
            {
                UserName = registerDto.Username,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Role = "Employee"
            };
            var result = await _userManager.CreateAsync(userToCreate, registerDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }
            

            return Ok(new { Message = "User registered successfully" });
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
            };

            return Ok(userDto);
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userManager.Users.ToList();

            var userDtos = users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            }).ToList();

            return Ok(userDtos);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserForUpdateDto updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Update user properties
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;

            // If modifying sensitive data like Email or UserName, we need to update the SecurityStamp
            if (user.Email != updateUserDto.Email)
            {
                user.Email = updateUserDto.Email;
                user.UserName = updateUserDto.Email; // Assuming you want UserName to match Email
            }

            // Ensure SecurityStamp is updated if user data is modified
            var updateSecurityStampResult = await _userManager.UpdateSecurityStampAsync(user);
            if (!updateSecurityStampResult.Succeeded)
            {
                return BadRequest("Failed to update security stamp.");
            }

            // Perform the update
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(user);
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "User deleted successfully" });
        }

        private async Task<string> CreateToken(User user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var tokenOptions = GenerateTokenOptions(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.UTF8.GetBytes(jwtSettings["secretKey"]);
            var secret = new SymmetricSecurityKey(key);

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            return new JwtSecurityToken(
                issuer: jwtSettings["validIssuer"],
                audience: jwtSettings["validAudience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
                signingCredentials: signingCredentials
            );
        }
    }
}
