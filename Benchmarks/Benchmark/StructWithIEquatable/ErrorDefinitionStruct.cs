namespace Benchmark.StructWithIEquatable;

public class ErrorDefinitionStruct
{
    public const int ConstInt = 10;
    public static readonly int StaticReadonlytInt = 10;
    public static readonly ErrorStruct PropertyWithAssigningReadonlyErrorStruct = new ErrorStruct("Code", "Message");
    public static ErrorStruct PropertyWithAssigningErrorStruct = new ErrorStruct("Code", "Message");
    public static ErrorStruct PropertyErrorStruct => new ErrorStruct("Code", "Message");

    public static ErrorStruct MethodErrorStruct() => new ErrorStruct("Code", "Message");

    public static readonly ErrorStructWithIEquatable PropertyErrorWithAssigningReadonlyStructWithIEquatable = new ErrorStructWithIEquatable("Code", "Message");
    public static ErrorStructWithIEquatable PropertyErrorWithAssigningStructWithIEquatable = new ErrorStructWithIEquatable("Code", "Message");
    public static ErrorStructWithIEquatable PropertyErrorStructWithIEquatable => new ErrorStructWithIEquatable("Code", "Message");

    public static ErrorStructWithIEquatable MethodErrorStructWithIEquatable() => new ErrorStructWithIEquatable("Code", "Message");
}
