﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructWithIEquatable;

public class ErrorDefinitionStruct
{
    public static ErrorStruct PropertyErrorStruct => new ErrorStruct("Code", "Message");

    public static ErrorStruct MethodErrorStruct() => new ErrorStruct("Code", "Message");

    public static ErrorStructWithIEquatable PropertyErrorStructWithIEquatable => new ErrorStructWithIEquatable("Code", "Message");

    public static ErrorStructWithIEquatable MethodErrorStructWithIEquatable() => new ErrorStructWithIEquatable("Code", "Message");
}
