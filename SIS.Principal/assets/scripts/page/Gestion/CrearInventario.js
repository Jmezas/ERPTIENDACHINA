var Lista = {
    CargarSerieDoc: function () {

        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/GetSerieNum"),
            dataType: 'json',
            data: { Flag: 3 },
            success: function (response) {

                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error      

                    var lista = response.Nombre.split("-")
                    $("#txtSerie").val(lista[0])
                    $("#txtNumero").val(lista[1])
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
    CargarTipo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 13 },
            success: function (response) {
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstTipoMov"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
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

                $("#lstAlmacen").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstAlmacen"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
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
            data: { Id: 14 },
            success: function (response) {
                $("#lstOperacion").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstOperacion"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
                $("#lstOperacion").val('16')
            }

        });
    },
    Vars: {
        Detalle: []
    },
}

$(function () {

    Lista.CargarMoneda();
    Lista.CargarTipo();
    Lista.CargarOperacion();
    Lista.CargarSerieDoc();

    $("#lstOperacion").select2({}) 
    Lista.CargarAlmacen();

    $('#txtFechaEmision').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });
    $("#txtFechaEmision").datepicker().datepicker("setDate", new Date());




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
            destroy:true,
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
                    '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
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
                    '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
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
                    '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'
                );
            })
        }
        CalcularTotales();

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

        //limpia la tabla 
       // $("#tbMaterial").DataTable().ajax.reload();

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
                Importe: (parseFloat($("#txtPrecio").val()) * parseFloat($("#txtCantidad").val())).toFixed(2),
                Almacen: { IdAlmacen: $("#lstAlmacen").val() }
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
                        '<td>' + (oDetalle["Importe"] * 1).toFixed(2) + '</td>' +
                        '<td class="text-center">' +
                        '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                        '</td>' +
                        '</tr>'

                    );
                });
            }
        }
    });


    $("#lstAlmacen").change(function () {

        Lista.Vars.Detalle.map(function (data) {
            data.Almacen.IdAlmacen = $("#lstAlmacen").val()
        });
    })
    $("#btnGuardar").click(function () {


        if ($("#lstAlmacen").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Los campos deben de estar seleccionado');
        } else if (Lista.Vars.Detalle.length <= 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'No se ha ingresado el detalle');
        }
        else if ($("#lstOperacion").val()==0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Falta seleccionar la operacion');
        }  
        else {
            var oDatos = {

                FechaEmison: $("#txtFechaEmision").val(),
                Serie: $("#txtSerie").val(),
                Numero: $("#txtNumero").val(),
                Id: $("#lstTipoMov").val(),//pago
                Moneda: { IdMoneda: $("#lstMoneda").val() },
                Almacen: { IdAlmacen: $("#lstAlmacen").val() },
                Documento: { IdDocumento: $("#lstOperacion").val() },
                Cantidad: $("#mCant").html(),
                SubTotal: $("#subtotales").val(),
                Total: $("#Totales").val(),
                IGV: $("#igv").val(),
                Observacion: "-",
                contenedor: $("#txtcontenedor").val()
            }

            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Gestion/InstMovimiento'),
                dataType: 'json',
                data: { oDatos: oDatos, Detalle: Lista.Vars.Detalle },
                success: function (response) {
                    if (response.Id == 'success') {
                        Swal.fire(
                            'Exito ',
                            response.Message,
                            response.Id
                        )
                        LimpiarConteniedo();
                    } else {
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





});

function CalcularTotales() {
    let TotalValorVta = Total = ImpTotalVta = Igv = TotalSub = Cantidad = 0;

    if ($("#CalIGV").prop('checked')) {
        $.grep(Lista.Vars.Detalle, function (oDetalle) {

            TotalValorVta = oDetalle["Importe"];  //TOTAL
            TotalValorVta = TotalValorVta * 1
            Total += TotalValorVta


            var SubTotalCal = TotalValorVta / (18 / 100 + 1)//Subtotal
            SubTotalCal = SubTotalCal * 1;
            ImpTotalVta += SubTotalCal


            Cantidad += parseFloat(oDetalle["Cantidad"]);
            console.log(Cantidad)

        });
        IgvSuma = Total - ImpTotalVta;


        $('#MontoSubtotal').html(ImpTotalVta.toFixed(2)); // sub total 
        $('#MontoCalulado').html(Total.toFixed(2));//esto! // Total
        $('#MontoIGV').html(IgvSuma.toFixed(2)); // igv 
        $('#mCant').html(parseFloat(Cantidad.toFixed(2)));
        $('#subtotales').val(ImpTotalVta);
        $('#igv').val(IgvSuma);
        $('#Totales').val(Total);
    } else {

        $.grep(Lista.Vars.Detalle, function (oDetalle) {
            var subTotal = oDetalle["Importe"];//sub total
            subTotal = subTotal * 1;
            TotalSub += subTotal;


            Cantidad += parseFloat(oDetalle["Cantidad"]);
        });

        Igv = TotalSub * 0.18;
        Igv = Igv * 1;

        Total = TotalSub + Igv;

        $('#MontoSubtotal').html(TotalSub.toFixed(2)); // sub total
        $('#MontoCalulado').html(Total.toFixed(2));//esto! // Total
        $('#MontoIGV').html(Igv.toFixed(2)); // igv
        $('#mCant').html(parseFloat(Cantidad.toFixed(2)));

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
                    Importe: (parseFloat(response.PrecioCompra) * 0).toFixed(2),
                    Almacen: { IdAlmacen: $("#lstAlmacen").val() }
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

    Lista.CargarSerieDoc();
    $("#lstAlmacen").val(0);
    $("#lstOperacion").val(0)
    $("#txtFechaEmision").datepicker().datepicker("setDate", new Date());
    $("#mCant").html('0.00');
    $("#MontoSubtotal").html('0.00');
    $("#subtotales").val(0);
    $("#MontoIGV").html('0.00');
    $("#igv").val(0);
    $("#MontoCalulado").html('0.00');
    $("#Totales").val(0);
    $("#hMonedaComprobante").html('SOLES');
    $("#MontoIGV").val();
    $("#txtcontenedor").val('')

    Lista.Vars.Detalle = [];
    var $tb = $('#tbDetalle');
    $tb.find('tbody').empty();
    if (Lista.Vars.Detalle.length == 0) {
        $tb.find('tbody').append('<tr><td colspan="11">No existen registros</td></tr>')
    }


}