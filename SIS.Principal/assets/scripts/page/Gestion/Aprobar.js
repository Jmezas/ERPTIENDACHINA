

$(function () {

    $("#hdf_Pagina").val('1');
    ListaOC();
    $("#txtBusqueda").keyup(function () {

        $("#txtBusqueda").val();
        ListaOC()
    });

    $('input[type=search]').on('search', function () {
        // search logic here
        ListaOC()
    });


    $("#btnFactura").click(function () {

        var Proveedor = $('#LstVende').val(),

            NumPag = $("#hdf_Pagina").val(),
            allreg = $("#IdTotal").is(':checked') === true ? '0' : '1',
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteCompraPDF?Proveedor=' + Proveedor +
            "&NumPag=" + NumPag + "&all=" + allreg + "&Cantidad=" + CantiFill);
    });

    $("#btnExcel").click(function () {

        var Proveedor = $('#LstVende').val(),

            NumPag = $("#hdf_Pagina").val(),
            allreg = $("#IdTotal").is(':checked') === true ? '0' : '1',
            CantiFill = $("#TotalReg").val();

        window.location.href = General.Utils.ContextPath('Gestion/ReporteCompraExcel?Proveedor=' + Proveedor +
            "&NumPag=" + NumPag + "&AllReg=" + allreg + "&CantiFill=" + CantiFill);

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

    $("#tbCompra").on('click', 'tbody .Aprobar', function () {
        var data = $(this).closest('tr').attr('data-id');
        $("#hdfId").val(data);
        var Nombre = $(this).parents("tr").find("td").eq(0).text();
        $("#Nombre").text(Nombre);

        Swal.fire({
            title: ' ',
            text: '¿Estas Seguro de aprobar la OC Nro  ' + Nombre + ' ?',
            icon: 'success',
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
                    url: General.Utils.ContextPath('Gestion/ApronarOrdenC'),
                    dataType: 'json',
                    data: { IdOc: $("#hdfId").val(), Motivo: "", Flag: 1 },
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

    $("#tbCompra").on('click', 'tbody .Eliminar', function () {
        var data = $(this).closest('tr').attr('data-id');
        $("#hdfId").val(data);
        var Nombre = $(this).parents("tr").find("td").eq(0).text();
        $("#Nombre").text(Nombre);

        Swal.fire({
            title: ' ',
            text: '¿Estas seguro desaprobar la OC Nro  ' + Nombre + ' ?',
            input: 'text',
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'SI',
            cancelButtonColor: '#d33',
            showClass: {
                popup: 'animate__animated animate__fadeInDown'
            },
            hideClass: {
                popup: 'animate__animated animate__fadeOutUp'
            },
            inputAttributes: {
                autocapitalize: 'off'
            }, preConfirm: (login) => {
                $.ajax({
                    async: true,
                    type: 'post',
                    url: General.Utils.ContextPath('Gestion/ApronarOrdenC'),
                    dataType: 'json',
                    data: { IdOc: $("#hdfId").val(), Motivo: login, Flag: 2 },
                    success: function (response) {
                       
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
            },
            allowOutsideClick: () => !Swal.isLoading()
        })

    });
});




function ListaOC() {
    var Filtro = $("#txtBusqueda").val(),

        numPaginas = parseInt($("#hdf_Pagina").val()),
        AllReg = $("#IdTotal").is(':checked') === true ? 0 : 1;
    let DesPagina;
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Gestion/ListaOrdenCompra'),
        dataType: 'json',
        data: { Filltro: Filtro, numPag: numPaginas, allReg: AllReg, Cant: 10 },
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
                    if (item["EstadoDoc"] == "APROBADO" || item["EstadoDoc"] == "CANCELADO") {
                        $tb.find('tbody').append(
                            '<tr data-id="' + item["IdCompra"] + '">' +
                            '<td>' + item["Serie"] + '</td>' +
                            '<td>' + item["Proveedor"]["Nombre"] + '</td>' +
                            '<td>' + item["Text"] + '</td>' +
                            '<td>' + item["Moneda"]["Nombre"] + '</td>' +
                            '<td>' + item["FechaRegistro"] + '</td>' +
                            '<td>' + item["FechaPago"] + '</td>' +
                            '<td>' + item["SubTotal"] + '</td>' +
                            '<td>' + item["IGV"] + '</td>' +
                            '<td>' + item["Total"] + '</td>' +
                            '<td>' + item["EstadoDoc"] + '</td>' +
                            '<td class="text-center">' +

                            '</tr>'
                        );
                    } else {

                        $tb.find('tbody').append(
                            '<tr data-id="' + item["IdCompra"] + '">' +
                            '<td>' + item["Serie"] + '</td>' +
                            '<td>' + item["Proveedor"]["Nombre"] + '</td>' +
                            '<td>' + item["Text"] + '</td>' +
                            '<td>' + item["Moneda"]["Nombre"] + '</td>' +
                            '<td>' + item["FechaRegistro"] + '</td>' +
                            '<td>' + item["FechaPago"] + '</td>' +
                            '<td>' + item["SubTotal"] + '</td>' +
                            '<td>' + item["IGV"] + '</td>' +
                            '<td>' + item["Total"] + '</td>' +
                            '<td>' + item["EstadoDoc"] + '</td>' +
                            '<td class="text-center">' +
                            '<button class="btn-crud btn btn-success  btn-sm Aprobar" title="Aprobar"><i class="fa fa-thumbs-up"></i> </button>' +
                            "&nbsp; " +
                            '<button class="btn-crud btn btn-danger  btn-sm Eliminar" title="Desaprobar"><i class="fa fa-thumbs-down"></i> </button>' +
                            '</td>' +
                            '</tr>'
                        );
                    }
                });
                $("#TotalReg").val(response[0]["TotalR"]);
                $('#pHelperProductos').html('Existe(n) ' + response[0]["TotalR"] + ' resultado(s) para mostrar.');
                $("#lblNumPagina").html(DesPagina);
            }
      
        }
    });
}