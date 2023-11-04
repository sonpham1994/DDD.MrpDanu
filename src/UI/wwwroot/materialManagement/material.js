let selectOptionSuppliers = ''

function addMaterialCost() {
    let materialCostTemplate = getMaterialCostTemplate()
    $("#materialCost").find("tbody").prepend(materialCostTemplate)
}

function loadMaterialCost(data) {
    let materialCostTemplate = getMaterialCostTemplate()
    let tbodyMaterialCost = $("#materialCost").find("tbody")
    
    tbodyMaterialCost.append(materialCostTemplate)
    tbodyMaterialCost.find('tr:last select#supplierId').val(data.supplier.id).change()
    tbodyMaterialCost.find('tr:last #currencyType').html(data.supplier.currencyTypeName)
    tbodyMaterialCost.find("tr:last input[name='surcharge']").val(data.surcharge)
    tbodyMaterialCost.find("tr:last input[name='minQuantity']").val(data.minQuantity)
    tbodyMaterialCost.find("tr:last input[name='price']").val(data.price)
    
}

function changeSupplier(ele) {
    let currencyType = ele.find("option:selected").attr("currency-type")
    let tr = ele.closest("tr")
    tr.find("#currencyType").html(currencyType)
}

function getMaterialForm(ele) {
    let form = ele.closest("form")
    let basicMaterialInformation = form.find("#BasicMaterialInformation")
    let materialCosts = form.find("#MaterialCostManagement tbody tr")

    let formSubmit = {
        code: basicMaterialInformation.find("input[name='code']").val(),
        name: basicMaterialInformation.find("input[name='name']").val(),
        colorCode: basicMaterialInformation.find("input[name='colorCode']").val(),
        width: basicMaterialInformation.find("input[name='width']").val(),
        weight: basicMaterialInformation.find("input[name='weight']").val(),
        unit: basicMaterialInformation.find("input[name='unit']").val(),
        varian: basicMaterialInformation.find("input[name='varian']").val(),
        regionalMarketId: basicMaterialInformation.find("select[name='regionalMarketId'] option:selected").val(),
        materialTypeId: basicMaterialInformation.find("select[name='materialTypeId'] option:selected").val(),
        materialCosts: []
    }
    materialCosts.each(function () {
        let supplierId = $(this).find("#supplierId option:selected").val()
        let minQuantity = $(this).find("input[name='minQuantity']").val()
        let surcharge = $(this).find("input[name='surcharge']").val()
        let price = $(this).find("input[name='price']").val()
        let isDeleted = $(this).find("input[name='isDeleted']").val()

        formSubmit.materialCosts.push(
            {
                supplierId: supplierId,
                minQuantity: minQuantity,
                surcharge: surcharge,
                price: price,
                isDeleted: isDeleted
            })
    })

    return formSubmit
}

function deleteCost(ele) {
    let tr = ele.closest("tr")
    let td = ele.closest("td")
    tr.remove();
    //tr.attr("hidden", true)
    //td.find("input[name='isDeleted']").val(true)
}

function getMaterialCostTemplate() {
    let template = `<tr>
                    <td>
                        ${selectOptionSuppliers}
                    </td>
                    <td>
                        <input type="text" name="surcharge" class="form-control" aria-label="Default"/>
                    </td>
                    <td>
                        <input type="text" name="minQuantity" class="form-control" aria-label="Default"/>
                    </td>
                    <td>
                        <input type="text" name="price" class="form-control" aria-label="Default"/>
                    </td>
                    <td>
                        <label Id="currencyType" aria-label="Default"></label>
                    </td>
                    <td>
                        <button Id="deleteCost" type="button" aria-label="Default">Delete</button>
                        <input name="isDeleted" value="false" hidden/>
                    </td>
                </tr>`

    return template
}

function getMaterialTypes() {
    return httpGet(`${materialManagementApi}/material-types`)
        .done(function(data) {
            let result = data.result
            let select = $("select[name='materialTypeId']")
            for (var i = 0; i < result.length; i++) {
                select.append(`<option value='${result[i].id}'>${result[i].name}</option>`)
            }
        })
        .fail(function(data) {
            console.log(data)
        })
}

function getRegionalMarkets() {
    return httpGet(`${materialManagementApi}/regional-markets`)
        .done(function(data) {
            let result = data.result
            let select = $("select[name='regionalMarketId']")
            for (var i = 0; i < result.length; i++)
            {
                select.append(`<option value='${result[i].id}'>${result[i].name}</option>`)
            }
        })
        .fail(function(data) {
            console.log(data)
        })
}

function getSuppliers() {
    return httpGet(`${materialManagementApi}/transactional-partners/suppliers`)
        .done(function (data) {
            let suppliers = data.result;
            let optionSuppliers = "<option value='' currency-type=''>Please select Supplier</option>";

            for (let supplier of suppliers) {
                optionSuppliers += `<option value=${supplier.id} currency-type=${supplier.currencyTypeName}>${supplier.name}</option>`
            }

            selectOptionSuppliers = `<select class="form-select" id="supplierId" name="supplierId">${optionSuppliers}</select>`
        })
        .fail(function (data) {
            console.log(data)
        })
}

$(document).ready(function () {
    $(document).on("change", '#supplierId', function () {
        changeSupplier($(this))
    })
    $(document).on("click", "#deleteCost", function(e) {
        deleteCost($(this))
    })
})