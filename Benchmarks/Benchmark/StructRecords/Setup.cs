using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark.StructRecords;

public readonly record struct RecordStruct(Guid Id1, Guid Id2, Guid Id3);