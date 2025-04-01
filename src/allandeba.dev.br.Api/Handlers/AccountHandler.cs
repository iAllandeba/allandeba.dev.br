using System.Net.Mail;
using allandeba.dev.br.Api.Data.Entities;
using allandeba.dev.br.Api.Extensions;
using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Models.Account;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Core.Responses;
using allandeba.dev.br.Core.Responses.Account;
using Microsoft.AspNetCore.Identity;

namespace allandeba.dev.br.Api.Handlers;

public class AccountHandler(SignInManager<Users> signInManager) : IAccountHandler
{
    public async Task<Response<AccountResponse?>> LoginAsync(LoginRequest request)
    {
        try
        {
            var user = await signInManager.UserManager.FindByEmailAsync(request.Email);
            if (user is null)
                throw new ApplicationException("Invalid email or password");

            var result = await signInManager.PasswordSignInAsync(user, request.Password, true, false);

            return result.Succeeded
                ? new Response<AccountResponse?>()
                : new Response<AccountResponse?>(null, 404, "Ocorreu um erro ao efetuar o login");
        }
        catch
        {
            return new Response<AccountResponse?>(null, 500, "Não foi possível efetuar o login");
        }
    }

    public async Task<Response<AccountResponse?>> RegisterAsync(RegisterRequest request)
    {
        try
        {
            var mailAddress = new MailAddress(request.Email);
            var newUser = new Users
            {
                Email = mailAddress.Address,
                EmailConfirmed = true,
                UserName = mailAddress.User
            };

            var result = await signInManager.UserManager.CreateAsync(newUser, request.Password);

            return result.Succeeded
                ? new Response<AccountResponse?>()
                : new Response<AccountResponse?>(null, 404, "Ocorreu um erro ao criar o usuário",
                    result.Errors.ToText());
        }
        catch
        {
            return new Response<AccountResponse?>(null, 500, "Não foi possível criar um usuário");
        }
    }

    public async Task<Response<AccountResponse?>> LogoutAsync()
    {
        try
        {
            await signInManager.SignOutAsync();

            return new Response<AccountResponse?>();
        }
        catch
        {
            return new Response<AccountResponse?>(null, 500, "Não foi possível efetuar o logout");
        }
    }
}