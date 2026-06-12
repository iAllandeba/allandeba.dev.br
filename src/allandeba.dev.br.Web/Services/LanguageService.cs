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

    // Synchronous so it runs before the first paint (called from Home.OnInitialized).
    // The boot script in index.html already resolved the language — stored choice or
    // navigator detection — into <html lang>, so the first render is correct and no
    // re-render is needed.
    public void Init()
        => Current = DetectFromHtmlLang();

    public static string ToCulture(ELanguageType language)
        => language == ELanguageType.Portuguese ? "pt-BR" : "en";

    private ELanguageType DetectFromHtmlLang()
    {
        if (js is not IJSInProcessRuntime jsSync)
            return ELanguageType.English;

        var htmlLang = jsSync.Invoke<string>("getHtmlLang");
        return htmlLang?.StartsWith("pt", StringComparison.OrdinalIgnoreCase) == true
            ? ELanguageType.Portuguese
            : ELanguageType.English;
    }
}
