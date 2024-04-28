function deleteTodo(i, name)
{
    $.ajax({
        url: 'Home/Delete',
        type: 'POST',
        data: {
            id: i,
            name: name
        },
        success: function (response) {
            if (response.success) {
                window.location.reload();
                alert(`Todo item "${name}" deleted successfully.`);
            } else {
                alert(response.message);
            }
        }
    });
}

function updateTodo(i, name) {
    $.ajax({
        url: 'Home/PopulateForm',
        type: 'GET',
        data: {
            id: i,
            name: name
        },
        dataType: 'json',
        success: function (response) {
            $("#Name").val(response.name);
            $("#Id").val(response.id);
            $("#form-button").val("Update Todo");
            $("#form-action").attr("action", "/Home/Update");
        }
    });
}

