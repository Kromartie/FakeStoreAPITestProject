namespace FakeStoreApi.Integrations.Extensions;
public static class Extensions
{
    public static T SelectRandom<T>(this IEnumerable<T> source)
    {
        if (!source.Any()) return default!;
        return source.ElementAt(Random.Shared.Next(source.Count()));
    }
}
