document.addEventListener("DOMContentLoaded", () => {

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
                    requestForm.reset();

                    successBox?.classList.remove("hidden");
                } else {
                    alert("Something went wrong. Try again.");
                }
            } catch (err) {
                alert("Network error.");
            }
        });
    }
});
