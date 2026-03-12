using AutoMapper; // Add this using directive
using Demo.Application.Contracts;
using Demo.Application.CustomException;
using Demo.Identity.Configuration;
using Demo.Identity.Context;
using Demo.Identity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace Demo.Identity
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDemoIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            #region Identity Resigter
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddDbContext<DemoIdentityContext>(option =>
                option.UseSqlServer(configuration.GetConnectionString("DemoIdentityContext")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
            {
                option.User.RequireUniqueEmail = true;


            }).AddEntityFrameworkStores<DemoIdentityContext>()
            .AddUserManager<UserManager<ApplicationUser>>()
            .AddDefaultTokenProviders();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.User.RequireUniqueEmail = true;
                opt.User.AllowedUserNameCharacters = string.Empty;

                opt.Password = new PasswordOptions()
                {
                    RequireDigit = false,
                    RequireLowercase = false,
                    RequireUppercase = false,
                    RequiredLength = 6,
                    RequiredUniqueChars = 1,
                    RequireNonAlphanumeric = false
                };
            });
            services.Configure<IdentityConfiguration>(configuration.GetSection("IdentitySetting"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.SaveToken = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,
                      ValidIssuer = configuration["IdentitySetting:Issuer"],
                      ValidAudience = configuration["IdentitySetting:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["IdentitySetting:Key"]))
                  };
                  // Add event handlers for authentication events in case of token issues
                  o.Events = new JwtBearerEvents()
                  {
                      ///toen issue
                      OnAuthenticationFailed = c =>
                      {
                          throw new UnauthorizedException("Unauthorized");
                      },
                      //Authentication Required
                      OnChallenge = context =>
                      {
                          throw new UnauthorizedException("Unauthorized");
                      },
                      //unauthorized
                      OnForbidden = context =>
                      {
                          throw new UnauthorizedException("Unauthorized");
                      },
                      //in case of signalR  
                      OnMessageReceived = context =>
                      {
                          var acessToken = context.Request.Query["access_token"];
                          var path = context.HttpContext.Request.Path;
                          if (!string.IsNullOrEmpty(acessToken) && path.StartsWithSegments("/Notification"))
                          {
                              context.Token = acessToken;
                          }
                          return Task.CompletedTask;
                      }
                  };
              });
            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnSigningIn = async (signinContext) =>
                {
                    var principal = signinContext.Principal;
                    var identity = principal.Identity as ClaimsIdentity;

                    foreach (var claim in principal.Claims.ToList())
                    {
                        if (claim.Type == "Permission")
                        {
                            identity.RemoveClaim(claim);
                        }
                    }
                };
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
              .AddCookie(options =>
              {
                  options.Cookie.SameSite = SameSiteMode.None;
                  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                  options.Cookie.IsEssential = true;
              });
            var dataContext = services.BuildServiceProvider().GetRequiredService<DemoIdentityContext>();
            dataContext.Database.EnsureCreated();
            services.AddScoped<IAuthentactionService, Service.AuthenticationService>();
            services.AddAuthorization();
            #endregion
            return services;
        }
    }
}
