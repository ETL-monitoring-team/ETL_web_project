document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("roleConfirmModal");
    const inputPassword = document.getElementById("adminPasswordInput");
    const btnCancel = document.getElementById("modalCancel");
    const btnConfirm = document.getElementById("modalConfirm");

    let currentRoleForm = null;

    document.querySelectorAll(".js-role-save").forEach(btn => {
        btn.addEventListener("click", () => {
            currentRoleForm = btn.closest("form");
            if (!currentRoleForm) return;

            inputPassword.value = "";

            modal.classList.remove("hidden");
            inputPassword.focus();
        });
    });

    btnCancel.addEventListener("click", () => {
        modal.classList.add("hidden");
        currentRoleForm = null;
    });

    btnConfirm.addEventListener("click", () => {
        if (!currentRoleForm) return;

        const pwd = inputPassword.value.trim();
        if (!pwd) {
            inputPassword.focus();
            return;
        }

        let hidden = currentRoleForm.querySelector("input[name='AdminPassword']");
        if (!hidden) {
            hidden = document.createElement("input");
            hidden.type = "hidden";
            hidden.name = "AdminPassword";
            currentRoleForm.appendChild(hidden);
        }

        hidden.value = pwd;

        modal.classList.add("hidden");
        currentRoleForm.submit();
        currentRoleForm = null;
    });

    inputPassword.addEventListener("keydown", (e) => {
        if (e.key === "Enter") {
            e.preventDefault();
            btnConfirm.click();
        }
    });

    document.addEventListener("keydown", (e) => {
        if (e.key === "Escape" && !modal.classList.contains("hidden")) {
            modal.classList.add("hidden");
            currentRoleForm = null;
        }
    });

    modal.addEventListener("click", (e) => {
        if (e.target === modal) {
            modal.classList.add("hidden");
            currentRoleForm = null;
        }
    });
});
