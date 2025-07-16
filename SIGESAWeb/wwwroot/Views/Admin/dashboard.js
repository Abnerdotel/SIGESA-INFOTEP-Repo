document.addEventListener('DOMContentLoaded', () => {
    cargarMetricas();
});

function cargarMetricas() {
    fetch('/AdminDashboard/Metricas')
        .then(response => response.json())
        .then(data => {
            document.getElementById('totalReservas').innerText = data.totalReservas;
            document.getElementById('espaciosActivos').innerText = data.espaciosActivos;
            document.getElementById('reservasCanceladas').innerText = data.reservasCanceladas;
            document.getElementById('incidencias').innerText = data.incidenciasReportadas;
        })
        .catch(error => {
            console.error("Error al cargar métricas:", error);
            Swal.fire("Error", "No se pudieron cargar las métricas", "error");
        });
}