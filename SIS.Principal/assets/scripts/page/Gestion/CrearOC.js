
var Lista = {
    CargarSerieDoc: function () {

        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/GetSerieNum"),
            dataType: 'json',
            data: { Flag: 1 },
            success: function (response) {

                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error      

                    var lista = response.Nombre.split("-")
                    $("#txtSerie").val(lista[0])
                    $("#txtNumero").val(lista[1])
                }
            }
        });
    },
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
    Vars: {
        Detalle: []
    },

}


$(function () {
    Lista.CargarSerieDoc();
    Lista.CargarCombo();
    Lista.CargarTipoPago();
    Lista.CargarMoneda();
    $('input[name="txtPrecio"]').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('input[name="txtCantidad"]').Validate({ type: TypeValidation.Numeric });

    $('#dFechaPago').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });

    $("#dFechaPago").datepicker().datepicker("setDate", new Date());

    $('#dFechaAten').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });


    $("#dFechaAten").datepicker().datepicker("setDate", new Date());

    $("#dFechare").datepicker({ dateFormat: 'dd/mm/yy' }).datepicker("setDate", new Date());


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
    $("#lstMoneda").change(function () {
        $("#hMonedaComprobante").html($("#lstMoneda option:selected").text());
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
                    data: { filtro: vRuc, Tipo: Tipodoc, flag: 1 },
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
                            des: item.Nombre + '|' + item.Codigo + '|' + item.PrecioCompra + '|' + item.PrecioVenta,
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
            $("#txtProducto").val(ui.item ? ui.item.cod : jQuery("#txtProducto").val());
            $("#sCodigo").val(ui.item ? ui.item.des.toString().split('|')[1] : '');
            $("#txtPrecio").val(ui.item ? ui.item.des.toString().split('|')[2] : '');
            $("#sCategoria").val(ui.item ? ui.item.cat.toString() : '');
            $("#sMarca").val(ui.item ? ui.item.mar.toString() : '');
            $("#sUnidad").val(ui.item ? ui.item.und.toString() : '');
            $("#txtCantidad").focus();
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

    $('#tbMaterial tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');


    });

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
            Obtener(rowId)
        });

        // SOLO PARA DEMOSTRACIÓN
        // El siguiente código no es necesario en producción

        // Salida de datos de formulario a una consola
        $('#example-console-rows').text(rows_selected.join(","));

        // Salida de datos de formulario a una consola
        $('#example-console-form').text($(form).serialize());

        // Eliminar elementos añadidos
        $('input[name="id\[\]"]', form).remove();

    });

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
                Importe: (parseFloat($("#txtPrecio").val()) * parseFloat($("#txtCantidad").val())).toFixed(2)
            });
            CalcularTotales();
            $("#IdProducto").val('')
            $("#txtProducto").val('')
            $("#sCodigo").val('')
            $("#sCategoria").val('')
            $("#sMarca").val('')
            $("#sUnidad").val('')
            $("#txtCantidad").val('')
            $("#txtPrecio").val('')
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
                        '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                        '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '">' + '</td>' +
                        '<td>' + oDetalle["Importe"] + '</td>' +
                        '<td class="text-center">' +
                        '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                        '</td>' +
                        '</tr>'

                    );
                });
            }
        }
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

                data.Importe = input * data.Precio;
                data.Cantidad = parseInt(input);
                //  data.Precio = Precio;
            }
        })


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
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '">' + '</td>' +
                    '<td>' + oDetalle["Importe"] + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'

                );
            });
        }
    });

    //Actulizar Precio
    $('#tbDetalle').on('change', '.Price', function () {
        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');

        var input = Number($(this).val());
        Lista.Vars.Detalle.map(function (data) {
            if (parseInt(data.Material.IdMaterial) == parseInt(Id)) {
                data.Importe = (input * data.Cantidad) * 1;
                data.Precio = parseFloat(input);
            }
        })
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
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '">' + '</td>' +
                    '<td>' + oDetalle["Importe"] + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'

                );
            });
        }
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
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '">' + '</td>' +
                    '<td>' + oDetalle["Importe"] + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'
                );
            })
        }
        CalcularTotales();

    });

    //guardar

    $("#btnGuardar").click(function () {
        if ($("#hdfId").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Ingrese el Proveedor');
        } else if (Lista.Vars.Detalle.length <= 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'No se ha ingresado el detalle');
        }
        else if ($("#lstTipoPago").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Seleccione el forma de pago');
        } else {
            var oDatos = {
                IdCompra: 0,
                Proveedor: { IdProveedor: $("#hdfId").val() },
                Moneda: { IdMoneda: $("#lstMoneda").val() },
                Id: $("#lstTipoPago").val(),
                Serie: $("#txtSerie").val(),
                Numero: $("#txtNumero").val(),
                FechaRegistro: $("#dFechare").val(),
                FechaAtencion: $("#dFechaAten").val(),
                FechaPago: $("#dFechaPago").val(),
                Cantidad: $("#mCant").html(),
                SubTotal: $("#subtotales").val(),
                IGV: $("#igv").val(),
                Total: $("#Totales").val(),
                ProceIGV: 18,
                TipoCambio: 3.5,
            }
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Gestion/InstOrdenCompra'),
                dataType: 'json',
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                data: { oDatos: oDatos, Detalle: Lista.Vars.Detalle },
                success: function (response) {

                    Swal.fire(
                        'Exito ',
                        response.Message,
                        response.Id
                    )
                    window.location.href = General.Utils.ContextPath('Gestion/ReporteCompraExcelAll?Id=' + response.Additionals[0])
                    LimpiarConteniedo();
                }
            })
        }


    });

    $("#limpiar").click(function () {
        LimpiarConteniedo();
    })

})

function CalcularTotales() {
    var TotalValorVta = Total = ImpTotalVta = Igv = TotalSub = Cantidad = 0;

    if ($("#CalIGV").prop('checked')) {
        $.grep(Lista.Vars.Detalle, function (oDetalle) {

            TotalValorVta = oDetalle["Importe"];  //TOTAL
            TotalValorVta = TotalValorVta * 1
            Total += TotalValorVta


            var SubTotalCal = TotalValorVta / (18 / 100 + 1)//Subtotal
            SubTotalCal = SubTotalCal * 1;
            ImpTotalVta += SubTotalCal


            Cantidad += oDetalle["Cantidad"];
            console.log(Cantidad)

        });
        IgvSuma = Total - ImpTotalVta;


        $('#MontoSubtotal').html(ImpTotalVta.toFixed(2)); // sub total 
        $('#MontoCalulado').html(Total.toFixed(2));//esto! // Total
        $('#MontoIGV').html(IgvSuma.toFixed(2)); // igv 
        $('#mCant').html(Cantidad);
        $('#subtotales').val(ImpTotalVta);
        $('#igv').val(IgvSuma);
        $('#Totales').val(Total);
    } else {

        $.grep(Lista.Vars.Detalle, function (oDetalle) {
            var subTotal = oDetalle["Importe"];//sub total
            subTotal = subTotal * 1;
            TotalSub += subTotal;


            Cantidad += oDetalle["Cantidad"];
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
                    Precio: response.PrecioCompra,
                    Importe: (parseFloat(response.PrecioCompra) * 0).toFixed(2)
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
                            '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + oDetalle["Cantidad"] + '">' + '</td>' +
                            '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + oDetalle["Precio"] + '">' + '</td>' +
                            '<td>' + oDetalle["Importe"] + '</td>' +
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

    Lista.CargarSerieDoc();
    $("#lstTipoDoc").val(0);
    $("#lstTipoPago").val(0)
    $("#txtDocCli").val('');
    $("#hdfId").val(0);
    $("#txtNomCli").val('');
    $("#txtDireccionInicio").val('');
    $("#MontoSubtotal").html('0.00');
    $("#subtotales").val(0);
    $("#mCant").html('0.00');
    $("#dFechaAten").datepicker().datepicker("setDate", new Date());
    $("#dFechaPago").datepicker().datepicker("setDate", new Date());
    $("#MontoIGV").html('0.00');
    $("#igv").val(0);
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


}