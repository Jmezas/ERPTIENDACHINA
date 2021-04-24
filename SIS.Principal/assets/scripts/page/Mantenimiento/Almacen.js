
var Lista = {
    CargarSucursal: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 7 },
            success: function (response) {
                $("#lstSucursal").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstSucursal"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));

                    });
                }
            }
        });
    },
    CargarDepartamento: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListarUbigeo"),
            dataType: 'json',
            data: { Acction: "DEPARTAMENTO", IdPais: '001', IdDep: "", IdProv: "", IdDis: "" },
            success: function (response) {
                $("#lstDepartamento").append($('<option>', { value: 0, text: 'Seleccione' }));
                $("#lstProvincia").append($('<option>', { value: 0, text: 'Seleccione' }));
                $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstDepartamento"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                    });
                }
            }
        });
    },
}

$(function () {
    Lista.CargarSucursal();
    Lista.CargarDepartamento();

    var table = $("#tbAlmacen").DataTable({
        "language": {
            "lengthMenu": "Mostrar _MENU_ registros por p&aacute;gina",
            "zeroRecords": "No se encontraron datos.",
            "info": "Mostrando la p&aacute;gina _PAGE_ de _PAGES_",
            "infoEmpty": "No hay registros disponibles",
            "infoFiltered": "(filtrando _MAX_ total de registros)",
            "search": "Buscar:",
            "paginate": {
                "first": "Primero",
                "previous": "Anterior",
                "next": "Siguiente",
                "last": "&Uacute;timo"
            },
        },


        "processing": true,

        "order": [],
        "ajax": {
            "url": General.Utils.ContextPath('Mantenimiento/ListaAlmacen'),
            "contentType": "application/json",
            "type": "POST",
            "dataType": "JSON",
            "data": function (d) {
                console.log(d);
                return JSON.stringify(d);
            },

        },
        "columns": [
            { "data": "Sucursal.Nombre" },
            { "data": "Nombre" },
            { "data": "Telefono" },
            { "data": "Direccion" },
            { "data": "Ubigeo.CodigoDepartamento" },
            { "data": "Ubigeo.CodigoProvincia" },
            { "data": "Ubigeo.CodigoDistrito" },
            {
                "data": "IdAlmacen", "render": function (data) {
                    return '<button class="btn btn-warning btn-sm btn-xs evento" data-toggle="modal" data-target="#ModalNuevo" onclick="Obtener(' + data + ');"><i class="fa fa-edit"></i> </button>' +
                        "&nbsp; " + '<button class="btn btn-danger btn-sm Eliminar" data-toggle="modal" data-target="#Eliminar"  "><i class="fa fa-trash"></i> </button>';

                },

                "orderable": false, "searchable": false, "width": "12%"
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdf',
                text: "<i class='fa fa-file-pdf-o'> PDF</i>",
                titleAttr: "Exportar PDF",
                className: "btn btn-danger btn-xs",
            },
            {

                extend: 'excelHtml5',
                text: "<i class='fa fa-file-excel-o'> Excel</i>",
                titleAttr: "Exportar Excel",
                className: "btn btn-success btn-xs",
                customize: function (xlsx) {
                    var sheet = xlsx.xl.worksheets['sheet1.xml'];

                    $('row c[r^="C"]', sheet).attr('s', '2');
                }
            }
        ]

    });

    $("#lstDepartamento").change(function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListarUbigeo"),
            dataType: 'json',
            data: { Acction: "PROVINCIA", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: "", IdDis: "" },
            success: function (response) {
                $("#lstProvincia").empty();
                $("#lstDistrito").empty();
                $("#lstProvincia").append($('<option>', { value: 0, text: 'Seleccione' }));
                $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                    $.grep(response, function (oDocumento) {
                        $('select[name="lstProvincia"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                    });
                }
            }

        });
    });

    $("#lstProvincia").change(function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListarUbigeo"),
            dataType: 'json',
            data: { Acction: "DISTRITO", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: $("#lstProvincia").val(), IdDis: "" },
            success: function (response) {
                $("#lstDistrito").empty();
                $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                    $.grep(response, function (oDocumento) {
                        $('select[name="lstDistrito"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                    });
                }
            }

        });
    });


    $("#btnGrabar").click(function () {

        var $form = $("#ModalNuevo");
        var oDatos = General.Utils.SerializeForm($form);

        if (General.Utils.ValidateForm($form)) {
            var oDatos = {
                IdAlmacen: $("#hdfId").val(),
                Nombre: $("#txtNombre").val(),
                Telefono: $("#txtTelefono").val(), 
                Direccion: $("#txtDireccion").val(), 
                Sucursal: {
                    IdSucursal: $("#lstSucursal").val()
                },

                Ubigeo: {
                    CodigoDepartamento: $("#lstDepartamento").val(),
                    CodigoProvincia: $("#lstProvincia").val(),
                    CodigoDistrito: $("#lstDistrito").val(),

                },

            }

            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Mantenimiento/InstAlmacen'),
                dataType: 'json',
                data: oDatos,
                success: function (response) {

                    if (response["Id"] == TypeMessage.Success) {

                        Swal.fire(
                            'Exito!',
                            response.Message,
                            response.Id
                        )

                    } else {

                        Swal.fire(
                            'Error!',
                            response.Message,
                            response.Id
                        )
                    }
                    $("#tbAlmacen").DataTable().ajax.reload();
                    $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                    Limpiar();
                }
            });



        }
    });
    $("#tbAlmacen").on('click', 'tbody .Eliminar', function () {
        var data = table.row($(this).parents("tr")).data();
        $("#hdfId").val(data.IdAlmacen);
        var Nombre = $(this).parents("tr").find("td").eq(1).text();
        $("#Nombre").text(Nombre);

        Swal.fire({
            title: 'Eliminar',
            text: '¿Desea eliminar el registro ' + Nombre + ' ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirmar',
            cancelButtonColor: '#d33',
            showClass: {
                popup: 'animate__animated animate__fadeInDown'
            },
            hideClass: {
                popup: 'animate__animated animate__fadeOutUp'
            }
        }).then((result) => {
            if (result.value) {

                $.ajax({
                    async: true,
                    type: 'post',
                    url: General.Utils.ContextPath('Mantenimiento/Eliminar'),
                    dataType: 'json',
                    data: { Id: $("#hdfId").val(), IdFlag: 9 },
                    success: function (response) {
                        console.log(response)
                        if (response["Id"] == TypeMessage.Success) {
                            Swal.fire(
                                'Exito!',
                                response.Message,
                                response.Id
                            )
                        } else {
                            Swal.fire(
                                'Error!',
                                response.Message,
                                response.Id
                            )

                        }
                        $("#tbAlmacen").DataTable().ajax.reload();


                    }
                });

            }
        })
    });

    $("#btnNuevo").click(function () {

        Limpiar();
    });
});

var Obtener = function (Id) {
    var senData = {
        Id: Id
    }
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Mantenimiento/ListaEditAlmacen'),
        dataType: 'json',
        data: senData,
        success: function (response) {
             
            $("#hdfId").val(response.IdAlmacen);
            $("#lstSucursal").val(response.Sucursal.IdSucursal);
            $("#txtNombre").val(response.Nombre);
            $("#txtTelefono").val(response.Telefono);
            $("#txtDireccion").val(response.Direccion);
            $("#txtReferencia").val(response.Sucursal.IdSucursal);
 

            $("#lstDepartamento").val(response.Ubigeo.CodigoDepartamento);
            var Prov = response.Ubigeo.CodigoProvincia;
            var Dist = response.Ubigeo.CodigoDistrito;

            //provincia
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListarUbigeo"),
                dataType: 'json',
                data: { Acction: "PROVINCIA", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: "", IdDis: "" },
                success: function (response) {

                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                        $.grep(response, function (oDocumento) {
                            $('select[name="lstProvincia"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                        });
                        $("#lstProvincia").val(Prov);
                    }

                }

            });

            //distrito
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListarUbigeo"),
                dataType: 'json',
                data: { Acction: "DISTRITO", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: Prov, IdDis: "" },
                success: function (response) {
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                        $.grep(response, function (oDocumento) {
                            $('select[name="lstDistrito"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                        });
                        $("#lstDistrito").val(Dist);
                    }
                }

            });
        }

    });
}


function Limpiar() {
    $("#hdfId").val(0);
    $("#txtNombre").val('');
    $("#txtTelefono").val('');
    $("#lstSucursal").val('0');
    $("#txtDireccion").val('');
    $("#lstDistrito").empty();
    $("#lstProvincia").empty(); 
    $("#lstDepartamento").val(0);
    Lista.CargarDepartamento();

}