var _systemListener = null;
var _dotNetRef = null;

export function apply(theme) {
    document.documentElement.setAttribute('data-theme', theme);
}

export function getSystemPreference() {
    return window.matchMedia('(prefers-color-scheme: dark)').matches;
}

export function watchSystem(dotNetRef) {
    stopWatching();
    _dotNetRef = dotNetRef;
    _systemListener = function (e) {
        _dotNetRef.invokeMethodAsync('OnSystemPreferenceChanged', e.matches);
    };
    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', _systemListener);
}

export function stopWatching() {
    if (_systemListener) {
        window.matchMedia('(prefers-color-scheme: dark)').removeEventListener('change', _systemListener);
        _systemListener = null;
        _dotNetRef = null;
    }
}
