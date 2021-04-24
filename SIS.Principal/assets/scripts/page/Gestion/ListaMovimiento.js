

$(function () {

    $("#hdf_Pagina").val('1');
    ListaOC();


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


    $("#txtBusqueda").keyup(function () {

        $("#txtBusqueda").val();
        ListaOC()
    });

    $('input[type=search]').on('search', function () {
        // search logic here
        ListaOC()
    });

    $("#btnFactura").click(function () {

        var Filtro = $("#txtBusqueda").val(),
            FechaInicio = $("#txtFechaInicio").val(),
            FechaFin = $("#txtFechaInicio").val(),
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteMovimientoPDF?Filltro=' + Filtro + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +

            "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill);


    });

    $("#btnExcel").click(function () {

        var Filtro = $("#txtBusqueda").val(),
            FechaInicio = $("#txtFechaInicio").val(),
            FechaFin = $("#txtFechaInicio").val(),
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteMovimientoExcel?Filltro=' + Filtro + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +

            "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill);
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
            ListaOC();
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

            ListaOC();
        }
    });



    $("#tbCompra").on('click', 'tbody .Eliminar', function () {
        var data = $(this).closest('tr').attr('data-id');
        $("#hdfId").val(data);
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
                    data: { Id: $("#hdfId").val(), IdFlag: 12 },
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

                        ListaOC();

                    }
                });

            }
        })
    });
    $('#checkAfectaStock').change(function () {

        ListaOC();
    });
    $('#IncluyeIgv').change(function () {

        ListaOC();
    });
    $("#btnFiltrar").click(function () {
        ListaOC();
    })
    $("#tbCompra").on('click', 'tbody .evento', function () {
        var data = $(this).closest('tr').attr('data-id');

        CargarLista(data);
    })
});




function ListaOC() {
    var Filtro = $("#txtBusqueda").val(),
        FechaInicio = $("#txtFechaInicio").val(),
        FechaFin = $("#txtFechaInicio").val(),
        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1;
    let DesPagina;
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Gestion/ListaMovimientoCab'),
        dataType: 'json',
        data: { Filltro: Filtro, FechaIncio: FechaInicio, FechaFin: FechaFin, numPag: numPaginas, allReg: AllReg, Cant: 10 },
        success: function (response) {
            console.log(response);
            var $tb = $("#tbCompra");
            $tb.find('tbody').empty();
            if (response["Total"] == 0) {
                return false;
            }
            if (response.length == 0) {
                $tb.find('tbody').html('<tr><td colspan="20">No hay resultados para el filtro ingresado</td></tr>');
            } else {
                $("#hdf_TotalPagina").val(response[0].TotalPagina);
                DesPagina = $("#hdf_Pagina").val() + "  de  " + response[0].TotalPagina;
                $.grep(response, function (item) {

                    $tb.find('tbody').append(
                        '<tr data-id="' + item["IdMovimiento"] + '">' +
                        '<td>' + item["Serie"] + '</td>' +
                        '<td>' + item["Text"] + '</td>' +
                        '<td>' + item["FechaEmison"] + '</td>' +
                        '<td>' + item["Moneda"]["Nombre"] + '</td>' +
                        '<td>' + item["Documento"]['Nombre'] + '</td>' +
                        '<td>' + formatNumber(item["SubTotal"]) + '</td>' +
                        '<td>' + formatNumber(item["IGV"]) + '</td>' +
                        '<td>' + formatNumber(item["Total"]) + '</td>' +
                        '<td class="text-center">' +
                        '<button class="btn-crud btn btn-info  btn-sm evento"  data-toggle="modal" data-target="#Detalle"  title="Ver Detalle"><i class="fa fa-search"></i> </button>' +
                        '</td>' +
                        '</tr>'
                    );


                });
                $("#TotalReg").val(response[0]["TotalR"]);
                $('#pHelperProductos').html('Existe(n) ' + response[0]["TotalR"] + ' resultado(s) para mostrar.');
                $("#lblNumPagina").html(DesPagina);
            }

        }
    });
}

function CargarLista(Id) {


    $.ajax({
        async: false,
        type: 'post',
        url: General.Utils.ContextPath('Gestion/ListaMovimientoDet'),
        dataType: 'json',
        data: { IdMov: Id, },
        success: function (response) {
            console.log(response)
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error
                var $tb = $('#tbDetalle')
                $tb.find('tbody').empty();
                if (response.length == 0) {
                    $tb.find('tbody').html('<tr><td colspan="12">No hay resultados para el filtro ingresado</td></tr>');
                    $('#pHdet').html('');
                } else {
                    $.grep(response, function (item) {
                        $tb.find('tbody').append(
                            '<tr data-id="' + item["Movimiento"]["Id"] + '">' +
                            '<td class="text-center">' + item["Item"] + '</td>' +
                            '<td class="text-center">' + item["Material"]["Codigo"] + '</td>' +
                            '<td class="text-center">' + item["Material"]["Nombre"] + '</td>' +
                            '<td class="text-center">' + item["Material"]["Unidad"]["Nombre"] + '</td>' +
                            '<td class="text-center">' + item["Material"]["Categoria"]["Nombre"] + '</td>' +
                            '<td class="text-center">' + formatNumber(item["Cantidad"]) + '</td>' +
                            '<td class="text-center">' + formatNumber(item["Precio"]) + '</td>' +
                            '<td class="text-center">' + formatNumber(item["Precio"] * item["Cantidad"]) + '</td>' +
                            '</tr>'


                        );

                    });

                    $('#pHdet').html('Existe(n) ' + response[0]["Total"] + ' resultado(s) para mostrar.' +
                        (response["Total"] > 0 ? ' Del&iacute;cese hacia abajo para visualizar m&aacute;s...' : ''));
                }
            }
        }
    });
};