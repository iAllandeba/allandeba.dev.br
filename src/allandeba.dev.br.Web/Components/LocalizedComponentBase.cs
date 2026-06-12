using allandeba.dev.br.Web.Common.Enums;
using allandeba.dev.br.Web.Services;
using Microsoft.AspNetCore.Components;

namespace allandeba.dev.br.Web.Components;

public abstract class LocalizedComponentBase : ComponentBase, IDisposable
{
    [Inject] protected EventAggregator EventAggregator { get; set; } = default!;

    protected override void OnInitialized()
        => EventAggregator.Subscribe<ELanguageType>(OnLanguageChanged);

    private void OnLanguageChanged(ELanguageType language)
        => _ = InvokeAsync(StateHasChanged);

    public virtual void Dispose()
        => EventAggregator.Unsubscribe<ELanguageType>(OnLanguageChanged);
}
