axios.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

function mostrarNotificacion(mensaje, estatus) {
    UIkit.notification({
        message: mensaje,
        status: estatus,
        pos: 'top-center',
        timeout: 5000
    });
}