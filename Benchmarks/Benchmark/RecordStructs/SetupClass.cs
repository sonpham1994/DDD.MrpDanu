namespace Benchmark.RecordStructs;

public record struct RecordStruct(Guid Value);

public readonly record struct RecordStruct2(Guid Id1, Guid Id2, Guid Id3);

public static class Ext
{
    public static void AssignGuidUsingInterfaceExt<T>(this T data) 
        where T : IRecordStruct
    {
        data.AssignGuid();
    }
    
    public static void AssignGuidUsingStructExt<T>(this T data) 
        where T : IRecordStruct
    {
        data.Id = Guid.NewGuid();
    }
    
    public static void NotAssignGuidExt<T>(this T data) 
        where T : IRecordStruct
    {
        data.NotAssignGuid();
    }
}

public interface IRecordStruct
{
    public Guid Id { get; set; }

    public void AssignGuid()
    {
        Id = Guid.NewGuid();
    }
    
    public void NotAssignGuid()
    {
        var a = Guid.NewGuid();
    }
}

public record struct UserId : IRecordStruct
{
    public Guid Id { get; set; }
}