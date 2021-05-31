UIkit.util.ready(function () {
    const tabla = new Tabla("tabla-personas", "/PersonasFisicas/Personas", callbackColumnas, callbackFila);
    const modalCrear = document.getElementById("modal-crear"),
        modalEditar = document.getElementById("modal-editar");
    let currentID = 0;

    document.querySelector("#form-editar").addEventListener("submit", function (e) {
        e.preventDefault();
        update();
    });

    document.querySelector("#form-crear").addEventListener("submit", function (e) {
        e.preventDefault();
        crear();
    });

    /**
     * callback para las columnas
     * @param {HTMLTableRowElement} tr
     */
    function callbackColumnas(tr) {
        let th = document.createElement("th"),
            span = document.createElement("span");

        span.innerText = "Acciones";
        th.appendChild(span);
        tr.appendChild(th);
    }

    /**
     * callback para filas
     * @param {HTMLTableRowElement} tr
     * @param {{id: number}} data
     */
    function callbackFila(tr, data) {
        let td = document.createElement("td"),
            a = document.createElement("a"),
            a2 = document.createElement("a");
        a.href = "#";
        a.classList.add("uk-icon-link");
        a.setAttribute("uk-icon", "file-edit");
        a.addEventListener("click", function (e) {
            e.preventDefault();
            editar(data.id);            
        });
        a2.href = "#";
        a2.classList.add("uk-icon-link");
        a2.setAttribute("uk-icon", "trash");
        a2.addEventListener("click", function (e) {
            e.preventDefault();
            desactivar(data.id);
        });
        td.appendChild(a);
        td.appendChild(a2);
        tr.appendChild(td);
    }

    /**
     * Obtiene los datos y los agrega al modal
     * @param {number} id
     */
    function editar(id) {
        currentID = id;
        axios({
            url: '/PersonasFisicas/ObtenerPersona',
            method: 'post',
            data: {
                id
            }
        }).then(res => {
            setToModal(res.data.persona);
        }).catch(err => {
            console.error(err);
        });

        function setToModal(persona) {
            modalEditar.querySelector("input[name='Nombre']").value = persona["Nombre"];
            modalEditar.querySelector("input[name='ApellidoPaterno']").value = persona["ApellidoPaterno"];
            modalEditar.querySelector("input[name='ApellidoMaterno']").value = persona["ApellidoMaterno"];
            modalEditar.querySelector("input[name='RFC']").value = persona["RFC"];
            modalEditar.querySelector("input[name='FechaNacimiento']").value = moment(persona["FechaNacimiento"]).format("YYYY-MM-DD");
            UIkit.modal(modalEditar).show();
        }
    }

    function update() {
        let data = {
            Nombre: modalEditar.querySelector("input[name='Nombre']").value,
            ApellidoPaterno: modalEditar.querySelector("input[name='ApellidoPaterno']").value,
            ApellidoMaterno: modalEditar.querySelector("input[name='ApellidoMaterno']").value,
            RFC: modalEditar.querySelector("input[name='RFC']").value,
            FechaNacimiento: modalEditar.querySelector("input[name='FechaNacimiento']").value,
            Id: currentID
        }
        axios({
            url: '/PersonasFisicas/Actualizar',
            method: 'put',
            data: data
        }).then(res => {
            let estatus = res.data.code === 200 ? "success" : "danger";
            mostrarNotificacion(res.data.msj, estatus);

            if (estatus === "success")
                setTimeout(function () { location.reload(); }, 1500);
        }).catch(err => {
            console.error(err);
        });
    }

    function crear() {
        let data = {
            Nombre: modalCrear.querySelector("input[name='Nombre']").value,
            ApellidoPaterno: modalCrear.querySelector("input[name='ApellidoPaterno']").value,
            ApellidoMaterno: modalCrear.querySelector("input[name='ApellidoMaterno']").value,
            RFC: modalCrear.querySelector("input[name='RFC']").value,
            FechaNacimiento: modalCrear.querySelector("input[name='FechaNacimiento']").value
        }
        axios({
            url: '/PersonasFisicas/Crear',
            method: 'post',
            data: data
        }).then(res => {
            let estatus = res.data.code === 200 ? "success" : "danger";
            mostrarNotificacion(res.data.msj, estatus);

            if (estatus === "success")
                setTimeout(function () { location.reload(); }, 1500);
        }).catch(err => {
            console.error(err);
        });
    }

    function desactivar(id) {
        axios({
            url: '/PersonasFisicas/Desactivar',
            method: 'delete',
            data: {
                id: id
            }
        }).then(res => {
            let estatus = res.data.code === 200 ? "success" : "danger";
            mostrarNotificacion(res.data.msj, estatus);

            if (estatus === "success")
                setTimeout(function () { location.reload(); }, 1500);
        }).catch(err => {
            console.error(err);
        });
    }
});