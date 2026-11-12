import { trackEvent } from './analytics.js';

export function trackContact(channel, placement) {
    trackEvent('contact_click', {
        contact_channel: channel,
        contact_placement: placement
    });
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
