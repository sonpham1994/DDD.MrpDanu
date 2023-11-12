using BenchmarkDotNet.Attributes;

namespace Benchmark.ConvertGuidToStringAndViceVersa;

[MemoryDiagnoser]
public class ConvertGuidToStringAndViceVersaBenchmark
{
    private Guid _id;
    private string _urlFriendlyBase64;
    private string _urlFriendlyBase64Op;
    
    [GlobalSetup]
    public void Setup()
    {
        _id = Guid.Parse("7878b9fe-f3ed-4824-90bc-f6f52b754eef");
        _urlFriendlyBase64 = _id.ToStringFromGuid();
        _urlFriendlyBase64Op = _id.ToStringFromGuidOp();
        //Console.WriteLine("Guid: " +_id);
    }
    
    [Benchmark]
    public void ToBase64StringFromGuid()
    {
        var base64Id = Convert.ToBase64String(_id.ToByteArray());
        //Console.WriteLine("ToBase64StringFromGuid: " + base64Id);
    }
    
    [Benchmark]
    public void ToUrlFriendlyBase64StringFromGuid()
    {
        var urlFriendlyBase64 = _id.ToStringFromGuid();
        //Console.WriteLine("ToUrlFriendlyBase64StringFromGuid: " + urlFriendlyBase64);
    }
    
    [Benchmark]
    public void ToUrlFriendlyBase64StringFromGuidOp()
    {
        var urlFriendlyBase64 = _id.ToStringFromGuidOp();
        //Console.WriteLine("ToUrlFriendlyBase64StringFromGuidOp: " + urlFriendlyBase64);
    }
    
    [Benchmark]
    public void ToUrlFriendlyBase64StringFromGuidOpWithTryWriteBytes()
    {
        var urlFriendlyBase64 = _id.ToStringFromGuidWithGuidTryWriteBytes();
        //Console.WriteLine("ToUrlFriendlyBase64StringFromGuidOpWithTryWriteBytes: " + urlFriendlyBase64);
    }
    
    [Benchmark]
    public void ToUrlFriendlyBase64StringFromGuidOp_SteveGordon()
    {
        var urlFriendlyBase64 = _id.EncodeBase64String();
        //Console.WriteLine("ToUrlFriendlyBase64StringFromGuidOp_SteveGordon: " + urlFriendlyBase64);
    }
    
    [Benchmark]
    public void ToUrlFriendlyBase64StringFromGuidOp_SteveGordonWithTryWriteBytes()
    {
        var urlFriendlyBase64 = _id.EncodeBase64StringWithTryWriteBytes();
        //Console.WriteLine("ToUrlFriendlyBase64StringFromGuidOp_SteveGordonWithTryWriteBytes: " + urlFriendlyBase64);
    }
    
    [Benchmark]
    public void ToGuidFromString()
    {
        var idAgain = _urlFriendlyBase64.ToGuidFromString();
        //Console.WriteLine("ToGuidFromString: " + idAgain);
    }
    
    [Benchmark]
    public void ToGuidFromStringOp()
    {
        var idAgain = Guider.ToGuidFromStringOp(_urlFriendlyBase64Op);
        //Console.WriteLine("ToGuidFromStringOp: " + idAgain);
    }
}