using Cadmus.Import.Excel;
using Cadmus.Import.Proteus;
using Cadmus.Ndp.Parts;
using Fusi.Tools.Configuration;
using Microsoft.Extensions.Logging;
using Proteus.Core;
using Proteus.Core.Regions;
using System;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI text passage columns entry region parser. This handles a pair of
/// columns, the citation of the passage illustrated by the woodblock (e.g.
/// <c>Vite dei Santi Padri, I,1; paulo primo romito</c>, where the semicolon
/// is part of the citation) and its title, both going into the first passage
/// of the item's <see cref="TextPassagesPart"/>, respectively as its citation
/// and text.
/// <para>Tag: <c>entry-region-parser.vpi.col-text-passage</c>.</para>
/// </summary>
/// <seealso cref="ColEntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-text-passage")]
public sealed class ColTextPassageEntryRegionParser : ColEntryRegionParser,
    IEntryRegionParser, IConfigurable<ColTextPassageEntryRegionParserOptions>
{
    private ColTextPassageEntryRegionParserOptions _options = new();

    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => [_options.CitationTag, _options.TextTag];

    /// <summary>
    /// Configures this parser.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <exception cref="ArgumentNullException">Null options.</exception>
    public void Configure(ColTextPassageEntryRegionParserOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        ConfigureBaseOptions(options);
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

        TextPassagesPart part =
            context.EnsurePartForCurrentItem<TextPassagesPart>();
        if (part.Passages.Count == 0) part.Passages.Add(new TextPassage());
        TextPassage passage = part.Passages[0];

        if (region.Tag == _options.CitationTag)
        {
            passage.Citation = value;
        }
        else
        {
            passage.Text = value;
            if (string.IsNullOrEmpty(passage.Citation))
            {
                Logger?.LogWarning("Text passage title \"{Title}\" without " +
                    "citation at region {Region}", value, region);
            }
        }
    }
}

/// <summary>
/// Options for <see cref="ColTextPassageEntryRegionParser"/>.
/// </summary>
public class ColTextPassageEntryRegionParserOptions : DisabledOptions
{
    /// <summary>
    /// Gets or sets the tag of the citation column, which must come before
    /// the text column. Default is <c>col-text_passage_vsp</c>.
    /// </summary>
    public string CitationTag { get; set; } = "col-text_passage_vsp";

    /// <summary>
    /// Gets or sets the tag of the text column. Default is
    /// <c>col-text_passage_title</c>.
    /// </summary>
    public string TextTag { get; set; } = "col-text_passage_title";
}
