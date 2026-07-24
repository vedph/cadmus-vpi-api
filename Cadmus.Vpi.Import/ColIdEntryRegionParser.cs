using Cadmus.General.Parts;
using Cadmus.Import.Proteus;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column ID entry region parser. This adds a metadata part with an
/// <c>eid</c> metadatum.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
/// <remarks>
/// Initializes a new instance of the <see cref="ColIdEntryRegionParser"/>
/// class.
/// </remarks>
/// <param name="logger">The logger.</param>
[Tag("entry-region-parser.vpi.col-id")]
public sealed class ColIdEntryRegionParser : EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => ["col-object_name"];

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
            Logger?.LogError("ID column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "ID column without any item at region " + region);
        }

        DecodedTextEntry txt = entrySet.GetEntryAt<DecodedTextEntry>(
            entryIndex + 1)!;
        string id = ImportHelper.FilterValue(txt.Value, false) ??
            throw new InvalidOperationException("no ID column at region " + region);

        if (!string.IsNullOrEmpty(id))
        {
            // metadata
            MetadataPart part = ctx.EnsurePartForCurrentItem<MetadataPart>();
            part.Metadata.Add(new Metadatum
            {
                Type = "string",
                Name = "eid",
                Value = id
            });
            Logger?.LogInformation("-- ID: {Id}", id);
        }

        return entryIndex + 3;
    }
}
