using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.IdentityModel.Tokens;
using RedMango_API.Models;
using RedMango_API.Utilities;
using RedMango_Business.Repository.IRepository;
using RedMango_DataLayer.Models;
using RedMango_Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;

namespace RedMango_API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class AuthController : ControllerBase
{
    private string SecretKey;
    private readonly IUserRepository _userRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    public AuthController(IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager
        , IUserRepository userRepository)
    {
        SecretKey = configuration.GetValue<string>("ApiSettings:Secret");
        _userManager = userManager;
        _roleManager = roleManager;
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequestDTO)
    {
        try
        {
            var userFromDb = await _userRepository.GetUserByUsername(registerRequestDTO.Username);
            var response = ApiResponseConfiguration.ConfigureResponse(true, HttpStatusCode.Created, null, null);
            if (userFromDb != null)
            {
                response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadRequest, "This User Already Exists", null);
                return Ok(response);
            }

            ApplicationUser newUser = new()
            {
                Email = registerRequestDTO.Username,
                NormalizedEmail = registerRequestDTO.Username.ToUpper(),
                Name = registerRequestDTO.Name,
                UserName = registerRequestDTO.Username,
            };

            var result = await _userManager.CreateAsync(newUser, registerRequestDTO.Password);

            if (result.Succeeded)
            {
                if (!_roleManager.RoleExistsAsync(SD.AdminRole).GetAwaiter().GetResult())
                {
                    await _roleManager.CreateAsync(new IdentityRole(SD.AdminRole));
                    await _roleManager.CreateAsync(new IdentityRole(SD.CustomerRole));
                }

                if (registerRequestDTO.Role.ToLower() == SD.AdminRole)
                {
                    await _userManager.AddToRoleAsync(newUser, SD.AdminRole);
                }
                else
                {
                    await _userManager.AddToRoleAsync(newUser, SD.CustomerRole);
                }

                return Ok(response);
            }
            else
            {
                response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadGateway, result.Errors.First().Description, null);
                return Ok(response);
            }
        }
        catch (Exception e)
        {
            var response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadGateway, e.Message, null);
            return Ok(response);
        }
    }


    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
    {
        try
        {
            var userFromDb = await _userRepository.GetUserByUsername(loginRequestDTO.Username);

            var response = new ApiResponse();

            if (userFromDb == null)
            {
                response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadRequest, "User Not Found", null);
                return BadRequest(response);
            }

            var isValid = await _userManager.CheckPasswordAsync(userFromDb, loginRequestDTO.Password);


            //generate token
            var roles = await _userManager.GetRolesAsync(userFromDb);
            JwtSecurityTokenHandler tokenHandler = new();
            byte[] key = Encoding.ASCII.GetBytes(SecretKey);

            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("fullName", userFromDb.Name),
                    new Claim("id", userFromDb.Id.ToString()),
                    new Claim(ClaimTypes.Email, userFromDb.UserName.ToString()),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);



            var loginResponse = new LoginResponseDTO()
            {
                Email = userFromDb.Email,
                Token = tokenHandler.WriteToken(token),
            };


            if (isValid)
            {
                response = ApiResponseConfiguration.ConfigureResponse(true, HttpStatusCode.OK, null, loginResponse);
                return Ok(response);
            }
            else
            {
                response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadRequest, "Could Not Log The User In", null);
                return Ok(response);
            }
        }
        catch (Exception e)
        {
            var response = ApiResponseConfiguration.ConfigureResponse(false, HttpStatusCode.BadGateway, "Something Went Wrong", null);
            return Ok(response);
        }
    }
}
