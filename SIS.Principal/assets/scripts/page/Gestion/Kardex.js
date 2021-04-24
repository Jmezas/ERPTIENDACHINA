var Lista = {
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

}

$(function () {
    $('#hdfIdP').val(0);

    Lista.CargarAlmacen();

    $("#hdf_Pagina").val('1');

    $('input[name="txtFechaInicio"]').Validate({ type: 'date', blockBefore: false, blockAfter: false });
    $('input[name="txtFechaFin"]').Validate({ type: 'date', blockBefore: false, blockAfter: false });

    $('#txtFechaInicio').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });

    $('#txtFechaFin').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });

    setTimeout(function myfunction() {
        ListaMovimientos();


    }, 1000);

    $('#lstAlmacen').change(function () {
        $('#hdfIdP').val(0);



        $('#txtCodProd').val('');
    });

    $("#btnExcel").click(function () {

        var FechaInicio = $("#txtFechaInicio").val(),
            FechaFin = $("#txtFechaFin").val(),
            idAlm = $('#lstAlmacen').val(),
            idMat = $('#hdfIdP').val().length == 0 ? 0 : $('#hdfIdP').val(),
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 1 : 0,
            CantiFill = 20


        if (FechaInicio.length == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Ingrese la fecha de inicio');
        }
        else if (FechaFin.length == 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'Ingrese la fecha final');
        }
        else {
            window.location.href = General.Utils.ContextPath('Gestion/ReporteKardex?FechaIncio=' + FechaInicio + "&FechaFin=" + FechaFin + "&idAlm=" + idAlm + "&idMat=" + idMat + "&numPagina=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill);
        }

    });

    $("#btnAnteror").click(function () {
        let numPaginas = parseInt($("#hdf_Pagina").val());
        let TotalPagina = parseInt($("#hdf_TotalPagina").val());
        if (numPaginas == 0 || numPaginas == 1) {
            General.Utils.ShowMessage(TypeMessage.Information, 'Límite de Página..');

        } else {
            numPaginas = numPaginas - 1

            $("#hdf_Pagina").val(numPaginas);

            $("#lblNumPagina").html(numPaginas + '  de  ' + TotalPagina);
            ListaMovimientos();
        }

    });

    $("#btnSiguiente").click(function () {
        let numPaginas = parseInt($("#hdf_Pagina").val());
        let TotalPagina = parseInt($("#hdf_TotalPagina").val());

        if (TotalPagina == numPaginas) {
            General.Utils.ShowMessage(TypeMessage.Information, 'Límite de Página..');
        } else {
            numPaginas = numPaginas + 1
            $("#hdf_Pagina").val(numPaginas);
            $("#lblNumPagina").html(numPaginas + '  de  ' + TotalPagina);

            ListaMovimientos();
        }
    });

    $("#btnFiltrar").click(function () {
        ListaMovimientos();
    });

    $("#txtCodProd").select2({
        ajax: {
            url: General.Utils.ContextPath('Shared/FiltroBusquedaProducto'),
            dataType: 'json',
            delay: 250,
            type: 'POST',
            data: function (params) {
                return {
                    filtro: params.term,
                    idAlmacen: $('#lstAlmacen').val()
                };
            },
            processResults: function (data, params) {
                //console.log(data);
                return {
                    results: $.map(data, function (item) {
                        return {
                            id: item.Id,
                            text: item.Text + '-' + item.Nombre,
                            sCodigo: item.sCodigo,
                            codigo: item.Text,
                            producto: item.Nombre
                        };
                    })
                };
            },
            cache: true
        },
        placeholder: 'Buscar Producto',
        allowClear: true,
        escapeMarkup: function (markup) { return markup; }, // let our custom formatter work
        minimumInputLength: 1,
        language: {
            inputTooShort: function () {
                return "Buscar Producto";
            }
        }
    });

    $('#txtCodProd').on('select2:select', function (e) {
        var data = e.params.data;
        //console.log(data)
        $('#hdfIdP').val(data.id);



    })
});


function ListaMovimientos() {
    if (parseInt($("#hdf_Pagina").val()) == 0) {
        $("#hdf_Pagina").val(1);
    }
    var FechaInicio = $("#txtFechaInicio").val(),
        FechaFin = $("#txtFechaFin").val(),
        idAlm = $('#lstAlmacen').val(),
        idMat = $('#hdfIdP').val().length == 0 ? 0 : $('#hdfIdP').val(),
        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 1 : 0;
    //console.log($("#lstAlmacen").val());
    let DesPagina;
    let TotalR;
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Gestion/ListaKardex'),
        dataType: 'json',
        data: { FechaIncio: FechaInicio, FechaFin: FechaFin, idAlm: idAlm, idMat: idMat, numPag: numPaginas, allReg: AllReg, Cant: 10 },
        success: function (response) {
            //console.log(response);
            var $tb = $("#tbKardex");
            $tb.find('tbody').empty();
            if (response.length == 0) {
                $("#hdf_TotalPagina").val(0);
                $("#hdf_Pagina").val(0);
                TotalR = 0;
                $tb.find('tbody').html('<tr><td colspan="20">No hay resultados para el filtro ingresado</td></tr>');
                DesPagina = $("#hdf_Pagina").val() + "  de  " + $("#hdf_TotalPagina").val();
            } else {

                if ($("#IdTotal").is(':checked') === true) {
                    $("#hdf_Pagina").val(1);
                    $("#hdf_TotalPagina").val(1);
                }
                else {
                    $("#hdf_TotalPagina").val(response[0].TotalPagina);
                }
                DesPagina = $("#hdf_Pagina").val() + "  de  " + $("#hdf_TotalPagina").val();
                TotalR = response[0]["TotalR"];
                $.grep(response, function (item) {

                    $tb.find('tbody').append(
                        '<tr>' +
                        '<td>' + item["FechaEmison"] + '</td>' +
                        '<td>' + item["material"] + '</td>' +
                        '<td>' + item["Almacen"]["Nombre"] + '</td>' +
                        '<td style="text-align: right;">' + item["Cantidad"] + '</td>' +
                        '<td style="text-align: right;">' + formatNumber(item["precioEntrada"]) + '</td>' +
                        '<td style="text-align: right;">' + formatNumber(item["costoEntrada"]) + '</td>' +
                        '<td style="text-align: right;">' + item["cantidadSalida"] + '</td>' +
                        '<td style="text-align: right;">' + formatNumber(item["precioSalida"]) + '</td>' +
                        '<td style="text-align: right;">' + formatNumber(item["costoSalida"]) + '</td>' +
                        '<td style="text-align: right;">' + item["totalStock"] + '</td>' +
                        '</tr>'
                    );


                });

            }
            $("#TotalReg").val(TotalR);
            $('#pHelperProductos').html('Existe(n) ' + TotalR + ' resultado(s) para mostrar.');
            $("#lblNumPagina").html(DesPagina);

        }
    });
}
