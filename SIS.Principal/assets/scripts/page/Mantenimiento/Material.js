
var Lista = {
    CargarUnidad: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 2 },
            success: function (response) {

                $("#lstUnidad").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstUnidad"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },


    CargarLinea: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 5 },
            success: function (response) {
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstLinea"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    CargarCodigo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/CoidgoGenerado"),
            dataType: 'json',
            success: function (response) {
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $("#txtCodigo").val(response.Nombre)
                }
            }

        });
    },
    CargarSexo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 21 },
            success: function (response) {
                $("#lstGenero").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstGenero"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    cargarSubCategoria: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 4 },
            success: function (response) {

                $("#lstSubCategoria").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstSubCategoria"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },


    cargarTemporada: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 22 },
            success: function (response) {

                $("#lstTemporada").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstTemporada"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    cargarTalla: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 23 },
            success: function (response) {

                $("#lstTalla").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstTalla"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
}

$(function () {
    //INICIALIZAR CAMPOS
    $('#txtPrecioComp').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('#txtPrecioVent').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('#txtDescuento').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('#txtPrecioDocena').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('#txtPrecioCaja').Validate({ type: TypeValidation.Numeric, special: '.' });



    CodigoEnviar = [];
    Lista.CargarUnidad();
    Lista.CargarLinea();
    Lista.CargarCodigo();
    Lista.CargarSexo();
    Lista.cargarSubCategoria();
    Lista.cargarTemporada();
    Lista.cargarTalla();


    //* cargar lista predetermianda*//
    cargarTipo(1);
    ListaMarca(1);
    ListCategoria(1);
    ListaColor(1);
    ListaModelo(1);



    $("#lstLinea").change(function () {
        cargarTipo($("#lstLinea").val());
        ListaMarca($("#lstLinea").val());
        ListCategoria($("#lstLinea").val());
        ListaColor($("#lstLinea").val());
        ListaModelo($("#lstLinea").val());
        if ($("#lstLinea").val() == 1) {
            $("#idMercaderiacategoria").show();
            $("#idmercaderiaGe").show();
            $("#idMercaderiaTemp").show();
            $("#idMercaderiaTalla").show();
        } else {
            $("#idMercaderiacategoria").hide();
            $("#idmercaderiaGe").hide();
            $("#idMercaderiaTemp").hide();
            $("#idMercaderiaTalla").hide();
        }

    })
    
    var table = $("#tbMaterial").DataTable({
        select: true,
        pageLength: 10,
        processing: true,
        serverSide: true,
        filter: true,
        bSort: true,
        orderMulti: false,
        order: [],
        language: {
            "url": "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json"
        },
        dom: 'Bfrtip',
        ajax: {
            url: General.Utils.ContextPath('Mantenimiento/ListadoMaterial'),
            type: "POST",
            datatype: "json",
            data: function (d) {
                
            }
        },
        columns: [
            { "data": "sCodigo", "name": "sCodigo" },
            { "data": "IdMaterial", "name": "IdMaterial" },
            { "data": "Text", "name": "Text" },
            { "data": "Codigo", "name": "Codigo" },
            { "data": "Nombre", "name": "Nombre" },
            { "data": "Unidad.Nombre", "name": "Unidad" },
            //{ "data": "Marca.Nombre", "name": "Marca" },
            //{ "data": "Modelo.Nombre", "name": "Modelo" },
            { "data": "PrecioCompra", "name": "PrecioCompra" },
            { "data": "PrecioVenta", "name": "PrecioVenta" },
            { "data": "Categoria.Nombre", "name": "Categoria" },
            { "data": "SubCateoria.Nombre", "name": "SubCateoria" },
            //{ "data": "Descuento" },
            {
                    "data": "IdMaterial", "render": function (data) {
                        return '<button class="btn btn-warning btn-xs evento" data-toggle="modal" data-target="#ModalNuevo" onclick="Obtener(' + data + ');"><i class="fa fa-edit"></i> </button>' +
                            "&nbsp; " + '<button class="btn btn-danger btn-xs Eliminar" data-toggle="modal" data-target="#Eliminar"  "><i class="fa fa-trash"></i> </button>';

                    },

        
        }
            
        ],
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
           
        ],
        'columnDefs': [
            {
                'targets': 0,
                'checkboxes': {
                    'selectRow': true
                }
            }
        ],
        'select': {

            'style': 'multi'
        },
        'fnCreatedRow': function (nRow, aData, iDataIndex) {
            $(nRow).attr('data-id', aData.IdMaterial); // or whatever you choose to set as the id
            $(nRow).attr('id', 'id_' + aData.IdMaterial); // or whatever you choose to set as the id
        },

    });

    

    $("#btnBarra").click(function () {
        var form = this;
        CodigoEnviar = []
        var rows_selected = table.column(0).checkboxes.selected();

        // Iterar sobre todas las casillas seleccionadas
        $.each(rows_selected, function (index, rowId) {
            // Create a hidden element 
            $(form).append(
                $('<input>')
                    .attr('type', 'hidden')
                    .attr('name', 'id[]')
                    .val(rowId)
            );
            //  CodigoEnviar.push(rowId)
            //   console.log(JSON.stringify(CodigoEnviar))
            CodigoEnviar.push({
                Id: rowId.split('|')[0],
                codigo: rowId.split('|')[1],
                nombre: rowId.split('|')[2],
                precio: rowId.split('|')[3],
                cantidad: 1

            })
            //console.log(CodigoEnviar)
        });
        var $tb = $('#tbDetalle');
        $tb.find('tbody').empty();
        if (CodigoEnviar.length == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'No ha seleccionado Producto para generar')
        } else {
            $.grep(CodigoEnviar, function (oDetalle) {
                //console.log(oDetalle)
                $tb.find('tbody').append(
                    '<tr data-index=' + oDetalle["Id"] + '>' +
                    '<td>' + oDetalle["codigo"] + '</td>' +
                    '<td>' + oDetalle["nombre"] + '</td>' +
                    '<td>' + oDetalle["precio"] + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="descuento"  value="' + oDetalle["cantidad"] + '">' + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'

                );
            });
            CallCantidad();
        } 
   
        // Eliminar elementos añadidos
        $('input[name="id\[\]"]', form).remove();

    });


    $('#tbDetalle').on('change', '.Cant', function () {

        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');
        var input = Number($(this).val());
        CodigoEnviar.map(function (data) {

            if (parseInt(data.Id) == parseInt(Id)) {
                data.cantidad = parseInt(input);
            }
        })
        var $tb = $('#tbDetalle');
        $tb.find('tbody').empty();
        $.grep(CodigoEnviar, function (oDetalle) {

            $tb.find('tbody').append(
                '<tr data-index=' + oDetalle["Id"] + '>' +
                '<td>' + oDetalle["codigo"] + '</td>' +
                '<td>' + oDetalle["nombre"] + '</td>' +
                '<td>' + oDetalle["precio"] + '</td>' +
                '<td>' + '<input type="text" class="form-control Cant" id="descuento"  value="' + oDetalle["cantidad"] + '">' + '</td>' +
                '<td class="text-center">' +
                '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                '</td>' +
                '</tr>'
            );
        });
        CallCantidad();
    })

    $('#tbDetalle').find('tbody').on('click', '.btn-danger', function () {
        var $btn = $(this);
        var $tb = $('#tbDetalle');

        var Id = $btn.closest('tr').attr('data-index');

        BuscarIndexDetalleEnTabla(Id);

        arrDetalle = CodigoEnviar.filter(function (x) {
            return x.Id != Id;
        });

        CodigoEnviar = [];
        CodigoEnviar = arrDetalle;
        $tb.find('tbody').empty();
        $.grep(CodigoEnviar, function (oDetalle) {
            $tb.find('tbody').append(
                '<tr data-index=' + oDetalle["Id"] + '>' +
                '<td>' + oDetalle["codigo"] + '</td>' +
                '<td>' + oDetalle["nombre"] + '</td>' +
                '<td>' + oDetalle["precio"] + '</td>' +
                '<td>' + '<input type="text" class="form-control Cant" id="descuento"  value="' + oDetalle["cantidad"] + '">' + '</td>' +
                '<td class="text-center">' +
                '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                '</td>' +
                '</tr>'
            );
        });
        CallCantidad();
    })

    $("#btnGenerar").click(function () {

        if (CodigoEnviar.length === 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'No ha seleccionado Producto para generar')
        } else {
            if ($("#IdTotales").val() <= 39) {
                $.ajax({
                    async: true,
                    type: 'post',
                    url: General.Utils.ContextPath('Shared/ImprimirBarra'),
                    dataType: 'json',
                    data: { sLista: CodigoEnviar },
                    beforeSend: General.Utils.StartLoading,
                    complete: General.Utils.EndLoading,
                    success: function (response) {
                        CodigoEnviar = [];
                        var $tb = $('#tbDetalle');
                        $tb.find('tbody').empty();
                        window.open(General.Utils.ContextPath('Shared/ImprimirEtiqueta'));
                        //  console.log(response);
                    }
                });
            } else {
                General.Utils.ShowMessage(TypeMessage.Error, 'la cantidad supera')
            }

        }


    });

    $("#btnGrabar").click(function () {

        var $form = $("#ModalNuevo");
        var oDatos = General.Utils.SerializeForm($form);

        if (General.Utils.ValidateForm($form)) {
            var oDatos = {
                IdMaterial: $("#hdfId").val(),
                Id: $("#lstLinea").val(),
                Codigo: $("#txtCodigo").val(),
                Nombre: $("#txtNombre").val(),
                Descripcion: $("#txtDescripcion").val(),
                Modelo: {
                    IdModelo: $("#lstModelo").val(),
                },
                PrecioVenta: $("#txtPrecioVent").val(),
                PrecioCompra: $("#txtPrecioComp").val(),
                Descuento: $("#txtDescuento").val(),
                Unidad: { IdUnidad: $("#lstUnidad").val(), },
                Marca: { IdMarca: $("#lstMarca").val(), },
                Categoria: { IdCateogira: $("#lstCategoria").val(), },
                SubCateoria: { IdSubCategoira: $("#lstSubCategoria").val(), },
                genero: {
                    IdGenero: $("#lstGenero").val(),
                },
                Etipo: {
                    IdTipo: $("#lsTipo").val(),
                },
                EColor: {
                    IdColor: $("#lstColor").val(),
                },
                Temporada: {
                    IdTemporada: $("#lstTemporada").val(),
                },
                Talla: {
                    IdTalla: $("#lstTalla").val(),
                },
                PrecioDocena: $("#txtPrecioDocena").val(),
                PrecioCaja: $("#txtPrecioCaja").val(),
            }

            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Mantenimiento/InstMaterial'),
                dataType: 'json',
                data: oDatos,
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
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
                    $("#tbMaterial").DataTable().ajax.reload();
                    $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                    Limpiar();
                }
            });



        }
    });
    $("#limpiarModal").click(function () {
        CodigoEnviar = [];
        var $tb = $('#tbDetalle');
        $tb.find('tbody').empty();
    })
    $("#tbMaterial").on('click', 'tbody .Eliminar', function () {
        var data = table.row($(this).parents("tr")).data();
        $("#hdfId").val(data.IdMaterial);
        var Nombre = $(this).parents("tr").find("td").eq(2).text();
        $("#Nombre").text(Nombre);

        Swal.fire({
            title: 'Eliminar',
            text: '¿Desea eliminar el registro ' + Nombre + ' ?',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Confirmar',
            cancelButtonColor: '#d33',
            cancelButtonText: 'Cancelar',
            showLoaderOnConfirm: true,
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
                    data: { Id: $("#hdfId").val(), IdFlag: 1 },
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
                        $("#tbMaterial").DataTable().ajax.reload();


                    }
                });

            }
        })
    });

    $("#btnNuevo").click(function () {
        Limpiar();
        Lista.CargarCodigo();
    });
});

var Obtener = function (Id) {
    var senData = {
        Id: Id
    }
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Mantenimiento/ListaEditMaterial'),
        dataType: 'json',
        data: senData,
        beforeSend: General.Utils.StartLoading,
        complete: General.Utils.EndLoading,
        success: function (response) {
            //console.log(response)
            $("#hdfId").val(response.IdMaterial);
            $("#txtNombre").val(response.Nombre);
            $("#lstLinea").val(response.Id);


            $("#txtCodigo").val(response.Codigo);
            $("#txtNombre").val(response.Nombre);
            $("#txtDescripcion").val(response.Descripcion);
            $("#txtPrecioComp").val(response.PrecioCompra);
            $("#txtPrecioVent").val(response.PrecioVenta);
            $("#txtDescuento").val(response.Descuento);
            $("#lstUnidad").val(response.Unidad.IdUnidad);
            $("#lstSubCategoria").val(response.SubCateoria.IdSubCategoira);
            $("#lstGenero").val(response.genero.IdGenero);
            $("#lstTemporada").val(response.Temporada.IdTemporada);
            $("#lstTalla").val(response.Talla.IdTalla);
            $("#txtPrecioDocena").val(response.PrecioDocena);
            $("#txtPrecioCaja").val(response.PrecioCaja);

            if (response.Id == 1) {
                $("#idMercaderiacategoria").show();
                $("#idmercaderiaGe").show();
                $("#idMercaderiaTemp").show();
                $("#idMercaderiaTalla").show();
            } else {
                $("#idMercaderiacategoria").hide();
                $("#idmercaderiaGe").hide();
                $("#idMercaderiaTemp").hide();
                $("#idMercaderiaTalla").hide();
            }

            var tipo = response.Etipo.IdTipo
            var marca = response.Marca.IdMarca
            var categoria = response.Categoria.IdCateogira
            var color = response.EColor.IdColor
            var modelo = response.Modelo.IdModelo
            //*tipó*/
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListaComboId"),
                dataType: 'json',
                data: { flag: 3, Id: response.Id },
                success: function (response) {
                    $("#lsTipo").empty();
                    $("#lsTipo").append($('<option>', { value: 0, text: 'Seleccione' }));
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                        $.grep(response, function (oDocumento) {

                            $('select[name="lsTipo"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                        });
                    }
                    $("#lsTipo").val(tipo)
                }

            });

            /*marca*/
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListaComboId"),
                dataType: 'json',
                data: { flag: 4, Id: response.Id },
                success: function (response) {
                    $("#lstMarca").empty();
                    $("#lstMarca").append($('<option>', { value: 0, text: 'Seleccione' }));
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                        $.grep(response, function (oDocumento) {

                            $('select[name="lstMarca"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                        });
                    }
                    $("#lstMarca").val(marca)
                }

            });
            /*categoria*/

            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListaComboId"),
                dataType: 'json',
                data: { flag: 5, Id: response.Id },
                success: function (response) {

                    $("#lstCategoria").empty();
                    $("#lstCategoria").append($('<option>', { value: 0, text: 'Seleccione' }));
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                        $.grep(response, function (oDocumento) {

                            $('select[name="lstCategoria"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                        });
                    }
                    $("#lstCategoria").val(categoria)
                }

            });
            /*corlor*/
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListaComboId"),
                dataType: 'json',
                data: { flag: 6, Id: response.Id },
                success: function (response) {
                    //console.log(response)
                    $("#lstColor").empty();
                    $("#lstColor").append($('<option>', { value: 0, text: 'Seleccione' }));
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                        $.grep(response, function (oDocumento) {
                            $('select[name="lstColor"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                        });
                    }
                    $("#lstColor").val(color);
                }

            });
            /*modelo*/
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListaComboId"),
                dataType: 'json',
                data: { flag: 7, Id: response.Id },
                success: function (response) {
                    $("#lstModelo").empty();
                    $("#lstModelo").append($('<option>', { value: 0, text: 'Seleccione' }));
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                        $.grep(response, function (oDocumento) {
                            $('select[name="lstModelo"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                        });
                    }
                    $("#lstModelo").val(modelo);
                }

            });
        }

    });
}


function Limpiar() {
    $("#hdfId").val(0);
    $("#lstTipo").val('0');
    $("#txtNombre").val('');
    $("#lsTipo").val('0');
    $("#lstCategoria").val('0');
    $("#lstSubCategoria").val('0');
    $("#lstUnidad").val('0');
    $("#lstGenero").val('0');
    $("#lstModelo").val('0');
    $("#lstColor").val('0');
    $("#lstTemporada").val('0');
    $("#lstTalla").val('0');
    $("#lstMarca").val('0');
    $("#txtModelo").val('');
    $('#txtPrecioComp').val('0.00');
    $('#txtPrecioVent').val('0.00');
    $('#txtDescuento').val('0.00');
    $('#txtPrecioDocena').val('0.00');
    $('#txtPrecioCaja').val('0.00');
}

function cargarTipo(id) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("Shared/ListaComboId"),
        dataType: 'json',
        data: { flag: 3, Id: id },
        success: function (response) {
            $("#lsTipo").empty();
            $("#lsTipo").append($('<option>', { value: 0, text: 'Seleccione' }));
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                $.grep(response, function (oDocumento) {

                    $('select[name="lsTipo"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                });
            }
        }

    });
}
function ListaMarca(id) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("Shared/ListaComboId"),
        dataType: 'json',
        data: { flag: 4, Id: id },
        success: function (response) {
            $("#lstMarca").empty();
            $("#lstMarca").append($('<option>', { value: 0, text: 'Seleccione' }));
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                $.grep(response, function (oDocumento) {

                    $('select[name="lstMarca"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                });
            }
        }

    });
}
function ListCategoria(id) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("Shared/ListaComboId"),
        dataType: 'json',
        data: { flag: 5, Id: id },
        success: function (response) {

            $("#lstCategoria").empty();
            $("#lstCategoria").append($('<option>', { value: 0, text: 'Seleccione' }));
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                $.grep(response, function (oDocumento) {

                    $('select[name="lstCategoria"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                });
            }
        }

    });
}
function ListaColor(id) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("Shared/ListaComboId"),
        dataType: 'json',
        data: { flag: 6, Id: id },
        success: function (response) {
            //console.log(response)
            $("#lstColor").empty();
            $("#lstColor").append($('<option>', { value: 0, text: 'Seleccione' }));
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                $.grep(response, function (oDocumento) {
                    $('select[name="lstColor"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                });
            }
        }

    });
}
function ListaModelo(id) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("Shared/ListaComboId"),
        dataType: 'json',
        data: { flag: 7, Id: id },
        success: function (response) {
            $("#lstModelo").empty();
            $("#lstModelo").append($('<option>', { value: 0, text: 'Seleccione' }));
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                $.grep(response, function (oDocumento) {
                    $('select[name="lstModelo"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                });
            }
        }

    });

}

function CallCantidad() {
    var Total = 0;
    $.grep(CodigoEnviar, function (oDetalle) {
        Total = Total + oDetalle["cantidad"]

    })
    $('#Totales').html(Total)
    $("#IdTotales").val(Total)
}
function BuscarIndexDetalleEnTabla(id) {
    for (var i = 0; i < CodigoEnviar.length; i += 1) {
        if (CodigoEnviar[i]["Id"] == id) {
            return i;
        }
    }
    return -1;
}
