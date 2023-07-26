using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructWithIEquatable;

public class ErrorDefinitionClass
{
    public static ErrorClass PropertyErrorClass => new ErrorClass("Code", "Message");

    public static ErrorClass MethodErrorClass() => new ErrorClass("Code", "Message");
}
