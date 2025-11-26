// wwwroot/js/etl/factsChart.js

(function () {
    if (!window.factTrendData || !Array.isArray(window.factTrendData)) {
        return;
    }

    const ctx = document.getElementById("factTrendChart");
    if (!ctx) return;

    const labels = window.factTrendData.map(p => {
        const d = new Date(p.date);
        // backend'de ISO geliyor
        if (!isNaN(d.getTime())) {
            return d.toLocaleDateString();
        }
        // string ise direkt
        return p.date;
    });

    const dataValues = window.factTrendData.map(p => p.totalAmount);

    // Chart.js global yüklü varsayıyoruz (dashboard'da olduğu gibi)
    // eslint-disable-next-line no-undef
    new Chart(ctx, {
        type: "line",
        data: {
            labels: labels,
            datasets: [{
                label: "Sales Amount",
                data: dataValues,
                tension: 0.35,
                borderWidth: 2,
                fill: true
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: { display: false }
            },
            scales: {
                x: {
                    ticks: {
                        color: "#9ca3c7",
                        maxRotation: 0,
                        autoSkip: true
                    },
                    grid: { display: false }
                },
                y: {
                    ticks: {
                        color: "#9ca3c7"
                    },
                    grid: {
                        color: "rgba(148,163,184,0.15)"
                    }
                }
            }
        }
    });
})();
