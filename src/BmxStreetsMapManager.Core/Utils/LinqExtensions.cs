using FuzzySharp;

namespace BmxStreetsMapManager.Core.Utils;
public static class LinqExtensions
{
    public static T? FuzzyFind<T>(this IEnumerable<T> source, Func<T, string> propertySelector, string searchText, int minMatchPercent = 60)
    {
        var item = (from x in source
                    let ratio = Fuzz.Ratio(propertySelector(x), searchText)
                    orderby ratio descending
                    where ratio > minMatchPercent
                    select x).FirstOrDefault();
        return item;
    }

    public static IEnumerable<(TFirst Source, TSecond? Matched)> FuzzyJoin<TFirst, TSecond>(this IEnumerable<TFirst> first,
        IEnumerable<TSecond> second,
        Func<TFirst, string> firstPropertySelector,
        Func<TSecond, string> secondPropertySelector,
        int minMatchPercent = 60)
    {
        return first.Select(x => (x, second.FuzzyFind(secondPropertySelector, firstPropertySelector(x), minMatchPercent)));
    }
}
