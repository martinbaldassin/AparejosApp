
function EliminarPedido(ID) {
    var oData = {};
    oData.id = ID;
    swal({
        type: 'warning',
        title: '¡Cuidado!',
        text: 'Se va a eliminar el pedido seleccionado. ¿Desea continuar?',
        confirmButtonColor: '#DD6B55',
        confirmButtonText: '¡Si!',
        cancelButtonText: "No, me equivoqué!",
        showCancelButton: true,
        showConfirmButton: true,
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: '/Pedidos/Delete/',
                data: oData,
                dataType: 'json',
                type: 'POST',
                async: false,
                cache: false,
                success: function (response) {
                    swal({
                        type: "success",
                        title: '¡Ok!',
                        text: 'Se elimino el pedido correctamente.',
                        showConfirmButton: true
                    });
                    window.location.reload();
                    
                },
                error: function (response) {
                    swal({
                        type: "error",
                        title: '¡Ocurrio un error!',
                        text: 'Reportelo a Sistemas.',
                        timer: 3000
                    });
                }
            });
        }
    });

}