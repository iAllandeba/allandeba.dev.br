using allandeba.dev.br.Web.Common.Enums;
using allandeba.dev.br.Web.Services;

namespace allandeba.dev.br.Web.Localization;

public interface IContentProvider
{
    ISiteContent Content { get; }
}

public class ContentProvider(LanguageService languageService) : IContentProvider
{
    private readonly ISiteContent _english = new EnglishContent();
    private readonly ISiteContent _portuguese = new PortugueseContent();

    public ISiteContent Content =>
        languageService.Current == ELanguageType.Portuguese ? _portuguese : _english;
}
