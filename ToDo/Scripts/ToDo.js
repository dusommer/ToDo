function CloseModal(id) {
    $("#" + id).modal("hide");
}
function AddEditItem(id, parentId, description) {
    var modalLabel = "Insira novo item";
    document.getElementById("btnConvertItem").classList.add("d-none");
    document.getElementById("btnRemoveItem").classList.add("d-none");
    document.getElementById("inptParentItemId").value = "";
    document.getElementById("inptDescription").value = "";
    document.getElementById("inptItemId").value = "";

    if (id !== undefined && id !== "") {
        modalLabel = "Edite o item";
        document.getElementById("btnRemoveItem").classList.remove("d-none");
        document.getElementById("inptParentItemId").value = "";
        document.getElementById("inptDescription").value = description;
        document.getElementById("inptItemId").value = id;
    }

    if (parentId !== undefined && parentId !== "") {
        modalLabel = "Insira novo sub-item";
        if (id !== undefined && id !== "") {
            modalLabel = "Edite o sub-item";
            document.getElementById("btnConvertItem").classList.remove("d-none");
        }
        document.getElementById("inptParentItemId").value = parentId;
    }

    document.getElementById("itemModalLabel").innerText = modalLabel;
    $("#addEditItemModal").modal("show");
}

function OpenModalEmail() {
    document.getElementById("inptEmail").value = "";
    document.getElementById("erroEmail").innerText = "";
    $("#EmailModal").modal("show");
}
function SendEmail(url) {
    var email = document.getElementById("inptEmail").value;
    var nomeLista = document.getElementById("nomeLista").innerText;
    var userEmail = document.getElementById("userEmail").innerText;
    $.ajax({
        url: url,
        data: { email: email, nomeLista: nomeLista, userEmail: userEmail },
        type: 'POST',
        success: function (data) {
            if (data.success)
                CloseModal("EmailModal");
            else
                document.getElementById("erroEmail").innerText = data.erroMessage;
        },
        error: function (xhr, status, error) {
            CloseModal("EmailModal");
        }
    });
}

function AddList() {
    document.getElementById("inptName").value = "";
    $("#addListModal").modal("show");
}