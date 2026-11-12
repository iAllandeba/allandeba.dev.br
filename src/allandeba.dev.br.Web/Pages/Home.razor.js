import { initContactTracking, disposeContactTracking } from '../js/contact-analytics.js';

var _clockInterval = null;
var _chromeSelectors = ['.t-nav', '#statusbar'];
var _activeSection = 'hero';

function _typewriter(selector, perCharDuration) {
    var el = document.querySelector(selector);
    if (!el) return;
    var chars = el.querySelectorAll('.char');
    if (!chars.length) return;
    gsap.fromTo(chars,
        { opacity: 0 },
        { opacity: 1, stagger: perCharDuration, duration: 0.01, ease: 'none' }
    );
}

function _initHero() {
    // Keep the role, evidence and contact actions visible throughout the reveal.
    _typewriter('.hero-section .p-cmd', 0.038);
    gsap.fromTo('.hero-name',
        { y: 12 },
        { y: 0, duration: 0.5, ease: 'power3.out' }
    );
}

function _initScrollTriggers() {
    document.querySelectorAll('.section-reveal').forEach(function (section) {
        var elements = section.querySelectorAll('.anim');
        if (!elements.length) return;
        gsap.fromTo(elements,
            { opacity: 0, y: 16 },
            {
                opacity: 1,
                y: 0,
                stagger: 0.08,
                duration: 0.5,
                ease: 'power2.out',
                scrollTrigger: {
                    trigger: section,
                    start: 'top 72%',
                    end: 'bottom 20%',
                    toggleActions: 'play reverse play reverse'
                }
            }
        );
    });

    document.querySelectorAll('.section-reveal').forEach(function (section) {
        var elementsX = section.querySelectorAll('.anim-x');
        if (!elementsX.length) return;
        gsap.fromTo(elementsX,
            { opacity: 0, x: 20 },
            {
                opacity: 1,
                x: 0,
                stagger: 0.08,
                duration: 0.5,
                ease: 'power2.out',
                scrollTrigger: {
                    trigger: section,
                    start: 'top 72%',
                    end: 'bottom 20%',
                    toggleActions: 'play reverse play reverse'
                }
            }
        );
    });

    document.querySelectorAll('section[data-section]').forEach(function (section) {
        ScrollTrigger.create({
            trigger: section,
            start: 'top 50%',
            end: 'bottom 50%',
            onEnter: function () { _updateSection(section.dataset.section); },
            onEnterBack: function () { _updateSection(section.dataset.section); }
        });
    });
}

function _updateSection(name) {
    _activeSection = name;
    var el = document.getElementById('sb-section');
    var section = document.getElementById(name);
    if (el && section) el.textContent = section.dataset.label || name;
    document.querySelectorAll('.nav-links a[data-section]').forEach(function (a) {
        a.classList.toggle('active', a.dataset.section === name);
    });
}

function _initNavMobile() {
    var toggle = document.getElementById('nav-mobile-toggle');
    var menu = document.getElementById('nav-mobile-menu');
    if (toggle && menu) {
        toggle.addEventListener('click', function () {
            menu.classList.toggle('open');
        });
        menu.querySelectorAll('a').forEach(function (a) {
            a.addEventListener('click', function () {
                menu.classList.remove('open');
            });
        });
    }

    document.querySelectorAll('a[data-section]').forEach(function (a) {
        a.addEventListener('click', function (e) {
            var target = document.getElementById(a.dataset.section);
            if (target) {
                e.preventDefault();
                target.scrollIntoView({ behavior: 'smooth' });
            }
        });
    });
}

function _startClock() {
    var update = function () {
        var el = document.getElementById('sb-clock');
        if (!el) return;
        var now = new Date();
        el.textContent = now.toLocaleTimeString(document.documentElement.lang || 'en', { hour12: false });
    };
    update();
    if (_clockInterval) clearInterval(_clockInterval);
    _clockInterval = setInterval(update, 1000);
}

function _skipAnimations() {
    document.querySelectorAll('.anim, .anim-x, [style*="opacity:0"]').forEach(function (el) {
        el.style.opacity = '1';
        el.style.transform = 'none';
    });
    _chromeSelectors.forEach(function (selector) {
        var el = document.querySelector(selector);
        if (el) el.style.opacity = '1';
    });
    var loaderEl = document.getElementById('loader');
    if (loaderEl) loaderEl.style.display = 'none';
}

function _runLoader(loader) {
    var chars = loader.querySelectorAll('.char');
    var sub = loader.querySelector('#loader-sub');
    var tl = gsap.timeline({
        onComplete: function () {
            _initHero();
            _initScrollTriggers();
            _initChatwoot();
        }
    });
    tl.fromTo(chars,
        { opacity: 0, y: 50 },
        { opacity: 1, y: 0, stagger: 0.04, duration: 0.8, ease: 'power3.out' }
    )
    .fromTo(sub,
        { opacity: 0 },
        { opacity: 1, duration: 0.7, ease: 'power2.out' },
        '-=0.4'
    )
    .to(loader, {
        yPercent: -100,
        duration: 0.75,
        ease: 'power3.inOut',
        delay: 0.5
    })
    .to(_chromeSelectors, {
        opacity: 1,
        duration: 0.4,
        ease: 'power2.out'
    }, '-=0.3');
}

function _initChatwoot() {
    var script = document.createElement('script');
    script.src = 'js/chatwoot.js';
    script.async = true;
    document.head.appendChild(script);
}

export function init() {
    _startClock();
    _initNavMobile();
    initContactTracking();
    _updateSection(_activeSection);

    if (typeof gsap === 'undefined' || typeof ScrollTrigger === 'undefined' ||
        window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
        _skipAnimations();
        _initChatwoot();
        return;
    }

    gsap.registerPlugin(ScrollTrigger);

    var loader = document.getElementById('loader');
    if (!loader) {
        gsap.set(_chromeSelectors, { opacity: 1 });
        _initHero();
        _initScrollTriggers();
        _initChatwoot();
        return;
    }

    _runLoader(loader);
}

export function dispose() {
    disposeContactTracking();
    if (_clockInterval) clearInterval(_clockInterval);
    _clockInterval = null;
}

export function setBodyClass(cls, add) {
    if (add) document.body.classList.add(cls);
    else document.body.classList.remove(cls);
}

export function refreshSection() {
    _updateSection(_activeSection);
}
