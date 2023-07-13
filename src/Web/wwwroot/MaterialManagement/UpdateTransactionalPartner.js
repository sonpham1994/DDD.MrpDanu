function getTransactionalPartnerForm(ele) {
    let form = ele.closest("form")
    let basicInformation = form.find("#BasicInformation")

    let formSubmit = {
        name: basicInformation.find("input[name='name']").val(),
        taxNo: basicInformation.find("input[name='taxNo']").val(),
        website: basicInformation.find("input[name='website']").val(),
        transactionalPartnerTypeId: basicInformation.find("select[name='transactionalPartnerTypeId'] option:selected").val(),
        currencyTypeId: basicInformation.find("select[name='currencyTypeId'] option:selected").val(),
        contactPersonName: basicInformation.find("input[name='contactPersonName']").val(),
        telNo: basicInformation.find("input[name='telNo']").val(),
        email: basicInformation.find("input[name='email']").val(),
        locationTypeId: basicInformation.find("select[name='locationTypeId'] option:selected").val(),
        address: {
            city: basicInformation.find("input[name='city']").val(),
            district: basicInformation.find("input[name='district']").val(),
            street: basicInformation.find("input[name='street']").val(),
            ward: basicInformation.find("input[name='ward']").val(),
            zipCode: basicInformation.find("input[name='zipCode']").val(),
            countryId: basicInformation.find("select[name='countryId'] option:selected").val(),
        }
    }
    
    return formSubmit
}