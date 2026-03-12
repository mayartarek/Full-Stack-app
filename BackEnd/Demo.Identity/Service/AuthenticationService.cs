using Demo.Application.Contracts;
using Demo.Application.CustomException;
using Demo.Application.Model.Authuntication;
using Demo.Identity.Configuration;
using Demo.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Identity.Service
{
    public class AuthenticationService : Demo.Application.Contracts.IAuthentactionService

    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IServiceProvider _provider;
        private readonly IdentityConfiguration _identityConfiguration;

        public AuthenticationService(SignInManager<ApplicationUser> signInManager,UserManager<ApplicationUser> userManager,RoleManager<ApplicationRole> roleManager, IOptions<IdentityConfiguration> identityConfiguration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _identityConfiguration = identityConfiguration.Value;

        }

        public async Task<AuthenticationResponseDto> Authentication(AuthenticationRequestDtoVm authenticationDto)
        {
            try
            {
                var user = new ApplicationUser();
                if (!string.IsNullOrWhiteSpace(authenticationDto.Email))
                {
                    user = await _userManager.Users.Where(a => a.Email == authenticationDto.Email).FirstOrDefaultAsync();
                    if (user == null)
                    {
                        throw new NotFoundException(nameof(user), authenticationDto.Email);
                    }
                }
                var result = await _signInManager.PasswordSignInAsync(user.Email, authenticationDto.Password, false, false);
                if (!result.Succeeded)
                {
                    var message = "";
                    if (authenticationDto.Email != null || authenticationDto.Email != "")
                    {
                        message = authenticationDto.Email;
                    }
                 
                    throw new BadRequestException("login failed");
                }
                    JwtSecurityToken jwtSecurityToken = await GenerateToken(user);
                var response = new AuthenticationResponseDto { Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken) };
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            //var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));

            }


            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("HireDate", user.HireDate.ToString() != null ? user.HireDate.ToString() : ""),
                new Claim("firstName",user.FirstName != null ? user.FirstName : ""),
                new Claim("lastName", user.LastName != null ? user.LastName : ""),
                new Claim("PhoneNumber", user.PhoneNumber != null ? user.PhoneNumber : ""),
                

            }
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_identityConfiguration.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _identityConfiguration.Issuer,
                audience: _identityConfiguration.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_identityConfiguration.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }

        public async Task<bool> LogoutAsync()
        {
            await _signInManager.SignOutAsync();
            return true;
        }
    }
}
