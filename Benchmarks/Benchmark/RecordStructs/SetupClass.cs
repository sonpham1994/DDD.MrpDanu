namespace Benchmark.RecordStructs;

public record struct RecordStruct(Guid Value);

public readonly record struct RecordStruct2(Guid Id1, Guid Id2, Guid Id3);