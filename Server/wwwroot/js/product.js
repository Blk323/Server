let URI = '/api/product'

function GetAllProducts() {
    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $.ajax({
        url: URI,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            WriteResponse(data);
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}

function GetProduct(id) {
    $.ajax({
        url: URI + "/" + id,
        type: 'GET',
        dataType: 'json',
        success: function (product) {
            $("#createBlock").css('display', 'none');
            $("#editBlock").css('display', 'block');
            $("#editId").val(product.id);
            $("#editTitle").val(product.title);
            $("#editPrice").val(product.price);
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}

function AddProduct() {
    var product = {
        Title: $('#addTitle').val(),
        Price: +$('#addPrice').val().replace(",","."),
    };

    $.ajax({
        url: URI,
        type: 'POST',
        data: JSON.stringify(product),
        contentType: "application/json;charset=utf-8",
        success: function () {
            GetAllProducts();
            alert("Объект успешно добавлен");
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}

function EditItem(el) {
    var id = $(el).attr('data-item');
    GetProduct(id);
}

function EditProduct() {
    var id = $('#editId').val()
    
    var product = {
        Title: $('#editTitle').val(),
        Price: +$('#editPrice').val()
    };

    $.ajax({
        url: URI + "/" + id,
        type: 'PUT',
        data: JSON.stringify(product),
        contentType: "application/json;charset=utf-8",
        success: function () {
            GetAllProducts();
            alert("Объект успешно добавлен");
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}

function DeleteItem(el) {
    var id = $(el).attr('data-item');
    $.ajax({
        url: URI + "/" + id,
        type: 'DELETE',
        contentType: "application/json;charset=utf-8",
        success: function () {
            GetAllProducts();
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}


function ExceptionHandling(errorInfo) {
    var response = jQuery.parseJSON(errorInfo.responseText);

    $('#errors').empty();
    $('#ValidName').empty();
    $('#ValidProductId').empty();
    $('#ValidCount').empty();

    if (response['status'] == 404)
        $("#errors").append("Запрос не дал результата");

    if (response['status'] == 400)
        $("#errors").append("Обнаружена ошибка в данных");

    if (response['Title']) {
        $.each(response['Title'], function (index, item) {
            $('#ValidName').append("<p>" + item + "</p>");
        });
    }

    if (response['Price']) {
        $.each(response['Price'], function (index, item) {
            $('#ValidPrice').append("<p>" + item + "</p>");
        });
    }
}

function ClearAll() {
    $('#errors').empty();
    $('#ValidName').empty();
    $('#ValidPrice').empty();
    $('#editName').val("");
    $('#editProductId').val("");
    $('#editCount').val("");
    $('#addName').val("");
    $('#addProductId').val("");
    $('#addCount').val("");
    $('#findName').val("");
}

function WriteResponse(products) {
    var strResult = "<table><th>Общий список товаров</th><tr><td style=\"background-color: skyblue;\"><b>ID</b></td><td style=\"background-color: skyblue;\"><b>Title</b></td><td style=\"background-color: skyblue;\"><b>Price</b></td></tr>";
    $.each(products, function (index, product) {
        strResult += "<tr> <td>" + product.id + "</td> <td>" + product.title + "</td><td>" + product.price + "</td>" +
            "<td style = \"background-color: #FFA07A;\"><a id='editItem' data-item='" + product.id + "' onclick='EditItem(this);' >Редактировать</a></td>" +
            "<td style = \"background-color: tomato;\"><a id='delItem' data-item='" + product.id + "' onclick='DeleteItem(this);' >Удалить</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);
}