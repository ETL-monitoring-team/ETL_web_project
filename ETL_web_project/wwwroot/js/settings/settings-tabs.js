document.addEventListener("DOMContentLoaded", () => {
    // TAB LOGIC
    const buttons = document.querySelectorAll(".tab-btn");
    const tabs = document.querySelectorAll(".tab-content");

    buttons.forEach(btn => {
        btn.addEventListener("click", () => {
            buttons.forEach(b => b.classList.remove("active"));
            tabs.forEach(t => t.classList.remove("active"));

            btn.classList.add("active");
            const target = document.getElementById(btn.dataset.tab);
            if (target) {
                target.classList.add("active");
            }
        });
    });

    // REQUEST FORM - Web3Forms AJAX submit
    const requestForm = document.getElementById("request-form");
    const successBox = document.getElementById("request-success");

    if (requestForm) {
        requestForm.addEventListener("submit", async (e) => {
            e.preventDefault();

            const formData = new FormData(requestForm);

            try {
                const response = await fetch(requestForm.action, {
                    method: "POST",
                    body: formData
                });

                if (response.ok) {
                    // formu temizle
                    requestForm.reset();

                    // name & email readonly olduğu için tekrar doldur
                    const nameInput = document.getElementById("req-name");
                    const emailInput = document.getElementById("req-email");
                    if (nameInput && nameInput.dataset.initial) {
                        nameInput.value = nameInput.dataset.initial;
                    }
                    if (emailInput && emailInput.dataset.initial) {
                        emailInput.value = emailInput.dataset.initial;
                    }

                    if (successBox) {
                        successBox.classList.remove("hidden");
                    }
                } else {
                    alert("Something went wrong while sending your request. Please try again.");
                }
            } catch (err) {
                console.error(err);
                alert("Network error while sending your request.");
            }
        });
    }
});
