
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

$(document).ready(function () {
    $("input[name=tipoFiltro]").on('click', function (eventArgs) {
        if (eventArgs.target.value == 'PorRangoFechaYTerminados') {
            $("#RangoFechas").show("slow");
        } else {
            $("#RangoFechas").hide("slow");
        }
        
    });
    $('input[name=rangoFechas]').daterangepicker({
        "showWeekNumbers": true,
        "ranges": {
            "Hoy": [
                new moment().format("DD/MM/YYYY"),
                new moment().format("DD/MM/YYYY")
            ],
            "Ayer": [
                new moment().add(-1,'d').format("DD/MM/YYYY"),
                new moment().format("DD/MM/YYYY")
            ],
            "Última Semana": [
                new moment().add(-7,'d').format("DD/MM/YYYY"),
                new moment().format("DD/MM/YYYY")
            ],
            "Este mes": [
                new moment().add(-1, 'M').format("DD/MM/YYYY"),
                new moment().format("DD/MM/YYYY")
            ],

        },
        "locale": {
            "format": "DD/MM/YYYY",
            "separator": " - ",
            "applyLabel": "Aplicar",
            "cancelLabel": "Cancelar",
            "fromLabel": "Desde",
            "toLabel": "Hasta",
            "customRangeLabel": "Personalizado",
            "weekLabel": "Sem",
            "daysOfWeek": [
                "Do",
                "Lu",
                "Ma",
                "Mie",
                "Jue",
                "Vie",
                "Sa"
            ],
            "monthNames": [
                "Enero",
                "Febrero",
                "Marzo",
                "Abril",
                "Mayo",
                "Junio",
                "Julio",
                "Agosto",
                "Septiembre",
                "Octubre",
                "Noviembre",
                "Diciembre"
            ],
            "firstDay": 1
        },
        "autoUpdateInput": true,
        "showCustomRangeLabel": true,
        "alwaysShowCalendars": false,
        "startDate": new Date(),
        "endDate": new Date(),
        "opens": "center"
    }, function (start, end, label) {
        });

    $('input[name=rangoFechas]').val("");
});