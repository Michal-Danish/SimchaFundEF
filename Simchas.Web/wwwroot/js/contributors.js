$(() => {

    $("#newcontributor").on('click', function () {
        $("#contmodal").modal('show');
    })

    $(".edit").on('click', function () {
        let id = $(this).data('id');
        let firstName = $(this).data('first');
        let lastName = $(this).data('last');
        let cellNumber = $(this).data('cell');
        $("#editmodal #id").val(id);
        $("#editmodal #firstName").val(firstName);
        $("#editmodal #lastName").val(lastName);
        $("#editmodal #cellNumber").val(cellNumber);
        $("#editmodal").modal('show');
    })

    $(".deposit").on('click', function () {
        let id = $(this).data('id');
        $("#depositmodal #contributorid").val(id);
        $("#depositmodal").modal('show');
    })
})