
var Lista = {
    CargarCombo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 6 },
            success: function (response) {
                //console.log(response)
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
                $("#lstDepartamento").append($('<option>', {value:0,text:'Seleccione'}));
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

   


    var table = $("#tbCliente").DataTable({ 
        select: true,
        pageLength: 10,
        processing: true,
        serverSide: true,
        filter: true,
        bSort: true,
        orderMulti: false,
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
         
        "ajax": {
            "url": General.Utils.ContextPath('Mantenimiento/ListaCliente'), 
            "type": "POST",
            "dataType": "JSON",
            "data": function (d) {
                
            },

        },
        "columns": [
            { "data": "IdCliente", "name": "IdCliente" },
            { "data": "Text", "name": "Text"  },
            { "data": "NroDocumento", "name": "NroDocumento"},
            { "data": "Razonsocial", "name": "Razonsocial" },
            { "data": "Telefono", "name": "Telefono"},
            { "data": "Celular", "name": "Celular"},
            { "data": "Email", "name": "Email" },
            { "data": "Direccion" },
            {
                "data": "IdCliente", "render": function (data) {
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
    $("#btnBuscar").click(function () {
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
                    IdCliente: $("#hdfId").val(),
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
                    url: General.Utils.ContextPath('Mantenimiento/InstCliente'),
                    dataType: 'json',
                    beforeSend: General.Utils.StartLoading,
                    complete: General.Utils.EndLoading,
                    data: oDatos,
                    success: function (response) {
                        //console.log(response);
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
                        $("#tbCliente").DataTable().ajax.reload();
                        $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                        Limpiar();
                    }
                });



            }
        }

       
    });
    $("#tbCliente").on('click', 'tbody .Eliminar', function () {
        var data = table.row($(this).parents("tr")).data();
        $("#hdfId").val(data.IdCliente);
        var Nombre = $(this).parents("tr").find("td").eq(2).text();
        $("#Nombre").text(Nombre);

        Swal.fire({
            title: 'Eliminar',
            text: '¿Desea eliminar el cliente ' + Nombre + ' ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirmar',
            cancelButtonColor: '#d33',
            cancelButtonText:'Cancelar',
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
                        //console.log(response)
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
                        $("#tbCliente").DataTable().ajax.reload();


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
        IdCliente: Id
    }
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Mantenimiento/ListaEditCliente'),
        dataType: 'json',
        data: senData,
        success: function (response) {

            $("#hdfId").val(response.IdCliente);
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
                    //console.log(response)
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
    $("#lstTipoDoc").val('0');
    $("#txtDocumento").val('');
    $("#txtRazon").val('');
    $("#txtTelefono").val('');
    $("#txtCelular").val('');
    $("#txtEmail").val('');
    $("#txtDirecion").val('');
    $("#lstDepartamento").val('0');
    $("#lstProvincia").empty();
    $("#lstProvincia").append($('<option>', { value: 0, text: 'Seleccione' }));
    $("#lstDistrito").empty();
    $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
}

function BuscarSunat() {

    var RUC = $("#txtDocumento").val();
    if ($("#lstTipoDoc").val() == 3) {
        if (isNaN(RUC) || RUC < 10000000000 || RUC > 99999999999) {
            General.Utils.ShowMessage(TypeMessage.Error, 'El RUC debe contener 11 dígitos');
        } else {
            $.ajax({
                type: 'post',
                url: General.Utils.ContextPath('Shared/SearchSunatRUC'),
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                data: {
                    numeroRuc: RUC

                },
                success: function (response) {
                    console.log(response)
                    if (response == null) {
                        General.Utils.ShowMessage("error", "Nro. de ruc invalido, ingrese los datos manualmente");
                    } else {


                        $("#txtRazon").val(response.RazonSocial.replace(/\s+/g, ""));
                        $("#txtDireccion").val(response.DomicilioFiscal.replace(/\s+/g, " "));
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
                url: General.Utils.ContextPath('Shared/SearchSunatDNI'),
                data: {
                    numeroRuc: RUC

                },
                success: function (response) {
                    console.log(response)
                    if (response == null) {
                        General.Utils.ShowMessage("error", "Nro. de dni invalido, ingrese los datos manualmente");
                    } else {


                        $("#txtRazon").val(`${response.nombre} ${response.apellidoPaterno} ${response.apellidoMaterno}`);
                        $("#txtDireccion").val(response.Direccion);
                    }
                }
            });
        }
    } else {
        General.Utils.ShowMessage(TypeMessage.Error, 'digite manualmente');
    }

} {

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