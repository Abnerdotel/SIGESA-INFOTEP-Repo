$(document).ready(function () {
    $('#btnGenerarReporte').click(function () {
        const tipo = $('#tipoReporte').val();
        const desde = $('#fechaInicio').val();
        const hasta = $('#fechaFin').val();

        window.open(`/Reporte/Generar?tipo=${tipo}&desde=${desde}&hasta=${hasta}`, '_blank');
    });
});