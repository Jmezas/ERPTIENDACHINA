var Lista = {
    CargarSerieDoc: function () {

        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("venta/SerieNumero"),
            dataType: 'json',
            data: { documento: 2 },
            success: function (response) {

                $("#txtSerie").val(response.Text);
                $("#txtNumero").val(response.Nombre);

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
    CargarDoc: function () {
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
                $("#lstTipoDoc").val(1);
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
    CargarTipoDoc: function () {
        var sucursal = $("#sucursal").val();
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaComboId"),
            dataType: 'json',
            data: { flag: 9, Id: sucursal },
            success: function (response) {
                console.log(response)
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstDocumento"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
                changeDoc($("#lstDocumento").val())
                //$("#lstDocumento").val(2)
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
    CargarCombo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 6 },
            success: function (response) {

                $("#lstTipoDocCliente").empty()
                $("#lstTipoDocCliente").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstTipoDocCliente"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
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
    Vars: {
        Detalle: []
    },
}
$(function () {
    Lista.CargarSerieDoc();
    Lista.CargarDoc();
    Lista.CargarMoneda();

    Lista.CargarTipoDoc();
    Lista.CargarPago();
    Lista.CargarCombo();
    Lista.CargarDepartamento();

    //INICIALIZAR CAMPOS
    $('#txtrecibida').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('#txtPago').Validate({ type: TypeValidation.Numeric, special: '.' });
    $('#txtCostoEnvio').Validate({ type: TypeValidation.Numeric, special: '.' });


    //$('#lstPago').select2();

    $('#lstPago').select2({
        dropdownParent: $('#ModalEfectivo')
    });

    $('#lstDepartamento').select2({
        dropdownParent: $('#ModalNuevo')
    });
    $('#lstProvincia').select2({
        dropdownParent: $('#ModalNuevo')
    });
    $('#lstDistrito').select2({
        dropdownParent: $('#ModalNuevo')
    });




    Lista.CargarAlmacen();
    $("#lstDocumento").change(function () {
        changeDoc($("#lstDocumento").val())
    })

    $('#dFechaPago').datepicker({
        dateFormat: 'dd/mm/yy',
    });
    $("#dFechaPago").datepicker().datepicker("setDate", new Date());



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



    $("#txtDocCsli").autocomplete({
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

        },
        change: function (event, ui) {
            $("#txtDocCli").val(ui.item.cod + ' - ' + ui.item.des.toString().split('|')[0]);
            $("#producto").focus()
        }

    });

    $("#productso").autocomplete({
        source: function (request, response) {

            var filtro = $("#producto").val();
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
                            label: item.Nombre + ' - ' + item.Codigo + ' - ' + item.PrecioVenta,
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
            $("#txtProducto").val(ui.item ? ui.item.cod : $("#txtProducto").val());
            $('#IdProducto').val(ui.item.id);
        },
        change: function (event, ui) {

            Lista.Vars.Detalle.push({
                Material: {
                    IdMaterial: ui.item.id,
                    Codigo: ui.item.cod,
                    Nombre: ui.item.des.toString().split('|')[1],
                },
                Categoria: ui.item.cat.toString(),
                Marca: ui.item.mar.toString(),
                Unidad: ui.item.und.toString(),
                Cantidad: 1,
                Precio: ui.item.des.toString().split('|')[3],
                Importe: ui.item.des.toString().split('|')[3] * 1,
                descuentopor: 0,
                descuento: 0,
                operacion: 1,
                Almacen: { IdAlmacen: $("#lstAlmacen").val() },
                Sucursal: { IdSucursal: $("#lstSucursalCab").val() }
            });
            Lista.CargarOperacion();
            var $tb = $('#tbDetalle');
            $tb.find('tbody').empty();

            if (Lista.Vars.Detalle.length == 0) {
                $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
            } else {

                $.grep(Lista.Vars.Detalle, function (oDetalle) {
                    $tb.find('tbody').append(
                        '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                        '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                        '<td>' + oDetalle["Unidad"] + '</td>' +
                        '<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                        '<td>' + '<input type="text" class="form-control desc" id="descuento"   value="' + oDetalle["descuento"] + '">' + '</td>' +
                        '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + (oDetalle["Cantidad"]) + '">' + '</td>' +
                        '<td>' + '<input type="text" class="form-control Price" id="Precio"  value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                        '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
                        '<td class="text-center">' +
                        '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                        '</td>' +
                        '</tr>'

                    );
                });
            }

            $("#producto").val('')
            $("#producto").focus()
            $("#lstOperacion option[value='1']").attr("selected", true)
            CalcularTotales();
        }
    });


    $("#txtDocCli").select2({
        ajax: {
            url: General.Utils.ContextPath('Shared/FiltroProvCli'),
            dataType: 'json',
            delay: 250,
            type: 'POST',
            data: function (params) {
                return {
                    filtro: params.term,
                    Tipo: $("#lstTipoDoc").val(),
                    flag: 2
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            id: item.Id,
                            text: item.Text + '-' + item.Nombre,
                            des: item.Nombre + '|' + item.Dir
                        };
                    })
                };
            },
            cache: true
        },
        placeholder: 'Buscar Cliente',
        allowClear: true,
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 1,
        language: {
            inputTooShort: function () {
                return "Buscar Cliente";
            }
        }
    })
    $('#txtDocCli').on('select2:select', function (e) {
        var data = e.params.data;
        console.log(data)
        $('#hdfId').val(data.id);
    })



    $("#producto").select2({
        ajax: {
            url: General.Utils.ContextPath('Shared/FiltroProducto'),
            dataType: 'json',
            delay: 250,
            type: 'POST',
            data: function (params) {
                return {
                    filtro: params.term // search ter
                };
            },
            processResults: function (data, params) {
                return {
                    results: $.map(data, function (item) {
                        return {
                            text: item.Nombre,
                            id: item.IdMaterial,
                            cod: item.Nombre,
                            des: item.Nombre + '|' + item.Codigo + '|' + item.PrecioCompra + '|' + item.PrecioVenta + '|' + item.Descuento,
                            cat: item.Categoria.Nombre,
                            mar: item.Marca.Nombre,
                            und: item.Unidad.Nombre
                        };
                    })
                };
            },
            cache: true
        },
        placeholder: 'Busqueda producto',
        allowClear: true,
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 1,
        language: {
            inputTooShort: function () {
                return "Busqueda producto";
            }
        }
    });

    $('#producto').on('select2:select', function (e) {
        $("#producto").empty();
        var data = e.params.data;

        if (BuscarDetalleEnTabla(data.id)) {
            General.Utils.ShowMessage(TypeMessage.Error, 'El producto ya existe en la tabla');
        } else {
            VerificarStock(data.id, 1, $("#lstAlmacen").val(), function (errorLanzado, datosDevueltos) {
                if (errorLanzado) // Ha habido un error, deberías manejarlo :/
                {
                    General.Utils.ShowMessage(TypeMessage.Error, 'Algo salio mal verificar');
                    return;
                }

                if (datosDevueltos.Id == 'error') {
                    General.Utils.ShowMessage(datosDevueltos.Id, datosDevueltos.Message);
                } else {
                    Lista.Vars.Detalle.push({
                        Material: {
                            IdMaterial: data.id,
                            Nombre: data.des.toString().split('|')[0],
                        },
                        Categoria: data.cat.toString(),
                        Marca: data.mar.toString(),
                        Unidad: data.und.toString(),
                        Cantidad: 1,
                        Precio: data.des.toString().split('|')[3],
                        Importe: (data.des.toString().split('|')[3] * 1) - (data.des.toString().split('|')[4] * 1),
                        descuentopor: 0,
                        descuento: data.des.toString().split('|')[4],
                        operacion: 1,
                        Almacen: { IdAlmacen: $("#lstAlmacen").val() },
                        Sucursal: { IdSucursal: $("#lstSucursalCab").val() }
                    });

                    Lista.CargarOperacion();
                    var $tb = $('#tbDetalle');
                    $tb.find('tbody').empty();

                    if (Lista.Vars.Detalle.length == 0) {
                        $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
                    } else {

                        $.grep(Lista.Vars.Detalle, function (oDetalle) {
                            $tb.find('tbody').append(
                                '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                                '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                                '<td>' + oDetalle["Unidad"] + '</td>' +
                                //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                                '<td>' + '<input type="text" class="form-control desc" id="descuento"  value="' + oDetalle["descuento"] + '">' + '</td>' +
                                '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + (oDetalle["Cantidad"]) + '">' + '</td>' +
                                '<td>' + '<input type="text" class="form-control Price" id="Precio" disabled value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                                '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
                                '<td class="text-center">' +
                                '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                                '</td>' +
                                '</tr>'

                            );
                        });
                    }

                    $("#producto").empty();
                    $("#producto").focus()
                    $("#lstOperacion option[value='1']").attr("selected", true)
                    CalcularTotales();
                }
            })
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
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //'<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento"  value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + formatNumber(oDetalle["Cantidad"]) + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio" disabled value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                    '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
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

    //ACtulizar cantidad
    $('#tbDetalle').on('change', '.Cant', function () {
        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');
        //var Cantidad = 0;
        var input = parseInt($(this).val());
        //console.log($(this).val())
        //$('input', row).each(function () {
        //    input = 
        //});

        //VerificarStock(Id, input, $("#lstAlmacen").val(), function (errorLanzado, datosDevueltos) {
        //    if (errorLanzado) // Ha habido un error, deberías manejarlo :/
        //    {
        //        General.Utils.ShowMessage(TypeMessage.Error, 'Algo salio mal verificar');
        //        return;
        //    }

        //    if (datosDevueltos.Id == 'error') {
        //        General.Utils.ShowMessage(datosDevueltos.Id, datosDevueltos.Message);
        //    } else {

        //    }
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
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //  '<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento" value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + (oDetalle["Cantidad"]) + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio" disabled value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                    '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
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
                data.descuento = 0;
                data.descuentopor = 0;
            }
        })
        CalcularTotales();
        Lista.CargarOperacion()
        var $tb = $('#tbDetalle');
        $tb.find('tbody').empty();
        if (Lista.Vars.Detalle.length == 0) {
            $tb.find('tbody').append('<tr><td colspan="15">No existen registros</td></tr>')
        } else {

            $.grep(Lista.Vars.Detalle, function (oDetalle) {
                $tb.find('tbody').append(
                    '<tr data-index=' + oDetalle["Material"]["IdMaterial"] + '>' +
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //  '<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento" value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + (oDetalle["Cantidad"]) + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio" disabled value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                    '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'

                );
            });
        }
    });
    //Actulizar descuento %
    $('#tbDetalle').on('change', '.por', function () {
        var row = $(this).closest('tr');
        var Id = $(this).closest('tr').attr('data-index');


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


        ////

        var diferenciaDescuento = 0;
        var desUnidad = 0;




        var input = Number($(this).val());
        Lista.Vars.Detalle.map(function (data) {
            if (parseInt(data.Material.IdMaterial) == parseInt(Id)) {

                diferenciaDescuento = (data.Precio * input) / 100;
                desUnidad = data.Precio - diferenciaDescuento

                data.Importe = (desUnidad * data.Cantidad);
                data.descuento = (data.Precio * data.Cantidad) - data.Importe;
                data.descuentopor = input;

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
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //  '<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento"  value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + (oDetalle["Cantidad"]) + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio" disabled value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                    '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
                    '<td class="text-center">' +
                    '<button type="submit" class="btn btn-danger btn-xs"><i class="fa fa-trash"></i></button>' +
                    '</td>' +
                    '</tr>'
                );
            });
        }
    });

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
                    '<td>' + oDetalle["Material"]["Nombre"] + '</td>' +
                    '<td>' + oDetalle["Unidad"] + '</td>' +
                    //  '<td>' + '<select id="lstOperacion" name="lstOperacion" class="form-control select"></select>' + '</td>' +
                    '<td>' + '<input type="text" class="form-control desc" id="descuento" value="' + oDetalle["descuento"] + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Cant" id="Cantidad"  value="' + (oDetalle["Cantidad"]) + '">' + '</td>' +
                    '<td>' + '<input type="text" class="form-control Price" id="Precio" disabled value="' + formatNumber(oDetalle["Precio"]) + '">' + '</td>' +
                    '<td>' + formatNumber(oDetalle["Importe"]) + '</td>' +
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

    $("#btnEfectivo").click(function () {
        //agregamos atributo enabled cuando es efectivo
        $("#txtrecibida").prop('disabled', false);
        $("#btn10").prop('disabled', false);
        $("#btn20").prop('disabled', false);
        $("#btn50").prop('disabled', false);
        $("#btn100").prop('disabled', false);
        $("#btnLimpiar").prop('disabled', false);

        numero = 0
        $("#txtrecibida").val('0.00');
        $("#txtCambio").val('0.00');
        $("#txtCostoEnvio").val('0.00');

        $("#txtPago").val($("#Totales").val());
        $("#lstPago").val(1);
    })

    $("#btntargeta").click(function () {
        $("#txtrecibida").val($("#Totales").val())
        $("#txtCambio").val('0.00')
        $("#txtCostoEnvio").val('0.00')
        //agregamos atributo disabled cuando es tarjeta
        $("#txtrecibida").prop('disabled', true);
        $("#btn10").prop('disabled', true);
        $("#btn20").prop('disabled', true);
        $("#btn50").prop('disabled', true);
        $("#btn100").prop('disabled', true);
        $("#btnLimpiar").prop('disabled', true);

        $("#txtPago").val($("#Totales").val())
        $("#lstPago").val(2)
        $("#lstPago").trigger('change');
    })
    $("#btncredito").click(function () {
        $("#txtrecibida").val('0.00')
        $("#txtCostoEnvio").val('0.00')
        $("#txtCambio").val('0.00')

        $("#txtPago").val($("#Totales").val())
        $("#lstPago").val(3)
    })

    $("#btnLimpiarFormulario").click(function () {
        LimpiarConteniedo();
    })

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
    $("#btnBuscar").click(function () {
        BuscarSunat();
    })
    $("#txtDocumento").change(function () {
        BuscarSunat();
    })
    $("#btnGrabar").click(function () {

        var $form = $("#ModalNuevo");
        var oDatos = General.Utils.SerializeForm($form);

        if ($("#lstTipoDocCliente").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Seleccione tipo de documento');

        } else if ($("#lstDistrito").val() == '' || $("#lstDistrito").val() == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Ingrese el distrito');
        } else {
            if (General.Utils.ValidateForm($form)) {
                var oDatos = {
                    IdCliente: $("#hdfIdCliente").val(),
                    Id: $("#lstTipoDocCliente").val(),
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
                    data: oDatos,
                    beforeSend: General.Utils.StartLoading,
                    complete: General.Utils.EndLoading,
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
                        $("[data-dismiss=modal]").trigger({ type: "click" });//cerrar modal
                        Limpiar();
                    }
                });
            }
        }


    });

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
                Documento: { IdDocumento: 3 },
                fechaEmision: $("#dFechaPago").val(),
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
                Sucursal: { IdSucursal: $("#lstSucursalCab").val() },

                montoRecibido: $("#txtrecibida").val(),
                vuelto: $("#txtCambio").val(),
                metodoPago: $("#lstPago").val(),
                NotaPago: $("#txtNotaPago").val(),
                CostoEnvio: $("#txtCostoEnvio").val(),
            }

            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Venta/InstRegistrarPost'),
                dataType: 'json',
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                data: { oDatos: oDatos, Detalle: Lista.Vars.Detalle },
                success: function (response) {
                   

                    //Swal.fire(
                    //    ' ',
                    //    response.Message,
                    //    response.Id
                    //)
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
                                var URL = General.Utils.ContextPath('Venta/imprimirFacBol?Id=' + response.Additionals[0] + "&Envio=" + 0 + "&venta=" + 0);
                                fileDownnload(URL);
                            }
                        })
                        LimpiarConteniedo();
                    } else {
                        Swal.fire(
                            'Alerta!',
                            response.Message,
                            response.Id
                        )
                    }

                }
            })
        }


    });
})
function fileDownnload(url) {
    var options = {
        height: "600px",
        page: '2'

    };
    PDFObject.embed(url, "#PDFViewer", options);
    // $("#myReportPrint").modal('show');

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


function LimpiarConteniedo() {

    Lista.CargarSerieDoc();
    $("#lstTipoDoc").val(1);
    //$("#lstDocumento").val(2)
    $("#txtDocCli").val('');
    $("#hdfId").val(0);
    $("#MontoSubtotal").html('0.00');
    $("#subtotales").val(0);
    $("#mCant").html('0.00');
    $("#dFechaAten").datepicker().datepicker("setDate", new Date());
    $("#MontoIGV").html('0.00');
    $("#igv").val(0);
    $("#Montodescuento").html('0.00');
    $("#descuento").val(0);
    $("#Totales").val(0);
    $("#MontoCalulado").html('0.00');
    $("#hMonedaComprobante").html('SOLES');
    $("#txtDocCli").empty();
    Lista.Vars.Detalle = [];
    var $tb = $('#tbDetalle');
    $tb.find('tbody').empty();
    if (Lista.Vars.Detalle.length == 0) {
        $tb.find('tbody').append('<tr><td colspan="11">No existen registros</td></tr>')
    }
    changeDoc($("#lstDocumento").val());

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

function CalcularTotales() {
    var TotalValorVta = ImpTotalVta = Igv = TotalSub = Cantidad = exonerada = inafecta = grabada = gratuita = 0;
    var desc = 0;
    var totalfac = 0;


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

        $('#Montodescuento').html(formatNumber(desc)); // descuento
        $('#MontoSubtotal').html(TotalSub.toFixed(2)); // sub total
        $('#MontoCalulado').html(Total.toFixed(2));//esto! // Total
        $('#MontoIGV').html(Igv.toFixed(2)); // igv
        $('#mCant').html(Cantidad);

        $('#subtotales').val(TotalSub);
        $('#igv').val(Igv);
        $('#Totales').val(Total);
    }


}
function Limpiar() {
    //$("#hdfIdCliente").val(0);
    //$("#txtNombre").val('');
    //$("#txtSigla").val('');
    //$("#lstCategoria").val('0');

    $("#hdfId").val(0);

    $("#txtDocumento").val('');
    $("#txtRazon").val('');
    $("#txtTelefono").val('');
    $("#txtCelular").val('');
    $("#txtEmail").val('');
    $("#txtDirecion").val('');
    $("#lstDepartamento").val('0');
    $("#lstTipoDoc").val('0');
    //$("#lstTipoDoc").empty();

    $("#lstProvincia").empty();
    $("#lstProvincia").append($('<option>', { value: 0, text: 'Seleccione' }));
    $("#lstDistrito").empty();
    $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));

}
function BuscarSunat() {

    var RUC = $("#txtDocumento").val();
    if ($("#lstTipoDocCliente").val() == 3) {
        if (isNaN(RUC) || RUC < 10000000000 || RUC > 99999999999) {
            General.Utils.ShowMessage(TypeMessage.Error, 'El RUC debe contener 11 dígitos');
        } else {
            $.ajax({
                type: 'post',
                url: General.Utils.ContextPath('Shared/SearchSunatRUCDNI'),
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                data: {
                    tipo: $("#lstTipoDocCliente").val(),
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
                            $('#lstDepartamento').trigger('change');
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
                                        $('#lstProvincia').trigger('change');
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

                                    }
                                    $("#lstDistrito").val(ubigeo.substr(4, 2));
                                    $('#lstDistrito').trigger('change');
                                }

                            });


                        }
                    }
                }
            });
        }
    } else if ($("#lstTipoDocCliente").val() == 1) {
        if (isNaN(RUC) || RUC.length != 8) {
            General.Utils.ShowMessage(TypeMessage.Error, 'El DNI debe contener 8 dígitos');
        } else {
            $.ajax({
                type: 'post',
                url: General.Utils.ContextPath('Shared/SearchSunatRUCDNI'),
                data: {
                    tipo: $("#lstTipoDocCliente").val(),
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
