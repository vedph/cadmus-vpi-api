using Cadmus.Import.Proteus;
using Cadmus.Refs.Bricks;
using Cadmus.General.Parts;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column links entry region parser. This targets PinLinksPart.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-links")]
public sealed class ColLinksEntryRegionParser :
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => [
        "col-image_tags_(iconclass)",
        "col-image_tags_(index_of_medieval_art)" ];

    /// <summary>
    /// Parses the region of entries at <paramref name="regionIndex" />
    /// in the specified <paramref name="regions" />.
    /// </summary>
    /// <param name="set">The entries set.</param>
    /// <param name="regions">The regions.</param>
    /// <param name="regionIndex">Index of the region in the set.</param>
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
            Logger?.LogError("Links column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "Links column without any item at region " + region);
        }

        DecodedTextEntry txt = (DecodedTextEntry)
            entrySet.Entries[region.Range.Start.Entry + 1];
        string? value = ImportHelper.FilterValue(txt.Value, false);

        PinLinksPart part = ctx.EnsurePartForCurrentItem<PinLinksPart>();

        foreach (string id in ImportHelper.GetValueList(value, false, [';']))
        {
            part.Links.Add(new AssertedCompositeId
            {
                Target = new PinTarget
                {
                    Gid = id,
                    Label = id,
                }
            });
        }

        return entryIndex + 1;
    }
}
