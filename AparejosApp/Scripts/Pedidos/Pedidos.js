var productoSeleccionado = {};

function ValidateFormPedido() {
    var formPedido = document.getElementById('form-pedido');
    var formPedidoJS = $("#form-pedido");
    //$(formPedidoJS).validate({
    //    rules: {
    //        FechaEstimadaEntrega: {
    //            date: true,
    //        }
    //    },
    //    messages: {
    //        FechaEstimadaEntrega: {
    //            date: '',
    //        }
    //    }
        
    //});
}



$(document).ready(function () {
    if (document.getElementById('FechaEstimadaHidden')) {
        var fechaEstimadaAEditar = $("#FechaEstimadaHidden").val();
        $("input[name=FechaEstimadaEntrega]").val(fechaEstimadaAEditar);
    }


    
    $(".input-group.date").datepicker({
        language: "es",
        format: "dd/mm/yyyy",
        autoClose: true,
        startDate: new Date()
    });

    $("select[name=ProductoID]").on('change', function (eventArgs) {
        productoSeleccionado = initialJsonData.ListProductos.filter(function (producto) {
            return producto.ID == eventArgs.target.value;
        })[0];

        if ($("input[name=Cantidad]").val() != '') {
            var subtotal = 0;
            var cantidad = parseInt($("input[name=Cantidad]").val());
            subtotal = cantidad * productoSeleccionado.Precio;
            $("input[name=TotalPrecio]").val(subtotal.toString().replace(".", ","));
        }
    });

    $("input[name=Cantidad]").on('change', function (eventArgs) {
        var subtotal = 0;
        var cantidad = parseInt(eventArgs.target.value);
        if (productoSeleccionado) {
            subtotal = cantidad * productoSeleccionado.Precio;
        }
        $("input[name=TotalPrecio]").val(subtotal.toString().replace(".",","));
    });
});