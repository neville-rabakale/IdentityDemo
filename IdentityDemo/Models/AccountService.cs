using IdentityDemo.Views.Account;

namespace IdentityDemo.Models
{
    public class AccountService
    {
        public string TryRegister(RegisterVM viewModel)
        {
            // Todo: Try to create a new user
            return "Failed to create user";
        }

        public bool TryLogin(LoginVM viewModel)
        {
            // Todo: Try to sign user
            return false;
        }
    }
}
