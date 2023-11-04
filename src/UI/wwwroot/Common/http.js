function getIdByUrl() {
    let elePathNames = window.location.pathname.split('/')
    let id = elePathNames[elePathNames.length - 1]

    return id
}

function httpPost(formData, url) {
    return $.ajax({
        type: 'POST',
        data: JSON.stringify(formData),
        url: url,
        contentType: "application/json",
        dataType: "json"
    })
}
function httpPut(formData, url) {
    return $.ajax({
        type: 'PUT',
        data: JSON.stringify(formData),
        url: url,
        contentType: "application/json",
        dataType: "json"
    })
}

function httpGet(url) {
    return $.ajax({
        type: 'GET',
        url: url,
        dataType: "json"
    })
}

function httpDelete(url) {
    return $.ajax({
        type: 'DELETE',
        url: url,
        dataType: "json"
    })
}



// $.ajax({
//     type: 'POST',
//     data: JSON.stringify(formData),
//     url: url,
//     contentType: "application/json",
//     dataType: "json",
//     success: function (result) {
//
//     },
//     error: function (data) {
//
//     }
// });