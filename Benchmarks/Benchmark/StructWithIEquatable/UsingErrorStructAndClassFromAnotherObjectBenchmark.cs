﻿using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructWithIEquatable;

[MemoryDiagnoser]
public class UsingErrorStructAndClassFromAnotherObjectBenchmark
{
    [Benchmark]
    public void PropertyErrorStruct()
    {
        var a = ErrorDefinitionStruct.PropertyErrorStruct;
    }
    
    [Benchmark]
    public void PropertyWithAssigningErrorStruct()
    {
        var a = ErrorDefinitionStruct.PropertyWithAssigningErrorStruct;
    }

    [Benchmark]
    public void PropertyWithAssigningReadonlyErrorStruct()
    {
        var a = ErrorDefinitionStruct.PropertyWithAssigningReadonlyErrorStruct;
    }

    [Benchmark]
    public void ConstInt()
    {
        var a = ErrorDefinitionStruct.ConstInt;
    }

    [Benchmark]
    public void StaticReadonlyInt()
    {
        var a = ErrorDefinitionStruct.StaticReadonlytInt;
    }

    [Benchmark]
    public void MethodErrorStruct()
    {
        var a = ErrorDefinitionStruct.MethodErrorStruct();
    }

    [Benchmark]
    public void PropertyErrorStructWithIEquatable()
    {
        var a = ErrorDefinitionStruct.PropertyErrorStructWithIEquatable;
    }
    
    [Benchmark]
    public void PropertyErrorWithAssigningStructWithIEquatable()
    {
        var a = ErrorDefinitionStruct.PropertyErrorWithAssigningStructWithIEquatable;
    }

    [Benchmark]
    public void PropertyErrorWithAssigningReadonlyStructWithIEquatable()
    {
        var a = ErrorDefinitionStruct.PropertyErrorWithAssigningReadonlyStructWithIEquatable;
    }

    [Benchmark]
    public void MethodErrorStructWithIEquatable()
    {
        var a = ErrorDefinitionStruct.MethodErrorStructWithIEquatable();
    }

    [Benchmark]
    public void PropertyErrorClass()
    {
        var a = ErrorDefinitionClass.PropertyErrorClass;
    }

    [Benchmark]
    public void MethodErrorClass()
    {
        var a = ErrorDefinitionClass.MethodErrorClass();
    }


    [Benchmark]
    public void PropertyErrorStructTwice()
    {
        var a = ErrorDefinitionStruct.PropertyErrorStruct;
        var b = ErrorDefinitionStruct.PropertyErrorStruct;
    }
    
    [Benchmark]
    public void PropertyWithAssigningErrorStructTwice()
    {
        var a = ErrorDefinitionStruct.PropertyWithAssigningErrorStruct;
        var b = ErrorDefinitionStruct.PropertyWithAssigningErrorStruct;
    }

    [Benchmark]
    public void PropertyWithAssigningReadonlyErrorStructTwice()
    {
        var a = ErrorDefinitionStruct.PropertyWithAssigningReadonlyErrorStruct;
        var b = ErrorDefinitionStruct.PropertyWithAssigningReadonlyErrorStruct;
    }

    [Benchmark]
    public void ConstIntTwice()
    {
        var a = ErrorDefinitionStruct.ConstInt;
        var b = ErrorDefinitionStruct.ConstInt;
    }

    [Benchmark]
    public void StaticReadonlyIntTwice()
    {
        var a = ErrorDefinitionStruct.StaticReadonlytInt;
        var b = ErrorDefinitionStruct.StaticReadonlytInt;
    }

    [Benchmark]
    public void MethodErrorStructTwice()
    {
        var a = ErrorDefinitionStruct.MethodErrorStruct();
        var b = ErrorDefinitionStruct.MethodErrorStruct();
    }

    [Benchmark]
    public void PropertyErrorStructWithIEquatableTwice()
    {
        var a = ErrorDefinitionStruct.PropertyErrorStructWithIEquatable;
        var b = ErrorDefinitionStruct.PropertyErrorStructWithIEquatable;
    }
    
    [Benchmark]
    public void PropertyErrorWithAssigningStructWithIEquatableTwice()
    {
        var a = ErrorDefinitionStruct.PropertyErrorWithAssigningStructWithIEquatable;
        var b = ErrorDefinitionStruct.PropertyErrorWithAssigningStructWithIEquatable;
    }

    [Benchmark]
    public void PropertyErrorWithAssigningReadonlyStructWithIEquatableTwice()
    {
        var a = ErrorDefinitionStruct.PropertyErrorWithAssigningReadonlyStructWithIEquatable;
        var b = ErrorDefinitionStruct.PropertyErrorWithAssigningReadonlyStructWithIEquatable;
    }

    [Benchmark]
    public void MethodErrorStructWithIEquatableTwice()
    {
        var a = ErrorDefinitionStruct.MethodErrorStructWithIEquatable();
        var b = ErrorDefinitionStruct.MethodErrorStructWithIEquatable();
    }

    [Benchmark]
    public void PropertyErrorClassTwice()
    {
        var a = ErrorDefinitionClass.PropertyErrorClass;
        var b = ErrorDefinitionClass.PropertyErrorClass;
    }

    [Benchmark]
    public void MethodErrorClassTwice()
    {
        var a = ErrorDefinitionClass.MethodErrorClass();
        var b = ErrorDefinitionClass.MethodErrorClass();
    }
}
