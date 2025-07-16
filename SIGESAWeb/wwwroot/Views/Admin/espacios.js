let tablaEspacios;
let idEditar = 0;
const controlador = "Espacio";

// Inicializacion
$(document).ready(function () {
    tablaEspacios = inicializarTabla();
    inicializarSelect2();
    configurarEventos();
});

function inicializarTabla() {
    return $('#tbEspacios').DataTable({
        responsive: true,
        scrollX: true,
        ajax: {
            url: `/${controlador}/Lista`,
            type: "GET",
            datatype: "json",
            dataSrc: "data"
        },
        columns: [
            { title: "Nombre", data: "nombre" },
            { title: "Capacidad", data: "capacidad" },
            { title: "Tipo", data: "tipo" },
            { title: "Fecha de Creación", data: "fechaCreacion" },
            {
                title: "Acciones", data: null, orderable: false, searchable: false,
                render: function (data, type, row) {
                    return `
                        <div class="btn-group dropstart">
                            <button class="btn btn-secondary dropdown-toggle" data-bs-toggle="dropdown">Acción</button>
                            <ul class="dropdown-menu">
                                <li><button class="dropdown-item btn-editar">Editar</button></li>
                                <li><button class="dropdown-item btn-eliminar">Eliminar</button></li>
                            </ul>
                        </div>`;
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}

function inicializarSelect2() {
    $("#tipoEspacio").select2({
        dropdownParent: $("#modalEspacio"),
        theme: "bootstrap-5",
        placeholder: "Seleccione tipo de espacio",
        allowClear: true
    });
}

function configurarEventos() {
    $("#btnNuevoEspacio").click(() => abrirModal());

    $("#formEspacio").on("submit", function (e) {
        e.preventDefault();
        guardarEspacio();
    });

    $('#tbEspacios tbody')
        .on('click', '.btn-editar', function () {
            const data = tablaEspacios.row($(this).closest('tr')).data();
            abrirModal(data);
        })
        .on('click', '.btn-eliminar', function () {
            const data = tablaEspacios.row($(this).closest('tr')).data();
            eliminarEspacio(data);
        });
}

function abrirModal(data = null) {
    idEditar = data?.idEspacio || 0;
    $('#formEspacio')[0].reset();
    $('#tipoEspacio').val(null).trigger('change');

    swalCargarTipos(() => {
        if (data) {
            $('#nombreEspacio').val(data.nombre);
            $('#capacidad').val(data.capacidad);
            $('#observaciones').val(data.observaciones);
            $('#tipoEspacio').val(data.idTipoEspacio).trigger("change");
        }
        $('#modalEspacio').modal('show');
    });
}

function guardarEspacio() {
    const nombre = $('#nombreEspacio').val().trim();
    const capacidad = parseInt($('#capacidad').val());
    const idTipoEspacio = parseInt($('#tipoEspacio').val());
    const observaciones = $('#observaciones').val().trim();

    if (!nombre || !capacidad || !idTipoEspacio) {
        return Swal.fire("Error", "Todos los campos obligatorios deben completarse.", "warning");
    }

    const objeto = { nombre, capacidad, observaciones, idTipoEspacio };
    if (idEditar > 0) objeto.idEspacio = idEditar;

    const url = `/${controlador}/${idEditar > 0 ? "Editar" : "Guardar"}`;
    const metodo = idEditar > 0 ? "PUT" : "POST";

    fetch(url, {
        method: metodo,
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(objeto)
    })
        .then(res => res.json())
        .then(res => {
            if (res.data) {
                Swal.fire("Éxito", `Espacio ${idEditar > 0 ? "actualizado" : "creado"} correctamente.`, "success");
                $('#modalEspacio').modal('hide');
                tablaEspacios.ajax.reload();
                idEditar = 0;
            } else {
                Swal.fire("Error", res.error || "No se pudo guardar.", "error");
            }
        })
        .catch(err => {
            console.error(err);
            Swal.fire("Error", "Ocurrió un error inesperado.", "error");
        });
}

function eliminarEspacio(data) {
    Swal.fire({
        title: "¿Eliminar espacio?",
        text: `¿Deseas eliminar \"${data.nombre}\"?`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, eliminar",
        cancelButtonText: "Cancelar"
    }).then(res => {
        if (res.isConfirmed) {
            fetch(`/${controlador}/Eliminar?id=${data.idEspacio}`, { method: "DELETE" })
                .then(r => r.json())
                .then(r => {
                    if (r.data) {
                        Swal.fire("Eliminado", "Espacio eliminado correctamente.", "success");
                        tablaEspacios.ajax.reload();
                    } else {
                        Swal.fire("Error", r.error || "No se pudo eliminar.", "error");
                    }
                });
        }
    });
}

function swalCargarTipos(callback) {
    fetch("/TipoEspacio/Lista")
        .then(r => {
            if (!r.ok) throw new Error("No se pudo cargar la lista de tipos.");
            return r.json();
        })
        .then(res => {
            const data = res.data || [];
            const sel = $("#tipoEspacio").empty().append('<option value="">Seleccione...</option>');
            data.forEach(t => sel.append(`<option value="${t.idTipoEspacio}">${t.nombre}</option>`));
            if (typeof callback === 'function') callback();
        })
        .catch(err => Swal.fire("Error", err.message, "error"));
}
