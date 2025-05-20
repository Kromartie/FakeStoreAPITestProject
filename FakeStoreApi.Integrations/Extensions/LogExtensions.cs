using NUnit.Framework;
using System.Text.Json;

namespace FakeStoreApi.Integrations.Extensions;

// Extension methods for logging objects and lists
public static class LogExtensions
{
    // Logs a message as Json.
    public static void LogAsJson<T>(this T obj)
    {
        TestContext.WriteLine($"~ \"{typeof(T).Name}\": \n {JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true})}");
    }

    // Logs a list of objects in a table format.
    public static void LogToTable<T>(this List<T> items, params string[] fieldNames)
    {
        if (fieldNames == null || fieldNames.Length == 0)
            fieldNames = typeof(T).GetProperties().Select(p => p.Name).ToArray();

        // Calculate the maximum width for each column
        Dictionary<string, int> columnWidths = new Dictionary<string, int>();
        foreach (var fieldName in fieldNames)
        {
            int maxwidth = fieldName.Length;
            foreach (var item in items)
            {
                var value = item.GetType().GetProperty(fieldName)?.GetValue(item)?.ToString() ?? "null"; maxwidth = Math.Max(maxwidth, value.Length);
            }
            columnWidths[fieldName] = maxwidth;
        }

        //var digitCount = items.Count.ToString().Length; // Get the number of digits in the count
        var whiteSpace = new string(' ', items.Count.ToString().Length); // Two spaces for padding

        // Print column titles
        TestContext.WriteLine($"#.{whiteSpace}| {string.Join(" | ", fieldNames.Select(f => f.PadRight(columnWidths[f])))}");

        for (int i = 0; i < items.Count; i++)
        {
            List<string> values = new List<string>();
            foreach (var fieldName in fieldNames)
            {

                var value = items[i].GetType().GetProperty(fieldName)?.GetValue(items[i])?.ToString() ?? "null";
                values.Add(value.PadRight(columnWidths[fieldName]));
            }

            // Adjust row number formatting

            string rowNumber = i < 10 ? $"{i}. " : $"{i}.";

            TestContext.WriteLine($"{rowNumber} | {string.Join(" | ", values)}");
        }
    }
}
