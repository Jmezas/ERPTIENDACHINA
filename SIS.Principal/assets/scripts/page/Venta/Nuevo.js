
var Lista = {
    //CargarSerieDoc: function () {

    //    $.ajax({
    //        async: true,
    //        type: 'post',
    //        url: General.Utils.ContextPath("venta/SerieNumero"),
    //        dataType: 'json',
    //        data: { documento: 1 },
    //        success: function (response) {

    //            $("#txtSerie").val(response.Text)
    //            $("#txtNumero").val(response.Nombre)
    //        }
    //    });
    //},
    CargarCombo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 6 },
            success: function (response) {

                $("#lstTipoDoc").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstTipoDoc"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    CargarTipoPago: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 9 },
            success: function (response) {

                $("#lstTipoPago").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstTipoPago"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });

                    $("#lstTipoPago").val(3)
                }
            }

        });
    },
    CargarMoneda: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 10 },
            success: function (response) {
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstMoneda"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    CargarOperacion: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 16 },
            success: function (response) {
                $('select[name="lstOperacion"]').empty();
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstOperacion"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    CargarTipoDoc: function () {
        var sucursal = $("#sucursal").val();
        console.log(sucursal)
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaComboId"),
            dataType: 'json',
            data: { flag: 9, Id: sucursal },
            success: function (response) {
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstDocumento"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
                changeDoc($("#lstDocumento").val())
            }

        });
    },
    CargarPago: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 19 },
            success: function (response) {

                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error  
                    $.grep(response, function (oDocumento) {
                        $('select[name="lstPago"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    CargarAlmacen: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaComboSucursal"),
            dataType: 'json',
            success: function (response) {

                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstAlmacen"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
            }

        });
    },
    Vars: {
        Detalle: []
    },

}


$(function () {
    //Lista.CargarSerieDoc();
    Lista.CargarCombo();
    Lista.CargarTipoPago();
    Lista.CargarMoneda();
    Lista.CargarTipoDoc();
    Lista.CargarAlmacen();

    Lista.CargarPago(); //cargar pago modal cobrar

    $('input[name="descuentoPor"]').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('input[name="descuento"]').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('input[name="txtPrecio"]').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('input[name="txtCantidad"]').Validate({ type: TypeValidation.Numeric });



    $('#dFechaAten').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });

    $('#lstPago').select2({
        dropdownParent: $('#ModalCobrar')
    });


    $("#dFechaAten").datepicker().datepicker("setDate", new Date());

    $('#dFechaPago').datepicker({
        dateFormat: 'dd/mm/yy',
    });
    $("#dFechaPago").datepicker().datepicker("setDate", new Date());


    $("#lstMoneda").change(function () {
        $("#hMonedaComprobante").html($("#lstMoneda option:selected").text());
    });

    $("#lstTipoPago").change(function () {
        var TipoPago = parseInt($("#lstTipoPago").val())

        var fecha = $('#dFechaPago').val().split(/\//);
        fecha = [fecha[1], fecha[0], fecha[2]].join('/');


        switch (TipoPago) {
            case 1:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(15);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)

                break;
            case 2:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(30);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            case 3:


                $("#dFechaPago").datepicker().datepicker("setDate", new Date());
                break;
            case 4:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(120);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            case 5:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(60);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            case 6:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(90);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            case 7:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(45);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            case 8:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(7);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            case 9:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(30);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            case 10:
                var TuFecha = new Date();
                //dias a sumar
                var dias = parseInt(15);
                //nueva fecha sumada
                TuFecha.setDate(TuFecha.getDate() + dias);
                //formato de salida para la fecha
                var FechaGold = ((TuFecha.getDate() < 10 ? '0' + TuFecha.getDate() : TuFecha.getDate())) + '/' +
                    ((TuFecha.getMonth() + 1) < 10 ? '0' + (TuFecha.getMonth() + 1) : (TuFecha.getMonth() + 1)) + '/' + TuFecha.getFullYear();

                $("#dFechaPago").val(FechaGold)
                break;
            default:
            // code block
        }
    });

    $("#txtDocCli").autocomplete({
        source: function (request, response) {
            if ($("#lstTipoDoc").val() == 0) {
                General.Utils.ShowMessage(TypeMessage.Warning, 'Seleccione Documento');
            } else {
                var vRuc = $("#txtDocCli").val();
                var Tipodoc = $("#lstTipoDoc").val();
                $.ajax({
                    url: General.Utils.ContextPath('Shared/FiltroProvCli'),
                    type: "POST",
                    dataType: "json",
                    data: { filtro: vRuc, Tipo: Tipodoc, flag: 2 },
                    success: function (data) {
                        response($.map(data, function (item) {
                            return {
                                id: item.Id,
                                cod: item.Text,
                                label: item.Text + ' - ' + item.Nombre,
                                des: item.Nombre + '|' + item.Dir
                            };
                        }));
                    }
                });
            }
        },
        minLength: 2,
        select: function (event, ui) {
            $('#hdfId').val(ui.item.id);
            $("#txtDocCli").val(ui.item ? ui.item.cod : $("#txtDocCli").val());
            $('#hdfId').val(ui.item.id);
            console.log($('#hdfId').val());

        },
        change: function (event, ui) {
            $("#txtDocCli").val(ui.item ? ui.item.cod : jQuery("#txtDocCli").val());

            $("#txtNomCli").val(ui.item ? ui.item.des.toString().split('|')[0] : '');
            $("#txtDireccionInicio").val(ui.item ? ui.item.des.toString().split('|')[1] : '');
        }
    });

    //material
    $("#txtProducto").autocomplete({
        source: function (request, response) {

            var filtro = $("#txtProducto").val();
            $.ajax({
                url: General.Utils.ContextPath('Shared/FiltroProducto'),
                type: "POST",
                dataType: "json",
                data: { filtro: filtro },
                success: function (data) {
                    response($.map(data, function (item) {
                        return {
                            id: item.IdMaterial,
                            cod: item.Nombre,
                            label: item.Nombre + ' - ' + item.Codigo,
                            des: item.Nombre + '|' + item.Codigo + '|' + item.PrecioCompra + '|' + item.PrecioVenta
                                + '|' + item.PrecioUnidad + '|' + item.PrecioDocena + '|' + item.PrecioCaja,
                            cat: item.Categoria.Nombre,
                            mar: item.Marca.Nombre,
                            und: item.Unidad.Nombre
                        };
                    }));
                }
            });

        },
        minLength: 2,
        select: function (event, ui) {
            $('#IdProducto').val(ui.item.id);
            $("#txtProducto").val(ui.item ? ui.item.cod : $("#txtProducto").val());
            $('#IdProducto').val(ui.item.id);


        },
        change: function (event, ui) {
            console.log(ui)
            $("#txtProducto").val(ui.item ? ui.item.cod : jQuery("#txtProducto").val());
            $("#sCodigo").val(ui.item ? ui.item.des.toString().split('|')[1] : '');
            $("#txtPrecio").val(ui.item ? ui.item.des.toString().split('|')[3] : '');
            $("#unidad").val(ui.item ? ui.item.des.toString().split('|')[4] : '');
            $("#docena").val(ui.item ? ui.item.des.toString().split('|')[5] : '');
            $("#caja").val(ui.item ? ui.item.des.toString().split('|')[6] : '');
            $("#sCategoria").val(ui.item ? ui.item.cat.toString() : '');
            $("#sMarca").val(ui.item ? ui.item.mar.toString() : '');
            $("#sUnidad").val(ui.item ? ui.item.und.toString() : '');
            $("#txtCantidad").val('');
            $("#txtTotal").val('');
            $("#txtDescuentoPor").val('');
            $("#txtDescuento").val('');
            $("#txtCantidad").focus();
            StockActual($("#IdProducto").val(), $("#lstAlmacen").val())
        }
    });

    //Material
    var table = ""
    $("#btnbuscar").click(function () {

        table = $("#tbMaterial").DataTable({
            select: true,
            pageLength: 10,
            processing: true,
            serverSide: true,
            filter: true,
            bSort: true,
            orderMulti: false,
            destroy: true,
            language: {
                "lengthMenu": "Mostrar _MENU_ registros por p&aacute;gina",
                "zeroRecords": "No se encontraron datos.",
                "info": "Mostrando la p&aacute;gina _PAGE_ de _PAGES_",
                "infoEmpty": "No hay registros disponibles",
                "infoFiltered": "(filtrando _MAX_ total de registros)",
                "search": "Buscar:",
                "paging": "false",

                "paginate": {
                    "first": "Primero",
                    "previous": "Anterior",
                    "next": "Siguiente",
                    "last": "&Uacute;timo"
                },
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
                { "data": "IdMaterial", "name": "IdMaterial" },
                { "data": "Codigo", "name": "Codigo" },
                { "data": "Nombre", "name": "Nombre" },
                { "data": "Unidad.Nombre", "name": "Unidad" },
                { "data": "Marca.Nombre", "name": "Marca" },
                { "data": "Modelo.Nombre", "name": "Modelo" },

            ],
            buttons: [],
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
    });


    $("#lstDocumento").change(function () {
        changeDoc($("#lstDocumento").val())
    })

    $('#tbMaterial tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');


    });
    ///
    $("#txtCantidad").on('change', function () {
        var descuento = $("#txtDescuento").val();
        var descuentoPorcentaje = $("#txtDescuentoPor").val();
        let Precio = $('#txtPrecio').val();
        let Cantidad = $('#txtCantidad').val();
        let Total = $('#txtTotal').val();

        Total = Total * 1
        Total = Precio * Cantidad
        descuento = (Precio * Cantidad * descuentoPorcentaje) / 100;


        $("#txtDescuento").val(descuento.toFixed(2));
        $("#txtTotal").val(Total.toFixed(2));
        $("#txtDescuentoPor").val(0.00);
    });

    $("#txtPrecio").on('change', function () {
        let Precio = $('#txtPrecio').val();
        let Cantidad = $('#txtCantidad').val();
        let Total = $('#txtTotal').val();

        Total = Precio * Cantidad;

        $('#txtTotal').val(Total.toFixed(2));
        $("#txtreal").val(Precio);
        $("#hdfprecio").val(Precio);
        $("#txtDescuentoPor").val(0.00);
    });

    $('#txtDescuentoPor').on('change', function () {
        var descuento = $("#txtDescuento").val();
        var descuentoPorcentaje = $("#txtDescuentoPor").val();
        var precio = $("#txtPrecio").val();
        var cantidad = $("#txtCantidad").val();
        var Total = $("#hdf_TotalImporte").val();

        var resta = 0;
        var diferenciaDescuento = 0;
        var desUnidad = 0;

        diferenciaDescuento = (precio * descuentoPorcentaje) / 100;

        desUnidad = precio - diferenciaDescuento
        Total = Total * 1
        Total = desUnidad * cantidad;
        descuento = (precio * cantidad) - Total;


        $("#txtDescuento").val(descuento.toFixed(2));
        $("#hdfprecio").val(desUnidad.toFixed(2));
        $("#txtTotal").val(Total.toFixed(2));

    });

    $('#txtDescuento').on('change', function () {
        var descuento = $("#txtDescuento").val();
        var descuentoPorcentaje = $("#txtDescuentoPor").val();
        var precio = $("#txtPrecio").val();
        var cantidad = $("#txtCantidad").val();


        var resta = 0;
        var Total = 0;
        var descuentoMonto = 0;

        var desUnidad = 0;


        descuentoPorcentaje = (descuento * 100) / (precio * cantidad);

        descuentoMonto = (precio * descuentoPorcentaje) / 100;

        desUnidad = precio - descuentoMonto;

        // = Total * 1
        Total = desUnidad * cantidad;
        // resta = Total - descuento;

        //alert(Total);   
        //alert(descuento);
        $("#hdfprecio").val(desUnidad.toFixed(2));
        $("#txtDescuentoPor").val(descuentoPorcentaje.toFixed(2));
        $("#txtTotal").val(Total.toFixed(2));
    });

    ///agregar del modal productos
    $("#btnAceptar").click(function () {
        var form = this;

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
            //console.log(rowId);

            VerificarStock(rowId, 1, $("#lstAlmacen").val(), function (errorLanzado, datosDevueltos) {

                if (errorLanzado) // Ha habido un error, deberías manejarlo :/
                {
                    General.Utils.ShowMessage(TypeMessage.Error, 'Algo salio mal verificar');
                    return;
                }
                if (datosDevueltos.Id == 'error') {
                    General.Utils.ShowMessage(datosDevueltos.Id, datosDevueltos.Message);
                } else {
                    Obtener(rowId)
                }
            })

        });


        setTimeout(function () { Lista.CargarOperacion(); }, 1000);
        // Eliminar elementos añadidos
        $('input[name="id\[\]"]', form).remove();

    });

    $("#lstAlmacen").change(function () {

        Lista.Vars.Detalle.map(function (data) {
            data.Almacen.IdAlmacen = $("#lstAlmacen").val()
        });
        StockActual($("#IdProducto").val(), $("#lstAlmacen").val())

    });
    $("#lstPrecio").change(function () {
        var IdPrecio = $("#lstPrecio").val();
        var unida = $("#unidad").val();
        var caja = $("#caja").val();
        var docena = $("#docena").val();

        if (IdPrecio == 1) {
            $("#txtPrecio").val(unida);
        } else if (IdPrecio == 2) {
            $("#txtPrecio").val(docena);
        } else if (IdPrecio == 3) {
            $("#txtPrecio").val(caja);
        }
    })

    $("#btnAgregar").click(function () {

        var iIdProducto = parseInt($("#IdProducto").val());
        var producto = $("#txtProducto").val()
        if ($("#txtProducto").val() == "") {
            General.Utils.ShowMessage(TypeMessage.Error, 'No se a cargado el producto');
        } else if ($("#txtCantidad").val() == "") {
            General.Utils.ShowMessage(TypeMessage.Error, 'Debe ingresar la cantidad');
        } else if (BuscarDetalleEnTabla(iIdProducto)) {
            General.Utils.ShowMessage(TypeMessage.Error, `El producto  ${producto} ya existe en la tabla`);
        } else {

            Lista.Vars.Detalle.push({
                Material: {
                    IdMaterial: $("#IdProducto").val(),
                    Codigo: $("#sCodigo").val(),
                    Nombre: $("#txtProducto").val(),
                },
                Categoria: $("#sCategoria").val(),
                Marca: $("#sMarca").val(),
                Unidad: $("#sUnidad").val(),
                Cantidad: $("#txtCantidad").val(),
                Precio: $("#txtPrecio").val(),
                Importe: $("#txtTotal").val(),
                descuentopor: $("#txtDescuentoPor").val(),
                descuento: $("#txtDescuento").val(),
                operacion: 1,
                Almacen: { IdAlmacen: $("#lstAlmacen").val() },
                Sucursal: { IdSucursal: $("#lstSucursalCab").val() }
            });
            Lista.CargarOperacion();


            $("#IdProducto").val('0')
            $("#txtProducto").val('')
            $("#sCodigo").val('')
            $("#sCategoria").val('')
            $("#sMarca").val('')
            $("#sUnidad").val('')
            $("#txtCantidad").val('0.00')
            $("#txtPrecio").val('0.00')
            $("#txtDescuentoPor").val('0.00')
            $("#txtDescuento").val('0.00')
            $("#txtTotal").val('0.00')
            var $tb = $('#tbDetalle');
            $tb.find('tbody').empty();

            if (Lista.Vars.Detalle.length == 0) {
                $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
            } else {

                $.grep(Lista.Vars.Detalle, function (oDetalle) {
                    $tb.find('tbody').append(
                        '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                        '<td>' + oDetalle["Material"]["Codigo"] + '</td>' +
                        '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                        '<td>' + oDetalle["Unidad"] + '</td>' +
                        //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                        //'<td>' + '<input type="text" class="form-control por" id="descuentoPor" value="' + oDetalle["descuentopor"] + '">' + '</td>' +
                        '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
                        '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + formatNumber(oDetalle["Cantidad"]) + '">' + '</td>' +
                        '<td>' + '<input type="text" class="form-control Price" id="Precio" disabled value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                        '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
                        '<td class="text-center">' +
                        '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                        '</td>' +
                        '</tr>'

                    );
                });
            }


        }
        $("#lstOperacion option[value='1']").attr("selected", true)

        CalcularTotales();


    });

    //ACtulizar cantidad
    $('#tbDetalle').on('change', '.Cant', function () {
        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');
        //var Cantidad = 0;
        var input = Number($(this).val());;
        //console.log($(this).val())
        //$('input', row).each(function () {
        //    input = 
        //});
        Lista.Vars.Detalle.map(function (data) {
            if (parseInt(data.Material.IdMaterial) == parseInt(Id)) {
                data.Cantidad = parseInt(input);
                data.Importe = (input * data.Precio) - (data.descuento * parseInt(input));
                data.descuento = data.descuento;
                data.descuentopor = 0;
            }
        })

        Lista.CargarOperacion();
        CalcularTotales();
        var $tb = $('#tbDetalle');
        $tb.find('tbody').empty();
        if (Lista.Vars.Detalle.length == 0) {
            $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
        } else {

            $.grep(Lista.Vars.Detalle, function (oDetalle) {
                $tb.find('tbody').append(
                    '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                    '<td>' + oDetalle["Material"]["Codigo"] + '</td>' +
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    //'<td>' + '<input type="text" class="form-control por" id="descuentoPor" value="' + oDetalle["descuentopor"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '" disabled>' + '</td>' +
                    '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'
                );
            });
        }
    });

    //Actualizar Precio
    $('#tbDetalle').on('change', '.Price', function () {
        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');

        var input = Number($(this).val());
        Lista.Vars.Detalle.map(function (data) {
            if (parseInt(data.Material.IdMaterial) == parseInt(Id)) {
                data.Importe = (input * data.Cantidad) * 1;
                data.Precio = parseFloat(input);
                data.descuento = 0;
                data.descuentopor = 0;
            }
        });

        CalcularTotales();
        Lista.CargarOperacion();
        var $tb = $('#tbDetalle');
        $tb.find('tbody').empty();
        if (Lista.Vars.Detalle.length == 0) {
            $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
        } else {

            $.grep(Lista.Vars.Detalle, function (oDetalle) {
                $tb.find('tbody').append(
                    '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                    '<td>' + oDetalle["Material"]["Codigo"] + '</td>' +
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    //'<td>' + '<input type="text" class="form-control por" id="descuentoPor" value="' + oDetalle["descuentopor"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '" disabled>' + '</td>' +
                    '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'

                );
            });
        }
    });
    //Actulizar descuento %
    //$('#tbDetalle').on('change', '.por', function () {
    //    var row = $(this).closest('tr');
    //    var Id = $(this).closest('tr').attr('data-index');
    //    ///
    //    var descuento = $("#txtDescuento").val();
    //    var descuentoPorcentaje = $("#txtDescuentoPor").val();
    //    var precio = $("#txtPrecio").val();
    //    var cantidad = $("#txtCantidad").val();
    //    var Total = $("#hdf_TotalImporte").val();

    //    var resta = 0;
    //    var diferenciaDescuento = 0;
    //    var desUnidad = 0;

    //    diferenciaDescuento = (precio * descuentoPorcentaje) / 100;

    //    desUnidad = precio - diferenciaDescuento
    //    Total = Total * 1
    //    Total = desUnidad * cantidad;
    //    descuento = (precio * cantidad) - Total;


    //    $("#txtDescuento").val(descuento.toFixed(2));
    //    $("#hdfprecio").val(desUnidad.toFixed(2));
    //    $("#txtTotal").val(Total.toFixed(2));


    //    ////

    //    var diferenciaDescuento = 0;
    //    var desUnidad = 0; 




    //    var input = Number($(this).val());
    //    Lista.Vars.Detalle.map(function (data) {
    //        if (parseInt(data.Material.IdMaterial) == parseInt(Id)) {

    //            diferenciaDescuento = (data.Precio * input) / 100;
    //            desUnidad = data.Precio - diferenciaDescuento

    //            data.Importe = (desUnidad * data.Cantidad);
    //            data.descuento = (data.Precio * data.Cantidad) - data.Importe;
    //            data.descuentopor = input;

    //        }
    //    })
    //    Lista.CargarOperacion();
    //    CalcularTotales();
    //    var $tb = $('#tbDetalle');
    //    $tb.find('tbody').empty();
    //    if (Lista.Vars.Detalle.length == 0) {
    //        $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
    //    } else {

    //        $.grep(Lista.Vars.Detalle, function (oDetalle) {
    //            $tb.find('tbody').append(
    //                '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
    //                '<td>' + oDetalle["Material"]["Codigo"] + '</td>' +
    //                '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
    //                '<td>' + oDetalle["Unidad"] + '</td>' +
    //                '<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
    //                //'<td>' + '<input type="text" class="form-control por" id="descuentoPor" value="' + oDetalle["descuentopor"] + '">' + '</td>' +
    //                '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
    //                '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
    //                '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '">' + '</td>' +
    //                '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
    //                '<td class="text-center">' +
    //                '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
    //                '</td>' +
    //                '</tr>'

    //            );
    //        });
    //    }
    //});

    $('#tbDetalle').on('change', '.desc', function () {
        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');
        var descuentoPorcentaje = 0;
        var descuentoMonto = 0;
        var desUnidad = 0;
        var input = Number($(this).val());

        Lista.Vars.Detalle.map(function (data) {
            if (parseInt(data.Material.IdMaterial) == parseInt(Id)) {
                descuentoPorcentaje = (input * 100) / (data.Precio * data.Cantidad);
                descuentoMonto = (data.Precio * descuentoPorcentaje) / 100;
                desUnidad = data.Precio - descuentoMonto
                data.Importe = (desUnidad * data.Cantidad);
                data.descuento = input;
                data.descuentopor = descuentoPorcentaje;
            }
        });

        Lista.CargarOperacion();
        CalcularTotales();
        var $tb = $('#tbDetalle');
        $tb.find('tbody').empty();
        if (Lista.Vars.Detalle.length == 0) {
            $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
        } else {

            $.grep(Lista.Vars.Detalle, function (oDetalle) {
                $tb.find('tbody').append(
                    '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                    '<td>' + oDetalle["Material"]["Codigo"] + '</td>' +
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    //'<td>' + '<input type="text" class="form-control por" id="descuentoPor" value="' + oDetalle["descuentopor"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '" disabled>' + '</td>' +
                    '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'

                );
            });
        }
    });

    $('#tbDetalle').on('change', '.select', function () {
        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');

        var input = Number($(this).val());
        console.log(input)
        Lista.Vars.Detalle.map(function (data) {
            if (parseInt(data.Material.IdMaterial) == parseInt(Id)) {
                data.operacion = input
            }
        })
        CalcularTotales();
    });

    $('#tbDetalle').find('tbody').on('click', '.btn-danger', function () {
        var $btn = $(this);
        var $tb = $('#tbDetalle');

        var Id = $btn.closest('tr').attr('data-index');

        BuscarIndexDetalleEnTabla(Id);
        arrDetalle = Lista.Vars.Detalle.filter(function (x) {
            return x.Material.IdMaterial != Id;
        });

        Lista.Vars.Detalle = [];
        Lista.Vars.Detalle = arrDetalle;

        $tb.find('tbody').empty();
        if (Lista.Vars.Detalle.length == 0) {
            $tb.find('tbody').append('<tr><td colspan="9">No existen registros</td></tr>')

        } else {
            $.grep(Lista.Vars.Detalle, function (oDetalle) {
                $tb.find('tbody').append(
                    '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                    '<td>' + oDetalle["Material"]["Codigo"] + '</td>' +
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    //'<td>' + '<input type="text" class="form-control por" id="descuentoPor" value="' + oDetalle["descuentopor"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '" disabled>' + '</td>' +
                    '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'
                );
            })
        }
        Lista.CargarOperacion();
        CalcularTotales();

    });

    //guardar

    $("#btnProcesar").click(function () {

        numero = 0
        $("#txtrecibida").val('0.00');
        $("#txtCambio").val('0.00');
        $("#txtCostoEnvio").val('0.00');

        $("#txtPago").val($("#Totales").val());

        $("#lstPago").val(1);

    });

    //datos del modal procesar venta
    var numero = 0;
    $("#btn10").click(function () {

        numero += 10;
        var total = $("#Totales").val();
        $("#txtrecibida").val(formatNumber(numero))
        var mostrarPago = numero - total
        $("#txtCambio").val(formatNumber(mostrarPago))

    })
    $("#btn20").click(function () {

        numero += 20;
        var total = $("#Totales").val();
        $("#txtrecibida").val(formatNumber(numero))
        var mostrarPago = numero - total
        $("#txtCambio").val(formatNumber(mostrarPago))
    })
    $("#btn50").click(function () {

        numero += 50;
        var total = $("#Totales").val();
        $("#txtrecibida").val(formatNumber(numero))
        var mostrarPago = numero - total
        $("#txtCambio").val(formatNumber(mostrarPago))
    })
    $("#btn100").click(function () {
        numero += 100;
        var total = $("#Totales").val();
        $("#txtrecibida").val(formatNumber(numero))
        var mostrarPago = numero - total
        $("#txtCambio").val(formatNumber(mostrarPago))
    })

    $("#btnLimpiar").click(function () {
        numero = 0
        $("#txtrecibida").val('0.00')
        $("#txtCambio").val('0.00')
    })

    $("#txtrecibida").change(function () {
        numero = parseFloat($("#txtrecibida").val())
        var total = $("#Totales").val();
        var mostrarPago = numero - total
        $("#txtCambio").val(mostrarPago)

    });
    // fin de los datos del modal procesar venta

    $("#btnGuardar").click(function () {
        if ($("#hdfId").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Ingrese el cliente');
        } else if (Lista.Vars.Detalle.length <= 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'No se ha ingresado el detalle');
        } else if ($("#txtrecibida").val() == '0.00') {
            General.Utils.ShowMessage(TypeMessage.Error, 'Ingrese monto a cobrar');
        } else {
            var oDatos = {
                cliente: { IdCliente: $("#hdfId").val() },
                Comprobante: { Id: $("#lstDocumento").val() },
                moneda: { IdMoneda: $("#lstMoneda").val() },
                Documento: { IdDocumento: $("#lstTipoPago").val() },
                fechaEmision: $("#dFechaAten").val(),
                fechaPago: $("#dFechaPago").val(),
                serie: $("#txtSerie").val(),
                numero: $("#txtNumero").val(),
                cantidad: $("#mCant").html(),
                grabada: $("#subtotales").val(),
                inafecta: $("#inafecta").val(),
                exonerada: $("#exonerada").val(),
                gratuita: $("#gratuita").val(),
                igv: $("#igv").val(),
                total: $("#Totales").val(),
                descuento: $("#descuento").val(),
                cambio: 3.5,
                observacion: $("#txtObservacion").val(),


                montoRecibido: $("#txtrecibida").val(),
                vuelto: $("#txtCambio").val(),
                metodoPago: $("#lstPago").val(),
                NotaPago: $("#txtNotaPago").val(),
                CostoEnvio: $("#txtCostoEnvio").val(),
            }
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Venta/InstRegistrarVenta'),
                dataType: 'json',
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                data: { oDatos: oDatos, Detalle: Lista.Vars.Detalle },
                success: function (response) {
                    if (response.Id == 'success') {
                        $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                        Swal.fire({
                            title: 'title',
                            html: response.Message,
                            icon: response.Id,
                            showCancelButton: true,

                            confirmButtonText: 'ok'
                        }).then((result) => {
                            if (result.isConfirmed) {
                                document.getElementById('reporte').click();
                                var URL = General.Utils.ContextPath('Venta/imprimirFacBol?Id=' + response.Additionals[0] + "&Envio=" + 0 + "&venta=" + 1);
                                fileDownnload(URL);
                            }
                        })
                        LimpiarConteniedo();
                    }
                    else {
                        Swal.fire(
                            'Alerta!',
                            response.Message,
                            response.Id
                        )
                    }
                }
            });
        }

    });



    $("#limpiar").click(function () {

        LimpiarConteniedo();
    })

})
function fileDownnload(url) {
    var options = {
        height: "600px",
        page: '2'

    };
    PDFObject.embed(url, "#PDFViewer", options);
    // $("#myReportPrint").modal('show');

}
function CalcularTotales() {
    var TotalValorVta = ImpTotalVta = Igv = TotalSub = Cantidad = exonerada = inafecta = grabada = gratuita = 0;
    var totalfac = 0;
    var desc = 0;
    if ($("#CalIGV").prop('checked')) {
        $.grep(Lista.Vars.Detalle, function (oDetalle) {
            if (oDetalle["operacion"] === 1) {
                TotalValorVta = oDetalle["Importe"];  //TOTAL
                TotalValorVta = TotalValorVta * 1
                grabada += TotalValorVta

                var SubTotalCal = TotalValorVta / (18 / 100 + 1)//Subtotal
                SubTotalCal = SubTotalCal * 1;
                ImpTotalVta += SubTotalCal


            } else if (oDetalle["operacion"] === 2) {//inafecta
                TotalValorVta = oDetalle["Importe"];  //TOTAL
                TotalValorVta = TotalValorVta * 1
                exonerada += TotalValorVta



            } else if (oDetalle["operacion"] === 3) {//exonerada
                TotalValorVta = oDetalle["Importe"];  //TOTAL
                TotalValorVta = TotalValorVta * 1;
                inafecta += TotalValorVta;


            }
            totalfac = grabada + exonerada + inafecta;
            Cantidad += parseInt(oDetalle["Cantidad"]);
            desc += parseInt(oDetalle["descuento"] * parseInt(oDetalle["Cantidad"]));

        });

        IgvSuma = grabada - ImpTotalVta;


        $('#Montodescuento').html(formatNumber(desc)); // descuento
        $('#MontoSubtotal').html(formatNumber(ImpTotalVta)); // sub total 
        $('#MontoCalulado').html(formatNumber(totalfac));//esto! // Total
        $('#MontoIGV').html(IgvSuma.toFixed(2)); // igv 
        $('#mCant').html(Cantidad);
        $('#subtotales').val(ImpTotalVta);
        $('#igv').val(IgvSuma);
        $('#Totales').val(totalfac);

        $('#Montoinafecta').html(formatNumber(exonerada));
        $('#Montoexonerada').html(formatNumber(inafecta));
        $('#exonerada').val(exonerada);
        $('#inafecta').val(inafecta);
        $('#gratuita').val(gratuita);
    }
    else {

        $.grep(Lista.Vars.Detalle, function (oDetalle) {
            var subTotal = oDetalle["Importe"];//sub total
            subTotal = subTotal * 1;
            TotalSub += subTotal;


            Cantidad += parseInt(oDetalle["Cantidad"]);
        });

        Igv = TotalSub * 0.18;
        Igv = Igv * 1;

        Total = TotalSub + Igv;

        $('#MontoSubtotal').html(TotalSub.toFixed(2)); // sub total
        $('#MontoCalulado').html(Total.toFixed(2));//esto! // Total
        $('#MontoIGV').html(Igv.toFixed(2)); // igv
        $('#mCant').html(Cantidad);

        $('#subtotales').val(TotalSub);
        $('#igv').val(Igv);
        $('#Totales').val(Total);
    }


}
function mostrarMensaje() {

    if ($("#CalIGV").prop('checked')) {
        General.Utils.ShowMessage(TypeMessage.Warning, 'Los precios incluyen IGV');

        CalcularTotales();

    } else {
        General.Utils.ShowMessage(TypeMessage.Warning, 'Los precios no incluyen IGV');

        CalcularTotales();
    }
}
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
        success: function (response) {
            console.log(response);

            if (BuscarDetalleEnTabla(response.IdMaterial)) {
                General.Utils.ShowMessage(TypeMessage.Error, `El producto  ${response.Nombre} ya existe en la tabla`);
            } else {

                Lista.Vars.Detalle.push({

                    Material: {
                        IdMaterial: response.IdMaterial,
                        Codigo: response.Codigo,
                        Nombre: response.Nombre,
                    },
                    Categoria: response.Categoria.Nombre,
                    Marca: response.Marca.Nombre,
                    Unidad: response.Unidad.Nombre,
                    Cantidad: 1,
                    Precio: response.PrecioVenta,
                    Importe: (parseFloat(response.PrecioVenta) * 1) - (response.Descuento * 1),
                    descuentopor: 0,
                    descuento: response.Descuento,
                    operacion: 1,
                    Almacen: { IdAlmacen: $("#lstAlmacen").val() },
                    Sucursal: { IdSucursal: $("#lstSucursalCab").val() }
                });

                CalcularTotales();

                var $tb = $('#tbDetalle');
                $tb.find('tbody').empty();

                if (Lista.Vars.Detalle.length == 0) {
                    $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
                } else {

                    $.grep(Lista.Vars.Detalle, function (oDetalle) {
                        $tb.find('tbody').append(
                            '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                            '<td>' + oDetalle["Material"]["Codigo"] + '</td>' +
                            '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                            '<td>' + oDetalle["Unidad"] + '</td>' +
                            //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"  value="' + oDetalle["operacion"] + '"></select>' + '</td>' +
                            //'<td>' + '<input type="text" class="form-control por" id="descuentoPor" value="' + oDetalle["descuentopor"] + '">' + '</td>' +
                            '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
                            '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                            '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '" disabled>' + '</td>' +
                            '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
                            '<td class="text-center">' +
                            '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                            '</td>' +
                            '</tr>'

                        );
                    });
                }
            }
        }

    });

    $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal.


}

function BuscarDetalleEnTabla(iIdProducto) {
    var bFound = false;
    $.each(Lista.Vars.Detalle, function (index, item) {
        if (item["Material"].IdMaterial == iIdProducto) {
            bFound = true;
            return false;
        }
    });
    return bFound;
}

function BuscarIndexDetalleEnTabla(id) {
    for (var i = 0; i < Lista.Vars.Detalle.length; i += 1) {
        if (Lista.Vars.Detalle[i]["Material"]["IdMaterial"] == id) {
            return i;
        }
    }
    return -1;
}


function LimpiarConteniedo() {

 
    $("#lstTipoDoc").val(0);
    $("#lstTipoPago").val(3)
    $("#txtDocCli").val('');
    $("#hdfId").val(0);
    $("#txtNomCli").val('');
    $("#txtDireccionInicio").val('');
    $("#MontoSubtotal").html('0.00');
    $("#subtotales").val(0);
    $("#mCant").html('0.00');
    $("#dFechaAten").datepicker().datepicker("setDate", new Date());
    $("#MontoIGV").html('0.00');
    $("#MontoIGV").html('0.00');
    $("#igv").val(0);
    $("#Montodescuento").html('0.00');
    $("#descuento").val(0);
    $("#Totales").val(0);
    $("#MontoCalulado").html('0.00');
    $("#hMonedaComprobante").html('SOLES');
    $("#MontoIGV").val();
    Lista.Vars.Detalle = [];
    var $tb = $('#tbDetalle');
    $tb.find('tbody').empty();
    if (Lista.Vars.Detalle.length == 0) {
        $tb.find('tbody').append('<tr><td colspan="11">No existen registros</td></tr>')
    }
    changeDoc($("#lstDocumento").val());

}
function changeDoc(doc) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("venta/SerieNumero"),
        dataType: 'json',
        data: { documento: doc },
        success: function (response) {
            $("#txtSerie").val(response.Text)
            $("#txtNumero").val(response.Nombre)
        }
    });
}

var VerificarStock = function (Producto, Cantidad, almacen, callback) {

    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Venta/ConsultaStock'),
        dataType: 'json',
        data: { idProducto: Producto, almacen: almacen, iCantidad: Cantidad },
        success: function (response) {
            callback(null, response)

        }
    });

}
function StockActual(Producto, almacen) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Shared/VisorStock'),
        dataType: 'json',
        data: { Material: Producto, Almacen: almacen },
        success: function (response) {
       
            $("#txtStock").val(response["Num"])
        }
    });

}