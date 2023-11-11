using System.Text.RegularExpressions;

namespace Api;

public class ToKebabParameterTransformer : IOutboundParameterTransformer
{
    // replace url at the start up application, url api is kebab case.
    // For example MaterialManagement would be material-management
    public string TransformOutbound(object? value) => value != null
       ? Regex.Replace(value.ToString(), "([a-z])([A-Z])", "$1-$2").ToLower() // to kebab 
       : null;
}
