﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoList.Model;
using ToDoList.Interface;

namespace ToDoList.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly UserManager<ApiUser> _userManager;
    private readonly JwtConfig _jwtConfig;

    public AuthenticationService(UserManager<ApiUser> userManager, IOptions<JwtConfig> jwtConfig)
    {
        _userManager = userManager;
        _jwtConfig = jwtConfig.Value;
    }

    public async Task<AuthResult> Register(UserRegistrationRequest registrationRequest)
    {
        var userExist = await _userManager.FindByEmailAsync(registrationRequest.Email);
        if (userExist != null)
            return new AuthResult
            {
                Result = false,
                Errors = ["Email already exist"]
            };

        var newUser = new ApiUser
        {
            Email = registrationRequest.Email,
            UserName = registrationRequest.Email,
            FamilyId = registrationRequest.FamilyId,
        };

        var isCreated = await _userManager.CreateAsync(newUser, registrationRequest.Password);

        if (!isCreated.Succeeded)
            return new AuthResult
            {
                Errors = ["Server error"],
                Result = false
            };

        var token = GenerateJwtToken(newUser);
        return new AuthResult
        {
            Result = true,
            Token = token
        };
    }

    public async Task<AuthResult> Login(UserLoginRequest loginRequest)
    {
        var existingUser = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (existingUser == null)
        {
            return new AuthResult
            {
                Errors = ["There is no user with this email"],
                Result = false
            };
        }

        var isCorrect = await _userManager.CheckPasswordAsync(existingUser, loginRequest.Password);

        if (!isCorrect)
        {
            return new AuthResult
            {
                Errors = ["Password and email dont match"],
                Result = false
            };
        }

        var jwtToken = GenerateJwtToken(existingUser);

        return new AuthResult
        {
            Token = jwtToken,
            Result = true
        };
    }

    private string GenerateJwtToken(IdentityUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtConfig.Secret);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity([
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.Now.ToUniversalTime().ToString())
            ]),

            Expires = DateTime.Now.AddMonths(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        return jwtTokenHandler.WriteToken(token);
    }
}