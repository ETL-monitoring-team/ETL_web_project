// ---------------------------
// Fullscreen Toggle
// ---------------------------
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

// ---------------------------
// Notification Dropdown & Count
// ---------------------------
document.addEventListener('DOMContentLoaded', function () {
    const notificationBtn = document.getElementById('notificationBtn');
    const notificationDropdown = document.getElementById('notificationDropdown');
    const countSpan = document.querySelector('.notification-count');

    // Dinamik bildirim sayısını güncelle
    function updateNotificationCount() {
        const notifications = document.querySelectorAll('.notification-dropdown .notification-item');
        let count = notifications.length;

        if (count > 999) countSpan.textContent = '+999';
        else if (count > 99) countSpan.textContent = '+99';
        else countSpan.textContent = count;
    }

    updateNotificationCount();

    // Butona tıklanınca aç/kapa
    notificationBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        notificationDropdown.style.display = notificationDropdown.style.display === 'block' ? 'none' : 'block';
    });

    // Sayfanın başka bir yerine tıklanınca dropdown kapanır
    document.addEventListener('click', function () {
        notificationDropdown.style.display = 'none';
    });

    // Dropdown tıklandığında kapanmayı engelle
    notificationDropdown.addEventListener('click', function (e) {
        e.stopPropagation();
    });
});
