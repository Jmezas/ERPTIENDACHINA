
var Lista = {
    CargarCombo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 6 },
            success: function (response) {
                console.log(response)
                $("#lstTipoDoc").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstTipoDoc"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
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
                $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
                $("#lstProvincia").append($('<option>', { value: 0, text: 'Seleccione' }));
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

    Lista.CargarCombo();
    Lista.CargarDepartamento();

    $('#lstDepartamento').select2({
        dropdownParent: $('#ModalNuevo')
    });
    $('#lstProvincia').select2({
        dropdownParent: $('#ModalNuevo')
    });
    $('#lstDistrito').select2({
        dropdownParent: $('#ModalNuevo')
    });
   


    var table = $("#tbProveedor").DataTable({
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
            "url": General.Utils.ContextPath('Mantenimiento/ListaProveedor'),
            "contentType": "application/json",
            "type": "POST",
            "dataType": "JSON",
            "data": function (d) {
                console.log(d);
                return JSON.stringify(d);
            },

        },
        "columns": [
            { "data": "Text" },
            { "data": "NroDocumento" },
            { "data": "Razonsocial" },
            { "data": "Telefono" },
            { "data": "Celular" },
            { "data": "Email" },
            { "data": "Direccion" },
            {
                "data": "IdProveedor", "render": function (data) {
                    return '<button class="btn btn-warning btn-xs evento" data-toggle="modal" data-target="#ModalNuevo" onclick="Obtener(' + data + ');"><i class="fa fa-edit"></i> </button>' +
                        "&nbsp; " + '<button class="btn btn-danger Eliminar" data-toggle="modal" data-target="#Eliminar"  "><i class="fa fa-trash"></i> </button>';

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

    $("#btnBuscar").change(function () {
        BuscarSunat();
    })
    $("#txtDocumento").change(function () {
        BuscarSunat();
    })
    $("#btnGrabar").click(function () {

        var $form = $("#ModalNuevo");
        var oDatos = General.Utils.SerializeForm($form);
        if ($("#lstTipoDoc").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Seleccione tipo de documento');

        } else if ($("#lstDistrito").val() == '' || $("#lstDistrito").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Ingrese el distrito');
        } else {
            if (General.Utils.ValidateForm($form)) {
                var oDatos = {
                    IdProveedor: $("#hdfId").val(),
                    Id: $("#lstTipoDoc").val(),
                    NroDocumento: $("#txtDocumento").val(),
                    Razonsocial: $("#txtRazon").val(),
                    Telefono: $("#txtTelefono").val(),
                    Celular: $("#txtCelular").val(),
                    Email: $("#txtEmail").val(),
                    Direccion: $("#txtDireccion").val(),
                    Ubigeo: {
                        CodigoDepartamento: $("#lstDepartamento").val(),
                        CodigoProvincia: $("#lstProvincia").val(),
                        CodigoDistrito: $("#lstDistrito").val(),

                    },

                }

                $.ajax({
                    async: true,
                    type: 'post',
                    url: General.Utils.ContextPath('Mantenimiento/InstProveedor'),
                    dataType: 'json',
                    beforeSend: General.Utils.StartLoading,
                    complete: General.Utils.EndLoading,
                    data: oDatos,
                    success: function (response) {
                        console.log(response);
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
                        $("#tbProveedor").DataTable().ajax.reload();
                        $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                        Limpiar();
                    }
                });
            }
        }

        
    });
    $("#tbProveedor").on('click', 'tbody .Eliminar', function () {
        var data = table.row($(this).parents("tr")).data();
        $("#hdfId").val(data.IdProveedor);
        var Nombre = $(this).parents("tr").find("td").eq(2).text();
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
                    data: { Id: $("#hdfId").val(), IdFlag: 6 },
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
                        $("#tbProveedor").DataTable().ajax.reload();


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
        url: General.Utils.ContextPath('Mantenimiento/ListaEditProveedor'),
        dataType: 'json',
        data: senData,
        success: function (response) {
            console.log(response)
            $("#hdfId").val(response.IdProveedor);
            $("#lstTipoDoc").val(response.Id);
            $("#txtDocumento").val(response.NroDocumento);
            $("#txtRazon").val(response.Razonsocial);
            $("#txtTelefono").val(response.Telefono);
            $("#txtCelular").val(response.Celular);
            $("#txtEmail").val(response.Email);
            $("#txtDireccion").val(response.Direccion);
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
                    console.log(response)
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
    $("#txtSigla").val('');
    $("#lstCategoria").val('0');
}

function BuscarSunat() {

    var RUC = $("#txtDocumento").val();
    if ($("#lstTipoDoc").val() == 3) {
        if (isNaN(RUC) || RUC < 10000000000 || RUC > 99999999999) {
            General.Utils.ShowMessage(TypeMessage.Error, 'El RUC debe contener 11 dígitos');
        } else {
            $.ajax({
                type: 'post',
                url: General.Utils.ContextPath('Shared/SearchSunatRUCDNI'),
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                data: {
                    tipo: $("#lstTipoDoc").val(),
                    numeroRuc: RUC

                },
                success: function (response) {
                    if (response == null) {
                        General.Utils.ShowMessage("error", "Nro. de ruc invalido, ingrese los datos manualmente");
                    } else {

                        $("#txtRazon").val(response.razonSocial);
                        $("#txtDireccion").val(response.direccion);

                        var ubigeo = response.ubigeo
                        if (ubigeo != null) {



                            $("#lstDepartamento").val(ubigeo.substr(0, 2));
                            $.ajax({
                                async: true,
                                type: 'post',
                                url: General.Utils.ContextPath("Shared/ListarUbigeo"),
                                dataType: 'json',
                                data: { Acction: "PROVINCIA", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: "", IdDis: "" },
                                success: function (response) {
                                    //console.log(response)
                                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                                        $.grep(response, function (oDocumento) {
                                            $('select[name="lstProvincia"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                                        });
                                        $("#lstProvincia").val(ubigeo.substr(2, 2));
                                    }
                                }

                            });

                            //distrito
                            $.ajax({
                                async: true,
                                type: 'post',
                                url: General.Utils.ContextPath("Shared/ListarUbigeo"),
                                dataType: 'json',
                                data: { Acction: "DISTRITO", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: ubigeo.substr(2, 2), IdDis: "" },
                                success: function (response) {
                                    // console.log(response)
                                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                                        $.grep(response, function (oDocumento) {
                                            $('select[name="lstDistrito"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                                        });
                                        $("#lstDistrito").val(ubigeo.substr(4, 2));

                                    }
                                }

                            });



                        }
                    }
                }
            });
        }
    } else if ($("#lstTipoDoc").val() == 1) {
        if (isNaN(RUC) || RUC.length != 8) {
            General.Utils.ShowMessage(TypeMessage.Error, 'El DNI debe contener 8 dígitos');
        } else {
            $.ajax({
                type: 'post',
                url: General.Utils.ContextPath('Shared/SearchSunatRUCDNI'),
                data: {
                    tipo: $("#lstTipoDoc").val(),
                    numeroRuc: RUC

                },
                success: function (response) {
                    //console.log(response)
                    if (response == null) {
                        General.Utils.ShowMessage("error", "Nro. de dni invalido, ingrese los datos manualmente");
                    } else {
                        $("#txtRazon").val(response.nombres + ' ' + response.apellidoPaterno + ' ' + response.apellidoMaterno);
                        $("#txtDireccion").val("-");
                    }
                }
            });
        }
    } else {
        General.Utils.ShowMessage(TypeMessage.Error, 'digite manualmente');
    }

}