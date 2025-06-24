let tablaData;
let idEditar = 0;
const controlador = "Usuario";

document.addEventListener("DOMContentLoaded", function () {
    tablaData = $('#tbData').DataTable({
        responsive: true,
        scrollX: true,
        ajax: {
            url: `/${controlador}/Lista`,
            type: "GET",
            datatype: "json"
        },
        columns: [
            { title: "Nro Documento", data: "numeroDocumentoIdentidad" },
            { title: "Nombres", data: "nombre" },
            { title: "Apellidos", data: "apellido" },
            { title: "Correo", data: "correo" },
            { title: "Fecha Creación", data: "fechaCreacion" },
            {
                title: "Rol", data: "rol", width: "120px"
            },
            {
                title: "", data: "idUsuario", render: function (data) {
                    return `
                    <div class="btn-group dropstart">
                        <button type="button" class="btn btn-secondary dropdown-toggle" data-bs-toggle="dropdown">
                            Acción
                        </button>
                        <ul class="dropdown-menu">
                            <li><button class="dropdown-item btn-editar">Editar</button></li>
                            <li><button class="dropdown-item btn-eliminar">Desactivar</button></li>
                        </ul>
                    </div>`;
                }
            }
        ],
        language: {
            url: "https://cdn.datatables.net/plug-ins/1.11.5/i18n/es-ES.json"
        }
    });
});

$("#btnNuevo").on("click", function () {
    idEditar = 0;
    $("#txtNroDocumento").val("");
    $("#txtNombres").val("");
    $("#txtApellidos").val("");
    $("#txtCorreo").val("");
    $("#txtClave").val("");
    $("#selectRol").val("3");
    $("#mdData").modal("show");
});

$("#tbData tbody").on("click", ".btn-editar", function () {
    const fila = $(this).closest("tr");
    const data = tablaData.row(fila).data();

    idEditar = data.idUsuario;
    $("#txtNroDocumento").val(data.numeroDocumentoIdentidad);
    $("#txtNombres").val(data.nombre);
    $("#txtApellidos").val(data.apellido);
    $("#txtCorreo").val(data.correo);
    $("#txtClave").val("");
    $("#selectRol").val(data.idRolUsuario || "3");

    $("#mdData").modal("show");
});

$("#tbData tbody").on("click", ".btn-eliminar", function () {
    const fila = $(this).closest("tr");
    const data = tablaData.row(fila).data();

    Swal.fire({
        text: `¿Deseas desactivar al usuario ${data.nombre} ${data.apellido}?`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, desactivar",
        cancelButtonText: "Cancelar"
    }).then((result) => {
        if (result.isConfirmed) {
            fetch(`/${controlador}/Eliminar?Id=${data.idUsuario}`, {
                method: "DELETE"
            }).then(res => res.json())
                .then(res => {
                    if (res.data) {
                        Swal.fire("Desactivado", "El usuario ha sido desactivado.", "success");
                        tablaData.ajax.reload();
                    } else {
                        Swal.fire("Error", "No se pudo desactivar.", "error");
                    }
                });
        }
    });
});

$("#btnGuardar").on("click", function () {
    if (
        $("#txtNroDocumento").val().trim() === "" ||
        $("#txtNombres").val().trim() === "" ||
        $("#txtApellidos").val().trim() === "" ||
        $("#txtCorreo").val().trim() === "" ||
        ($("#txtClave").val().trim() === "" && idEditar === 0)
    ) {
        Swal.fire("Error", "Faltan datos obligatorios.", "warning");
        return;
    }

    const objeto = {
        IdUsuario: idEditar,
        NumeroDocumentoIdentidad: $("#txtNroDocumento").val().trim(),
        Nombre: $("#txtNombres").val().trim(),
        Apellido: $("#txtApellidos").val().trim(),
        Correo: $("#txtCorreo").val().trim(),
        Clave: $("#txtClave").val(),
        IdRolUsuario: parseInt($("#selectRol").val())
    };

    const metodo = idEditar === 0 ? "POST" : "PUT";
    const url = `/${controlador}/${idEditar === 0 ? "Guardar" : "Editar"}`;

    fetch(url, {
        method: metodo,
        headers: { "Content-Type": "application/json;charset=utf-8" },
        body: JSON.stringify(objeto)
    }).then(res => res.json())
        .then(res => {
            if (res.data) {
                Swal.fire("Éxito", "Usuario guardado correctamente.", "success");
                $("#mdData").modal("hide");
                tablaData.ajax.reload();
                idEditar = 0;
            } else {
                Swal.fire("Error", "No se pudo guardar.", "error");
            }
        });
});
