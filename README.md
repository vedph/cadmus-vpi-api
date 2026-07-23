# Cadmus VPI

🐋 Quick Docker image build:

```sh
docker buildx create --use

docker buildx build . --platform linux/amd64,linux/arm64,windows/amd64 -t vedph2020/cadmus-vpi-api:0.0.2 -t vedph2020/cadmus-vpi-api:latest --push
```

(replace with the current version).

## Facets

The list of facets is given here with their conventional groupings used in the editor UI. The 3-letters abbreviation after each part type name refers to Cadmus model spaces different from the generic one. Here we have `COD`=codicology, `BOK`=books. Also, 🔗 means a potential _internal_ link; where there is a links part without this indication, the usual implication is that it contains _external_ links.

- **print edition**: the abstract edition of a print work, which may have multiple print instances.
  - _identity_
    - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md) 🔗 authors, editors
  - _history_
    - [chronotopes](https://github.com/vedph/cadmus-general/blob/master/docs/chronotopes.md):`prn` 🚩 (print date/place)
    - [chronotopes](https://github.com/vedph/cadmus-general/blob/master/docs/chronotopes.md):`pub` 🚩 (publication date/place)
  - _content_
    - [fonts](https://github.com/vedph/cadmus-ndp-books/blob/master/docs/print-fonts-part.md) (BOK)
    - [layouts](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-layouts.md):`prn` 🚩 (COD)
    - [watermarks](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-watermarks.md) (COD)
    - [figurative plan](https://github.com/vedph/cadmus-ndp-books/blob/master/docs/figurative-plan-part.md) (BOK) 🔗 artist narrative
    - [decorations](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-decorations.md) (COD)
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`inc` 🚩 (incipit)
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`col` 🚩 (colophon)
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

- **print instance**: a physical print copy of a print edition. Its group ID is the EID of the print edition.
  - _identity_
    - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md) 🔗 print edition (+group)
    - [shelfmarks](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-shelfmarks.md)
  - _history_
    - [historical events](https://github.com/vedph/cadmus-general/blob/master/docs/historical-events.md)`pri` 🚩
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`hist` 🚩
  - _material_
    - [bindings](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-bindings.md) (COD)
    - [sheet labels](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-sheet-labels.md) (COD)
    - [measurements](https://github.com/vedph/cadmus-general/blob/master/docs/physical-measurements.md):`pri`
    - [preservation states](https://github.com/vedph/cadmus-general/blob/master/docs/physical-states.md)
  - _content_
    - [layouts](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-layouts.md):`prn` 🚩 (COD)
    - [figurative plan implementation](https://github.com/vedph/cadmus-ndp-books/blob/master/docs/figurative-plan-impl-part.md) (BOK)
    - [edits](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-edits.md) (COD)
  - _content_
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

- **person**: authority entity for figures.
  - _identity_
    - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md) (VIAF, Wikidata, IMA)
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`fig` 🚩: figure type.
  - _content_
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

- **illustration**: an illustration in a print instance. Its group ID is the EID of the print instance.
  - _identity_
    - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md) 🔗 woodcut block
  - _content_
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`ill` 🚩: illustration types.
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`reuse` 🚩: reuse types.
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

- **woodcut block**: a woodcut block used for printing.
  - _identity_
    - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md) 🔗 illustration, person
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`wblk` 🚩 block types
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`ico` 🚩 iconographies
  - _material_
    - [measurements](https://github.com/vedph/cadmus-general/blob/master/docs/physical-measurements.md):`wblk` 🚩
  - _content_
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`narr` 🚩 narrative types
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`txt` 🚩
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
    - [keywords](https://github.com/vedph/cadmus-general/blob/master/docs/it.vedph.index-keywords.md)
    - [text passages](https://github.com/vedph/cadmus-ndp/blob/master/docs/text-passages.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

With reference to the original schema:

- edition is the print edition item.
- copy is the print instance item.
- textual unit is the text passages part inside the edition item.
- narrative type is a taxonomy for text passages part.
- illustration instance is a decoration in the decorations part inside the print edition item. It is linked to subject, figure, narrative via links from each decoration.
- woodcut block is the woodcut block item. Its reuse (and its metadata) is implicit in the links from the decoration to the block.

## Parts Matrix

| part          | print-ed  | print-inst | person | illustration | woodblock     |
| ------------- | --------- | ---------- | ------ | ------------ | ------------- |
| bindings      |           | X          |        |              |               |
| categories    |           |            | fig    | ill reuse    | wblk ico narr |
| chronotopes   | prn pub   |            |        |              |               |
| decorations   | X         |            |        |              |               |
| edits         |           | X          |        |              |               |
| events        |           | pri        |        |              |               |
| keywords      |           |            |        |              | X             |
| links         | X         | X          | X      | X            | X             |
| measurements  |           | X          |        |              | X             |
| metadata      | X         | X          | X      | X            | X             |
| fig plan      | X         |            |        |              |               |
| fig plan impl |           | X          |        |              |               |
| fonts         | X         |            |        |              |               |
| layouts       | prn       | prn        |        |              |               |
| note          | X inc col | X hist     | X      | X            | X txt         |
| references    | X         | X          | X      | X            | X             |
| sheet labels  |           | X          |        |              |               |
| shelfmarks    |           | X          |        |              |               |
| states        |           | X          |        |              |               |
| text passages |           |            |        |              | X             |
| watermarks    | X         |            |        |              |               |

## Import

Mapping for woodblocks import from an Excel file.

- for each row: ⚙️ [Row](Cadmus.Vpi.Import/RowEntryRegionParser.cs).

- **A** (`Object name`) (string): ID 🎯 `MetadataPart`: `eid`=value ⚙️ [ColIdEntryRegionParser](Cadmus.Vpi.Import/ColIdEntryRegionParser.cs)
- **B** (`folio`) (string): location 🎯 `MetadataPart`: `location`=value ⚙️ [ColLocEntryRegionParser](Cadmus.Vpi.Import/ColLocEntryRegionParser.cs)
- **C** (`Object measures (h x w)`): size with format `NxN` for height and width, mm. 🎯 `PhysicalMeasurementsPart` ⚙️ [ColMeasuresEntryRegionParser](Cadmus.Vpi.Import/ColMeasuresEntryRegionParser.cs)
- **P** (`Text`) (string): inscription's text 🎯 `MetadataPart`: `inscription`=value (when it is not present, no inscription is present, so the boolean flag is redundant) ⚙️ [ColTextEntryRegionParser](Cadmus.Vpi.Import/ColTextEntryRegionParser.cs)
- **Q** (`Controlled Keywords`): keywords (separated by `;`) 🎯 `IndexKeywordsPart` ⚙️ [ColKeywordsEntryRegionParser](Cadmus.Vpi.Import/ColKeywordsEntryRegionParser.cs)
- **R** (`Image tags (Iconclass)`): IconClass tags (separated by `;`) 🎯 `PinLinksPart` ⚙️ [ColLinksEntryRegionParser](Cadmus.Vpi.Import/ColLinksEntryRegionParser.cs)
- **S** (`Ico-Category`) category IDs (separated by `|`) 🎯 `CategoriesPart:ico` 📚 `categories_ico@en` ⚙️ [ColCategoriesEntryRegionParser](Cadmus.Vpi.Import/ColCategoriesEntryRegionParser.cs)
- **T** (`Image tags (Index of medieval art)`): IMA tags (separated by `;`) 🎯 `PinLinksPart` ⚙️ [ColLinksEntryRegionParser](Cadmus.Vpi.Import/ColLinksEntryRegionParser.cs)
- **W** (`no. of cut`) (string): title suffix 🎯 `item.title`=`RGT_` + 3-digits number from W and `MetadataPart`: `cut-number`=value ⚙️ [ColCutEntryRegionParser](Cadmus.Vpi.Import/ColCutEntryRegionParser.cs).

### Code Template

Template for region parser:

- `__TAG__`: the region tag.
- `__NAME__` the class name.

```cs
public sealed class Col__NAME__EntryRegionParser :
    EntryRegionParser, IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => [ "col-__TAG__" ];

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
            Logger?.LogError("__TAG__ column without any item at region {Region}",
                region);
            throw new InvalidOperationException(
                "__TAG__ column without any item at region " + region);
        }

        DecodedTextEntry txt = (DecodedTextEntry)
            entrySet.Entries[region.Range.Start.Entry + 1];
        string? value = VpiHelper.FilterValue(txt.Value, false);

        // TODO

        return entryRegionIndex + 1;
    }   
}
```
