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
/// VPI column categories entry region parser. This targets CategoriesPart:ico.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-categories")]
public sealed class ColCategoriesEntryRegionParser :
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => ["col-ico-category"];

    private static string[] MapCategory(string text)
    {
        return text.ToLowerInvariant() switch
        {
            "persecutions" => ["ico.persecutions"],
            "ascetic life and practices" => ["ico.ascetic-life-practices"],
            "ascetic-life-and-practices" => ["ico.ascetic-life-practices"],
            "death and afterlife" => ["ico.death-afterlife"],
            "death, vision, afterlife" => ["ico.death-afterlife", "ico.visions"],
            "vision" => ["ico.visions"],
            "visions" => ["ico.visions"],
            "charitable care and healing" => ["ico.charitable-care-healing"],
            "miracles" => ["ico.miraculous-intervention"],
            "miraculous intervention" => ["ico.miraculous-intervention"],
            "community and monastic life" => ["ico.community-monastic-life"],
            "teaching" => ["ico.teaching"],
            "teaching and preaching" => ["ico.teaching"],
            "demons and temptations" => ["ico.demons", "ico.temptations"],
            "temptations" => ["ico.temptations"],
            "animals" => ["ico.animal-communion"],
            _ => []
        };
    }

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
            Logger?.LogError("Categories column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "Categories column without any item at region " + region);
        }

        DecodedTextEntry txt = entrySet.GetEntryAt<DecodedTextEntry>(
            entryIndex + 1)!;
        string? value = ImportHelper.FilterValue(txt.Value, false);

        if (!string.IsNullOrEmpty(value))
        {
            HashSet<string> ids = [];
            foreach (string label in ImportHelper.GetValueList(value, false, [';']))
            {
                string[] mapped = MapCategory(label);
                if (mapped.Length == 0)
                {
                    Logger?.LogWarning("Unmapped category label for {Tag}: \"{Label}\" " +
                        "at region {Region}", region.Tag, label, region);
                    continue;
                }

                foreach (string m in mapped)
                {
                    string id = ImportHelper.GetThesaurusId(
                        ctx, region, "categories_ico@en", m, Logger);
                    if (id == null)
                    {
                        Logger?.LogError(
                            "Unknown category label for {Tag}: \"{Label}\" " +
                            "at region {Region}", region.Tag, m, region);
                        continue;
                    }
                    ids.Add(id);
                }
            }

            if (ids.Count > 0)
            {
                CategoriesPart part = ctx.EnsurePartForCurrentItem<CategoriesPart>(
                    "ico");
                foreach (string id in ids) part.Categories.Add(id);
            }
        }

        return entryIndex + 3;
    }   
}
