import { trackContact } from '../../js/contact-analytics.js';

export function openContact() {
    if (window.$chatwoot) {
        trackContact('chat', 'contact');
        window.$chatwoot.toggle();
    } else {
        trackContact('email', 'chat_fallback');
        window.location = 'mailto:allandeba@icloud.com';
    }
}
