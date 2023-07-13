namespace Benchmark.EmptyCollectionAndToList.Models;

public static class Mapping
{
    public static IReadOnlyList<MyClassViewModel> ToViewModel(this IEnumerable<MyClass> myclasses)
    {
        return myclasses.Select(x => new MyClassViewModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
    
    public static IReadOnlyList<MyClassViewModel> ToViewModelCheckingEmpty(this IEnumerable<MyClass> myclasses)
    {
        if (!myclasses.Any())
            return Array.Empty<MyClassViewModel>();
        
        return myclasses.Select(x => new MyClassViewModel
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();
    }
}