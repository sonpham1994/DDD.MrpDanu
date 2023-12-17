using System.Buffers.Text;

namespace Application.Extensions;

public static class GuidExtension
{
    //https://www.youtube.com/watch?v=B2yOjLyEZk0&ab_channel=NickChapsas
    //https://www.stevejgordon.co.uk/using-high-performance-dotnetcore-csharp-techniques-to-base64-encode-a-guid
    //Please check Benchmarks/Benchmark/ConvertGuidToStringAndViceVersa to see how to reduce memory
    
    private const char Base64PaddingChar = '=';
    private const char Slash = '/';
    private const char Plus = '+';
    private const byte ForwardSlashByte = (byte)'/';
    private const byte PlusByte = (byte)'+';
    private const char Underscore = '_';
    private const char Dash = '-';
    
    /// <summary>
    /// Convert Guid to user friendly encode base64 string
    /// </summary>
    /// <param name="id"></param>
    /// <returns>user friendly encode base64 string</returns>
    /// Encode base64 string con be collision: https://stackoverflow.com/questions/1032376/guid-to-base64-for-url
    public static string ToEncodeBase64String(this Guid id)
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
    
    /// <summary>
    /// Convert user friendly encode base64 string to Guid 
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Guid</returns>
    public static Guid ToGuidFromString(ReadOnlySpan<char> id)
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
}