export function trackContact(channel, placement) {
    if (typeof window.gtag !== 'function') return;

    try {
        window.gtag('event', 'contact_click', {
            contact_channel: channel,
            contact_placement: placement,
            page_language: document.documentElement.lang || 'en',
            transport_type: 'beacon'
        });
    } catch (error) {
        console.warn('Contact analytics unavailable.', error);
    }
}

function onContactClick(event) {
    const link = event.target.closest?.('a[data-contact-channel]');
    if (!link || event.defaultPrevented) return;
    trackContact(link.dataset.contactChannel, link.dataset.contactPlacement);
}

export function initContactTracking() {
    document.addEventListener('click', onContactClick);
}

export function disposeContactTracking() {
    document.removeEventListener('click', onContactClick);
}
