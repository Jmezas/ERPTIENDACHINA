$(function () {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath("Reportes/ListaProductoMasVendido"),
        dataType: 'json',
        //data: { Id: 20 },
        success: function (response) {
            var $tb = $("#tbVendidas");
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
                            //'<td>' + item["id"] + '</td>' +
                            '<td>' + item["codigo"] + '</td>' +
                            '<td>' + item["producto"] + '</td>' +
                            '<td>' + formatNumber(item["cantidad"]) + '</td>' +
                        '</tr>'
                    );
                });
                $("#TotalReg").val(response[0]["TotalR"]);
                $('#pHelperProductos').html('Existe(n) ' + response[0]["TotalR"] + ' resultado(s) para mostrar.');
                $("#lblNumPagina").html(DesPagina);
            }
        }
    });
});