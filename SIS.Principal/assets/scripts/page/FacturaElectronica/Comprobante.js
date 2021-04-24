var Lista = {
    CargarSucursal: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListaCombo"),
            dataType: 'json',
            data: { Id: 7 },
            success: function (response) {
                $("#lstSucursal").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstSucursal"]').append($('<option>', { value: oDocumento["Id"], text: oDocumento["Nombre"] }));

                    });
                }
                cargarComprobantes()
            }
        });
    }
}
var CompPagoEnviar = [];
$(function () {

    Lista.CargarSucursal();
    cargarComprobantes();
    $('input[name="fechaInicio"]').datepicker({
        dateFormat: 'yy-mm-dd',
        prevText: '<i class="fa fa-angle-left"></i>',
        nextText: '<i class="fa fa-angle-right"></i>',
        container: $('.container')
    });

    $('#lstSucursal').change(function () {
        cargarComprobantes();
    });

    $('#btnFiltrar').click(function () {
        cargarComprobantes();
    });

    $('#tbComprobante tbody').delegate('.chkEnviar', 'click', function () {
        var tmp = $(this).parent().parent().attr('data-id');
        if ($(this).prop('checked')) {
            CompPagoEnviar.push(tmp);
        } else {
            EliminarCompPago(tmp);
        }
    });
    $('#selectAll').click(function () {
        if ($(this).hasClass('seleccionando')) {
            $(this).removeClass('seleccionando');
            $(this).addClass('deseleccionando');
            $('.chkEnviar:not(:checked)').trigger('click');
            $(this).html('Deseleccionar todo');
        }
        else {
            $(this).removeClass('deseleccionando');
            $(this).addClass('seleccionando');
            $('.chkEnviar:checked').trigger('click');
            $(this).html('Seleccionar todo');
        }
    });
    $('#dvComprobantes').scroll(function () {
        var iComienzo = $('#tbComprobante').find('tbody tr').length,
            iSucursal = $('#lstSucursal').val(),
            vFechaEmi = $('input[name="fechaInicio"]').val();
        // Si el scroll se encuentra abajo de todo el DOM
        if ($(this).scrollTop() + $(this).innerHeight() >= $(this)[0].scrollHeight) {
            // Mostrar más resultados
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('FacturaElectronica/ListarComprobante'),
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                dataType: 'json',
                data: { Comienzo: 0, Medida: 20, Sucursal: iSucursal, FechaEmi: vFechaEmi },
                success: function (response) {
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error
                        var $tb = $('#tbComprobante');
                        if (response["Total"] == 0) {
                            return false;
                        }
                        if (response["Datos"].length == 0) {
                            General.Utils.ShowMessage(TypeMessage.Information, 'No existen m&aacute;s resultados para mostrar.');
                        } else {
                            $.grep(response["Datos"], function (item) {
                                $tb.find('tbody').append(
                                    '<tr data-id="' + item["empresa"]["RUC"] + '|' + item["fechaEmision"] + '|' + item["Documento"].Nombre + '|' + item["serie"] + '">' +
                                    '<td class="text-center">' + item["Documento"].Nombre + '</td>' +
                                    '<td class="text-center">' + item["serie"] + '</td>' +
                                    '<td class="text-center">' + item["fechaEmision"] + '</td>' +
                                    '<td class="text-center">' + formatNumber(item["total"]) + '</td>' +
                                    '<td class="text-center">' + '<input type="checkbox" class="chkEnviar"></input>' + '</td>' +
                                    '</tr>'
                                );
                            });
                            $('#pHelperProductos').html('Existe(n) ' + response["TotalR"] + ' resultado(s) para mostrar.' +
                                (response["TotalR"] > 0 ? ' Del&iacute;cese hacia abajo para visualizar m&aacute;s...' : ''));
                        }
                    }
                }
            });
        }
    });
    $('#btnEnviar').click(function () {
        if (CompPagoEnviar.length === 0) {
            General.Utils.ShowMessage(TypeMessage.Error, 'No ha seleccionado Comprobantes a Enviar')
        } else {
            $.ajax({
                url: General.Utils.ContextPath('FacturaElectronica/EnviarCompPagoFact'),
               // beforeSend: General.Utils.StartLoading,
                //complete: General.Utils.EndLoading,
                type: 'post',
                data: {
                    sLista: JSON.stringify(CompPagoEnviar)
                },
                success: function (response) {
                    console.log(response);
                    General.Utils.ShowModalMessage(response.Id, "Información de Envío a SUNAT", response.Message, function () { location.reload() });
                }
            });
        }
    });
}); 

function EliminarCompPago(id) {
    CompPagoEnviar = CompPagoEnviar.filter((item) => {
        return item !== id;
    });
}
function cargarComprobantes() {
    CompPagoEnviar = [];
    var iSucursal = $('#lstSucursal').val();
    var vFechaEmi = $('input[name="fechaInicio"]').val();
    $.ajax({
        async: false,
        type: 'post',
        url: General.Utils.ContextPath('FacturaElectronica/ListarComprobante'),
        beforeSend: General.Utils.StartLoading,
        complete: General.Utils.EndLoading,
        dataType: 'json',
        data: { Comienzo: 0, Medida: 20, Sucursal: iSucursal, FechaEmi: vFechaEmi },
        success: function (response) {
            console.log(response)
            if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error
                var $tb = $('#tbComprobante')
                $tb.find('tbody').empty();
                if (response.length == 0) {
                    $tb.find('tbody').html('<tr><td colspan="6">No hay resultados para el filtro ingresado</td></tr>');
                    $('#pHelperProductos').html('');
                } else {
                    $.grep(response, function (item) {
                        $tb.find('tbody').append(
                            '<tr data-id="' + item["empresa"]["RUC"] + '|' + item["fechaEmision"] + '|' + item["Documento"].Nombre + '|' + item["serie"] + '">' +
                            '<td class="text-center">' + item["Documento"].Nombre + '</td>' +
                            '<td class="text-center">' + item["serie"] + '</td>' +
                            '<td class="text-center">' + item["fechaEmision"] + '</td>' +
                            '<td class="text-center">' + formatNumber(item["total"]) + '</td>' +
                            '<td class="text-center">' + '<input type="checkbox" class="chkEnviar"></input>' + '</td>' +
                            '</tr>'
                        );
                    });
                    $('#pHelperProductos').html('Existe(n) ' + response[0]["TotalR"] + ' resultado(s) para mostrar.' +
                        (response[0]["TotalR"] > 0 ? ' Del&iacute;cese hacia abajo para visualizar m&aacute;s...' : ''));
                }
            }
        }
    });
};