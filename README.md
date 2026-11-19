# Cadmus VPI

🐋 Quick Docker image build:

```sh
docker buildx create --use

docker buildx build . --platform linux/amd64,linux/arm64 -t vedph2020/cadmus-vpi-api:1.0.0 -t vedph2020/cadmus-vpi-api:latest --push
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
    - [categories](https://github.com/vedph/cadmus-general/blob/master/docs/categories.md):`reuse` 🚩: reuse types.
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

| part          | print-ed  | print-inst | person | illustration | woodblock           |
| ------------- | --------- | ---------- | ------ | ------------ | ------------------- |
| bindings      |           | X          |        |              |                     |
| categories    |           |            | fig    | ill reuse    | wblk ico narr reuse |
| chronotopes   | prn pub   |            |        |              |                     |
| decorations   | X         |            |        |              |                     |
| edits         |           | X          |        |              |                     |
| events        |           | pri        |        |              |                     |
| keywords      |           |            |        |              | X                   |
| links         | X         | X          | X      | X            | X                   |
| measurements  |           | X          |        |              | X                   |
| metadata      | X         | X          | X      | X            | X                   |
| fig plan      | X         |            |        |              |                     |
| fig plan impl |           | X          |        |              |                     |
| fonts         | X         |            |        |              |                     |
| layouts       | prn       | prn        |        |              |                     |
| note          | X inc col | X hist     | X      | X            | X txt               |
| references    | X         | X          | X      | X            | X                   |
| sheet labels  |           | X          |        |              |                     |
| shelfmarks    |           | X          |        |              |                     |
| states        |           | X          |        |              |                     |
| text passages |           |            |        |              | X                   |
| watermarks    | X         |            |        |              |                     |

## VPI Import

The CLI app `vpi-tool` in this solution is used to import items from Excel files via the Proteus framework, as done for [Cadmus TES](https://github.com/vedph/cadmus-tes). Source Excel files have the columns listed below for each facet; their mapping into Cadmus items and parts follows the same conventions used in TES:

- ❌ = ignored.
- ▶️ = target part type ID or target item property.
- 📚 = ID of thesaurus.
- **asterisk** \* marks a required column. If not specified, the cell can be null/empty and is just skipped. If a cell belonging to a required column has no value, an error is logged and no data stored for that cell.
- **colon** after a part name specifies the part's role ID (e.g. `CategoriesPart:wblk`).

To import:

▶️ (1) fire the API without seeding any items (set item seed count=0 in `appsettings.json`) to create an empty VPI MongoDB database.

▶️ (2) run the import command once per pipeline (change the path to your pipeline file), e.g. for the first woodblocks file:

```sh
./vpi-tool import c:/users/dfusi/desktop/import/Import-w1.json
```

Pipelines are in `vpi-tool/Assets`, named `Dump-X-md.json` (Markdown dump for diagnostics) and `Import-X.json` (MongoDB), where `X` is:

| X     | source file          | target             |
| ----- | -------------------- | ------------------ |
| `w1`  | `woodblocks1.xlsx`   | woodblock items    |
| `w2`  | `woodblocks2.xlsx`   | woodblock items    |
| `ill` | `illustrations.xlsx` | illustration items |

Source files are expected in the `import` folder of your desktop, and dumps are written in a `vpi-dump-X` subfolder of it. There is also a `Dump-xlsx.json` pipeline, dumping the entries read from `woodblocks1.xlsx` into Excel files, to diagnose the reading of the source.

▶️ (3) finally, index the imported database:

```sh
./vpi-tool index cadmus-vpi D:/Projects/Cadmus/Vpi/CadmusVpi/CadmusVpiApi/wwwroot/seed-profile.json
```

### Import Overview

The `ExcelEntryReader` reads each row of an Excel file as a sequence of decoded entries; each row is a region generating an item of the facet of its file, and each column is a region within that row, whose tag is `col-` + the column name lowercased, with whitespaces replaced by `_` (e.g. `Object measures (h x w)` becomes `col-object_measures_(h_x_w)`). Entry region parsers remodel each column into parts of the item. Finally, an exporter saves each item in a Markdown file for diagnostic purposes, or into the MongoDB database.

Most columns are handled by the generic configurable parsers shared by all Cadmus projects, from the core library `Cadmus.Import.Excel` (prefix `it.vedph.entry-region-parser.xlsx.`):

| parser ID        | purpose                                                                                                          |
| ---------------- | ---------------------------------------------------------------------------------------------------------------- |
| `row-item`       | create an item with the specified facet (`facetId`) for each row.                                                |
| `col-metadata`   | add metadata (`name`, `type`, optionally multiple via `separator`), set the item's title or group ID.            |
| `col-categories` | map labels or entry IDs (optionally with `idPrefix`, `lowercaseId`, or explicit `mappings`) to `CategoriesPart`. |
| `col-ext-link`   | external links with a scope to `PinLinksPart`, with GID and label equal to the cell's value(s).                  |
| `col-keywords`   | keywords with a language to `IndexKeywordsPart`.                                                                 |
| `col-measures`   | measurements from `HxW` values to `PhysicalMeasurementsPart`.                                                    |
| `col-note`       | append text to `NotePart`.                                                                                       |

The VPI-specific parsers (namespace `Cadmus.Vpi.Import`, prefix `entry-region-parser.vpi.`) are:

| parser ID             | purpose                                                                                   |
| --------------------- | ----------------------------------------------------------------------------------------- |
| `col-iconclass-links` | Iconclass links from `Image tags (Iconclass)`, split as described below.                  |
| `col-monogram`        | monogram metadata from `Notes`, or `NotePart` for other notes.                            |
| `col-text-passage`    | `Text passage VSP` and `Text passage title` into the first passage of `TextPassagesPart`. |

**Sample dump commands** assuming that the Excel files and the pipelines from `vpi-tool/Assets` are in your desktop `vpi` folder:

```ps1
./vpi-tool import c:/users/dfusi/desktop/vpi/Dump-w1-md.json
Move-Item "D:\Projects\Cadmus\Vpi\CadmusVpi\vpi-tool\bin\Debug\net11.0\vpi-log*.txt" "C:\Users\dfusi\Desktop\vpi\vpi-log-w1.txt"

./vpi-tool import c:/users/dfusi/desktop/vpi/Dump-w2-md.json
Move-Item "D:\Projects\Cadmus\Vpi\CadmusVpi\vpi-tool\bin\Debug\net11.0\vpi-log*.txt" "C:\Users\dfusi\Desktop\vpi\vpi-log-w2.txt"

./vpi-tool import c:/users/dfusi/desktop/vpi/Dump-ill-md.json
Move-Item "D:\Projects\Cadmus\Vpi\CadmusVpi\vpi-tool\bin\Debug\net11.0\vpi-log*.txt" "C:\Users\dfusi\Desktop\vpi\vpi-log-ill.txt"
```

### 🟢 Woodblock

Each row is one item of facet `woodblock`.

(1) **A** (`Object name`)\*: woodblock EID (e.g. `ih00232000_00200777_a2r_(1)`, `ih00234500_02122542_a2r_(1)`) ▶️ `MetadataPart.metadata`: `type="string"`, `name="eid"`, `value`=cell.

(2) **B** (`folio`)\*: folio (e.g. `a2r`) ▶️ `MetadataPart.metadata`: `type="string"`, `name="location"`, `value`=cell.

(3) **C** (`Object measures (h x w)`): size as `HxW`, height by width, integer millimetres between 46 and 310 (e.g. `48x75`) ▶️ `PhysicalMeasurementsPart:wblk.measurements`: two measurements, `name="height"` and `name="width"`, both with `unit="mm"`.

(4) **D** (`Cropped (binding)`): ❌

(5) **E** (`Position within the page - Titlepage`): ❌

(6) **F** (`Position within the page - In text woodcut`): ❌

(7) **G** (`Position within the page - Full page woodcut`): ❌

(8) **H** (`Position within the page - Colophon`): ❌

(9) **I** (`Type of cut - Decorated initial`): ❌

(10) **J** (`Type of cut - Border`): ❌

(11) **K** (`Type of cut - Center`): ❌

(12) **L** (`Type of cut - Bottom Border`): ❌

(13) **M** (`Type of Cut - Top Border`): ❌

(14) **N** (`Type of cut - Right Border`): ❌

(15) **O** (`Type of cut - Left Border`): ❌

(16) **P** (`Text`): inscription text; a cell may hold more than one text, separated by `;` (e.g. `S.ANTONIO;S.PAULO`) ▶️ `MetadataPart.metadata`: `type="string"`, `name="inscription"`, one metadatum per inscription.

(17) **Q** (`Controlled Keywords`): keywords separated by `;` (e.g. `castity;assault;capture;captivity`) ▶️ `IndexKeywordsPart.keywords`: one keyword per value, `language="en"`, no `indexId`.

(18) **R** (`Image tags (Iconclass)`): Iconclass notations with their labels (e.g. `42E23 burial;11Q331 anchorite, hermit;25K16 desert;11P31511 cell ~ monastery`) ▶️ `PinLinksPart.links`: `scope="iconclass"`, one link per value. Split on `;` at bracket depth zero, when followed by a digit, optionally after a single space; each resulting value is an ID and a label separated by the first space at bracket depth zero: ID → `target.gid`, label → `target.label`.

(19) **S** (`Ico-Category`): iconographic category labels separated by `;` (e.g. `Demons;Visions`) ▶️ `CategoriesPart:ico.categories` via 📚 `categories_ico@en`: each label is mapped to one or two entry IDs according to this mapping (labels are matched after trimming, collapsing inner spaces and lowercasing -- both `Persecutions` and `persecutions` may occur).

| label                       | entry ID                                        |
| --------------------------- | ----------------------------------------------- |
| Animals                     | `ico.animal-communion`                          |
| Ascetic life and practices  | `ico.ascetic-life-practices`                    |
| Charitable care and healing | `ico.charitable-care-healing`                   |
| Community and monastic life | `ico.community-monastic-life`                   |
| Death and afterlife         | `ico.death-afterlife`                           |
| Demons                      | `ico.demons`                                    |
| Demons and temptations      | `ico.demons`, `ico.temptations` (two entry IDs) |
| Miraculous intervention     | `ico.miraculous-intervention`                   |
| Other                       | `ico.other`                                     |
| Persecutions                | `ico.persecutions`                              |
| Teaching                    | `ico.teaching`                                  |
| Teaching and preaching      | `ico.teaching`                                  |
| Temptations                 | `ico.temptations`                               |
| Vision                      | `ico.visions`                                   |
| Visions                     | `ico.visions`                                   |

(20) **T** (`Index of medieval art`): IMA subject headings separated by `;` (e.g. `Martyrdom, by Insects`) ▶️ `PinLinksPart.links`: `scope="ima"`, one link per value, `target.gid` and `target.label`=value.

(21) **U** (`Image tags (Warburg)`): ❌

(22) **V** (`Technique`): ❌

(23) **W** (`no. of cut`)\*: progressive number of the woodblock, an integer (1-189 in RGT, 1-158 in SCZ) ▶️ `MetadataPart.metadata`: `type="number"`, `name="cut-number"`, `value`=cell as is (e.g. `5`).

(24) **X** (`Place of printing`): ❌

(25) **Y** (`Printer`): ❌

(26) **Z** (`Year of printing`): ❌

(27) **AA** (`Date of printing`): ❌

(28) **AB** (`Essling no.`): ❌

(29) **AC** (`Sander no. `): ❌

(30) **AD** (`HPB`): ❌

(31) **AE** (`Edit16`): ❌

(32) **AF** (`Person (s): artists: designer (linked to CT)`): ❌

(33) **AG** (`Person (s): artists: cutter (linked to CT)`): ❌

(34) **AH** (`Artistic style`)\*: `Venetian` in every RGT row, `Milanese` in every SCZ row ▶️ `CategoriesPart:wblk.categories` via 📚 `categories_wblk@en`: the cell holds the last segment of the entry ID, capitalized; lowercase it and prefix with `style.` (`style.venetian`, `style.milanese`).

(35) **AI** (`State of conservation`): ❌

(36) **AJ** (`Copy examined`): ❌

(37) **AK** (`Shelfmark`): ❌

(38) **AL** (`Creator of the image and description (scholar) `): ❌

(39) **AM** (`Creator of the image and description (owner institution) `): ❌

(40) **AN** (`Date that the image was made`): ❌

(41) **AO** (`Copyright`): ❌

(42) **AP** (`Known cases of reuse`): ISTC IDs of the editions where the block is attested, separated by `;` (e.g. `ib00644000`) ▶️ `PinLinksPart.links`: `scope="istc"`, one link per ID, `target.gid` and `target.label`=ID.

(43) **AQ** (`Copy specific notes`): ❌

(44) **AR** (`Bibliography`): ❌

(45) **AS** (`Notes`): monogram mark, as `monogram (x)` with a single letter; RGT only ▶️ `MetadataPart.metadata`: if the cell contains `monogram`, add `type="boolean"`, `name="monogram"`, `value="1"`, and `type="string"`, `name="monogram-type"`, `value`=the letter in parentheses. Any cell not containing `monogram` goes to `NotePart.text` instead.

(46) **AT** (`Illustrative object type`)\*: `vignette` or `architectural-frontispiece` ▶️ `CategoriesPart:wblk.categories` via 📚 `categories_wblk@en`: the cell holds the last segment of the entry ID; lowercase it and prefix with `type.` (`type.vignette`, `type.architectural-frontispiece`).

(47) **AU** (`Visual narrative `): note the trailing space in the label. `monoscenic-narrative`, `polyscenic-narrative`, `continuous-narrative` or `non-narrative` ▶️ `CategoriesPart:narr.categories` via 📚 `categories_narr@en`: the cell holds the last segment of the entry ID; lowercase it and prefix with `visual.` (e.g. `visual.non-narrative`).

(48) **AV** (`Text passage VSP`): reference to the passage illustrated by the block: work, book and chapter (e.g. `Vite dei Santi Padri, I,1; paulo primo romito`, `PRATO SPIRITUALE, IV,81`; the `;` here is part of the citation, not a separator); free text; always together with AW ▶️ `TextPassagesPart.passages[0].citation`. This is imported together with AW as separate sections in the same text.

(49) **AW** (`Text passage title`): rubric of that passage (e.g. `Come fu da Dio rivelato ad santo Antonio e come lo trovoe`) ▶️ `TextPassagesPart.passages[0].text`, in the same passage as AV.

(50) **AX** (`Item title`)\*: `RGT_001`…`RGT_189`, `SCZ_001`…`SCZ_158` ▶️ `Item.title`.

(51) **AY** (`Group ID`)\*: EID of the print edition, constant per file: `ih00232000` (RGT), `ih00234500` (SCZ) ▶️ `Item.groupId`.

(52) **AZ** (`Derived from (woodblock eid)`): EID of the RGT woodblock the SCZ block derives from, the kind of derivation being given in BA (e.g. `ih00232000_00200777_a2r_(1)`); SCZ only ▶️ `PinLinksPart.links`: `scope="derivation"`, `target.gid` and `target.label`=cell.

(53) **BA** (`Derivation type`): full entry ID, branch `type.image.*` (`type.image.copy-after`, `type.image.derived-from`, `type.image.related-to`); SCZ only, always together with AZ ▶️ `CategoriesPart:reuse.categories` via 📚 `categories_reuse@en` (entry ID, not label).

(54) **BB** (`Image tags (ThEMA)`): ThEMA IDs separated by `;` (e.g. `TE001580`, or `TE001676; TE002642` for two); RGT only ▶️ `PinLinksPart.links`: `scope="thema"`, one link per ID, `target.gid` and `target.label`=ID.

(55) **BC** (`Designer (person eid)`): EID of the `person` item of the designer, `cnp01415325` (CERL Thesaurus) in every filled row; RGT only ▶️ `PinLinksPart.links`: `scope="designer"`, `target.gid` and `target.label`=cell.

---

### 🟢 Illustration

Each row is one item of facet `illustration`.

(1) **A** (`Object name`)\*: impression EID: `ih00232000_00200777_ill_008` for the first impression of a block, `ih00232000_00200777_ill_008.1`, `ih00232000_00200777_ill_008.2` for its reuses ▶️ `MetadataPart.metadata`: `type="string"`, `name="eid"`, `value`=cell.

(2) **B** (`Item title`)\*: block number and impression ordinal (`RGT_008_01`, `RGT_008_02`) ▶️ `Item.title`.

(3) **C** (`Group ID`)\*: EID of the print instance, `ih00232000_00200777` in every row ▶️ `Item.groupId`.

(4) **D** (`folio`)\*: folio of the impression (e.g. `a6r`) ▶️ `MetadataPart.metadata`: `type="string"`, `name="location"`, `value`=cell.

(5) **E** (`Woodcut block eid`)\*: EID of the woodblock the impression is printed from (one of the 189 RGT woodblock EIDs) ▶️ `PinLinksPart.links`: `scope="woodblock"`, `target.gid` and `target.label`=cell.

(6) **F** (`Impression condition`)\*: full entry ID, branch `imp.*` (`imp.good`, `imp.damaged`) ▶️ `CategoriesPart:ill.categories` via 📚 `categories_ill@en` (entry ID, not label).

(7) **G** (`Text-image relation`): full entry ID, branch `tir.*` (`tir.text-specific`, `tir.typological-adaptation`, `tir.repertorial`, `tir.indeterminate`); empty only for `RGT_189_01` ▶️ `CategoriesPart:ill.categories` via 📚 `categories_ill@en` (entry ID, not label), in the same part as F.

(8) **H** (`Reuse type`): full entry ID, branch `type.block.*` (`type.block.unmodified`, `type.block.modified`) ▶️ `CategoriesPart:reuse.categories` via 📚 `categories_reuse@en` (entry ID, not label).

(9) **I** (`Reuse context`): full entry ID, branch `context.*` (`context.inter-cycle-reuse`, `context.cross-cycle-reuse`); empty in one row where H is filled ▶️ `CategoriesPart:reuse.categories` via 📚 `categories_reuse@en` (entry ID, not label), in the same part as H.

(10) **J** (`Related edition ISTC ID`): ISTC ID of the edition the block comes from (`ib00644000` in every filled row), filled only when I is `context.cross-cycle-reuse` ▶️ `PinLinksPart.links`: `scope="istc"`, `target.gid` and `target.label`=cell.

(11) **K** (`Occurrence note`): free text ▶️ `NotePart.text` unless empty/whitespace only.

### Code Template

Template for a column region parser. Prefer the generic configurable parsers listed above when possible; when a new parser is required, derive it from `ColEntryRegionParser` (in `Cadmus.Import.Excel`), which checks for the current item, gets the filtered cell value, and skips rows marked as skipped. Use `ImportHelper` (in `Cadmus.Import.Excel`) for thesauri, ranks, notes, and item IDs.

- `__TAG__`: the region tag.

```cs
using Cadmus.Import.Excel;
using Cadmus.Import.Proteus;
using Fusi.Tools.Configuration;
using Proteus.Core.Regions;

namespace Cadmus.Vpi.Import;

/// <summary>
/// VPI column __TAG__ entry region parser.
/// <para>Tag: <c>entry-region-parser.vpi.col-__TAG__</c>.</para>
/// </summary>
/// <seealso cref="ColEntryRegionParser" />
/// <seealso cref="IEntryRegionParser" />
[Tag("entry-region-parser.vpi.col-__TAG__")]
public sealed class Col__TAG__EntryRegionParser : ColEntryRegionParser,
    IEntryRegionParser
{
    /// <summary>
    /// Gets the tags of the regions that this parser can handle.
    /// </summary>
    public string[] RegionTags => ["col-__TAG__"];

    /// <summary>
    /// Parses the specified column value.
    /// </summary>
    /// <param name="context">The context, having a current item.</param>
    /// <param name="region">The column region.</param>
    /// <param name="value">The filtered value, or null if empty.</param>
    protected override void ParseValue(CadmusEntrySetContext context,
        EntryRegion region, string? value)
    {
        // use CheckRequired(region, value) for required columns
        if (value == null) return;

        // TODO
    }
}
```
