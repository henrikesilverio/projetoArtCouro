﻿$.extend(Portal, {
    Search: function (settings) {
        $(settings.WidgetSeletor).hide();
        $(settings.BotaoPesquisarSeletor).on("click", function () {
            var listaCampos = $(settings.FormularioSeletor).find("input, select");
            var contador = 0;
            $(listaCampos).each(function () {
                if (this.value === "") {
                    contador++;
                }
            });

            if (listaCampos.length === contador) {
                Portal.PreencherAlertaErros("Preencha pelo menos um campo", settings.AlertaMensagensSeletor);
                return;
            }

            var formData = $(settings.FormularioSeletor).serializeArray();
            if ($(settings.FormularioSeletor).valid()) {
                $.ajax({
                    url: settings.UrlDados,
                    data: formData,
                    type: "POST",
                    traditional: true
                }).success(function (ret) {
                    if (ret.TemErros) {
                        Portal.PreencherAlertaErros(ret.Mensagem, settings.AlertaMensagensSeletor);
                    } else {
                        $(settings.TabelaSeletor).dataTable({
                            //"paginationType": "bootstrap_full",
                            "sDom": "<'dt-toolbar'<'col-xs-12 col-sm-6'f><'col-sm-6 col-xs-6 hidden-xs'T>r>" +
                                    "t<'dt-toolbar-footer'<'col-sm-6 col-xs-12 hidden-xs'i><'col-sm-6 col-xs-12'p>>",
                            "oTableTools": {
                                "aButtons": [
                                   "copy",
                                   "csv",
                                   "xls",
                                      {
                                          "sExtends": "pdf",
                                          "sTitle": "SmartAdmin_PDF",
                                          "sPdfMessage": "SmartAdmin PDF Export",
                                          "sPdfSize": "letter"
                                      },
                                    {
                                        "sExtends": "print",
                                        "sMessage": "Generated by SmartAdmin <i>(press Esc to close)</i>"
                                    }
                                ],
                                "sSwfPath": "js/plugin/datatables/swf/copy_csv_xls_pdf.swf"
                            },
                            "autoWidth": true,
                            "iDisplayLength": 15,
                            "aaData": ret.ObjetoRetorno,
                            "aoColumns": settings.OrdenacaoDoCabecalho,
                            "aaSorting": [],
                            "oLanguage": {
                                "sInfo": "_START_ a _END_ em _TOTAL_ " + settings.TituloRodape,
                                "oPaginate": {
                                    "sFirst": "<<",
                                    "sLast": ">>",
                                    "sPrevious": "<",
                                    "sNext": ">"
                                }
                            }
                        });
                        $(settings.WidgetSeletor).show();
                    }
                }).error(function (ex) {
                    Portal.PreencherAlertaErros(ex.responseText, settings.AlertaMensagensSeletor);
                });
            }
        });
    }
});
