
var Lista = {
    CargarCombo: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 17 },
            success: function (response) {

                $("#lstCliente").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstCliente"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
                ListaGeneral();
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
                $("#lstMoneda").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {

                        $('select[name="lstMoneda"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));
                    });
                }
                ListaGeneral();
            }

        });
    },
}
$(function () {
    Lista.CargarCombo();
    Lista.CargarMoneda();
    $("#lstCliente").select2()
    $("#lstMoneda").select2()
    $("#hdf_Pagina").val('1');
    ListaGeneral();

    $("#btnFiltrar").click(function () {
        ListaGeneral();
    })
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

    $("#btnFactura").click(function () {
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
        var Filtro = $("#lstCliente").val(),
            moneda = $("#lstMoneda").val(),
            FechaInicio = inicio,
            FechaFin = Fin,
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Cotizacion/ReporteCompraPDF?cliente=' + Filtro + "&moneda=" + moneda + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +

            "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill);


    });
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
        var Filtro = $("#lstCliente").val(),
            moneda = $("#lstMoneda").val(),
            FechaInicio = inicio,
            FechaFin = Fin,
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Cotizacion/ReporteCompraExcel?cliente=' + Filtro + "&moneda=" + moneda + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +

            "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill);
    });

    $("#tbcotizacion").on('click', 'tbody .evento', function () {
        var data = $(this).closest('tr').attr('data-id');

        var URL = General.Utils.ContextPath('Cotizacion/imprimirCotizacion?Id=' + data);
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

    var Filtro = $("#lstCliente").val(),
        moneda = $("#lstMoneda").val(),
        FechaInicio = inicio,
        FechaFin = Fin,
        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1;
    let DesPagina;
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Cotizacion/ListaCotizacion'),
        dataType: 'json',
        data: { cliente: Filtro, moneda: moneda, FechaIncio: FechaInicio, FechaFin: FechaFin, numPag: numPaginas, allReg: AllReg, Cant: 10 },
        success: function (response) {
            console.log(response);
            var $tb = $("#tbcotizacion");
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
                        '<tr data-id="' + item["Idcotzacion"] + '">' +
                        '<td>' + item["serie"] + '</td>' +
                        '<td>' + item["cliente"]["Nombre"] + "-" + item["cliente"]["NroDocumento"] + '</td>' +
                        '<td>' + item["moneda"]["Nombre"] + '</td>' +
                        '<td>' + item["fechaEmision"] + '</td>' +
                        '<td>' + formatNumber(item["cantidad"]) + '</td>' +
                        '<td>' + formatNumber(item["grabada"]) + '</td>' +
                        '<td>' + formatNumber(item["inafecta"]) + '</td>' +
                        '<td>' + formatNumber(item["exonerada"]) + '</td>' +
                        '<td>' + formatNumber(item["igv"]) + '</td>' +
                        '<td>' + formatNumber(item["total"]) + '</td>' +
                        '<td>' + formatNumber(item["descuento"]) + '</td>' +
                        '<td class="text-center">' +
                        '<button class="btn-crud btn btn-info  btn-sm evento"  data-toggle="modal" data-target="#Report"  title="Ver Detalle"><i class="fa fa-search"></i> </button>' +
                        '</td>' +
                        '</tr>'
                    );


                });
            }
            $("#TotalReg").val(response[0]["TotalR"]);
            $('#pHelperProductos').html('Existe(n) ' + response[0]["TotalR"] + ' resultado(s) para mostrar.');
            $("#lblNumPagina").html(DesPagina);
        }
    });
}
function fileDownnload(url) {
    var options = {
        height: "600px",
        page: '2',
        pdfOpenParams: {
            view: 'FitV',
            pagemode: 'thumbs',
            search: 'lorem ipsum'
        }
    };
    PDFObject.embed(url, "#PDFViewer", options);
    // $("#myReportPrint").modal('show');

}
