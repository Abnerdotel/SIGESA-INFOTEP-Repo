let tablaEquipamiento;
let idEditar = 0;

$(document).ready(function () {
    inicializarTabla();

    $('#btnGuardarEquipo').click(function () {
        $('#formEquipo').submit();
    });

    $('#modalEquipamiento').on('hidden.bs.modal', function () {
        limpiarFormulario();
    });

    $('#formEquipo').submit(function (e) {
        e.preventDefault();

        const nombre = $('#nombreEquipo').val().trim();
        const descripcion = $('#descripcionEquipo').val().trim();

        if (!nombre) {
            Swal.fire("Error", "El nombre es obligatorio.", "warning");
            return;
        }

        // Validar nombre duplicado ignorando mayúsculas y espacios
        const nombreNormalizado = nombre.toLowerCase();
        const existeDuplicado = tablaEquipamiento
            .data()
            .toArray()
            .some(item =>
                item.nombre.toLowerCase() === nombreNormalizado &&
                item.idEquipamiento !== idEditar
            );

        if (existeDuplicado) {
            Swal.fire("Error", "Ya existe un equipamiento con ese nombre.", "warning");
            return;
        }

        const equipo = {
            idEquipamiento: idEditar,
            nombre: nombre,
            descripcion: descripcion
        };

        const metodo = idEditar === 0 ? "POST" : "PUT";
        const url = `/Equipamiento/${idEditar === 0 ? "Guardar" : "Editar"}`;

        fetch(url, {
            method: metodo,
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(equipo)
        })
            .then(response => {
                if (!response.ok) return response.json().then(err => Promise.reject(err));
                return response.json();
            })
            .then(r => {
                if (r.data) {
                    Swal.fire("Éxito", `Equipamiento ${idEditar === 0 ? "creado" : "actualizado"} correctamente.`, "success");
                    $('#modalEquipamiento').modal('hide');
                    tablaEquipamiento.ajax.reload();
                    idEditar = 0;
                } else {
                    Swal.fire("Error", r.error || "No se pudo guardar.", "error");
                }
            })
            .catch(err => {
                console.error(err);
                Swal.fire("Error", err.error || "Ocurrió un error inesperado.", "error");
            });
    });

    $('#tablaEquipamiento tbody').on('click', '.btn-editar', function () {
        const data = tablaEquipamiento.row($(this).closest('tr')).data();
        idEditar = data.idEquipamiento;
        $('#nombreEquipo').val(data.nombre);
        $('#descripcionEquipo').val(data.descripcion);
        $('#modalEquipamiento').modal('show');
    });

    $('#tablaEquipamiento tbody').on('click', '.btn-eliminar', function () {
        const data = tablaEquipamiento.row($(this).closest('tr')).data();

        Swal.fire({
            title: "¿Eliminar equipamiento?",
            text: `¿Deseas eliminar el equipo \"${data.nombre}\"?`,
            icon: "warning",
            showCancelButton: true,
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        }).then(result => {
            if (result.isConfirmed) {
                fetch(`/Equipamiento/Eliminar?id=${data.idEquipamiento}`, { method: "DELETE" })
                    .then(r => r.json())
                    .then(r => {
                        if (r.data) {
                            Swal.fire("Eliminado", "Equipo eliminado correctamente.", "success");
                            tablaEquipamiento.ajax.reload();
                        } else {
                            Swal.fire("Error", r.error || "No se pudo eliminar.", "error");
                        }
                    });
            }
        });
    });
});

function inicializarTabla() {
    tablaEquipamiento = $('#tablaEquipamiento').DataTable({
        responsive: true,
        scrollX: true,
        ajax: {
            url: '/Equipamiento/Lista',
            type: 'GET',
            datatype: 'json'
        },
        columns: [
            { data: 'nombre' },
            { data: 'descripcion' },
            { data: 'fechaCreacion' },      
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

function limpiarFormulario() {
    idEditar = 0;
    $('#formEquipo')[0].reset();
    $('#nombreEquipo').val('');
    $('#descripcionEquipo').val('');
}
