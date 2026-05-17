var _clockInterval = null;

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
    var tl = gsap.timeline();
    tl.fromTo('.prompt-line',
        { opacity: 0 },
        { opacity: 1, duration: 0.3, ease: 'power2.out' }
    )
    .add(function () { _typewriter('.p-cmd', 0.038); })
    .fromTo('.hero-name',
        { opacity: 0, y: 20 },
        { opacity: 1, y: 0, duration: 0.5, ease: 'power3.out' },
        '+=0.5'
    )
    .fromTo('.hero-role',
        { opacity: 0, y: 12 },
        { opacity: 1, y: 0, duration: 0.4, ease: 'power2.out' },
        '-=0.1'
    )
    .fromTo('.hero-value',
        { opacity: 0, y: 12 },
        { opacity: 1, y: 0, duration: 0.4, ease: 'power2.out' },
        '-=0.05'
    )
    .fromTo('.hero-tech',
        { opacity: 0, y: 12 },
        { opacity: 1, y: 0, duration: 0.35, ease: 'power2.out' },
        '-=0.05'
    )
    .fromTo('.hero-buttons',
        { opacity: 0, y: 8 },
        { opacity: 1, y: 0, duration: 0.3, ease: 'power2.out' },
        '-=0.05'
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

function _initContactTrigger() {
    var contactSection = document.getElementById('contact');
    if (!contactSection) return;
    ScrollTrigger.create({
        trigger: contactSection,
        start: 'top 60%',
        once: true,
        onEnter: function () { _animateContact(); }
    });
}

function _animateContact() {
    var bar = document.getElementById('progress-bar-inner');
    var pct = document.getElementById('progress-pct');
    var blocks = document.getElementById('progress-blocks');
    var result = document.getElementById('contact-result');
    var chatBtn = document.getElementById('chat-btn');
    var statuses = document.querySelectorAll('.ct-ch-status');
    if (!bar) return;

    var totalBlocks = 20;
    gsap.fromTo(bar,
        { width: '0%' },
        {
            width: '100%',
            duration: 2.2,
            ease: 'linear',
            onUpdate: function () {
                var p = Math.round(this.progress() * 100);
                if (pct) pct.textContent = p + '%';
                if (blocks) {
                    var filled = Math.round((p / 100) * totalBlocks);
                    blocks.textContent =
                        '█'.repeat(filled) + '░'.repeat(totalBlocks - filled);
                }
                if (statuses[0] && p >= 33) { statuses[0].classList.add('ok'); statuses[0].textContent = '[ online ]'; }
                if (statuses[1] && p >= 66) { statuses[1].classList.add('ok'); statuses[1].textContent = '[ online ]'; }
                if (statuses[2] && p >= 90) { statuses[2].classList.add('ok'); statuses[2].textContent = '[ online ]'; }
            },
            onComplete: function () {
                if (result) result.style.display = 'flex';
                if (chatBtn) chatBtn.classList.add('ready');
            }
        }
    );
}

function _updateSection(name) {
    var labels = { hero: 'quem-sou-eu', about: 'sobre', experience: 'experiencia', projects: 'projetos', contact: 'contato' };
    var el = document.getElementById('sb-section');
    if (el) el.textContent = labels[name] || name;
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
        el.textContent = now.toLocaleTimeString('pt-BR', { hour12: false });
    };
    update();
    if (_clockInterval) clearInterval(_clockInterval);
    _clockInterval = setInterval(update, 1000);
}

function _skipAnimations() {
    document.querySelectorAll('[style*="opacity:0"]').forEach(function (el) { el.style.opacity = ''; });
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
    });
}

function _initChatwoot() {
    var script = document.createElement('script');
    script.src = 'js/chatwoot.js';
    script.async = true;
    document.head.appendChild(script);
}

export function init() {
    if (typeof gsap === 'undefined') return;
    gsap.registerPlugin(ScrollTrigger);

    // Sempre roda — comportamento, não animação
    _startClock();
    _initNavMobile();
    _initContactTrigger();

    if (window.matchMedia('(prefers-reduced-motion: reduce)').matches) {
        _skipAnimations();
        _initChatwoot();
        return;
    }

    var loader = document.getElementById('loader');
    if (!loader) {
        _initHero();
        _initScrollTriggers();
        _initChatwoot();
        return;
    }

    _runLoader(loader);
}

export function setBodyClass(cls, add) {
    if (add) document.body.classList.add(cls);
    else document.body.classList.remove(cls);
}
