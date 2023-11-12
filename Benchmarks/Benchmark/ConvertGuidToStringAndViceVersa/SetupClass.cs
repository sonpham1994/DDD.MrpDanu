using System.Buffers.Text;
using System.Runtime.InteropServices;

namespace Benchmark.ConvertGuidToStringAndViceVersa;

public static class Guider
{
    //https://www.youtube.com/watch?v=B2yOjLyEZk0&ab_channel=NickChapsas
    private const char Base64PaddingChar = '=';
    private const char Slash = '/';
    private const char Plus = '+';
    
    public static string ToStringFromGuidOp(this Guid id)
    {
        Span<byte> idBytes = stackalloc byte[16];
        Span<byte> base64Bytes = stackalloc byte[24];

        MemoryMarshal.TryWrite(idBytes, ref id);
        Base64.EncodeToUtf8(idBytes, base64Bytes, out _, out _);

        Span<char> finalChars = stackalloc char[22];
        
        for (var i = 0; i < 22; i++)
        {
            finalChars[i] = base64Bytes[i] switch
            {
                ForwardSlashByte => Dash,
                PlusByte => Underscore,
                _ => (char)base64Bytes[i]
            };
        }

        return new string(finalChars);
    }
    
    public static string ToStringFromGuidWithGuidTryWriteBytes(this Guid id)
    {
        Span<byte> idBytes = stackalloc byte[16];
        Span<byte> base64Bytes = stackalloc byte[24];

        id.TryWriteBytes(idBytes);
        Base64.EncodeToUtf8(idBytes, base64Bytes, out _, out _);

        Span<char> finalChars = stackalloc char[22];
        
        for (var i = 0; i < 22; i++)
        {
            finalChars[i] = base64Bytes[i] switch
            {
                ForwardSlashByte => Dash,
                PlusByte => Underscore,
                _ => (char)base64Bytes[i]
            };
        }

        return new string(finalChars);
    }
    
    //Replace with char is better than string, please check Benchmarks/Benchmark/StringBenchmarks
    public static string ToStringFromGuid(this Guid id)
    {
        return Convert.ToBase64String(id.ToByteArray())
            .Replace(Slash, Dash) 
            .Replace(Plus, Underscore)
            .Replace("=", string.Empty);
    }

    //Replace with char is better than string, please check Benchmarks/Benchmark/StringBenchmarks
    public static Guid ToGuidFromString(this string id)
    {
        var efficientBased64 = Convert.FromBase64String(
            id.Replace(Dash, Slash)
                .Replace(Underscore, Plus)
                + "==");

        return new Guid(efficientBased64);
    }
    
    public static Guid ToGuidFromStringOp(ReadOnlySpan<char> id)
    {
        Span<char> base64Chars = stackalloc char[24];

        for (int i = 0; i < 22; i++)
        {
            base64Chars[i] = id[i] switch
            {
                Dash => Slash,
                Underscore => Plus,
                _ => id[i]
            };
        }

        base64Chars[22] = Base64PaddingChar;
        base64Chars[23] = Base64PaddingChar;

        Span<byte> idBytes = stackalloc byte[16];
        Convert.TryFromBase64Chars(base64Chars, idBytes, out _);

        return new Guid(idBytes);
    }
    
    
    //https://www.stevejgordon.co.uk/using-high-performance-dotnetcore-csharp-techniques-to-base64-encode-a-guid
    private const byte ForwardSlashByte = (byte)'/';
    private const byte PlusByte = (byte)'+';
    private const char Underscore = '_';
    private const char Dash = '-';

    public static string EncodeBase64String(this Guid guid)
    {
        Span<byte> guidBytes = stackalloc byte[16];
        Span<byte> encodedBytes = stackalloc byte[24];

        MemoryMarshal.TryWrite(guidBytes, ref guid); // write bytes from the Guid
        Base64.EncodeToUtf8(guidBytes, encodedBytes, out _, out _);

        Span<char> chars = stackalloc char[22];

        // replace any characters which are not URL safe
        // skip the final two bytes as these will be '==' padding we don't need
        for (var i = 0; i < 22; i++)
        {
            switch (encodedBytes[i])
            {
                case ForwardSlashByte:
                    chars[i] = Dash;
                    break;
                case PlusByte:
                    chars[i] = Underscore;
                    break;
                default:
                    chars[i] = (char)encodedBytes[i];
                    break;
            }
        }

        var final = new string(chars);

        return final;
    }
    
    public static string EncodeBase64StringWithTryWriteBytes(this Guid guid)
    {
        Span<byte> guidBytes = stackalloc byte[16];
        Span<byte> encodedBytes = stackalloc byte[24];

        guid.TryWriteBytes(guidBytes);
        Base64.EncodeToUtf8(guidBytes, encodedBytes, out _, out _);

        Span<char> chars = stackalloc char[22];

        // replace any characters which are not URL safe
        // skip the final two bytes as these will be '==' padding we don't need
        for (var i = 0; i < 22; i++)
        {
            switch (encodedBytes[i])
            {
                case ForwardSlashByte:
                    chars[i] = Dash;
                    break;
                case PlusByte:
                    chars[i] = Underscore;
                    break;
                default:
                    chars[i] = (char)encodedBytes[i];
                    break;
            }
        }

        var final = new string(chars);

        return final;
    }
}