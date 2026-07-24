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
/// VPI column Iconclass links entry region parser. This targets PinLinksPart.
/// Each link is separated by an unbracketed semicolon, and each link is composed
/// of an ID and a label, separated by the first unbracketed space.
/// </summary>
/// <seealso cref="EntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-icc-links")]
public sealed class ColIccLinksEntryRegionParser :
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => [ "col-image_tags_(iconclass)" ];

    private static int FindFirstUnbracketedChar(string text, char target,
        int startIndex = 0)
    {
        if (string.IsNullOrEmpty(text)) return -1;

        int bracketLevel = 0;
        for (int i = startIndex; i < text.Length; i++)
        {
            char c = text[i];

            if (c == '(')
            {
                bracketLevel++;
            }
            else if (c == ')')
            {
                // decrement level, ensuring it doesn't go negative
                // on unmatched closing brackets
                if (bracketLevel > 0) bracketLevel--;
            }
            else if (c == target && bracketLevel == 0)
            {
                return i; // found the first target at depth zero
            }
        }

        return -1; // no matching target found
    }

    private static List<string> SplitTextAtUnbracketedSemicolon(string text)
    {
        // collect indexes of all unbracketed semicolons (in reverse order)
        List<int> indexes = [];
        int i = FindFirstUnbracketedChar(text, ';');
        while (i > -1)
        {
            indexes.Insert(0, i);
            i = FindFirstUnbracketedChar(text, ';', i + 1);
        }

        // split text at each collected index
        List<string> results = [];
        string reduced = text;
        foreach (int index in indexes)
        {
            results.Add(reduced[(index + 1)..]);
            reduced = reduced[..index];
        }
        // add the remaining part
        if (reduced.Length > 0) results.Add(reduced);

        // remove empty entries
        results.RemoveAll(s => s.Length == 0);

        return results;
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

        DecodedTextEntry txt = entrySet.GetEntryAt<DecodedTextEntry>(
            entryIndex + 1)!;
        string? value = ImportHelper.FilterValue(txt.Value, false);
        if (!string.IsNullOrEmpty(value))
        {
            List<string> texts = SplitTextAtUnbracketedSemicolon(value);
            List<AssertedCompositeId> ids = [];
            
            foreach (string text in texts)
            {
                // split text at first unbracketed space: left is ID,
                // right is label
                string id = text;
                string label = text;

                int i = FindFirstUnbracketedChar(text, ' ');
                if (i > -1)
                {
                    id = text[..i];
                    label = text[(i + 1)..];    
                }

                ids.Add(new AssertedCompositeId
                {
                    Target = new PinTarget
                    {
                        Gid = id,
                        Label = label,
                    }
                });
            }

            if (ids.Count > 0)
            {
                PinLinksPart part = ctx.EnsurePartForCurrentItem<PinLinksPart>();
                foreach (AssertedCompositeId id in ids) part.Links.Add(id);
            }
        }

        return entryIndex + 3;
    }
}
