

$(function () {

    $("#hdf_Pagina").val('1');

   
    setTimeout(function () { ListaOC() }, 1000);
     
 

    $("#txtBusqueda").keyup(function () {

        $("#txtBusqueda").val();
        ListaOC()
    });

    $('input[type=search]').on('search', function () {
        $("#lstSucursalCab").val(),
        ListaOC()
    });
    $("#lstSucursalCab").change(function () {

        ListaOC();
    })

    $("#btnFactura").click(function () {

        var Filtro = $("#txtBusqueda").val(),
            Sucursal = $("#lstSucursalCab").val(),          
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteStockPDF?Filtro=' + Filtro + "&numPag=" + numPaginas + "&allReg=" + AllReg + "&cantFill=" + CantiFill);


    });

    $("#btnExcel").click(function () {

        var Filtro = $("#txtBusqueda").val(),
            Sucursal = $("#lstSucursalCab").val(),
            numPaginas = parseInt($("#hdf_Pagina").val()),
            AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1,
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteStockExcel?Filtro=' + Filtro + "&numPag=" + numPaginas + "&allReg=" + AllReg + "&cantFill=" + CantiFill);
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
     
    
    $("#btnFiltrar").click(function () {
        ListaOC();
    })
  
});




function ListaOC() {
    var Filtro = $("#txtBusqueda").val(),  
        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1;
    let DesPagina;
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Gestion/ListaStock'),
        dataType: 'json',
        data: { Filtro: Filtro, numPag: numPaginas, allReg: AllReg, cantFill: 10 },
        success: function (response) {
            console.log(response);
            var $tb = $("#tbStock");
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
                        '<tr>' +
                        '<td>' + item["Item"] + '</td>' +
                        '<td>' + item["Material"]["Codigo"] + '</td>' +
                        '<td>' + item["Material"]["Nombre"] + '</td>' +
                        '<td>' + item["Material"].Marca.Nombre + '</td>' +
                        '<td>' + item["Material"].Unidad.Nombre + '</td>' +
                        '<td>' + item["Material"].Categoria.Nombre + '</td>' +
                        '<td>' + item["Almacen"].Nombre + '</td>' +
                        '<td>' + item["Cantidad"] + '</td>' +
                        '<td>' + item["Num"] + '</td>' +
                        '<td>' + item["Text"] + '</td>' +
                        '</tr>'
                    );


                });
                $("#TotalReg").val(response[0]["Total"]);
                $('#pHelperProductos').html('Existe(n) ' + response[0]["Total"] + ' resultado(s) para mostrar.');
                $("#lblNumPagina").html(DesPagina);
            }
        
        }
    });
}

 