using Cadmus.General.Parts;
using Cadmus.Import.Proteus;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column categories entry region parser. This targets links.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-ima-links")]
public sealed class ColImaLinksEntryRegionParser :
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => ["col-image_tags_(index_of_medieval_art)"];

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
            Logger?.LogError("IMA column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "IMA column without any item at region " + region);
        }

        DecodedTextEntry txt = entrySet.GetEntryAt<DecodedTextEntry>(
            entryIndex + 1)!;
        string? value = ImportHelper.FilterValue(txt.Value, false);

        List<AssertedCompositeId> ids = [];
        foreach (string id in ImportHelper.GetValueList(value, false, [';']))
        {
            ids.Add(new AssertedCompositeId
            {
                Target = new PinTarget
                {
                    Gid = id,
                    Label = id,
                }
            });
        }
        if (ids.Count > 0)
        {
            PinLinksPart part = ctx.EnsurePartForCurrentItem<PinLinksPart>();
            foreach (AssertedCompositeId id in ids) part.Links.Add(id);
        }

        return entryIndex + 3;
    }
}