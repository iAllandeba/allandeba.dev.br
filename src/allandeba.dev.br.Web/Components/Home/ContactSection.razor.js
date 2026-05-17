export function openContact() {
    if (window.$chatwoot) {
        window.$chatwoot.toggle();
    } else {
        window.location = 'mailto:allandeba@icloud.com';
    }
}
