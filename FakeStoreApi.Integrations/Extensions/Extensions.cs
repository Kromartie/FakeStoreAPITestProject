namespace FakeStoreApi.Integrations.Extensions;

// Extension methods for collections
public static class Extensions
{
    // Selects a random element from the collection
    public static T SelectRandom<T>(this IEnumerable<T> source)
    {
        if (!source.Any()) return default!;
        return source.ElementAt(Random.Shared.Next(source.Count()));
    }
}
