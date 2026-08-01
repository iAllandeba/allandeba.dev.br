using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Web.Components;
using allandeba.dev.br.Web.Localization;
using allandeba.dev.br.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace allandeba.dev.br.Web.Pages.Identity;

public partial class RegisterPage : LocalizedComponentBase
{
    #region Dependencies

    [Inject] public IAccountHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;
    [Inject] public IContentProvider ContentProvider { get; set; } = null!;

    #endregion

    #region Properties

    public bool IsBusy { get; set; } = false;
    public RegisterRequest InputModel { get; set; } = new();

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
            NavigationManager.NavigateTo("/");
    }

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.RegisterAsync(InputModel);

            if (result.IsSuccess)
            {
                Snackbar.Add(result.Message ?? ContentProvider.Content.Auth.RegisterSucceeded, Severity.Success);
                NavigationManager.NavigateTo("/login");
            }
            else
                Snackbar.Add(result.Message ?? ContentProvider.Content.Auth.RegisterFailed, Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
        finally
        {
            IsBusy = false;
        }
    }

    #endregion
}