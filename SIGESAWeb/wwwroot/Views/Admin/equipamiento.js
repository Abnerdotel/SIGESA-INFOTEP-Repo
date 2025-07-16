let tablaEquipamiento;
let idEditar = 0;

$(document).ready(function () {
    inicializarTabla();

    $('#btnGuardarEquipo').click(function () {
        $('#formEquipo').submit();
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
        const existeDuplicado = tablaEquipamiento
            .data()
            .toArray()
            .some(item => item.nombre.toLowerCase().trim() === nombre.toLowerCase());

        // Si estamos editando, permitir el mismo nombre si es el mismo registro
        if (existeDuplicado && idEditar === 0) {
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
            .then(r => r.json())
            .then(r => {
                if (r.data) {
                    Swal.fire("Éxito", `Equipamiento ${idEditar === 0 ? "creado" : "actualizado"} correctamente.`, "success");
                    $('#modalEquipamiento').modal('hide');
                    tablaEquipamiento.ajax.reload();
                    idEditar = 0;
                    // Limpiar campos después de guardar
                    $('#nombreEquipo').val('');
                    $('#descripcionEquipo').val('');
                } else {
                    Swal.fire("Error", r.error || "No se pudo guardar.", "error");
                }
            })
            .catch(err => {
                console.error(err);
                Swal.fire("Error", "Ocurrió un error inesperado.", "error");
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
                data: null, orderable: false, searchable: false,
                render: data => `
                    <div class="btn-group">
                        <button class="btn btn-sm btn-warning btn-editar"><i class="fas fa-edit"></i></button>
                        <button class="btn btn-sm btn-danger btn-eliminar"><i class="fas fa-trash"></i></button>
                    </div>`
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
}
