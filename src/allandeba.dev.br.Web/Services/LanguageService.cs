using allandeba.dev.br.Web.Common.Enums;
using Deba.Caching.Interfaces;
using Microsoft.JSInterop;

namespace allandeba.dev.br.Web.Services;

public class LanguageService(
    EventAggregator eventAggregator,
    ILocalStorageCacheService localStorageCache,
    IJSRuntime js)
{
    private const string _cacheKey = "lang";

    public ELanguageType Current { get; private set; } = ELanguageType.English;

    public async Task SetLanguageAsync(ELanguageType language)
    {
        Current = language;
        await localStorageCache.SetItemAsync(_cacheKey, language);
        await js.InvokeVoidAsync("setHtmlLang", ToCulture(language));
        eventAggregator.Publish(language);
    }

    public async Task<ELanguageType> InitAsync()
    {
        var stored = await localStorageCache.GetItemAsync<ELanguageType?>(_cacheKey);
        // No stored choice: trust the boot script's detection already applied to <html lang>.
        Current = stored ?? await DetectFromHtmlLangAsync();
        await js.InvokeVoidAsync("setHtmlLang", ToCulture(Current));
        return Current;
    }

    public static string ToCulture(ELanguageType language)
        => language == ELanguageType.Portuguese ? "pt-BR" : "en";

    private async Task<ELanguageType> DetectFromHtmlLangAsync()
    {
        var htmlLang = await js.InvokeAsync<string>("getHtmlLang");
        return htmlLang?.StartsWith("pt", StringComparison.OrdinalIgnoreCase) == true
            ? ELanguageType.Portuguese
            : ELanguageType.English;
    }
}
