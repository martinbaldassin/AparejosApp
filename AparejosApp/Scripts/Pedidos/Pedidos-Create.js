var productoSeleccionado = {};

function ValidateFormPedido() {
    var formPedido = document.getElementById('form-pedido');
    var formPedidoJS = $("#form-pedido");

    $.validator.addMethod("fechaValida", function (valor, elemento, parametro) {
        return valor.match(/^([0-3]?[1-9]{1})\/([0-1]?[1-9]{1})\/(\d{4})$/g);
    });

    $.validator.addMethod("numeroEntero", function (valor, elemento, parametro) {
        return valor.match(/^[0-9]+$/g);
    });

    $("#form-pedido").validate({
        rules: {
            FechaEstimadaEntrega: {
                required: true,
                fechaValida: true,
            },
            ClienteID: {
                required: true,
            },
            ProductoID: {
                required: true,
            },
            TipoCarroID: {
                required: true,
            },
            EstadoPagoPedidoID: {
                required: true,
            },
            TipoPedidoID: {
                required: true,
            },
            Cantidad: {
                required: true,
                numeroEntero: true,
            }
        },
        messages: {
            FechaEstimadaEntrega: {
                required: "Ingresar una fecha de entrega estimada.",
                fechaValida: "Este campo solo acepta fechas con el formato dd/mm/aaaa.",
            },
            ClienteID: {
                required: "Ingrese un cliente asociado al pedido.",
            },
            ProductoID: {
                required: "Ingrese un producto para realizar el pedido.",
            },
            TipoCarroID: {
                required: "Ingrese el tipo de carro del producto.",
            },
            EstadoPagoPedidoID: {
                required: "Ingrese el estado de pago del pedido.",
            },
            TipoPedidoID: {
                required: "Ingrese el tipo de pedido a realizar.",
            },
            Cantidad: {
                required: "Ingrese una cantidad para el producto a fabricar.",
                numeroEntero: "Este campo solo acepta valores numéricos enteros.",
            }
        },
        errorPlacement: function (error, element) {
            if (element.attr("name") == "FechaEstimadaEntrega") {
                error.insertAfter($(element).closest(".input-group"));
            } else {
                error.insertAfter(element);
            }
        },
        submitHandler: function (form) {
            if ($(form).valid()) {
                $(form).ajaxSubmit();
            }
           
        }

        
    });
}



$(document).ready(function () {

    ValidateFormPedido();

    if ($("select[name=ProductoID]").val() != "") {
        debugger;
        productoSeleccionado = initialJsonData.ListProductos.filter(function (producto) {
            return producto.ID == $("select[name=ProductoID]").val();
        })[0];
    }

    //Para la vista Editar:
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
        debugger;
        var cantidad = parseInt(eventArgs.target.value);
        if (productoSeleccionado) {
            subtotal = cantidad * productoSeleccionado.Precio;
        }
        $("input[name=TotalPrecio]").val(subtotal.toString().replace(".",","));
    });


});