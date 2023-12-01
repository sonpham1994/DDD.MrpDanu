namespace Domain.Extensions;

public static class GuidExtensions
{
    // public static Guid New()
    // {
    //     //https://github.com/dotnet/efcore/blob/main/src/EFCore/ValueGeneration/SequentialGuidValueGenerator.cs
    //     //https://davecallan.com/generating-sequential-guids-which-sort-sql-server-in-net/
    //     // source sequential guid from .net: https://github.com/dotnet/efcore/blob/main/src/EFCore/ValueGeneration/SequentialGuidValueGenerator.cs
    //
    //     //using mac address for sequential guid will not create if you use fake address: https://devblogs.microsoft.com/oldnewthing/20191120-00/?p=103118
    //     //using mac address for sequential guid might result in fragmentation and performance issue if you have many virtual machine
    //     // that contain your application (multi instances) access the same db server.
    //
    //     /*
    //      * https://dev.to/connerphillis/sequential-guids-in-entity-framework-core-might-not-be-sequential-3408
    //      * https://github.com/dotnet/efcore/issues/30753
    //      * https://learn.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.valuegeneration.sequentialguidvaluegenerator?view=efcore-7.0
    //      * setting a column to use a GUID as a key with DatabaseGeneratedOption.Identity does not mean that it 
    //      *  will be generated by the database. Instead, EF Core generates the sequential GUID itself, and then 
    //      *  inserts it into the database
    //      */
    //     //https://learn.microsoft.com/en-us/ef/core/modeling/generated-properties?tabs=fluent-api
    //     /*
    //      * For example, on SQL Server, when a GUID property is configured as a primary key, 
    //      *  the provider automatically performs value generation client-side, using an algorithm 
    //      *  to generate optimal sequential GUID values.
    //      */
    //     // From that links, The sequential Guid using SequentialGuidValueGenerator() as a default and generate
    //     // at client side, not database side. So you don't do anything because SequentialGuidValueGenerator() is
    //     // configured as a default of behaviour. You can test it when calling Attach or Add method from DbContext.
    //     //var id = RT.Comb.Provider.Sql.Create();
    //     var id = new SequentialGuidValueGenerator().Next(null);
    //     return id;
    // }

    // // Comparison operators from SqlGuid
    // https://stackoverflow.com/questions/29674395/how-to-sort-sequential-guids-in-c
    // source code: https://github.com/dotnet/runtime/blob/0dc5b590fa1fdf00bef65bb8c59b13f2dc601a0d/src/libraries/System.Data.Common/src/System/Data/SQLTypes/SQLGuid.cs#L113
    // private static EComparison Compare(SqlGuid x, SqlGuid y)
    // {
    //     // Comparison orders.
    //     ReadOnlySpan<byte> rgiGuidOrder = new byte[16] { 10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3 };
    //
    //     // Swap to the correct order to be compared
    //     ReadOnlySpan<byte> xBytes = x.m_value;
    //     ReadOnlySpan<byte> yBytes = y.m_value;
    //     for (int i = 0; i < SizeOfGuid; i++)
    //     {
    //         byte b1 = xBytes[rgiGuidOrder[i]];
    //         byte b2 = yBytes[rgiGuidOrder[i]];
    //         if (b1 != b2)
    //         {
    //             return (b1 < b2) ? EComparison.LT : EComparison.GT;
    //         }
    //     }
    //
    //     return EComparison.EQ;
    // }

    public static int SequentialGuidCompareTo(this Guid left, Guid right)
    {
        //https://github.com/dotnet/efcore/blob/main/src/EFCore/ValueGeneration/SequentialGuidValueGenerator.cs
        /*
         * from document above, we can see that .Net implements Sequential Guid from Guid.NewGuid() and then rewrite
         * data from 8 to 15 of indexes. 
         * Please check AnalyzeStructureOfSequentialGuid.xlsx to visualize the array of Guid
         */

        // Comparison orders from SqlSever with using SqlGuid. It will compare 10, 11, 12, 13, 14, 15 index first,
        // and then 8, 9, and then 6, 7, ...
        // Please check source code: https://github.com/dotnet/runtime/blob/0dc5b590fa1fdf00bef65bb8c59b13f2dc601a0d/src/libraries/System.Data.Common/src/System/Data/SQLTypes/SQLGuid.cs#L113
        // if application uses postgres, we may change the logic "compare to" here
        const byte sizeGuid = 16;
        ReadOnlySpan<byte> sqlGuidOrder = stackalloc byte[sizeGuid] { 10, 11, 12, 13, 14, 15, 8, 9, 6, 7, 4, 5, 0, 1, 2, 3 };

        Span<byte> leftGuids = stackalloc byte[sizeGuid];
        Span<byte> rightGuids = stackalloc byte[sizeGuid];
        left.TryWriteBytes(leftGuids);
        right.TryWriteBytes(rightGuids);

        //Asymptotic analysis:
        //  - Best case: Compare Guid with O(1) of time complexity, if Guid is not equal at the index 10
        //  - Average case: time complexity of 0(log n)
        //  - Worst case: time complexity of O(n), if Guid equals
        //Compare Sequential Guid implemented from EntityFramework Core by using technique from SqlGuid
        // Please check AnalyzeStructureOfSequentialGuid.xlsx with array 10_000 items, and we have from 5_000 to 5_0009
        // and 9_000 to 9_009. We can see the data with the last item will be sorted first
        // Comparison orders from SqlSever with using SqlGuid. It will compare 10, 11, 12, 13, 14, 15 index first,
        // and then 8, 9, and then 6, 7, ...
        for (int i = 0; i < sizeGuid; i++)
        {
            if (leftGuids[sqlGuidOrder[i]] > rightGuids[sqlGuidOrder[i]])
                return 1;
            else if (leftGuids[sqlGuidOrder[i]] < rightGuids[sqlGuidOrder[i]])
                return -1;
        }

        return 0;
    }
}