# History

## 1.0.1

- 2026-11-24: updated packages.
- 2026-11-23: updated packages (enabled history in editor).

## 1.0.0

- 2026-11-19:
  - updated packages.
  - added thesaurus `pin-link-scopes` and fixed missing groupKey in facet's `it.vedph.ndp.text-passages`.
- 2026-11-14:
  - ⚠️ upgraded to .NET 11 and Cadmus 18; moved to central package management (`Directory.Packages.props`), with `nuget.config` including the local feed.
  - ⚠️ reimplemented the import according to the specs in README: woodblocks and illustrations are imported with the generic parsers of `Cadmus.Import.Excel`, plus the VPI-specific parsers `col-iconclass-links`, `col-monogram` and `col-text-passage`. All the previous parsers were removed. Text passage citation and text were swapped in the previous implementation.
  - ⚠️ replaced `Dump-md.json` and `Import.json` with one dump and one import pipeline for each source file (`w1`, `w2`, `ill`).
  - added `Cadmus.Vpi.Import.Test` with unit tests for all the VPI parsers and the validation of all the pipelines.
  - CLI: aligned to the TES CLI (async indexing, loading of item IDs sets, shared parsers).
  - index: replaced the deprecated `Cadmus.Index.Ef.PgSql` with `Cadmus.Index.PgSql` (tags `item-index-writer.pgsql` and `item-index-reader.pgsql`, same schema).

- 2026-10-03: updated packages.
- 2026-09-28:
  - updated thesaurus `metadata-names`.
  - added index database command to CLI tool.

## 0.0.3

- 2026-09-28: fixed import rule for iconclass.
- 2026-09-27: changed import rule for iconclass (split at `;` + optional whitespace + digit unless `;` is inside `()`).
- 2026-09-25:
  - read ID rather than value for categories column in import.
  - updated thesauri.
  - updated packages.
- 2026-09-22: updated packages and adjusted parser code for async signatures.
- 2026-08-17: updated packages.
- 2026-08-11: added missing thesaurus in tool assets.
- 2026-08-03: fixed missing `@en` in thesaurus import.

## 0.0.2

- 2026-06-21: updated packages.

## 0.0.1

- 2026-06-15: updated packages.
- 2026-05-29:
  - updated packages.
  - added thesaurus alias `physical-size-units_pri`.
