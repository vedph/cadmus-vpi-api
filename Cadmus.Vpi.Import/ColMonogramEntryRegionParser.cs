using Cadmus.General.Parts;
using Cadmus.Import.Excel;
using Cadmus.Import.Proteus;
using Fusi.Tools.Configuration;
using Proteus.Core.Regions;
using System;
using System.Text.RegularExpressions;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column notes entry region parser, handling monogram marks. When the
/// cell contains <c>monogram</c>, like <c>monogram (b)</c>, this adds to the
/// item's <see cref="MetadataPart"/> a boolean <c>monogram</c> metadatum with
/// value <c>1</c>, and, when there is a letter in parentheses, a string
/// <c>monogram-type</c> metadatum with that letter. Any other text is
/// appended to the item's <see cref="NotePart"/> with the configured role.
/// <para>Tag: <c>entry-region-parser.vpi.col-monogram</c>.</para>
/// </summary>
/// <seealso cref="ColEntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-monogram")]
public sealed partial class ColMonogramEntryRegionParser : ColEntryRegionParser,
    IEntryRegionParser, IConfigurable<ColMonogramEntryRegionParserOptions>
{
    [GeneratedRegex(@"monogram\s*(?:\(\s*([^)\s]+)\s*\))?",
        RegexOptions.IgnoreCase)]
    private static partial Regex MonogramRegex();

    private ColMonogramEntryRegionParserOptions _options = new();

    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => _options.Tag != null ? [_options.Tag] : [];

    /// <summary>
    /// Gets a value indicating whether the value passed to
    /// <see cref="ParseValue" /> must be just trimmed, rather than filtered.
    /// This preserves the line breaks of notes.
    /// </summary>
    protected override bool RawValue => true;

    /// <summary>
    /// Configures this parser.
    /// </summary>
    /// <param name="options">The options.</param>
    /// <exception cref="ArgumentNullException">Null options.</exception>
    public void Configure(ColMonogramEntryRegionParserOptions options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        ConfigureBaseOptions(options);
    }

    /// <summary>
    /// Parses the specified column value.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="region">The column region.</param>
    /// <param name="value">The value, or null if empty.</param>
    protected override void ParseValue(CadmusEntrySetContext context,
        EntryRegion region, string? value)
    {
        if (value == null) return;

        Match m = MonogramRegex().Match(value);
        if (!m.Success)
        {
            NotePart note = context.EnsurePartForCurrentItem<NotePart>(
                _options.NoteRoleId);
            note.Text = ImportHelper.AppendToNote(note.Text, value);
            return;
        }

        MetadataPart part = context.EnsurePartForCurrentItem<MetadataPart>();
        part.Metadata.Add(new Metadatum
        {
            Type = "boolean",
            Name = "monogram",
            Value = "1"
        });
        if (m.Groups[1].Success)
        {
            part.Metadata.Add(new Metadatum
            {
                Type = "string",
                Name = "monogram-type",
                Value = m.Groups[1].Value
            });
        }
    }
}

/// <summary>
/// Options for <see cref="ColMonogramEntryRegionParser"/>.
/// </summary>
public class ColMonogramEntryRegionParserOptions : ColEntryRegionParserOptions
{
    /// <summary>
    /// Gets or sets the optional role ID of the note part receiving any text
    /// which is not a monogram mark.
    /// </summary>
    public string? NoteRoleId { get; set; }

    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="ColMonogramEntryRegionParserOptions"/> class.
    /// </summary>
    public ColMonogramEntryRegionParserOptions()
    {
        Tag = "col-notes";
    }
}
