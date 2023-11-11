function renderOption(data, nameAttribute) {
    let select = $(`select[name='${nameAttribute}']`)
    for (var i = 0; i < data.length; i++) {
        select.append(`<option value='${data[i].id}'>${data[i].name}</option>`)
    }
}