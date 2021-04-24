var CompPagoEnviar = [];
var Lista = {
    CargarUsuario: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Mantenimiento/ListaCBO_UsuarioAlmacen"),
            dataType: 'json',
            data: { Flag: 1 },
            success: function (response) {
                $("#lstUsuario").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstUsuario"]').append($('<option>', { value: oDocumento["Usuario"]["Usuario"], text: oDocumento["Usuario"]["Nombre"] }));

                    });
                }
            }
        });
    },
}

$(function () {
    Lista.CargarUsuario();

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
            "url": General.Utils.ContextPath('Mantenimiento/ListaPermisoAlmacen'),
            "contentType": "application/json",
            "type": "POST",
            "dataType": "JSON",
            "data": function (d) {
                console.log(d);
                return JSON.stringify(d);
            },

        },
        "columns": [
            { "data": "Usuario.Nombres" },
            { "data": "Usuario.NroDocumento" },
            { "data": "Usuario.Usuario" },
            {
                "data": "Usuario.Usuario", "render": function (data) {
                    return '<button class="btn btn-warning btn-xs evento" data-toggle="modal" data-target="#ModalNuevo" "><i class="fa fa-edit"></i> </button>' +
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
    $('#selectAll').click(function () {
        if ($(this).hasClass('seleccionando')) {
            $(this).removeClass('seleccionando');
            $(this).addClass('deseleccionando');
            $('.chkEnviar:not(:checked)').trigger('click');
            $(this).html('Deseleccionar todo');
        }
        else {
            $(this).removeClass('deseleccionando');
            $(this).addClass('seleccionando');
            $('.chkEnviar:checked').trigger('click');
            $(this).html('Seleccionar todo');
        }
    });

    $('#tbFilAlmacen tbody').delegate('.chkEnviar', 'click', function () {
        var tmp = $(this).parent().parent().attr('data-id');
         

        if ($(this).prop('checked')) {
            CompPagoEnviar.map(function (data) {
                if (data.IdAlmacen == tmp) {
                    data.Estado = 'A';
                }

                return data;
            });
        } else {
            CompPagoEnviar.map(function (data) {
                if (data.IdAlmacen == tmp) {
                    data.Estado = 'I';
                }

                return data;
            });
        }
        console.log(CompPagoEnviar)
    });

    $("#btnGrabar").click(function () {
        console.log(CompPagoEnviar)
         
        if ($("#lstUsuario").val() === '0') {
            General.Utils.ShowMessage(TypeMessage.Error, 'No ha seleccionado El usuario')
        }
        else if (CompPagoEnviar.length === 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'No ha seleccionado Comprobantes a Enviar')
        } else {
            $.ajax({
                url: General.Utils.ContextPath('Mantenimiento/InstUpdPermisoAlmacem'),
                type: 'post',
                data: {
                    sLista: CompPagoEnviar,
                    Usuario: $("#lstUsuario").val(),
                    Flag: $("#hdfFlag").val()
                },
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
                    $("#tbAlmacen").DataTable().ajax.reload();
                    $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                    Limpiar();
                }
            });
        }
        
    });
    $("#tbAlmacen").on('click', 'tbody .Eliminar', function () {
        var data = table.row($(this).parents("tr")).data();
        $("#hdfId").val(data.IdCateogira);
        var Nombre = $(this).parents("tr").find("td").eq(0).text();
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
                    data: { Id: $("#hdfId").val(), IdFlag: 4 },
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
                        $("#tbCategoria").DataTable().ajax.reload();


                    }
                });

            }
        })
    });
    $("#tbAlmacen").on('click', 'tbody .evento', function () {
        var data = table.row($(this).parents("tr")).data();
        console.log(data.Usuario.Usuario)
        $("#hdfFlag").val(1) // envio por id paramen
        $("#lstUsuario").val(data.Usuario.Usuario)
        CargarLista(1, data.Usuario.Usuario);
    })
    $("#btnNuevo").click(function () {

        Limpiar();
        CargarLista(0, '');
        $("#hdfFlag").val(0)
        $("#selectAll").html('Seleccionar todo')
    });
});



function Limpiar() {
    $("#hdfId").val(0);

    $("#txtNombre").val('');
}




function CargarLista(id, User) {
    CompPagoEnviar = [];
  
    $.ajax({
        async: false,
        type: 'post',
        url: General.Utils.ContextPath('Mantenimiento/ListaAlmacenPermisos'),
        dataType: 'json',
        data: { Flag: id, login: User },
        success: function (response) {
            console.log(response)
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error
                var $tb = $('#tbFilAlmacen')
                $tb.find('tbody').empty();
                if (response.length == 0) {
                    $tb.find('tbody').html('<tr><td colspan="6">No hay resultados para el filtro ingresado</td></tr>');
                    $('#pHelperProductos').html('');
                } else {
                    $.grep(response, function (item) {
                        $tb.find('tbody').append(
                            '<tr data-id="' + item["IdAlmacen"] + '">' +
                            '<td class="text-center">' + item["IdAlmacen"] + '</td>' +
                            '<td class="text-center">' + item["Nombre"] + '</td>' +
                            '<td class="text-center">' + item["Ubigeo"]["UbicacionGeografica"] + '</td>' +
                            '<td class="text-center">' + '<input type="checkbox" class="chkEnviar" name=' + item["Estado"] + '></input>' + '</td>' +
                            '</tr>' +
                            console.log(item["Permiso"])

                        );
                        if (item["Estado"] === 'A') {
                            $('input[name=' + item["Estado"] + ']').prop("checked", true);

                        }
                        CompPagoEnviar.push({
                            IdAlmacen: item["IdAlmacen"].toString(),
                            Estado: ($('input[name=' + item["Estado"] + ']').is(':checked') === true ? 'A' : 'I')
                        })
                    });




                    $('#pHelperProductos').html('Existe(n) ' + response["Total"] + ' resultado(s) para mostrar.' +
                        (response["Total"] > 0 ? ' Del&iacute;cese hacia abajo para visualizar m&aacute;s...' : ''));
                }
            }
        }
    });
};
/*
function EliminarPermiso(id) {
    CompPagoEnviar = CompPagoEnviar.filter((item) => {
        return item.IdAlmacen !== id.IdAlmacen;
    });
}*/