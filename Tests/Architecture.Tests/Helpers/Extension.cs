using System.Reflection;

namespace Architecture.Tests.Helpers;

public static class Extension
{
    public static bool IsInitOnlySetter(this PropertyInfo property)
    {
        if (!property.CanWrite)
        {
            return false;
        }
 
        var setMethod = property.SetMethod;
 
        // Get the modifiers applied to the return parameter.
        var setMethodReturnParameterModifiers = setMethod!.ReturnParameter.GetRequiredCustomModifiers();
 
        // Init-only properties are marked with the IsExternalInit type.
        return setMethodReturnParameterModifiers.Contains(typeof(System.Runtime.CompilerServices.IsExternalInit));
    }
    
    public static bool IsPublicSetter(this PropertyInfo property)
    {
        var setter = property.GetSetMethod(true);
        
        // There's a setter
        return setter!.IsPublic;
    }
    
    public static bool IsNoSetter(this PropertyInfo property)
    {
        if (!property.CanWrite)
        {
            return true;
        }

        return false;
    }
}