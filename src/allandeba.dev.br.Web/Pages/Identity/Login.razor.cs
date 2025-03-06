using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Core.Requests.Account;
using allandeba.dev.br.Web.Security;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace allandeba.dev.br.Web.Pages.Identity;

public partial class LoginPage : ComponentBase
{
    #region Services

    [Inject] public IAccountHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ISnackbar Snackbar { get; set; } = null!;
    [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion

    #region Properties

    public bool IsBusy { get; set; } = false;
    public LoginRequest InputModel { get; set; } = new();

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity is { IsAuthenticated: true })
            NaviagateToAuthenticatedUrl();
    }

    #endregion

    #region Methods

    public async Task OnValidSubmitAsync()
    {
        IsBusy = true;

        try
        {
            var result = await Handler.LoginAsync(InputModel);

            if (result.IsSuccess)
            {
                await AuthenticationStateProvider.GetAuthenticationStateAsync();
                AuthenticationStateProvider.NotifyAuthenticationStateChanged();
                NaviagateToAuthenticatedUrl();
            }
            else
                Snackbar.Add(result.Message!, Severity.Error);
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

    private void NaviagateToAuthenticatedUrl() =>
        NavigationManager.NavigateTo("/dashboard");

    #endregion
}