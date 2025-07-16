let tablaTipos;
let idTipoEditar = 0;
const controlador = "TipoEspacio";

$(document).ready(function () {
    tablaTipos = $('#tablaTiposEspacio').DataTable({
        responsive: true,
        scrollX: true,
        ajax: {
            url: `/${controlador}/Lista`,
            type: "GET",
            datatype: "json"
        },
        columns: [
            { data: "nombre" },
            { data: "fechaCreacion" },
            {
                data: null,
                orderable: false,
                searchable: false,
                className: "text-center",
                render: function (data) {
                    return `
                        <div class="btn-group">
                            <button class="btn btn-sm btn-warning btn-editar" title="Editar"><i class="fas fa-edit"></i></button>
                            <button class="btn btn-sm btn-danger btn-eliminar" title="Eliminar"><i class="fas fa-trash"></i></button>
                        </div>`;
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });

    // Abrir modal para nuevo tipo
    $('#btnNuevoTipo').click(function () {
        idTipoEditar = 0;
        $('#nombreTipoEspacio').val('');
        $('#modalTipoEspacio').modal('show');
    });

    // Guardar (crear o editar)
    $('#btnGuardarTipo').click(function () {
        const nombre = $('#nombreTipoEspacio').val().trim();
        if (!nombre) {
            Swal.fire("Advertencia", "El nombre es obligatorio.", "warning");
            return;
        }

        const modelo = { nombre };
        if (idTipoEditar !== 0) modelo.idTipoEspacio = idTipoEditar;

        const esNuevo = idTipoEditar === 0;
        const url = `/${controlador}/${esNuevo ? "Guardar" : "Editar"}`;
        const metodo = esNuevo ? "POST" : "PUT";

        fetch(url, {
            method: metodo,
            headers: { 'Content-Type': 'application/json;charset=utf-8' },
            body: JSON.stringify(modelo)
        })
            .then(res => res.json())
            .then(res => {
                if (res.data) {
                    Swal.fire("Éxito", `Tipo ${esNuevo ? "creado" : "actualizado"} correctamente.`, "success");
                    $('#modalTipoEspacio').modal('hide');
                    tablaTipos.ajax.reload();
                } else {
                    Swal.fire("Error", res.error || "No se pudo guardar.", "error");
                }
            })
            .catch(err => {
                console.error(err);
                Swal.fire("Error", "Ocurrió un error inesperado.", "error");
            });
    });

    // Editar
    $('#tablaTiposEspacio tbody').on('click', '.btn-editar', function () {
        const data = tablaTipos.row($(this).closest('tr')).data();
        idTipoEditar = data.idTipoEspacio;
        $('#nombreTipoEspacio').val(data.nombre);
        $('#modalTipoEspacio').modal('show');
    });

    // Eliminar
    $('#tablaTiposEspacio tbody').on('click', '.btn-eliminar', function () {
        const data = tablaTipos.row($(this).closest('tr')).data();

        Swal.fire({
            title: "¿Eliminar tipo?",
            text: `Se eliminará el tipo "${data.nombre}". Esta acción no se puede deshacer.`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        }).then(result => {
            if (result.isConfirmed) {
                fetch(`/${controlador}/Eliminar?id=${data.idTipoEspacio}`, {
                    method: "DELETE"
                })
                    .then(res => res.json())
                    .then(res => {
                        if (res.data) {
                            Swal.fire("Eliminado", "Tipo eliminado correctamente.", "success");
                            tablaTipos.ajax.reload();
                        } else {
                            Swal.fire("Error", res.error || "No se pudo eliminar.", "error");
                        }
                    });
            }
        });
    });
});
