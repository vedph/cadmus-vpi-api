using Cadmus.General.Parts;
using Cadmus.Import.Excel;
using Cadmus.Import.Proteus;
using Cadmus.Refs.Bricks;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core.Regions;
using System;
using System.Collections.Generic;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column Iconclass links entry region parser. The cell contains one or
/// more Iconclass notations with their labels, like
/// <c>42E23 burial;11Q331 anchorite, hermit</c>: each becomes an external
/// link in the item's <see cref="PinLinksPart"/>, whose GID is the notation
/// and whose label is the label.
/// <para>Values are separated by <c>;</c> at bracket depth zero, when
/// followed by a digit, optionally after a single space; so, a semicolon
/// inside brackets or followed by text is part of the label. Each value is
/// split into notation and label at its first space at bracket depth zero.
/// </para>
/// <para>Tag: <c>entry-region-parser.vpi.col-iconclass-links</c>.</para>
/// </summary>
/// <seealso cref="ColEntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-iconclass-links")]
public sealed class ColIconclassLinksEntryRegionParser : ColEntryRegionParser,
    IEntryRegionParser, IConfigurable<ColIconclassLinksEntryRegionParserOptions>
{
    private ColIconclassLinksEntryRegionParserOptions _options = new();

    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => _options.Tag != null ? [_options.Tag] : [];

    /// <summary>
    /// Configures this parser.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <exception cref="ArgumentNullException">Null options.</exception>
    public void Configure(ColIconclassLinksEntryRegionParserOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        ConfigureBaseOptions(options);
    }

    /// <summary>
    /// Splits the specified text into Iconclass entries, at each semicolon
    /// at bracket depth zero which is followed by a digit, optionally after
    /// a single whitespace.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The trimmed, non-empty entries.</returns>
    /// <exception cref="ArgumentNullException">text</exception>
    public static IList<string> SplitEntries(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        List<string> entries = [];
        int depth = 0, start = 0;
        for (int i = 0; i < text.Length; i++)
        {
            switch (text[i])
            {
                case '(':
                    depth++;
                    break;
                case ')':
                    if (depth > 0) depth--;
                    break;
                case ';':
                    if (depth > 0) break;
                    int j = i + 1;
                    if (j < text.Length && char.IsWhiteSpace(text[j])) j++;
                    if (j < text.Length && char.IsDigit(text[j]))
                    {
                        entries.Add(text[start..i]);
                        start = i + 1;
                    }
                    break;
            }
        }
        entries.Add(text[start..]);

        // trim also trailing separators, as in "42E23 burial;"
        List<string> result = [];
        foreach (string entry in entries)
        {
            string s = entry.Trim().TrimEnd(';').Trim();
            if (s.Length > 0) result.Add(s);
        }
        return result;
    }

    /// <summary>
    /// Splits the specified entry into its Iconclass notation and label, at
    /// the first space at bracket depth zero. If there is no such space, both
    /// the notation and the label are the entry itself.
    /// </summary>
    /// <param name="entry">The entry.</param>
    /// <returns>Notation and label.</returns>
    /// <exception cref="ArgumentNullException">entry</exception>
    public static (string Id, string Label) SplitIdLabel(string entry)
    {
        ArgumentNullException.ThrowIfNull(entry);

        int depth = 0;
        for (int i = 0; i < entry.Length; i++)
        {
            char c = entry[i];
            if (c == '(') depth++;
            else if (c == ')' && depth > 0) depth--;
            else if (c == ' ' && depth == 0)
                return (entry[..i], entry[(i + 1)..].Trim());
        }
        return (entry, entry);
    }

    /// <summary>
    /// Parses the specified column value.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="region">The column region.</param>
    /// <param name="value">The filtered value, or null if empty.</param>
    protected override void ParseValue(CadmusEntrySetContext context,
        EntryRegion region, string? value)
    {
        if (value == null) return;

        IList<string> entries = SplitEntries(value);
        if (entries.Count == 0) return;

        PinLinksPart part = context.EnsurePartForCurrentItem<PinLinksPart>();
        foreach (string entry in entries)
        {
            (string id, string label) = SplitIdLabel(entry);
            if (!char.IsDigit(id[0]))
            {
                Logger?.LogWarning("Iconclass notation \"{Id}\" not starting " +
                    "with a digit at region {Region}", id, region);
            }
            part.Links.Add(new AssertedCompositeId
            {
                Scope = _options.Scope,
                Target = new PinTarget
                {
                    Gid = id,
                    Label = label
                }
            });
        }
    }
}

/// <summary>
/// Options for <see cref="ColIconclassLinksEntryRegionParser"/>.
/// </summary>
public class ColIconclassLinksEntryRegionParserOptions :
    ColEntryRegionParserOptions
{
    /// <summary>
    /// Gets or sets the scope of the links. Default is <c>iconclass</c>.
    /// </summary>
    public string? Scope { get; set; } = "iconclass";

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ColIconclassLinksEntryRegionParserOptions"/> class.
    /// </summary>
    public ColIconclassLinksEntryRegionParserOptions()
    {
        Tag = "col-image_tags_(iconclass)";
    }
}
