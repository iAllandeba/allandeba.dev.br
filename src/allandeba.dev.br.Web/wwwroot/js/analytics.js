let siteVersion = 'unknown';

export function setSiteVersion(version) {
    siteVersion = version || 'unknown';
}

export function trackEvent(name, parameters = {}) {
    const payload = {
        ...parameters,
        page_language: document.documentElement.lang || 'en',
        site_version: siteVersion,
        transport_type: 'beacon'
    };
    try {
        if (typeof window.gtag === 'function')
            window.gtag('event', name, payload);
    } catch (error) {
        console.warn('Portfolio analytics unavailable.', error);
    }
}
