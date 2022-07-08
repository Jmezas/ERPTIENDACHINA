var Lista = {
    CargarUsuario: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Mantenimiento/ListaCBO_UsuarioAlmacen"),
            dataType: 'json',
            data: { Flag: 2 },
            success: function (response) {
                $("#lstUsuario").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstUsuario"]').append($('<option>', { value: oDocumento["Usuario"]["Usuario"], text: oDocumento["Usuario"]["Nombre"] }));

                    });
                }
               // ListaGeneral();
            }
        });
    },
    CargarSucursal: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaComboId"),
            dataType: 'json',
            data: { flag: 12, Id: 1 },
            success: function (response) {

                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error
                    $("#lstSucursal").append($('<option>', { value: 0, text: 'Seleccione' }));
                    $.grep(response, function (oDocumento) {
                        $('select[name="lstSucursal"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));

                    });
                    $('input[name="TextSucursal"]').val($("#lstSucursal option:selected").text())
                }
               // ListaGeneral();
            }
        });
    },
    Vars: {
        Detalle: []
    },
}

$(function () {
    $("#hdf_Pagina").val('1');
    Lista.CargarUsuario();
    Lista.CargarSucursal();
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
            numPaginas = parseInt($("#hdf_Pagina").val()),
            tipVenta = parseInt($("#lstTipoVenta").val()),
            //vendedor = $("#lstUsuario").val() == null ? 0 : parseInt($("#lstUsuario").val()),
            //sucursal = $("#lstSucursal").val() == null ? 0 : parseInt($("#lstSucursal").val()),
            vendedor = $("#lstUsuario").val() == "0" ? "" : $("#lstUsuario").val(),
            sucursal = $("#lstSucursal").val() == null ? 0 : parseInt($("#lstSucursal").val()),
            CantiFill = parseInt($("#TotalReg").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1;


        window.location.href = General.Utils.ContextPath('Reportes/ResumenExcelVentasXProductos?cliente=' + Filtro + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +
            "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill + "&vendedor=" + vendedor + "&sucursal=" + sucursal + "&venta=" + tipVenta);
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

});

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
        vendedor = $("#lstUsuario").val() == "0" ? "" : $("#lstUsuario").val(),
        sucursal = $("#lstSucursal").val() == null ? parseInt(0) : parseInt($("#lstSucursal").val()),
        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1;

    let DesPagina;

    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('reportes/ResumenVentasXProducto'),
        dataType: 'json',
        beforeSend: General.Utils.StartLoading,
        complete: General.Utils.EndLoading,
        data: { flag: tipVenta, filtro: Filtro, FechaIncio: FechaInicio, FechaFin: FechaFin, numPag: numPaginas, allReg: AllReg, Cant: 10, vendedor: vendedor, sucursal: sucursal },
        success: function (response) {
            //console.log(response);
            var $tb = $("#tbVenta");
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
                        '<td>' + item["Venta"]["fechaEmision"] + '</td>' +
                        '<td>' + item["Venta"]["serie"] + '-' + item["Venta"]["numero"] + '</td>' +
                        '<td>' + item["Venta"]["moneda"]["Nombre"] + '</td>' +
                        '<td>' + item["Venta"]["cliente"]["NroDocumento"] + '-' + item["Venta"]["cliente"]["Nombre"] + '</td>' +
                        '<td>' + item["material"]["Codigo"] + '</td>' +
                        '<td>' + item["material"]["Nombre"] + '</td>' +
                        '<td>' + item["material"]["Unidad"]["Nombre"] + '</td>' +
                        '<td>' + formatNumber(item["cantidad"]) + '</td>' +
                        '<td>' + formatNumber(item["precio"]) + '</td>' +
                        '<td>' + formatNumber(item["descuento"]) + '</td>' +
                        '<td>' + formatNumber(item["precio"] - item["descuento"]) + '</td>' +
                        '<td>' + formatNumber(item["Importe"]) + '</td>' +
                        //'<td>' + formatNumber(item["Venta"]["CostoEnvio"]) + '</td>' +
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
//function fileDownnload(url) {
//    var options = {
//        height: "600px",
//        page: '2'

//    };
//    PDFObject.embed(url, "#PDFViewer", options);

//}
