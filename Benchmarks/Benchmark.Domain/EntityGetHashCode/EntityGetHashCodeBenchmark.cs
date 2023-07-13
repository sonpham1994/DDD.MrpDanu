using Benchmark.Domain.EntityGetHashCode.BaseEntity;
using BenchmarkDotNet.Attributes;

namespace Benchmark.Domain.EntityGetHashCode
{
    [MemoryDiagnoser()]
    public class EntityGetHashCodeBenchmark
    {
        [Params(10, 100)]
        public int Length { get; set; }

        [Benchmark]
        public void GroupWithToStringGuidGetHashCode()
        {
            var test = GetMyClassToStringGuid();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }

        [Benchmark]
        public void GroupWithToStringIntGetHashCode()
        {
            var test = GetMyClassToStringInt();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }

        [Benchmark]
        public void GroupWithNameGuidGetHashCode()
        {
            var test = GetMyClassNameGuid();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }

        [Benchmark]
        public void GroupWithNameIntGetHashCode()
        {
            var test = GetMyClassNameInt();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }


        [Benchmark]
        public void CacheGroupWithToStringGuidGetHashCode()
        {
            var test = GetCacheMyClassToStringGuid();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }

        [Benchmark]
        public void CacheGroupWithToStringIntGetHashCode()
        {
            var test = GetCacheMyClassToStringInt();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }

        [Benchmark]
        public void CacheGroupWithNameGuidGetHashCode()
        {
            var test = GetCacheMyClassNameGuid();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }

        [Benchmark]
        public void CacheGroupWithNameIntGetHashCode()
        {
            var test = GetCacheMyClassNameInt();
            var result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
            if (result.Count < Length)
                throw new Exception();
            result = test.GroupBy(x => x).Select(x => x.Key.Id).ToList();
        }

        private List<MyClassToStringGuid> GetMyClassToStringGuid()
        {
            var result = new List<MyClassToStringGuid>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new MyClassToStringGuid() { Id = Guid.NewGuid() });
            }

            return result;
        }

        private List<MyClassToStringInt> GetMyClassToStringInt()
        {
            var result = new List<MyClassToStringInt>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new MyClassToStringInt() { Id = i });
            }

            return result;
        }

        private List<MyClassNameGuid> GetMyClassNameGuid()
        {
            var result = new List<MyClassNameGuid>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new MyClassNameGuid() { Id = Guid.NewGuid() });
            }

            return result;
        }

        private List<MyClassNameInt> GetMyClassNameInt()
        {
            var result = new List<MyClassNameInt>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new MyClassNameInt() { Id = i });
            }

            return result;
        }



        private List<CacheMyClassToStringGuid> GetCacheMyClassToStringGuid()
        {
            var result = new List<CacheMyClassToStringGuid>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new CacheMyClassToStringGuid() { Id = Guid.NewGuid() });
            }

            return result;
        }

        private List<CacheMyClassToStringInt> GetCacheMyClassToStringInt()
        {
            var result = new List<CacheMyClassToStringInt>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new CacheMyClassToStringInt() { Id = i });
            }

            return result;
        }

        private List<CacheMyClassNameGuid> GetCacheMyClassNameGuid()
        {
            var result = new List<CacheMyClassNameGuid>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new CacheMyClassNameGuid() { Id = Guid.NewGuid() });
            }

            return result;
        }

        private List<CacheMyClassNameInt> GetCacheMyClassNameInt()
        {
            var result = new List<CacheMyClassNameInt>();
            for (int i = 0; i < Length; i++)
            {
                result.Add(new CacheMyClassNameInt() { Id = i });
            }

            return result;
        }
    }

    

    public class MyClassToStringGuid : EntityWithToStringGetHashCode<Guid>
    {
    }

    public class MyClassToStringInt : EntityWithToStringGetHashCode<int>
    {
    }

    public class MyClassNameGuid : EntityWithNameGetHashCode<Guid>
    {
    }

    public class MyClassNameInt : EntityWithNameGetHashCode<int>
    {
    }


    public class CacheMyClassToStringGuid : CacheEntityWithToStringGetHashCode<Guid>
    {
    }

    public class CacheMyClassToStringInt : CacheEntityWithToStringGetHashCode<int>
    {
    }

    public class CacheMyClassNameGuid : CacheEntityWithNameGetHashCode<Guid>
    {
    }

    public class CacheMyClassNameInt : CacheEntityWithNameGetHashCode<int>
    {
    }
}
