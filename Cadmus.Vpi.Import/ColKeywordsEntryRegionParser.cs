using Cadmus.Import.Proteus;
using Cadmus.General.Parts;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column keywords entry region parser. This targets TODO.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-keywords")]
public sealed class ColKeywordsEntryRegionParser :
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => ["col_controlled_keywords"];

    /// <summary>
    /// Parses the region of entries at <paramref name="regionIndex" />
    /// in the specified <paramref name="entryRegions" />.
    /// </summary>
    /// <param name="entrySet">The entries set.</param>
    /// <param name="entryRegions">The regions.</param>
    /// <param name="entryRegionIndex">Index of the region in the set.</param>
    /// <returns>
    /// The index to the next region to be parsed.
    /// </returns>
    /// <exception cref="ArgumentNullException">set or regions</exception>
    protected override int DoParse(EntrySet entrySet, int entryIndex,
        IReadOnlyList<EntryRegion> entryRegions, int entryRegionIndex)
    {
        ArgumentNullException.ThrowIfNull(entrySet);
        ArgumentNullException.ThrowIfNull(entryRegions);

        CadmusEntrySetContext ctx = (CadmusEntrySetContext)entrySet.Context;
        EntryRegion region = entryRegions[entryRegionIndex];

        if (ctx.CurrentItem == null)
        {
            Logger?.LogError("Keywords column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "Keywords column without any item at region " + region);
        }

        DecodedTextEntry txt = (DecodedTextEntry)
            entrySet.Entries[region.Range.Start.Entry + 1];
        string? value = VpiHelper.FilterValue(txt.Value, false);
        if (string.IsNullOrEmpty(value)) return entryRegionIndex + 1;

        IndexKeywordsPart part = ctx.EnsurePartForCurrentItem<IndexKeywordsPart>();
        foreach (string s in VpiHelper.GetValueList(value, false, [';']))
        {
            part.Keywords.Add(new IndexKeyword
            {
                Language = "en",
                Value = s,
            });
        }

        return entryRegionIndex + 1;
    }
}
