using allandeba.dev.br.Core.Handlers;
using allandeba.dev.br.Web.Security;
using Microsoft.AspNetCore.Components;

namespace allandeba.dev.br.Web.Pages.Identity;

public partial class LogoutPage : ComponentBase
{
    #region Services

    [Inject] public IAccountHandler Handler { get; set; } = null!;
    [Inject] public NavigationManager NavigationManager { get; set; } = null!;
    [Inject] public ICookieAuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    #endregion

    #region Overrides

    protected override async Task OnInitializedAsync()
    {
        if (await AuthenticationStateProvider.CheckAuthenticatedAsync())
        {
            await Handler.LogoutAsync();
            await AuthenticationStateProvider.GetAuthenticationStateAsync();
            AuthenticationStateProvider.NotifyAuthenticationStateChanged();
        }

        await base.OnInitializedAsync();
    }

    #endregion
}