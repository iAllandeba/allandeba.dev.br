using allandeba.dev.br.Web.Common.Enums;
using allandeba.dev.br.Web.Components.ThemeManager;
using Deba.Caching.Interfaces;
using MudBlazor;

namespace allandeba.dev.br.Web.Services;

public class ThemeManagerService(EventAggregator eventAggregator, ILocalStorageCacheService localStorageCache)
{
    public event Action<EThemeType>? OnThemeChanged;

    public async Task SetThemeAsync(EThemeType theme)
    {
        await localStorageCache.SetItemAsync("theme", theme);
        OnThemeChanged?.Invoke(theme);
        eventAggregator.Publish(theme);
    }

    public async Task<EThemeType> GetCacheThemeAsync()
    {
        var cachedTheme = await localStorageCache.GetItemAsync<EThemeType>("theme");
        OnThemeChanged?.Invoke(cachedTheme);
        eventAggregator.Publish(cachedTheme);
        return cachedTheme;
    }

    public async Task<string> GetThemeIcon()
    {
        var theme = await GetCacheThemeAsync();
        return GetThemeIcon(theme);
    }

    public string GetThemeIcon(EThemeType theme)
    {
        switch (theme)
        {
            case EThemeType.Light:
                return Icons.Material.Filled.LightMode;
            case EThemeType.Dark:
                return Icons.Material.Filled.DarkMode;
            default:
                return Icons.Material.Filled.BrightnessAuto;
        }
    }
}