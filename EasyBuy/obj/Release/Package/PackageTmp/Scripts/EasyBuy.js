function CheckDataExists(TableName = "", ColumnName = "", ColumnValue = "", WhereCondition = "") {
    var flag = false;

    $.ajax({
        cache: false,
        type: "POST",
        async: false,
        url: "/Common/CheckDataExists",
        data: JSON.stringify({ TableName: TableName, ColumnName: ColumnName, ColumnValue: ColumnValue, WhereCondition: WhereCondition }),
        dataType: "json",
        contentType: "application/json;charset=UTF-8",
        success: function (data) {
            if (parseInt(data.cnt) > 0) {
                flag = true;
            }
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

    return flag;
}
