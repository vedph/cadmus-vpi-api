using Cadmus.Import.Proteus;
using Cadmus.Refs.Bricks;
using Cadmus.General.Parts;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Entries;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

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

    private static List<string> SplitTextAtUnbracketedSemicolonPlusDigit(string text)
    {
        // collect indexes of all unbracketed semicolons followed by optional
        // whitespaces and a digit, except when the semicolon is inside brackets
        // (in reverse order)
        List<int> indexes = [];
        int i = FindFirstUnbracketedChar(text, ';');
        while (i > -1)
        {
            // check if the semicolon is followed by a digit, possibly after whitespace
            if (i + 1 < text.Length && char.IsDigit(text[i + 1]) ||
                (char.IsWhiteSpace(text[i + 1]) && i + 2 < text.Length &&
                 char.IsDigit(text[i + 2])))
            {
                indexes.Insert(0, i);
            }
            i = FindFirstUnbracketedChar(text, ';', i + 1);
        }

        // split text at each collected index
        List<string> results = [];
        string reduced = text;
        foreach (int index in indexes)
        {
            results.Insert(0, reduced[(index + 1)..].Trim());
            reduced = reduced[..index];
        }
        // add the remaining part
        if (reduced.Length > 0) results.Insert(0, reduced.Trim());

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
    protected override async Task<int> DoParseAsync(EntrySet entrySet, int entryIndex,
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
        string? value = ImportHelper.FilterValue(txt.Value, false)?.TrimEnd(' ', ';');

        if (!string.IsNullOrEmpty(value))
        {
            List<string> texts = SplitTextAtUnbracketedSemicolonPlusDigit(value);
            List<AssertedCompositeId> ids = [];
            
            foreach (string text in texts.Where(s => s.Length > 0))
            {
                // split text at first unbracketed space: left is ID,
                // right is label, but just discard if does not start with digit
                // (some annotations are interspersed in the text)
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
                    Scope = "iconclass",
                    Target = new PinTarget
                    {
                        Gid = id,
                        Label = label,
                    }
                });

                // log a warning when id does not start with a digit
                if (!char.IsDigit(id[0]))
                {
                    Logger?.LogWarning(
                        "Iconclass link ID does not start with a digit: {Id}", id);
                }
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
