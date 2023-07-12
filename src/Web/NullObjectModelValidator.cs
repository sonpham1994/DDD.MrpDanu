using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Web;

public class NullObjectModelValidator : IObjectModelValidator
{
    public void Validate(ActionContext actionContext,
        ValidationStateDictionary validationState, string prefix, object model)
    {
    }
}