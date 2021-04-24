﻿
$(function () {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("Shared/ListaCombo"),
        dataType: 'json',
        data: { Id: 24 },
        success: function (response) {
            console.log(response)
            $("#lstCodigo").append($('<option>', { value: 0, text: 'Seleccione' }));
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                $.grep(response, function (oDocumento) {

                    $('select[name="lstCodigo"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                });
            }
        }

    });
    $("#lstCodigo").select2({
        dropdownParent: $("#ModalNuevo")
    })
    var table = $("#tbTipoDoc").DataTable({
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
            "url": General.Utils.ContextPath('Mantenimiento/ListaTipoSerie'),
            "contentType": "application/json",
            "type": "POST",
            "dataType": "JSON",
            "data": function (d) {
                console.log(d);
                return JSON.stringify(d);
            },

        },
        "columns": [
            { "data": "codigo" },
            { "data": "nombre" },
            { "data": "serie" },
            { "data": "numero" },
            {
                "data": "Id", "render": function (data) {
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


    $("#btnGrabar").click(function () {

        var $form = $("#ModalNuevo");
        var oDatos = General.Utils.SerializeForm($form);

        if (General.Utils.ValidateForm($form)) {
            var oDatos = {
                Id: $("#hdfId").val(),
                codigo: $("#lstCodigo").val(),
                nombre: $("#txtNombre").val(),
                serie: $("#txtSerie").val(),
                numero: $("#txtNumero").val(),
            }

            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Mantenimiento/Insertar_UpdateTipoDoc'),
                dataType: 'json',
                data: {
                    eMenu: oDatos
                },
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
                    $("#tbTipoDoc").DataTable().ajax.reload();
                    $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                    Limpiar();
                }
            });



        }
    });

    $("#tbTipoDoc").on('click', 'tbody .Eliminar', function () {
        
        var data = table.row($(this).parents("tr")).data();
        $("#hdfId").val(data.Id);
        var Nombre = $(this).parents("tr").find("td").eq(1).text();
        var serie = $(this).parents("tr").find("td").eq(2).text();
        $("#Nombre").text(Nombre);

        Swal.fire({
            title: 'Eliminar',
            text: '¿Desea eliminar la serie ' + Nombre +' con serie '+ serie +' ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirmar',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
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
                    data: { Id: $("#hdfId").val(), IdFlag:14 },
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
                        $("#tbTipoDoc").DataTable().ajax.reload();
                        $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal

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
        url: General.Utils.ContextPath('Mantenimiento/BuscarTipoSerieId'),
        dataType: 'json',
        data: senData,
        success: function (response) {
            console.log(parseInt(response.codigo));
            $("#hdfId").val(response.Id);
            $("#txtNombre").val(response.nombre);
            $("#lstCodigo").val(parseInt(response.codigo));
            $('#lstCodigo').trigger('change'); 
            $("#txtSerie").val(response.serie);
            $("#txtNumero").val(response.numero);
        }

    });
}


function Limpiar() {
    $("#hdfId").val(0);
    $("#txtNumero").val('');
    $("#txtSerie").val('');
    $("#lstCodigo").val('0');
    $("#txtNombre").val('');
}