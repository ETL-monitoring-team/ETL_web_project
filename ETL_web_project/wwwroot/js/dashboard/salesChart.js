document.addEventListener("DOMContentLoaded", function () {
    const canvas = document.getElementById("salesChart");
    const data = window.dashboardDailySales;

    if (!canvas || !data || !Array.isArray(data)) {
        return;
    }

    const ctx = canvas.getContext("2d");

    const labels = data.map(d => d.label);
    const values = data.map(d => d.value || 0);

    const gradient = ctx.createLinearGradient(0, 0, canvas.width || 400, 0);
    gradient.addColorStop(0, "#6B8DE3");
    gradient.addColorStop(1, "#7D1C8D");

    if (typeof Chart === "undefined") {
        console.error("Chart.js not loaded");
        return;
    }

    new Chart(ctx, {
        type: "line",
        data: {
            labels: labels,
            datasets: [
                {
                    label: "Revenue",
                    data: values,
                    borderColor: gradient,
                    backgroundColor: "rgba(123, 97, 255, 0.18)",
                    fill: true,
                    borderWidth: 3,
                    pointRadius: values.length > 1 ? 3 : 5,
                    pointHoverRadius: 5,
                    pointBackgroundColor: gradient,
                    tension: 0.45
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: {
                    backgroundColor: "#111827",
                    titleColor: "#e5e7eb",
                    bodyColor: "#e5e7eb",
                    borderColor: "#4b5563",
                    borderWidth: 1,
                    callbacks: {
                        label: ctx => {
                            const v = ctx.parsed.y || 0;
                            return "₺" + v.toFixed(2);
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: {
                        display: false
                    },
                    ticks: {
                        color: "#9ca3af"
                    }
                },
                y: {
                    grid: {
                        color: "rgba(55,65,81,0.6)",
                        drawBorder: false
                    },
                    ticks: {
                        color: "#9ca3af"
                    }
                }
            },
            animation: {
                duration: 800,
                easing: "easeOutCubic"
            }
        }
    });
});
