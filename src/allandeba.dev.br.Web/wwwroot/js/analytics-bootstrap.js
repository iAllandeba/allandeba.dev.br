(function () {
    const measurementId = 'G-LN4LRW0ZKC';
    const debug = ['localhost', '127.0.0.1', '[::1]', '::1', 'hml.allandeba.dev.br'].includes(location.hostname);

    window.dataLayer = window.dataLayer || [];
    window.gtag = function () { window.dataLayer.push(arguments); };
    window.gtag('js', new Date());
    if (debug)
        window.gtag('config', measurementId, { debug_mode: true });
    else
        window.gtag('config', measurementId);
    const script = document.createElement('script');
    script.async = true;
    script.src = 'https://www.googletagmanager.com/gtag/js?id=' + measurementId;
    document.head.appendChild(script);
})();
