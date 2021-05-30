class Tabla {
    id = "";
    data = null;    
    filas = [];
    columnas = [];
    coleccion = [];
    filasPagina = 5;
    paginaActual = 1;
    totalPaginas = 0;
    tabla = null;
    tbody = null;
    thead = null;
    paginacion = null;
    callbackColumnas = function () { };
    callbackFilas = function () { };

    constructor(id, data, callbackColumnas = function () { }, callbackFilas = function () { }) {
        this.id = id;
        this.data = data;
        this.callbackColumnas = callbackColumnas;
        this.callbackFilas = callbackFilas;

        this.tabla = document.getElementById(id);
        this.thead = this.tabla.querySelector("thead");
        this.tbody = this.tabla.querySelector("tbody");
        this.paginacion = document.getElementById(id + "-paginacion");

        if (typeof data === "string")
            this.obtenerDatos();
    }

    obtenerDatos() {
        axios({
            url: this.data,
            method: 'post'
        }).then(res => {
            this.init(res.data.tabla);
        }).catch(err => {
            console.error(err);
        })
    }

    init(tabla) {
        this.columnas = tabla["Columnas"];
        this.filas = tabla["Filas"];
        this.filasPagina = tabla["FilasPagina"];

        this.thead.innerHTML = "";
        let tr = this.thead.insertRow(0);

        this.columnas.forEach((columna, index) => {
            let th = document.createElement("th"),
                span = document.createElement("span");
            span.innerText = columna["Nombre"];
            th.appendChild(span);
            tr.appendChild(th);
        });

        this.callbackColumnas(tr);
        this.update();
    }

    update() {
        this.coleccion = this.filas.filter(f => f["Mostrar"]);
        this.tbody.innerHTML = "";
        this.paginacion.innerHTML = "";

        let filasColeccion = this.coleccion.length;
        this.totalPaginas = Math.ceil(filasColeccion / this.filasPagina) > 0 ? Math.ceil(filasColeccion / this.filasPagina) : 1;
        let index = (this.filasPagina * this.paginaActual) - this.filasPagina;
        this.coleccion = this.coleccion.slice(index, index + this.filasPagina);

        this.coleccion.forEach(fila => {
            let tr = document.createElement("tr");
            fila["Celdas"].forEach(celda => {
                let td = document.createElement("td"),
                    span = document.createElement("span");

                span.innerText = celda["Valor"];
                td.appendChild(span);
                tr.appendChild(td);
            });

            this.tbody.appendChild(tr);
            this.callbackFilas(tr, fila["Propiedades"]);
        });

        // crear paginación
        let liPrev = document.createElement("li"),
            aPrev = document.createElement("a"),
            spanPrev = document.createElement("span");
        spanPrev.setAttribute("uk-pagination-previous", "");
        aPrev.appendChild(spanPrev);
        liPrev.appendChild(aPrev);
        aPrev.addEventListener("click", function (e) {
            e.preventDefault();
            let nuevaPagina = this.paginaActual - 1;
            if (nuevaPagina > 0) {
                this.paginaActual = nuevaPagina;
                this.update();
            }
        }.bind(this));
        this.paginacion.appendChild(liPrev);
        
        let min = this.paginaActual - 2,
            max = this.paginaActual + 2;

        if (min < 1) {
            min = 1;
        }

        if (max > this.totalPaginas) {
            max = this.totalPaginas;
        }

        for (let i = min; i <= max; i++) {
            let liNumero = document.createElement("li"),
                aNumero = document.createElement("a");
            aNumero.innerText = i;
            if (i == this.paginaActual) {
                liNumero.classList.add("uk-active");
            }
            aNumero.addEventListener("click", function (e) {
                e.preventDefault();
                this.paginaActual = i;
                this.update();
            }.bind(this));
            liNumero.appendChild(aNumero);
            this.paginacion.appendChild(liNumero);
        }

        let liNext = document.createElement("li"),
            aNext = document.createElement("a"),
            spanNext = document.createElement("span");
        spanNext.setAttribute("uk-pagination-next", "");
        aNext.appendChild(spanNext);
        liNext.appendChild(aNext);
        aNext.addEventListener("click", function (e) {
            e.preventDefault();
            let nuevaPagina = this.paginaActual + 1;
            if (nuevaPagina <= this.totalPaginas) {
                this.paginaActual = nuevaPagina;
                this.update();
            }
        }.bind(this));
        this.paginacion.appendChild(liNext);
    }
}