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
public sealed class ColMeasuresEntryRegionParser(ILogger? logger = null) :
    EntryRegionParser(logger), IEntryRegionParser
{
    private const string COL_MEASURES = "col-object_measures_(h_x_w)";

    /// <summary>
    /// Determines whether this parser is applicable to the specified
    /// region. Typically, the applicability is determined via a configurable
    /// nested object, having parameters like region tag(s) and paths.
    /// </summary>
    /// <param name="set">The entries set.</param>
    /// <param name="regions">The regions.</param>
    /// <param name="regionIndex">Index of the region.</param>
    /// <returns>
    ///   <c>true</c> if applicable; otherwise, <c>false</c>.
    /// </returns>
    /// <exception cref="ArgumentNullException"></exception>
    public bool IsApplicable(EntrySet set, IReadOnlyList<EntryRegion> regions,
        int regionIndex)
    {
        ArgumentNullException.ThrowIfNull(set);
        ArgumentNullException.ThrowIfNull(regions);

        return regions[regionIndex].Tag == COL_MEASURES;
    }

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
    public int Parse(EntrySet set, IReadOnlyList<EntryRegion> regions,
        int regionIndex)
    {
        ArgumentNullException.ThrowIfNull(set);
        ArgumentNullException.ThrowIfNull(regions);

        CadmusEntrySetContext ctx = (CadmusEntrySetContext)set.Context;
        EntryRegion region = regions[regionIndex];

        if (ctx.CurrentItem == null)
        {
            Logger?.LogError("Measures column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "Measures column without any item at region " + region);
        }

        DecodedTextEntry txt = (DecodedTextEntry)
            set.Entries[region.Range.Start.Entry + 1];
        string? value = VpiHelper.FilterValue(txt.Value, false);

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
        
        return regionIndex + 1;
    }
}
