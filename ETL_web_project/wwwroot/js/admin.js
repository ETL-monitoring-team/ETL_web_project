document.addEventListener("DOMContentLoaded", () => {
    const modal = document.getElementById("roleConfirmModal");
    const inputPassword = document.getElementById("adminPasswordInput");
    const btnCancel = document.getElementById("modalCancel");
    const btnConfirm = document.getElementById("modalConfirm");

    let currentRoleForm = null;

    // Her "Save" butonuna tıklandığında popup aç
    document.querySelectorAll(".js-role-save").forEach(btn => {
        btn.addEventListener("click", () => {
            currentRoleForm = btn.closest("form");
            if (!currentRoleForm) return;

            // Şifre inputunu temizle
            inputPassword.value = "";
            // Modal'ı göster
            modal.classList.remove("hidden");
            inputPassword.focus();
        });
    });

    // Cancel: popup'ı kapat
    btnCancel.addEventListener("click", () => {
        modal.classList.add("hidden");
        currentRoleForm = null;
    });

    // Confirm: şifreyi forma ekle ve submit et
    btnConfirm.addEventListener("click", () => {
        if (!currentRoleForm) return;

        const pwd = inputPassword.value.trim();
        if (!pwd) {
            inputPassword.focus();
            return;
        }

        // Formda AdminPassword yoksa oluştur
        let hidden = currentRoleForm.querySelector("input[name='AdminPassword']");
        if (!hidden) {
            hidden = document.createElement("input");
            hidden.type = "hidden";
            hidden.name = "AdminPassword";
            currentRoleForm.appendChild(hidden);
        }

        hidden.value = pwd;

        // Modal'ı gizle ve formu submit et
        modal.classList.add("hidden");
        currentRoleForm.submit();
        currentRoleForm = null;
    });
    // ENTER ile onaylama (Save)
    inputPassword.addEventListener("keydown", (e) => {
        if (e.key === "Enter") {
            e.preventDefault(); // Formun otomatik submit etmesini engeller
            btnConfirm.click(); // Confirm butonunu tetikler
        }
    });

    // ESC ile kapatma
    document.addEventListener("keydown", (e) => {
        if (e.key === "Escape" && !modal.classList.contains("hidden")) {
            modal.classList.add("hidden");
            currentRoleForm = null;
        }
    });

    // Overlay'e tıklayınca kapatma (kartın dışı)
    modal.addEventListener("click", (e) => {
        if (e.target === modal) {
            modal.classList.add("hidden");
            currentRoleForm = null;
        }
    });
});
