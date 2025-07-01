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
            { title: "Rol", data: "rol", width: "120px" },
            {
                title: "Estado", data: "estaActivo", render: function (data) {
                    return data
                        ? '<span class="badge bg-success">Activo</span>'
                        : '<span class="badge bg-danger">Inactivo</span>';
                }
            },
            {
                title: "Acciones", data: null, render: function (data, type, row) {
                    const estadoBtn = row.estaActivo
                        ? `<button class="dropdown-item btn-desactivar">Desactivar</button>`
                        : `<button class="dropdown-item btn-activar">Activar</button>`;

                    return `
                        <div class="btn-group dropstart">
                            <button class="btn btn-secondary dropdown-toggle" data-bs-toggle="dropdown">Acción</button>
                            <ul class="dropdown-menu">
                                <li><button class="dropdown-item btn-editar">Editar</button></li>
                                <li>${estadoBtn}</li>
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

// NUEVO USUARIO
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

// EDITAR USUARIO
$("#tbData tbody").on("click", ".btn-editar", function () {
    const fila = $(this).closest("tr");
    const data = tablaData.row(fila).data();

    idEditar = data.idUsuario;
    $("#txtNroDocumento").val(data.numeroDocumentoIdentidad);
    $("#txtNombres").val(data.nombre);
    $("#txtApellidos").val(data.apellido);
    $("#txtCorreo").val(data.correo);
    $("#txtClave").val("");
    $("#selectRol").val(data.idRolUsuario?.toString() || "3");

    $("#mdData").modal("show");
});

// GUARDAR USUARIO
$(document).on("click", "#btnGuardar", function () {
    const nroDoc = $("#txtNroDocumento").val().trim();
    const nombres = $("#txtNombres").val().trim();
    const apellidos = $("#txtApellidos").val().trim();
    const correo = $("#txtCorreo").val().trim();
    const clave = $("#txtClave").val().trim();
    const idRol = parseInt($("#selectRol").val());

    if (!nroDoc || !nombres || !apellidos || !correo || (idEditar === 0 && !clave)) {
        Swal.fire("Error", "Faltan datos obligatorios.", "warning");
        return;
    }

    if (!idRol || isNaN(idRol) || idRol <= 0) {
        Swal.fire("Error", "Debe seleccionar un rol válido.", "warning");
        return;
    }

    const esNuevo = idEditar === 0;
    const objeto = esNuevo
        ? {
            numeroDocumentoIdentidad: nroDoc,
            nombre: nombres,
            apellido: apellidos,
            correo: correo,
            clave: clave,
            idRolUsuario: idRol
        }
        : {
            idUsuario: idEditar,
            numeroDocumentoIdentidad: nroDoc,
            nombre: nombres,
            apellido: apellidos,
            correo: correo,
            clave: clave,
            idRolUsuario: idRol
        };

    const metodo = esNuevo ? "POST" : "PUT";
    const url = `/${controlador}/${esNuevo ? "Guardar" : "Editar"}`;

    fetch(url, {
        method: metodo,
        headers: { "Content-Type": "application/json;charset=utf-8" },
        body: JSON.stringify(objeto)
    })
        .then(res => {
            if (!res.ok) return res.json().then(err => Promise.reject(err));
            return res.json();
        })
        .then(res => {
            if (res.data) {
                Swal.fire("Éxito", "Usuario guardado correctamente.", "success");
                $("#mdData").modal("hide");
                tablaData.ajax.reload();
                idEditar = 0;
            } else {
                Swal.fire("Error", res.error || "No se pudo guardar.", "error");
            }
        })
        .catch(err => {
            console.error("Error en la petición:", err);
            Swal.fire("Error", err.error || "Ocurrió un error inesperado.", "error");
        });
});

// ACTIVAR USUARIO
$("#tbData tbody").on("click", ".btn-activar", function () {
    const fila = $(this).closest("tr");
    const data = tablaData.row(fila).data();

    Swal.fire({
        title: "¿Activar usuario?",
        text: `¿Deseas activar a ${data.nombre} ${data.apellido}?`,
        icon: "question",
        showCancelButton: true,
        confirmButtonText: "Sí, activar",
        cancelButtonText: "Cancelar"
    }).then(result => {
        if (result.isConfirmed) {
            fetch(`/${controlador}/CambiarEstado?Id=${data.idUsuario}&activar=true`, {
                method: "PUT"
            }).then(res => res.json())
                .then(res => {
                    if (res.data) {
                        Swal.fire("Activado", "El usuario ha sido activado.", "success");
                        tablaData.ajax.reload();
                    } else {
                        Swal.fire("Error", "No se pudo activar.", "error");
                    }
                });
        }
    });
});

// DESACTIVAR USUARIO
$("#tbData tbody").on("click", ".btn-desactivar", function () {
    const fila = $(this).closest("tr");
    const data = tablaData.row(fila).data();

    Swal.fire({
        title: "¿Desactivar usuario?",
        text: `¿Deseas desactivar a ${data.nombre} ${data.apellido}?`,
        icon: "warning",
        showCancelButton: true,
        confirmButtonText: "Sí, desactivar",
        cancelButtonText: "Cancelar"
    }).then(result => {
        if (result.isConfirmed) {
            fetch(`/${controlador}/CambiarEstado?Id=${data.idUsuario}&activar=false`, {
                method: "PUT"
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
