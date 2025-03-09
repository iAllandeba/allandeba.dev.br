<!-- ChatWoot -->
let websiteToken = '';
if (window.location.hostname === 'localhost') {
  websiteToken = 'UbQRYkTj9zZKvjVm5kwPGa6a';  
} else if (window.location.hostname.includes("hml.")) {
    websiteToken = 'jdsnnR1hAb72TZLYZT8ZsLPf';
} else {
    websiteToken = 'pWLn4B1wXC6kHKEBtxjacAE1';
}

(
    function(d,t) {
        var BASE_URL="https://chat.allandeba.dev.br";
        
        var g= d.createElement(t), s= d.getElementsByTagName(t)[0];
        g.src=BASE_URL+"/packs/js/sdk.js";
        g.defer = true;
        g.async = true;
        s.parentNode.insertBefore(g, s);
        g.onload = function(){
            window.chatwootSDK.run({
                websiteToken: websiteToken,
                baseUrl: BASE_URL
            })
        }
    }
)(document,"script");