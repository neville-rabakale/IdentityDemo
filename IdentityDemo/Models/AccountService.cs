using IdentityDemo.Views.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityDemo.Models
{
    public class AccountService
    {
        UserManager<IdentityUser> userManager;
        SignInManager<IdentityUser> signInManager;
        RoleManager<IdentityRole> roleManager;

        public AccountService(
            IdentityDbContext identityContext,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            // Generates Identity tables (only works against an empty database)
            identityContext.Database.EnsureCreated();

            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        public async Task<string> TryRegisterAsync(RegisterVM viewModel)
        {
            IdentityUser identityUser = new IdentityUser
            {
                UserName = viewModel.Username
            };
            IdentityResult createResult = await
                userManager.CreateAsync(identityUser, viewModel.Password);

            bool createSucceeded = createResult.Succeeded;

            if (createSucceeded)
                return null;
            else

                return "Failed to create user";
        }

        public async Task<bool> TryLoginAsync(LoginVM viewModel)
        {
            SignInResult signInResult = await signInManager.PasswordSignInAsync(
                    viewModel.Username,
                    viewModel.Password,
                    isPersistent: false,
                    lockoutOnFailure: false);

            bool signInSucceeded = signInResult.Succeeded;

            if (signInSucceeded)
                return true;
            else
                return false;
        }
        
        public async Task Signout()
        {
            await signInManager.SignOutAsync();
        }
    }
}
