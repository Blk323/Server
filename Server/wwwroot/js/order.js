let URI = '/api/order'
let URI_product = '/api/product'

function GetAllOrders() {
    $("#createBlock").css('display', 'block');
    $("#editBlock").css('display', 'none');
    $("#findBlock").css('display', 'none');

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

    $.ajax({
        url: URI_product,
        type: 'GET',
        dataType: 'json',
        success: function (products) {
            var strResult = "<table><th>Все товары</th><tr><td><b>ID</b></td><td><b>Title</b></td><td><b>Price</b></td></tr>";
            $.each(products, function (index, product) {
                strResult += "<tr><td>" + product.id + "</td><td>" + product.title + "</td><td>" + product.price + "</td></tr>";
            });
            strResult += "</table>";
            $("#productList").html(strResult);
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}

function GetOrders(Name) {
    if (!Name || !Name.trim())
        return;

    $.ajax({
        url: URI + "/" + Name,
        type: 'GET',
        dataType: 'json',
        success: function (orders) {
            var strResult = "<table><th>" + "Все заказы " + orders[0].name + "<tr></th><td>ID</td><td>Product title</td><td>Single product price</td><td>Product count</td><td>Date of order</td></tr>";
            $.each(orders, function (index, order) {
                strResult += "<tr> <td>" + order.id + "</td><td>" + order.productTitle + "</td><td>" + order.productPrice + "</td><td>" + order.count + "</td><td>" + order.dateOfOrder + "</td>" +
                    "<td><a id='editItem' data-item='" + order.id + "' onclick='EditItem(this);' >Редактировать</a></td>" +
                    "<td><a id='delItem' data-item='" + order.id + "' onclick='DeleteItem(this);' >Удалить</a></td></tr>";
            });
            strResult += "</table>";
            $("#tableBlock").html(strResult);
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}

function GetOrder(id) {
    $.ajax({
        url: URI + "/element/" + id,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            ShowOrder(data);
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }
    });
}

function AddOrder() {
    var order = {
        Name: $('#addName').val(),
        ProductId: +$('#addProductId').val(),
        Count: +$('#addCount').val(),
    };

    $.ajax({
        url: URI,
        type: 'POST',
        data: JSON.stringify(order),
        contentType: "application/json;charset=utf-8",
        success: function () {
            GetAllOrders();
            alert("Объект успешно добавлен");
        },
        error: function (errorInfo) {
            ExceptionHandling(errorInfo);
        }  
    });
}

function EditItem(el) {
    var id = $(el).attr('data-item');
    GetOrder(id);

}

function ShowOrder(order) {
    if (order != null) {
        $("#createBlock").css('display', 'none');
        $("#findBlock").css('display', 'none');
        $("#editBlock").css('display', 'block');
        $("#editId").val(order.id);
        $("#editName").val(order.name);
        $("#editProductId").val(order.productId);
        $("#editCount").val(order.count);
    }
    else {
        alert("Такой товар не существует");
    }
}

function EditOrder() {
    var id = $('#editId').val()

    var order = {
        Name: $('#editName').val(),
        ProductId: +$('#editProductId').val(),
        Count: +$('#editCount').val(),
    };

    $.ajax({
        url: URI + "/" + id,
        type: 'PUT',
        data: JSON.stringify(order),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            GetAllOrders();
            alert("Объект успешно изменен");
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
            GetAllOrders();
            alert("Объект успешно удален");
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

    if (response['Name']) {
        $.each(response['Name'], function (index, item) {
            $('#ValidName').append("<p>" + item + "</p>");
        });
    }

    if (response['Count']) {
        $.each(response['Count'], function (index, item) {
            $('#ValidCount').append("<p>" + item + "</p>");
        });
    }

    if (response['ProductId']) {
        $.each(response['ProductId'], function (index, item) {
            $('#ValidProductId').append("<p>" + item + "</p>");
        });
    }
}

function ClearAll() {
    $('#errors').empty();
    $('#ValidName').empty();
    $('#ValidProductId').empty();
    $('#ValidCount').empty();
    $('#editName').val("");
    $('#editProductId').val("");
    $('#editCount').val("");
    $('#addName').val("");
    $('#addProductId').val("");
    $('#addCount').val("");
    $('#findName').val("");
}

function WriteResponse(orders) {
    var strResult = "<table><th>Все заказы</th><tr><td style=\"background-color: skyblue;\"><b>ID</b></td><td style=\"background-color: skyblue;\"><b>Name</b></td><td style=\"background-color: skyblue;\"><b>Product title</b></td><td style=\"background-color: skyblue;\"><b>Single product price</b></td><td style=\"background-color: skyblue;\"><b>Product count</b></td><td style=\"background-color: skyblue;\"><b>Date of order</b></td></tr>";
    $.each(orders, function (index, order) {
        strResult += "<tr> <td>" + order.id + "</td><td>" + order.name + "</td><td>" + order.productTitle + "</td><td>" + order.productPrice + "</td><td>" + order.count + "</td><td>" + order.dateOfOrder + "</td>" +
            "<td style = \"background-color: #FFA07A;\"><a id='editItem' data-item='" + order.id + "' onclick='EditItem(this);' >Редактировать</a></td>" +
            "<td style = \"background-color: tomato;\"><a id='delItem' data-item='" + order.id + "' onclick='DeleteItem(this);' >Удалить</a></td></tr>";
    });
    strResult += "</table>";
    $("#tableBlock").html(strResult);
}