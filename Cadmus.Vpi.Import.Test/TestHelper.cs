using Cadmus.Core;
using Cadmus.Import.Proteus;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cadmus.Vpi.Import.Test;

/// <summary>
/// Helpers for testing VPI column entry region parsers.
/// </summary>
internal static class TestHelper
{
    /// <summary>
    /// Creates a context with a current woodblock item.
    /// </summary>
    /// <returns>Context.</returns>
    public static CadmusEntrySetContext CreateContext()
    {
        CadmusEntrySetContext context = new();
        context.Items.Add(new Item
        {
            FacetId = "woodblock",
            Title = "RGT_001",
            CreatorId = "zeus",
            UserId = "zeus"
        });
        return context;
    }

    /// <summary>
    /// Parses a single column region with the specified tag and value.
    /// </summary>
    /// <param name="parser">The parser.</param>
    /// <param name="context">The context.</param>
    /// <param name="tag">The column region tag.</param>
    /// <param name="value">The cell value.</param>
    /// <returns>The index of the next entry.</returns>
    public static Task<int> ParseColumnAsync(IEntryRegionParser parser,
        CadmusEntrySetContext context, string tag, string? value)
    {
        List<DecodedEntry> entries =
        [
            new DecodedCommandEntry(0, 0, "col-start", "x", "1"),
            new DecodedTextEntry(0, 0, value),
            new DecodedCommandEntry(0, 0, "col-end")
        ];
        EntrySet set = new(entries, context);
        EntryRegion region = new(0,
            new EntryRange(new EntryPoint(0), new EntryPoint(2)), tag);
        return parser.ParseAsync(set, 0, [region], 0);
    }
}
