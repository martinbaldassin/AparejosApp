
function Eliminar(ID) {
    var oData = {};
    oData.idVendedor = ID;
    var seBorro = false;
    swal({
        type: 'warning',
        title: '¡Cuidado!',
        text: 'Se va a eliminar al vendedor seleccionado. ¿Desea continuar?',
        confirmButtonColor: '#DD6B55',
        confirmButtonText: '¡Si!',
        cancelButtonText: "No, me equivoqué!",
        showCancelButton: true,
        showConfirmButton: true,
    }, function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                url: '/Vendedores/Eliminar/',
                data: oData,
                dataType: 'json',
                type: 'POST',
                async: false,
                cache: false,
                success: function (response) {
                    seBorro = response;
                    swal({
                        type: "success",
                        title: '¡Ok!',
                        text: 'Los datos se guardaron correctamente',
                        timer: 3000
                    });
                    window.location.reload();
                }
            });
        }
    });

}


function Editar(ID) {
    var vendedor = initialJsonData.ListVendedores.filter(function (vend) {
        return vend.ID == ID;
    })[0];
    $("#nombre").val(vendedor.Nombre);
    $("#domicilio").val(vendedor.Domicilio);
    $("#cuit-cuil").val(vendedor.cuil_cuit);
    $("#email").val(vendedor.Email);
    $("#selectResponsabilidad").val(vendedor.ResponsabilidadID).trigger('change');;
    $("#telefono").val(vendedor.Telefono);
    $("#localidad").val(vendedor.Localidad);
    $("#codPostal").val(vendedor.CodPostal);
    $("#fax").val(vendedor.Fax);
    $("#idVendedor").val(ID);
    $("#modalAgregar").modal("show");
}

$(document).ready(function () {
    $("#telefono").mask("0000-000000");
    $("#fax").mask("0000-000000");
    $("#cuit-cuil").mask("00-00000000-0");

    $("#selectCodPostal").select2({
        placeholder: 'Seleccione...',
        allowClear: false,
        ajax: {
            url: '/Vendedores/GetCodigoPostal/',
            dataType: 'json',
            data: function (params) {
                return {
                    query: params.term
                }
            },
            processResults: function (data, page) {
                return {
                    results: data
                };
            },
            cache: false
        },
        minimumInputLength: 3,
        width: '100%',
        dropdownParent: $("#modalAgregar")
    }).on('change', function () {
        var localidad = $(this).select2('data')[0].text;
        var codigoPostal = $(this).val();
        $("#codPostal").val(codigoPostal);
        $("#localidad").val(localidad);
    });

    $("#selectResponsabilidad").select2({
        placeholder: 'Seleccione...',
        allowClear: false,
        data: initialJsonData.ListResponsabilidad,
        minimumResultsForSearch: Infinity,
        width: '100%'
    });

    $("#btnNuevo").on('click', function () {
        $("#modalAgregar").modal({
            backdrop: 'static',
            keyboard: false,
        }).modal("show");
    });

    $("#btnImprimir").on('click', function () {
        var doc = new jsPDF('landscape');
        var columnas = [
            { title: "Cod.", dataKey: "ID" },
            { title: "Nombre", dataKey: "Nombre" },
            { title: "Domicilio", dataKey: "Domicilio" },
            { title: "CUIT/CUIL", dataKey: "cuil_cuit" },
            { title: "Teléfono", dataKey: "Telefono" },
            { title: "Localidad", dataKey: "Localidad" },
            { title: "E-mail", dataKey: "Email" }
        ];
        doc.autoTable(columnas, initialJsonData.ListVendedores);

        doc.save('listaVendedores' + new Date().getTime() + '.pdf');
    });

    $("#btnSalir").on('click', function () {
        swal({
            type: 'warning',
            title: 'Aguarde',
            text: 'Si cargó algún dato éste se eliminará. ¿Desea continuar?',
            confirmButtonColor: '#DD6B55',
            confirmButtonText: 'Si, Borrálo!',
            cancelButtonText: "No, me equivoqué!",
            showCancelButton: true,
            showConfirmButton: true,
        }, function (isConfirm) {
            if (isConfirm) {
                swal("Cancelado", "¡Te esperamos!", "error");
                clearData();
                $("#modalAgregar").modal("hide");
            }
        });
    });

    $("#btnGuardar").on('click', function () {
        var oData = {};
        oData.ID = $("#idVendedor").val();
        if (oData.ID == '') {
            oData.ID = 0;
        } else {
            oData.ID = parseInt($("#idVendedor").val());
        }
        oData.Nombre = $("#nombre").val();
        oData.Domicilio = $("#domicilio").val();
        oData.CodPostal = $("#codPostal").val();
        oData.Localidad = $("#localidad").val();
        oData.cuil_cuit = $("#cuit-cuil").val();
        oData.Email = $("#email").val();
        oData.ResponsabilidadID = $("#selectResponsabilidad").val();
        oData.Telefono = $("#telefono").val();
        oData.Fax = $("#fax").val();
        $.ajax({
            url: '/Vendedores/Guardar/',
            data: oData,
            dataType: 'json',
            type: 'POST',
            async: false,
            cache: false,
            success: function (response) {
                $("#modalAgregar").modal("hide");
                swal({
                    type: "success",
                    title: '¡Ok!',
                    text: 'Los datos se guardaron correctamente',
                    timer: 4000,
                }, function () {
                    clearData();
                    window.location.reload();
                });


            }
        });

    });


    //inicializar tabla de vendedores;
    var tabla = $("#grillaVendedores").DataTable({
        "initComplete": function (settings, json) {
        },
        "destroy": true,
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por página",
            "zeroRecords": "No se han encontrado registros",
            "info": "Viendo la página _PAGE_ de _PAGES_",
            "infoEmpty": "No se han encontrado registros",
            "infoFiltered": "(filtrado de un total de _MAX_ registros)",
            "loadingRecords": "Cargando...",
            "processing": "Procesando...",
            "search": "Buscar:",
            "paginate": {
                "first": "Primera",
                "last": "Última",
                "next": "Sig.",
                "previous": "Ant."
            },
        },
        "data": initialJsonData.ListVendedores,
        "aoColumns": [
            { "mData": "Nombre", "sTitle": "Nombre" },
            { "mData": "Domicilio", "sTitle": "Descripcion" },
            { "mData": "Responsabilidad.Descripcion", "sTitle": "Responsabilidad AFIP" },
            { "mData": "Telefono", "sTitle": "Teléfono" },
            { "mData": "Localidad", "sTitle": "Localidad" },
            { "mData": "cuil_cuit", "sTitle": "CUIT / CUIL" },
            { "mData": "Email", "sTitle": "E-Mail" },
            {
                "mData": null,
                "sTitle": "Opciones",
                "mRender": function (o) {
                    return '<button type="button" onclick="Editar(' + o.ID + ')" class="btn btn-warning btn-block btn-sm">Editar</button>' +
                        '<button type="button" onclick="Eliminar(' + o.ID + ')" class="btn btn-danger btn-block btn-sm">Eliminar</button>';
                }
            },
        ]
    });
});
