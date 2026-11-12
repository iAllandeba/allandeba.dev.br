import { trackEvent, setSiteVersion } from './analytics.js';

var _seenSections = new Set();
var _visibleTitles = new Set();
var _exposureStarts = new Map();
var _observer;
var _frameId = null;

export function initPortfolioTracking(version) {
    disposePortfolioTracking();
    setSiteVersion(version);
    document.addEventListener('click', _onProjectClick);
    document.addEventListener('visibilitychange', _onVisibilityChange);
    if (typeof IntersectionObserver === 'undefined') return;
    _observer = new IntersectionObserver(entries => {
        for (const entry of entries) {
            var sectionId = entry.target.closest('section').id;
            if (entry.isIntersecting && entry.intersectionRatio >= 0.5 && !_seenSections.has(sectionId)) {
                _visibleTitles.add(entry.target);
            } else {
                _visibleTitles.delete(entry.target);
                _exposureStarts.delete(entry.target);
            }
        }
        _startSampling();
    }, { threshold: [0, 0.5] });
    for (const id of ['about', 'experience', 'projects', 'contact']) {
        const title = document.querySelector('#' + id + ' .prompt-line');
        if (title) _observer.observe(title);
    }
}

export function trackProjectsLoad(result, count, durationMs) {
    trackEvent('projects_load', {
        result,
        project_count: count,
        duration_ms: durationMs
    });
}

export function disposePortfolioTracking() {
    _observer?.disconnect();
    _observer = undefined;
    _stopSampling();
    _visibleTitles.clear();
    document.removeEventListener('click', _onProjectClick);
    document.removeEventListener('visibilitychange', _onVisibilityChange);
}

function _isExposed(title) {
    var rect = title.getBoundingClientRect();
    var visibleHeight = Math.min(rect.bottom, innerHeight) - Math.max(rect.top, 0);
    var loader = document.getElementById('loader');
    var loaderCoversPage = loader && getComputedStyle(loader).display !== 'none' &&
        loader.getBoundingClientRect().bottom > 0;
    return title.isConnected && document.visibilityState === 'visible' && !loaderCoversPage &&
        rect.height > 0 && visibleHeight / rect.height >= 0.5 &&
        getComputedStyle(title).visibility !== 'hidden' &&
        Number(getComputedStyle(title).opacity) >= 0.5;
}

function _startSampling() {
    if (_frameId !== null || !_visibleTitles.size || document.visibilityState !== 'visible') return;
    _frameId = requestAnimationFrame(_sampleViews);
}

function _stopSampling() {
    if (_frameId !== null) cancelAnimationFrame(_frameId);
    _frameId = null;
    _exposureStarts.clear();
}

function _sampleViews(timestamp) {
    _frameId = null;
    if (document.visibilityState !== 'visible') {
        _stopSampling();
        return;
    }
    for (const title of _visibleTitles) {
        if (!_isExposed(title)) {
            _exposureStarts.delete(title);
            continue;
        }
        var started = _exposureStarts.get(title);
        if (started === undefined) {
            _exposureStarts.set(title, timestamp);
        } else if (timestamp - started >= 2000) {
            var sectionId = title.closest('section').id;
            _seenSections.add(sectionId);
            _visibleTitles.delete(title);
            _exposureStarts.delete(title);
            _observer?.unobserve(title);
            trackEvent('section_view', { section_id: sectionId });
        }
    }
    _startSampling();
}

function _onVisibilityChange() {
    _stopSampling();
    _startSampling();
}

function _onProjectClick(event) {
    var link = event.target.closest?.('a[data-project-destination]');
    var card = link?.closest('[data-project-id]');
    if (!card || event.defaultPrevented) return;
    trackEvent('select_content', {
        content_type: 'project',
        content_id: card.dataset.projectId,
        destination_type: link.dataset.projectDestination,
        project_position: Number(card.dataset.projectPosition)
    });
}
