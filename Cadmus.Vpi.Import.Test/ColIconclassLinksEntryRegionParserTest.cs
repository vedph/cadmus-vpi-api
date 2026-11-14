using Cadmus.General.Parts;
using Cadmus.Import.Proteus;
using Cadmus.Refs.Bricks;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cadmus.Vpi.Import.Test;

public sealed class ColIconclassLinksEntryRegionParserTest
{
    private const string TAG = "col-image_tags_(iconclass)";

    [Fact]
    public void RegionTags_Default()
    {
        Assert.Equal([TAG], new ColIconclassLinksEntryRegionParser().RegionTags);
    }

    [Theory]
    // README sample
    [InlineData("42E23 burial;11Q331 anchorite, hermit;25K16 desert;" +
        "11P31511 cell ~ monastery",
        "42E23 burial|11Q331 anchorite, hermit|25K16 desert|" +
        "11P31511 cell ~ monastery")]
    // optional single space after the semicolon
    [InlineData("42E23 burial; 11Q331 hermit", "42E23 burial|11Q331 hermit")]
    // semicolon not followed by a digit is part of the label
    [InlineData("31A burial; with bells;11Q hermit",
        "31A burial; with bells|11Q hermit")]
    // semicolon inside brackets is part of the label
    [InlineData("11H(ANTHONY) St. Anthony (holding a book;1 bell);25K desert",
        "11H(ANTHONY) St. Anthony (holding a book;1 bell)|25K desert")]
    // trailing separator
    [InlineData("42E23 burial;", "42E23 burial")]
    [InlineData("42E23", "42E23")]
    public void SplitEntries_Ok(string text, string expected)
    {
        Assert.Equal(expected.Split('|'),
            ColIconclassLinksEntryRegionParser.SplitEntries(text));
    }

    [Theory]
    [InlineData("42E23 burial", "42E23", "burial")]
    [InlineData("11H(GREGORY)12 St. Gregory (with book)", "11H(GREGORY)12",
        "St. Gregory (with book)")]
    [InlineData("11H(ST. ANTHONY) Anthony", "11H(ST. ANTHONY)", "Anthony")]
    [InlineData("42E23", "42E23", "42E23")]
    public void SplitIdLabel_Ok(string entry, string id, string label)
    {
        Assert.Equal((id, label),
            ColIconclassLinksEntryRegionParser.SplitIdLabel(entry));
    }

    [Fact]
    public async Task Parse_AddsLinkPerEntry()
    {
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(new ColIconclassLinksEntryRegionParser(),
            context, TAG, "42E23 burial;11Q331 anchorite, hermit");

        List<AssertedCompositeId> links = context.CurrentItem!.Parts
            .OfType<PinLinksPart>().Single().Links;
        Assert.Equal(2, links.Count);
        Assert.All(links, l => Assert.Equal("iconclass", l.Scope));
        Assert.Equal(["42E23", "11Q331"], links.Select(l => l.Target!.Gid));
        Assert.Equal(["burial", "anchorite, hermit"],
            links.Select(l => l.Target!.Label));
    }

    [Fact]
    public async Task Parse_Empty_NoPart()
    {
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(new ColIconclassLinksEntryRegionParser(),
            context, TAG, " ; ");

        Assert.Empty(context.CurrentItem!.Parts);
    }
}
