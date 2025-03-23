<!-- ChatWoot -->
let websiteToken = '';
if (window.location.hostname === 'localhost') {
  websiteToken = 'DrNwBcyGPCPkv4ReNo3yDXub';  
} else if (window.location.hostname.includes("hml.")) {
    websiteToken = '5GFHmAciFyixRpems8W2kF21';
} else {
    websiteToken = 'uHchg8XcwWRsMQFQP71VMhh3';
}

(
    function(d,t) {
        var BASE_URL="https://app.chatwoot.com";
        
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