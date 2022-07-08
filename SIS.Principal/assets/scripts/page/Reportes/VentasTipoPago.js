
$(function () {
    $("#hdf_Pagina").val('1');
    ListaGeneral();

    $('#fechaFin').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });

    $('#fechaInicio').datepicker({
        dateFormat: 'dd/mm/yy',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>'
    });

    $("#btnFiltrar").click(function () {
        ListaGeneral();
        //window.$('#Report').modal();
        // $('#Report').modal('show');
    })
    $("#txtBuscar").change(function () {

        $("#txtBuscar").val();
        ListaGeneral()
    });

    //$("#btnFactura").click(function () {
    //    var inicio = $('#fechaInicio').val().split(/\//);
    //    inicio = [inicio[1], inicio[0], inicio[2]].join('/');

    //    var Fin = $('#fechaFin').val().split(/\//);
    //    Fin = [Fin[1], Fin[0], Fin[2]].join('/');

    //    if (inicio == "//") {
    //        inicio = "";
    //    }
    //    if (Fin == "//") {
    //        Fin = "";
    //    }
    //    var Filtro = $("#txtBuscar").val(),
    //        FechaInicio = inicio,
    //        FechaFin = Fin,
    //        numPaginas = parseInt($("#hdf_Pagina").val()),
    //        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
    //        CantiFill = $("#TotalReg").val();

    //    window.location.href = General.Utils.ContextPath('Venta/ReportePDF?filtro=' + Filtro + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +
    //        "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill + "&venta=" + 0);
    //});

    $("#btnExcel").click(function () {
        var inicio = $('#fechaInicio').val().split(/\//);
        inicio = [inicio[1], inicio[0], inicio[2]].join('/');

        var Fin = $('#fechaFin').val().split(/\//);
        Fin = [Fin[1], Fin[0], Fin[2]].join('/');

        if (inicio == "//") {
            inicio = "";
        }
        if (Fin == "//") {
            Fin = "";
        }

        var Filtro = $("#txtBuscar").val(),
            FechaInicio = inicio,
            FechaFin = Fin,
            tipVenta = parseInt($("#lstTipoVenta").val()),
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            CantiFill = $("#TotalReg").val();
        window.location.href = General.Utils.ContextPath('Reportes/ReporteVentasPorTipoPagoExcel?filtro=' + Filtro + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +
            "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill + "&venta=" + tipVenta + "&sucursal=" + 0);
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
            ListaGeneral();
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

            ListaGeneral();
        }
    });

    $("#tbVenta").on('click', 'tbody .evento', function () {
        var data = $(this).closest('tr').attr('data-id');
        //imprimirFacBol
        //ImprimirFacturaBolTikect
        var URL = General.Utils.ContextPath('Venta/imprimirFacBol?Id=' + data + "&Envio=" + 1 + "&venta=" + 0);
        // console.log(URL); 
        fileDownnload(URL);
    })



})
function ListaGeneral() {
    var inicio = $('#fechaInicio').val().split(/\//);
    inicio = [inicio[1], inicio[0], inicio[2]].join('/');

    var Fin = $('#fechaFin').val().split(/\//);
    Fin = [Fin[1], Fin[0], Fin[2]].join('/');

    if (inicio == "//") {
        inicio = "";
    }
    if (Fin == "//") {
        Fin = "";
    }
    var Filtro = $("#txtBuscar").val(),
        FechaInicio = inicio,
        FechaFin = Fin,
        tipVenta = parseInt($("#lstTipoVenta").val()),
        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1;
    let DesPagina;

    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Reportes/VentasPorTipoPago'),
        dataType: 'json',
        beforeSend: General.Utils.StartLoading,
        complete: General.Utils.EndLoading,
        data: { flag: tipVenta, filtro: Filtro, FechaIncio: FechaInicio, FechaFin: FechaFin, numPag: numPaginas, allReg: AllReg, Cant: 10, sucursal: 0 },
        success: function (response) {

            var $tb = $("#tbTipoPago");
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
                        '<tr data-id="' + item["Venta"]["Id"] + '">' +
                            '<td>' + item["Venta"]["serie"] + '-' + item["Venta"] ["numero"] + '</td>' +
                            '<td>' + item["Venta"] ["fechaEmision"] + '</td>' +
                            '<td>' + item["Venta"]["Text"] + '</td>' +
                            '<td>' + item["Venta"]["TextBanco"] + '</td>' +
                            '<td>' + formatNumber(item["precio"]) + '</td>' +
                            '<td>' + item["Venta"]["moneda"]["Nombre"] + '</td>' +
                            '<td>' + item["Sucursal"]["Nombre"] + '</td>' +
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
function fileDownnload(url) {
    var options = {
        height: "600px",
        page: '2'

    };
    PDFObject.embed(url, "#PDFViewer", options);
    // $("#myReportPrint").modal('show');

}
