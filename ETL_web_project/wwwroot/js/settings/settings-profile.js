document.addEventListener("DOMContentLoaded", () => {
    const saveBtn = document.querySelector(".js-profile-save");
    const modal = document.getElementById("profileConfirmModal");
    const passwordInput = document.getElementById("profilePasswordInput");
    const confirmHidden = document.getElementById("confirmPasswordHidden");
    const cancelBtn = document.getElementById("profileCancel");
    const confirmBtn = document.getElementById("profileConfirm");
    const profileForm = document.getElementById("profile-form");

    if (!saveBtn || !modal || !passwordInput || !confirmHidden || !profileForm) return;

    const openModal = () => {
        passwordInput.value = "";
        modal.classList.remove("hidden");
        passwordInput.focus();
    };

    const closeModal = () => {
        modal.classList.add("hidden");
    };

    saveBtn.addEventListener("click", (e) => {
        e.preventDefault();
        openModal();
    });

    cancelBtn?.addEventListener("click", () => closeModal());

    confirmBtn?.addEventListener("click", () => {
        const pwd = passwordInput.value.trim();
        if (!pwd) {
            alert("Please enter your password.");
            return;
        }
        confirmHidden.value = pwd;
        closeModal();
        profileForm.submit();
    });

    passwordInput.addEventListener("keydown", (e) => {
        if (e.key === "Enter") {
            e.preventDefault();
            confirmBtn.click();
        }
    });

    modal.addEventListener("click", (e) => {
        if (e.target === modal) closeModal();
    });

    window.addEventListener("keydown", (e) => {
        if (e.key === "Escape") closeModal();
    });
});
