document.addEventListener("DOMContentLoaded", function () {
    const canvas = document.getElementById("factSalesChart");
    const data = window.factSalesTrend;

    if (!canvas || !data || !Array.isArray(data) || data.length === 0) {
        return;
    }

    const ctx = canvas.getContext("2d");

    const labels = data.map(d => d.label);
    const values = data.map(d => d.value || 0);

    const gradient = ctx.createLinearGradient(0, 0, canvas.width || 400, 0);
    gradient.addColorStop(0, "#6366F1");
    gradient.addColorStop(1, "#A855F7");

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
                    label: "Sales Amount",
                    data: values,
                    borderColor: gradient,
                    backgroundColor: "rgba(79, 70, 229, 0.18)",
                    fill: true,
                    borderWidth: 3,
                    pointRadius: values.length > 1 ? 3 : 5,
                    pointHoverRadius: 5,
                    pointBackgroundColor: gradient,
                    tension: 0.4
                }
            ]
        },
        options: {
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false },
                tooltip: {
                    backgroundColor: "#020617",
                    titleColor: "#E5E7EB",
                    bodyColor: "#E5E7EB",
                    borderColor: "#1F2937",
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
                        color: "#9CA3AF"
                    }
                },
                y: {
                    grid: {
                        color: "rgba(31,41,55,0.7)",
                        drawBorder: false
                    },
                    ticks: {
                        color: "#9CA3AF"
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
