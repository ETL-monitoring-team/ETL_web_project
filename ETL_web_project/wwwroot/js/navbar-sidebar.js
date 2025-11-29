const toggleBtn = document.querySelector('.js-toggle-fullscreen-btn');

if (document.fullscreenEnabled || document.webkitFullscreenEnabled) {
    toggleBtn.hidden = false;

    toggleBtn.addEventListener('click', function () {
        if (document.fullscreenElement || document.webkitFullscreenElement) {
            if (document.exitFullscreen) document.exitFullscreen();
            else if (document.webkitCancelFullScreen) document.webkitCancelFullScreen();
        } else {
            if (document.documentElement.requestFullscreen) document.documentElement.requestFullscreen();
            else if (document.documentElement.webkitRequestFullScreen) document.documentElement.webkitRequestFullScreen();
        }
    });

    document.addEventListener('fullscreenchange', handleFullscreen);
    document.addEventListener('webkitfullscreenchange', handleFullscreen);

    function handleFullscreen() {
        if (document.fullscreenElement || document.webkitFullscreenElement) {
            toggleBtn.classList.add('on');
            toggleBtn.setAttribute('aria-label', 'Exit fullscreen mode');
        } else {
            toggleBtn.classList.remove('on');
            toggleBtn.setAttribute('aria-label', 'Enter fullscreen mode');
        }
    }
}