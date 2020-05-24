/**
 * Notificação padrão do telerik
 * @param {any} mensagem
 * @param {any} success
 */
function popupNotification(mensagem, success) {
    //Configurando o popup =======================================
    var popupMessage = $("#popupNotification").data("kendoNotification");
    popupMessage.setOptions({ position: { top: 60, left: null, bottom: 20, right: 20 } });
    popupMessage.show(kendo.toString(mensagem), success === false ? "error" : "success");
}

/**
 * Função que carrega informações de uma requisição
 * @param {any} e
 */
function RequestEnd(e) {
    if (e.type === 'read' || e.type === undefined) return;

    var retorno = e.response;

    if (retorno.Data["0"].errors.length > 0) {
        Error(retorno.Data["0"]);
    }
    else {
        var grid = $("#grid").data("kendoGrid");
        popupNotification(retorno.Data["0"].mensagem, retorno.Data["0"].success);
        grid.dataSource.fetch();
    }
}

/**
 * Função para mudar o nome do titulo da janela popup
 * Essa função é chamada quando clica no botão inserir da grid
 * @param {any} e
 */
 function Change(e) {
    if (e.action === "add") {
        setTimeout(function () {
            $('.k-window-title').text("Adicionar");
        }, 200);
    }
 }

/**
 * 
 * @param {any} e
 */
function Error(e) {

    if (e.errors) {

        for (var i = 0; i < e.errors.length; i++)
        {
            var mensagem = e.errors[i].message;
            popupNotification(mensagem, false);
        }

        var grid = $("#grid").data("kendoGrid");
        grid.one("dataBinding", function (e)
        {
            e.preventDefault();   // cancel grid rebind if error occurs                             
        });
    }
}

/**
 * Função para mudar o alert feio do windows
 * Essa função é chamada quando clica no botão excluir da grid
 * @param {any} e
 */
function onExcluir(e) {

    var row = $(e.target).closest("tr");
    $('<div id="confirmWindow"></div>').appendTo(document.body);
    $("#confirmWindow").kendoConfirm({
        title: "Excluir",
        content: "<p>Deseja realmente excluir o registro ?</p>",
        messages: { okText: "OK" },
        modal: true,
    }).data("kendoConfirm").result.done(function () {
        var grid = $("#grid").data('kendoGrid');
        grid.removeRow(row);
        grid.dataSource.read();
    }).fail(function () {
        //TODO caso queira fazer qnd o usuário cancelar
    });
}
