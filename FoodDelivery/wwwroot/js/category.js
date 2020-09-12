var dataTable;

$(document).ready(function () {
    loadList();
});

function loadList() {
    dataTable = $('#DT_load').dataTable({
        "ajax": {
            "url": "/api/category",
            "type": "GET",
            "data":"json"
        },
        "columns": [
            { data: "name", width: "40%" },
            { data: "displayOrder", width: "30%" },
            { data: "id", 
                "render": function (data) {
                    return `
                            <div class=""text-center>
                                <a href="/Admin/Category/Upsert?id=${data}" class="btn btn-success text-white" style="curose:pointer; width:100px">
                                <i class="far fa-edit"></i> Edit</a>
                                <a  onClick="Delete('/api/category/'+${data})" class="btn btn-danger text-white" style="curose:pointer; width:100px">
                                <i class="far fa-trash-alt"></i>Delete</a>
                            </div>`;

                }, width: "30%"
            }
        ],
        "language": {
            "emptyTable" : "no data found"
        },
        "width" : "100%"
    });
}

function Delete (url) {
    swal({
        title: "Are you sure you want to delete?",
        text: "You will not be able to restore the data!",
        icon: "warning",
        buttons: true,
        dangerMode: true
    }).then((willDelete) => {
        if (willDelete) {
            $.ajax({
                type: "DELETE",
                url: url,
                success: function (data) {
                    if (data.success) {
                        toastr.success(data.message);
                        $('#DT_load').DataTable().ajax.reload();
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
}