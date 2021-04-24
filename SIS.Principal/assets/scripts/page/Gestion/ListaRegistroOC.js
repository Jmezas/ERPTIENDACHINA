

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
            AfectasT = $("#checkAfectaStock").is(':checked') === true ? 1 : 0,
            IncluyeIgv = $("#IncluyeIgv").is(':checked') === true ? 1 : 0,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteRegistroPDF?Filltro=' + Filtro + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +
            "&Afecta=" + AfectasT + "&Inlcuye=" + IncluyeIgv +
            "&numPag=" + numPaginas + "&allReg=" + AllReg + "&Cant=" + CantiFill);


    });

    $("#btnExcel").click(function () {

        var Filtro = $("#txtBusqueda").val(),
            FechaInicio = $("#txtFechaInicio").val(),
            FechaFin = $("#txtFechaInicio").val(),
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            AfectasT = $("#checkAfectaStock").is(':checked') === true ? 1 : 0,
            IncluyeIgv = $("#IncluyeIgv").is(':checked') === true ? 1 : 0,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteRegistroExcel?Filltro=' + Filtro + "&FechaIncio=" + FechaInicio + "&FechaFin=" + FechaFin +
            "&Afecta=" + AfectasT + "&Inlcuye=" + IncluyeIgv +
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

    $("#tbCompra").on('click', 'tbody .evento', function () {
        var data = $(this).closest('tr').attr('data-id');

        window.location.href = General.Utils.ContextPath('Gestion/ReporteRegistroDet?Id=' + data)
    })

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

});




function ListaOC() {
    var Filtro = $("#txtBusqueda").val(),
        FechaInicio = $("#txtFechaInicio").val(),
        FechaFin = $("#txtFechaInicio").val(),
        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,

        AfectasT = $("#checkAfectaStock").is(':checked') === true ? 1 : 0,
        IncluyeIgv = $("#IncluyeIgv").is(':checked') === true ? 1 : 0;


    let DesPagina;
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Gestion/ListaRegistroOC'),
        dataType: 'json',
        data: { Filltro: Filtro, FechaIncio: FechaInicio, FechaFin: FechaFin, Afecta: AfectasT, Inlcuye: IncluyeIgv, numPag: numPaginas, allReg: AllReg, Cant: 10 },
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
                        '<tr data-id="' + item["IdCompra"] + '">' +
                        '<td>' + item["Serie"] + '</td>' +
                        '<td>' + item["Documento"]["Nombre"] + '</td>' +
                        '<td>' + item["Text"] + '</td>' +
                        '<td>' + item["FechaRegistro"] + '</td>' +
                        '<td>' + item["FechaPago"] + '</td>' +
                        '<td>' + item["sConcepto"] + '</td>' +
                        '<td>' + item["icluyeIGV"] + '</td>' +
                        '<td>' + item["AfectaStockString"] + '</td>' +
                        '<td>' + item["Proveedor"]["Nombre"] + '</td>' +
                        '<td>' + item["Moneda"]["Nombre"] + '</td>' +
                        '<td>' + formatNumber(item["SubTotal"]) + '</td>' +
                        '<td>' + formatNumber(item["IGV"]) + '</td>' +
                        '<td>' + formatNumber(item["Total"]) + '</td>' +
                        '<td class="text-center">' +
                        '<button class="btn-crud btn btn-info  btn-sm evento" title="Descargar"><i class="fa fa-file-pdf"></i> </button>' +
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