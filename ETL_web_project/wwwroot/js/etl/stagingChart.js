// wwwroot/js/etl/stagingChart.js

document.addEventListener("DOMContentLoaded", function () {
    const canvas = document.getElementById("stagingLoadChart");
    const data = window.stagingLoadTrend;

    if (!canvas || !data || !Array.isArray(data)) {
        return;
    }

    const ctx = canvas.getContext("2d");

    const labels = data.map(d => d.label);
    const values = data.map(d => d.value || 0);

    if (typeof Chart === "undefined") {
        console.error("Chart.js not loaded");
        return;
    }

    const gradient = ctx.createLinearGradient(0, 0, 0, canvas.height || 220);
    gradient.addColorStop(0, "rgba(129, 140, 248, 0.95)");
    gradient.addColorStop(1, "rgba(76, 29, 149, 0.2)");

    new Chart(ctx, {
        type: "bar",
        data: {
            labels: labels,
            datasets: [{
                label: "Rows Loaded",
                data: values,
                backgroundColor: gradient,
                borderColor: "#818cf8",
                borderWidth: 1.5,
                borderRadius: 8,
                maxBarThickness: 32
            }]
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: {
                    backgroundColor: "#020617",
                    titleColor: "#e5e7eb",
                    bodyColor: "#e5e7eb",
                    borderColor: "#4b5563",
                    borderWidth: 1,
                    callbacks: {
                        label: ctx => {
                            const v = ctx.parsed.y || 0;
                            return v.toLocaleString() + " rows";
                        }
                    }
                }
            },
            scales: {
                x: {
                    grid: { display: false },
                    ticks: { color: "#9ca3af" }
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
                duration: 700,
                easing: "easeOutCubic"
            }
        }
    });
});
