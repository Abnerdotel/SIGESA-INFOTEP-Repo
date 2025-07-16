$(document).ready(function () {
    $('#btnFiltrar').click(filtrarBitacora);
});

function filtrarBitacora() {
    const modulo = $('#filtroModulo').val();
    const usuario = $('#filtroUsuario').val();
    const desde = $('#filtroDesde').val();
    const hasta = $('#filtroHasta').val();

    $.get('/Bitacora/Lista', { modulo, usuario, desde, hasta }, function (res) {
        const tabla = $('#tablaBitacora tbody');
        tabla.empty();
        res.data.forEach(e => {
            tabla.append(`<tr>
                <td>${e.modulo}</td>
                <td>${e.accion}</td>
                <td>${e.detalle}</td>
                <td>${e.usuario}</td>
                <td>${e.fechaAccion}</td>
            </tr>`);
        });
    });
}