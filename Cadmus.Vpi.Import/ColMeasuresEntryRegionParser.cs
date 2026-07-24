using Cadmus.Import.Proteus;
using Cadmus.General.Parts;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;
using Cadmus.Mat.Bricks;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column measures entry region parser. This targets
/// <see cref="PhysicalMeasurementsPart"/>.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-measures")]
public sealed class ColMeasuresEntryRegionParser:
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => ["col-object_measures_(h_x_w)"];

    private (float w, float h) ParseMeasures(string value)
    {
        string[] parts = value.Split('x', StringSplitOptions.RemoveEmptyEntries);

        if (parts.Length != 2)
            Logger?.LogError("Invalid measures format, expected HxW: {Value}",
                value);

        if (!float.TryParse(parts[0], out float h))
            Logger?.LogError("Invalid height in measures: {Value}", value);

        if (!float.TryParse(parts[1], out float w))
            Logger?.LogError("Invalid width in measures: {Value}", value);
        
        return (w, h);
    }

    /// <summary>
    /// Parses the region of entries at <paramref name="entryRegionIndex" />
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
            Logger?.LogError("Measures column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "Measures column without any item at region " + region);
        }

        DecodedTextEntry txt = (DecodedTextEntry)
            entrySet.Entries[region.Range.Start.Entry + 1];
        string? value = ImportHelper.FilterValue(txt.Value, false);

        // parse HxW, e.g. 0.5x0.3
        (float w, float h) = ParseMeasures(value ?? "");
        if (w != 0.0 && h != 0.0)
        {
            PhysicalMeasurementsPart part = ctx.EnsurePartForCurrentItem
                <PhysicalMeasurementsPart>();
            part.Measurements.Add(new PhysicalMeasurement
            {
                Name = "width",
                Value = w,
                Unit = "mm"
            });
            part.Measurements.Add(new PhysicalMeasurement
            {
                Name = "height",
                Value = h,
                Unit = "mm"
            });
        }
        
        return entryIndex + 1;
    }
}
