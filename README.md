# Cadmus VPI

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
    - [figurative plan](https://github.com/vedph/cadmus-ndp-books/blob/master/docs/figurative-plan-part.md) (BOK) 🔗 artist
narrative
    - [decorations](https://github.com/vedph/cadmus-codicology/blob/master/docs/cod-decorations.md) (COD)
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`inc` 🚩 (incipit)
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md):`col` 🚩 (colophon)
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

- **print instance**: a physical print copy of a print edition.
  - _identity_
    - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md) 🔗 `print edition` (+group)
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
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md)
  - _content_
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`ill` 🚩: illustration type.
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`ico` 🚩: iconographic categories.
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`thm` 🚩: narrative themes.
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

- **woodcut block**: a woodcut block used for printing.
  - _identity_
    - [metadata](https://github.com/vedph/cadmus-general/blob/master/docs/metadata.md)
    - [links](https://github.com/vedph/cadmus-general/blob/master/docs/pin-links.md)
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`wblk` 🚩: types and styles. Given that we have only a couple of styles, it is probably best to have a more compact taxonomy with 2 branches, one for types and another for styles.
  - _material_
    - [measurements](https://github.com/vedph/cadmus-general/blob/master/docs/physical-measurements.md):`wblk` 🚩
  - _content_
    - [note](https://github.com/vedph/cadmus-general/blob/master/docs/note.md)
  - _references_
    - [references](https://github.com/vedph/cadmus-general/blob/master/docs/doc-references.md)

With reference to the original schema:

- edition is the print edition item.
- copy is the print instance item.
- textual unit is the text passages part inside the edition item.
- narrative type is a taxonomy for text passages part.
- illustration instance is a decoration in the decorations part inside the print edition item. It is linked to subject, figure, narrative via links from each decoration.
- woodcut block is the woodcut block item. Its reuse (and its metadata) is implicit in the links from the decoration to the block.

Part types list (from GEN, COD, NDP):

- it.vedph.categories:wblk
- it.vedph.chronotopes:prn
- it.vedph.chronotopes:pub
- it.vedph.codicology.bindings
- it.vedph.codicology.decorations
- it.vedph.codicology.edits
- it.vedph.codicology.layouts:prn
- it.vedph.codicology.sheet-labels
- it.vedph.codicology.shelfmarks
- it.vedph.codicology.watermarks
- it.vedph.doc-references
- it.vedph.metadata
- it.vedph.ndp.print-fig-plan
- it.vedph.ndp.print-fig-plan-impl
- it.vedph.ndp.print-fonts
- it.vedph.ndp.text-passages
- it.vedph.note
- it.vedph.note:inc
- it.vedph.note:col
- it.vedph.physical-measurements:pri
- it.vedph.pin-links
