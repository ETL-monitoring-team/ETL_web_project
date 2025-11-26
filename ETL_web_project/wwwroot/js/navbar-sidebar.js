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
// Notification Dropdown
// ---------------------------
document.addEventListener('DOMContentLoaded', function () {
    const notificationBtn = document.getElementById('notificationBtn');
    const notificationDropdown = document.getElementById('notificationDropdown');
    const countSpan = document.querySelector('.notification-count');

    function updateNotificationCount() {
        const notifications = document.querySelectorAll('.notification-dropdown .notification-item');
        let count = notifications.length;

        if (count > 999) countSpan.textContent = '+999';
        else if (count > 99) countSpan.textContent = '+99';
        else countSpan.textContent = count;
    }

    updateNotificationCount();

    notificationBtn.addEventListener('click', function (e) {
        e.stopPropagation();
        notificationDropdown.style.display =
            notificationDropdown.style.display === 'block' ? 'none' : 'block';
    });

    document.addEventListener('click', function () {
        notificationDropdown.style.display = 'none';
    });

    notificationDropdown.addEventListener('click', function (e) {
        e.stopPropagation();
    });

    // ---------------------------
    // Sidebar Search
    // ---------------------------
    const searchInput = document.querySelector(".search-input");
    const menuItems = document.querySelectorAll(".sidebar-links li");

    searchInput.addEventListener("keyup", function () {
        const filter = searchInput.value.toLowerCase();

        menuItems.forEach(item => {
            if (item.querySelector("h4")) return;

            const link = item.querySelector("a");
            const text = link ? link.textContent.toLowerCase().trim() : "";

            if (text.includes(filter)) item.style.display = "block";
            else item.style.display = "none";
        });
    });

});
