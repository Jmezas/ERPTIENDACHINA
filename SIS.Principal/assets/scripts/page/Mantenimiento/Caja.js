var table = null;
$(function () {   

   

    setTimeout(function myfunction() {      
        cargarLista($("#lstSucursalCab").val());
        
        
    }, 500);

    $("#lstSucursalCab").change(function () {
        table.destroy();
        cargarLista($("#lstSucursalCab").val());
        
    });
    

    $("#btnGrabar").click(function () {

        var $form = $("#ModalNuevo");
        var oDatos = General.Utils.SerializeForm($form);

        if (General.Utils.ValidateForm($form)) {
            var oDatos = {
                Id: $("#hdfId").val(),
                Text: $("#txtNombre").val(), 
                sucursal: $("#lstSucursalCab").val()
            }

            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Mantenimiento/RegistrarCaja'),
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
                    $("#tbCaja").DataTable().ajax.reload();
                    $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                    Limpiar();
                }
            });



        }
    });
    $("#tbCaja").on('click', 'tbody .Eliminar', function () {
        var data = table.row($(this).parents("tr")).data();
        $("#hdfId").val(data.Id);
        var Nombre = $(this).parents("tr").find("td").eq(0).text();
        //$("#Nombre").text(Nombre);

        Swal.fire({
            title: 'Eliminar',
            text: '¿Desea eliminar la caja ' + Nombre + ' ?',
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
                    data: { Id: $("#hdfId").val(), IdFlag: 13 },
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
                        $("#tbCaja").DataTable().ajax.reload();


                    }
                });

            }
        })
    });

    $("#btnNuevo").click(function () {
        Limpiar();
    });

    
});

function cargarLista(sucursal) {   
     table = $("#tbCaja").DataTable({
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
                "last": "&Uacute;ltimo"
            },
        },


        "processing": true,

        "order": [],
        "ajax": {            
            "url": General.Utils.ContextPath('Mantenimiento/ListadoCaja'),
            "contentType": "application/json",
            "type": "POST",
            "dataType": "JSON",
            "data": function (d) {               
                d.sucursal = sucursal;
                return JSON.stringify(d);
            },

        },
        "columns": [
            { "data": "Text" },
            {
                "data": "Id", "render": function (data) {
                    return '<button class="btn btn-warning btn-sm btn-xs evento" data-toggle="modal" data-target="#ModalNuevo" onclick="Obtener(' + data + ');"><i class="fa fa-edit"></i> </button>' +
                        "&nbsp; " + '<button class="btn btn-danger btn-sm Eliminar" data-toggle="modal" data-target="#Eliminar"  "><i class="fa fa-trash"></i> </button>';

                },

                "orderable": false, "searchable": false, "width": "12%"
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                extend: 'pdfHtml5',
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
}
var Obtener = function (Id) {
    var senData = {
        Id: Id
    }
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Mantenimiento/BuscarCajaId'),
        dataType: 'json',
        data: senData,
        success: function (response) { 
            $("#hdfId").val(response.Id); 
            $("#txtNombre").val(response.Text);
        }

    });
}


function Limpiar() {
    $("#hdfId").val(0);
    $("#txtNombre").val('');
 

}