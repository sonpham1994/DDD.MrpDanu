using System.Text;

namespace Benchmark;

public class StringInternOrHighFrequencyHeap
{
    //by default, string will store in high-frequency heap (Generation 2) if string value is not changed by run time,
    //  otherwise, it will be string interpolation, this string will allocate on Generation 1. String Intern refers
    // to string allocated on high-frequency heap
    
    public void CreateStringWithBeInterned()
    {
        Console.WriteLine("-------CreateStringWithBeInterned-------");
        // This string will be interned because it's a literal
        string internedString1 = "Hello, World!";
        string internedString2 = "Hello, World!";
        // These strings point to the same interned instance
        Console.WriteLine("internedString1 equals to internedString2 with the same memory address: " + object.ReferenceEquals(internedString1, internedString2)); // Output: True
        Console.WriteLine("internedString1 is Interned: " + (string.IsInterned(internedString1) != null ? "Interned": "Not Interned"));
        Console.WriteLine("internedString2 is Interned: " + (string.IsInterned(internedString2) != null ? "Interned": "Not Interned"));
        Console.WriteLine("internedString1 GC: " + GC.GetGeneration(internedString1));
        Console.WriteLine("internedString2 GC: " + GC.GetGeneration(internedString2));
        
        // Demonstrating the difference with String.Intern
        string internedString = string.Intern("This string is interned.");
        Console.WriteLine("internedString is interned: " + (string.IsInterned(internedString) != null ? "Interned": "Not Interned"));
        Console.WriteLine("internedString GC: " + GC.GetGeneration(internedString));
        
        // Using StringBuilder for temporary strings
        StringBuilder sb = new StringBuilder();
        sb.Append("This is a temporary string.");
        string tempString = sb.ToString();
        Console.WriteLine("tempString is interned: " + (string.IsInterned(tempString) != null ? "Interned": "Not Interned"));
        Console.WriteLine("value from tempString is interned: " + (string.IsInterned("This is a temporary string.") != null ? "Interned": "Not Interned"));
        Console.WriteLine("tempString GC: " + GC.GetGeneration(tempString));
        Console.WriteLine("value from tempString GC: " + GC.GetGeneration("This is a temporary string."));
        
        // This string will be interned, create a new string interned although we have the same value from internedString1
        string internedString3 = new string("Hello, World!".ToCharArray());
        // This string points to a different instance
        Console.WriteLine("internedString1 equals to internedString3 with the same memory address: " + object.ReferenceEquals(internedString1, internedString3)); // Output: False
        // Verify if strings are interned using String.IsInterned
        Console.WriteLine("internedString3 - IsInterned: " + (string.IsInterned(internedString3) != null ? "Interned": "Not Interned")); // Output: Interned
        Console.WriteLine("internedString3 GC: " + GC.GetGeneration(internedString3));
        
        var nameOf = nameof(StringInternOrHighFrequencyHeap);
        Console.WriteLine("nameOf - IsInterned: " + (string.IsInterned(nameOf) != null ? "Interned": "Not Interned")); // Output: Interned
        Console.WriteLine("nameOf GC: " + GC.GetGeneration(nameOf));
    }

    public void CreateStringWithNotBeInterned()
    {
        Console.WriteLine("-------CreateStringWithNotBeInterned-------");
        
        // Using String.Create to create a non-interned string
        string tempString2 = string.Create(25, "temporary", (span, value) =>
        {
            value.AsSpan().CopyTo(span);
            span[9] = ' ';
            span[10] = 's';
            span[11] = 't';
            span[12] = 'r';
            span[13] = 'i';
            span[14] = 'n';
            span[15] = 'g';
        });
        Console.WriteLine("value from tempString2: " + tempString2); 
        Console.WriteLine("tempString2 is interned: " + (string.IsInterned(tempString2) != null ? "Interned": "Not Interned")); //  Not Interned
        Console.WriteLine("tempString2 GC: " + GC.GetGeneration(tempString2)); // 0
        
        // by default if you pass a string like this, it will be interned
        Console.WriteLine("temporary - IsInterned: " + (string.IsInterned("temporary") != null ? "Interned": "Not Interned")); //  Interned
        Console.WriteLine("temporary GC: " + GC.GetGeneration("temporary")); //2147483647
        Console.WriteLine("adsasxsaxas - IsInterned: " + (string.IsInterned("adsasxsaxas") != null ? "Interned": "Not Interned")); //  Interned
        Console.WriteLine("adsasxsaxas GC: " + GC.GetGeneration("adsasxsaxas")); //2147483647
        
        // Using String.Create to create a non-interned string
        string nameOf1 = string.Create(19, string.Empty, (span, value) =>
        {
            nameof(ValueObjectEqualsBoxing.MyBoxingValueObject).AsSpan().CopyTo(span);
        });
        Console.WriteLine("value from nameOf1: " + nameOf1);
        Console.WriteLine("nameOf1 - IsInterned: " + (string.IsInterned(nameOf1) != null ? "Interned": "Not Interned")); // Interned
        // although this value is 0, but the `nameof(ValueObjectEqualsBoxing.MyBoxingValueObject)` instance might be
        // 2147483647. the nameOf1 isntance and `nameof(ValueObjectEqualsBoxing.MyBoxingValueObject)` instance are
        // different we get the nameOf1 is 0, but the `nameof(ValueObjectEqualsBoxing.MyBoxingValueObject)` is 2147483647
        Console.WriteLine("nameOf1 GC: " + GC.GetGeneration(nameOf1)); // 0
        
        // Define a char array for the string you want to create
        char[] charArray = new char[] { 'a', 'b', 'c', 'a', 'x', 'a', 's', 'd', 's', 'a', 'd', 'a', 's' };
        // Create a string from the char array
        string nonInternedString = new string(charArray);
        // Output the created string
        Console.WriteLine(nonInternedString); // Output: abcaxasdsadas
        // Verify if the string is interned
        Console.WriteLine("nonInternedString - isInterned: " + (string.IsInterned(nonInternedString) != null ? "Interned" : "Not Interned")); // Output: Not Interned
        Console.WriteLine("nonInternedString GC: " + GC.GetGeneration(nonInternedString)); // 0
    }
}