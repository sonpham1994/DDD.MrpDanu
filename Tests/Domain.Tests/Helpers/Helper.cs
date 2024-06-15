namespace Domain.Tests.Helpers;

public class Helper
{
    public static string CreateStringByLength(int length, char character = 's')
    {
        var result = new string(character, length);

        return result;
    }
}