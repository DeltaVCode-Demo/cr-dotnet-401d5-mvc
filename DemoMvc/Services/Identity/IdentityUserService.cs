using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using DemoMvc.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DemoMvc.Services.Identity
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState);
        Task<UserDto> Authenticate(LoginData data);
        Task<UserDto> GetUser(ClaimsPrincipal user);
        Task SetProfileImage(ClaimsPrincipal user, string url);
        Task Logout();
    }

    public class IdentityUserService : IUserService
    {
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        readonly IEmailService emailService;

        public IdentityUserService(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, ILogger<IdentityUserService> logger, IEmailService emailService)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            Logger = logger;
            this.emailService = emailService;
        }

        public ILogger<IdentityUserService> Logger { get; }

        public async Task<UserDto> Authenticate(LoginData data)
        {
            var user = await userManager.FindByNameAsync(data.Username);

            if (await userManager.CheckPasswordAsync(user, data.Password))
            {
                await signInManager.SignInAsync(user, false);
                return await CreateUserDto(user);
            }

            Logger.LogInformation("Invalid login for username '{Username}'", data.Username);
            return null;
        }

        public async Task<UserDto> GetUser(ClaimsPrincipal principal)
        {
            var user = await userManager.GetUserAsync(principal);
            if (user == null) return null;

            return await CreateUserDto(user);
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public async Task<UserDto> Register(RegisterData data, ModelStateDictionary modelState)
        {
            var user = new IdentityUser
            {
                Email = data.Email,
                UserName = data.Username,
                // PasswordHash = data.Password // NOOOOOOO
            };
            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                // Make sure staff are admins
                if (data.Email.EndsWith("@newbo.co"))
                    await userManager.AddToRoleAsync(user, "Administrator");

                if (data.MakeMeAnEditor)
                    await userManager.AddToRoleAsync(user, "Editor");

                await emailService.SendEmail(
                    data.Email,
                    "Welcome to 401d5 Demo!",
                    "Welcome!",
                    "<h1>Welcome</h1>"
                    );

                await signInManager.SignInAsync(user, false);
                return await CreateUserDto(user);
            }

            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.Username) :
                    "";
                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }

        public async Task SetProfileImage(ClaimsPrincipal principal, string url)
        {
            var user = await userManager.GetUserAsync(principal);

            // Could add this to ApplicationUser but Keith did not think ahead
            // user.ProfileUrl = url;
            // await userManager.UpdateAsync(user);

            // Remove any existing values
            var existingProfileUrls = principal.FindAll("ProfileUrl");
            await userManager.RemoveClaimsAsync(user, existingProfileUrls);

            await userManager.AddClaimAsync(user, new Claim("ProfileUrl", url));
            await signInManager.RefreshSignInAsync(user);
        }

        private async Task<UserDto> CreateUserDto(IdentityUser user)
        {
            return new UserDto
            {
                UserId = user.Id,
                Email = user.Email,
                Username = user.UserName,
            };
        }
    }
}
