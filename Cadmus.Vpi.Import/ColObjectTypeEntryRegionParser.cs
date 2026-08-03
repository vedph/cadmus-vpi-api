using Cadmus.Import.Proteus;
using Cadmus.General.Parts;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;
using Cadmus.Core.Config;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column object type entry region parser.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-object-type")]
public sealed class ColObjectTypeEntryRegionParser :
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => ["col-illustrative_object_type"];

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
            Logger?.LogError("col-illustrative_object_type column without any " +
                "item at region {Region}", region);
            throw new InvalidOperationException(
                "col-illustrative_object_type column without any item at region "
                + region);
        }

        DecodedTextEntry txt = entrySet.GetEntryAt<DecodedTextEntry>(
            entryIndex + 1)!;
        string? value = ImportHelper.FilterValue(txt.Value, true);

        if (!string.IsNullOrEmpty(value))
        {
            // build ID
            CategoriesPart part =
                ctx.EnsurePartForCurrentItem<CategoriesPart>("wblk");
            string id = "type." + value;

            // the thesaurus must be present
            Thesaurus? thesaurus = ctx.ThesaurusEntryMap?.GetThesaurus(
                "categories_wblk@en");
            if (thesaurus == null)
            {
                Logger?.LogError("col-illustrative_object_type value {Value} " +
                    "not checked in thesaurus categories_wblk@en at region {Region}",
                    value, region);
            }
            else
            {
                // the value must be present in the thesaurus
                if (thesaurus.GetEntryValue(id) == null)
                {
                    Logger?.LogError("col-illustrative_object_type value {Value} " +
                        "not found in thesaurus categories_wblk@en at region {Region}",
                        value, region);
                }
                else part.Categories.Add(id);
            }
        }

        return entryIndex + 3;
    }
}
