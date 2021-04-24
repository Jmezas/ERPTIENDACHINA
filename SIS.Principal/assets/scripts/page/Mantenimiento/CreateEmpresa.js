

var Lista = {

    CargarDepartamento: function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListarUbigeo"),
            dataType: 'json',
            data: { Acction: "DEPARTAMENTO", IdPais: '001', IdDep: "", IdProv: "", IdDis: "" },
            success: function (response) {
                $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
                $("#lstProvincia").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 

                    $.grep(response, function (oDocumento) {
                        $('select[name="lstDepartamento"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                    });
                }
            }
        });
    },
}

$(function () {
    Lista.CargarDepartamento()
    $('#imgSalida').attr("src", "/Imagenes/Usuario/avatar.jpg");

    if ($("#hdfId").val() != 0) {
        Obtener($("#hdfId").val())
    }
    $("#btnbuscar").click(function () {
        var RUC = $("#txtDocumento").val();
        if (isNaN(RUC) || RUC < 10000000000 || RUC > 99999999999) {
            General.Utils.ShowMessage(TypeMessage.Error, 'El RUC debe contener 11 dígitos');
        } else {
            $.ajax({
                type: 'post',
                url: General.Utils.ContextPath('Shared/SearchSunat'),
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                data: {
                    numeroRuc: RUC

                },
                success: function (response) {
                    //console.log(response)
                    if (response == null) {
                        General.Utils.ShowMessage("error", "Nro. de ruc invalido, ingrese los datos manualmente");
                    } else {


                        $("#txtRazon").val(response.RazonSocial);
                        $("#txtDireccion").val(response.Direccion);
                    }
                }
            });
        }
    });

    $("#lstDepartamento").change(function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListarUbigeo"),
            dataType: 'json',
            data: { Acction: "PROVINCIA", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: "", IdDis: "" },
            success: function (response) {
                $("#lstProvincia").empty();
                $("#lstDistrito").empty();
                $("#lstProvincia").append($('<option>', { value: 0, text: 'Seleccione' }));
                $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                    $.grep(response, function (oDocumento) {
                        $('select[name="lstProvincia"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                    });
                }
            }

        });
    });

    $("#lstProvincia").change(function () {
        $.ajax({
            async: true,
            type: 'post',
            url: General.Utils.ContextPath("Shared/ListarUbigeo"),
            dataType: 'json',
            data: { Acction: "DISTRITO", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: $("#lstProvincia").val(), IdDis: "" },
            success: function (response) {
                $("#lstDistrito").empty();
                $("#lstDistrito").append($('<option>', { value: 0, text: 'Seleccione' }));
                if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                    $.grep(response, function (oDocumento) {
                        $('select[name="lstDistrito"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                    });
                }
            }

        });
    });

    $('#Logo').change(function (e) {
        addImage(e);
    });

    $("#btnGuardar").click(function () {
        var $form = $("#dvRegistro");
        var data = new FormData($('#frm-adjuntar')[0]);  //el fromData

        var oDatos = General.Utils.SerializeForm($form);

        if (General.Utils.ValidateForm($form)) {
            var oEmpresa = {
                Id: $("#hdfId").val(),
                RUC: $("#txtDocumento").val(),
                RazonSocial: $("#txtRazon").val(),
                Ubigeo: $("#lstDepartamento").val() + $("#lstProvincia").val() + $("#lstDistrito").val(),
                Direccion: $("#txtDireccion").val(),
                Correo: $("#txtCorreo").val(),
                Contrasenia: $("#txtPass").val(),
                Telefono: $("#txtTelefono").val(),
                Celular: $("#txtCelular").val(),
                Logo: $("#hdfImg").val(),
                Certificado: $("#hdfImgCertificado").val(),
                EUbigeo: {
                    CodigoDepartamento: $("#lstDepartamento").val(),
                    CodigoProvincia: $("#lstProvincia").val(),
                    CodigoDistrito: $("#lstDistrito").val(),
                },
                PaginaWeb: $("#txtPagina").val(),
                UsuarioSol: $("#txtUsuario").val(),
                ClaveSol: $("#txtClaveSol").val(),
                //Certificado: $("#").val(), 
                ClaveCertificado: $("#txtClaveCertificado").val(),

            }

            for (var prop in oEmpresa) {
                var value = oEmpresa[prop];
                if (typeof value === 'object') {
                    for (var valueProp in value) {
                        data.append('oDatos.' + prop + '.' + valueProp, value[valueProp]);
                    }
                } else {
                    data.append('oDatos.' + prop, value);
                }
            }
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath('Mantenimiento/InstEmpresa'),
                beforeSend: General.Utils.StartLoading,
                complete: General.Utils.EndLoading,
                dataType: 'json',
                data: data,
                contentType: false,
                processData: false,
                success: function (response) {
                    Swal.fire({
                        title: '',
                        text: response.Message,
                        icon: response.Id,
                        confirmButtonColor: '#3085d6',
                        confirmButtonText: 'Ok'
                    }).then((result) => {
                        if (result.value) {
                            reload()
                        }
                    })

                },
            });
        }
    })

})
function reload() {

}

var Obtener = function (Id) {
    $.ajax({
        async: true,
        type: 'post',
        url: General.Utils.ContextPath('Mantenimiento/ObtenerEmpresa'),
        dataType: 'json',
        data: { IdEmpresa: Id },
        success: function (response) {
            console.log(response);
            $("#hdfId").val(response.Id);
            $("#txtDocumento").val(response.RUC);
            $("#txtRazon").val(response.Nombre);
            $("#txtCorreo").val(response.Correo);
            $("#txtPass").val(response.Contrasenia);
            $("#txtDireccion").val(response.Direccion);
            $("#txtTelefono").val(response.Telefono);
            $("#txtCelular").val(response.Celular);

            $("#txtPagina").val(response.PaginaWeb);
            $("#txtUsuario").val(response.UsuarioSol);
            $("#txtClaveSol").val(response.ClaveSol);
            $("#txtClaveCertificado").val(response.ClaveCertificado);


            $("#imgSalida").attr("src", response.Logo);
            //   $("#imgSalida").attr("src", response.Logo);

            var Img = response.Logo.split("/");
            console.log(Img[3])
            $("#hdfImg").val(Img[3]);

            var certificado = response.Certificado.split("/");
            console.log(certificado[2])
            $("#hdfImgCertificado").val(certificado[2]);
            ///cbo
            $("#lstDepartamento").val(response.EUbigeo.CodigoDepartamento);
            var Prov = response.EUbigeo.CodigoProvincia;
            var Dist = response.EUbigeo.CodigoDistrito;

            //provincia
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListarUbigeo"),
                dataType: 'json',
                data: { Acction: "PROVINCIA", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: "", IdDis: "" },
                success: function (response) {

                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                        $.grep(response, function (oDocumento) {
                            $('select[name="lstProvincia"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                        });
                        $("#lstProvincia").val(Prov);
                    }

                }

            });

            //distrito
            $.ajax({
                async: true,
                type: 'post',
                url: General.Utils.ContextPath("Shared/ListarUbigeo"),
                dataType: 'json',
                data: { Acction: "DISTRITO", IdPais: '001', IdDep: $("#lstDepartamento").val(), IdProv: Prov, IdDis: "" },
                success: function (response) {
                    if (!response.hasOwnProperty('ErrorMessage')) { // Si la petición no emitió error 
                        $.grep(response, function (oDocumento) {
                            $('select[name="lstDistrito"]').append($('<option>', { value: oDocumento["CodigoInei"], text: oDocumento["Nombre"] }));

                        });
                        $("#lstDistrito").val(Dist);
                    }
                }

            });

        }

    });

}

function addImage(e) {
    var file = e.target.files[0],
        imageType = /image.*/;

    if (!file.type.match(imageType))
        return;

    var reader = new FileReader();
    reader.onload = fileOnload;
    reader.readAsDataURL(file);

    var f = e.target.files,
        len = f.length;
    for (var i = 0; i < len; i++) {
        console.log(f[i].name);
        $("#hdfImagen").val(f[i].name);
    }
}
function fileOnload(e) {
    var result = e.target.result;
    $('#imgSalida').attr("src", result);

}