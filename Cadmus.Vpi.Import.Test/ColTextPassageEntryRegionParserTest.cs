using Cadmus.Import.Proteus;
using Cadmus.Ndp.Parts;
using System.Linq;
using System.Threading.Tasks;

namespace Cadmus.Vpi.Import.Test;

public sealed class ColTextPassageEntryRegionParserTest
{
    private const string CITATION = "col-text_passage_vsp";
    private const string TEXT = "col-text_passage_title";

    [Fact]
    public void RegionTags_Default()
    {
        Assert.Equal([CITATION, TEXT],
            new ColTextPassageEntryRegionParser().RegionTags);
    }

    [Fact]
    public async Task Parse_CitationAndText_SinglePassage()
    {
        ColTextPassageEntryRegionParser parser = new();
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(parser, context, CITATION,
            "Vite dei Santi Padri, I,1; paulo primo romito");
        await TestHelper.ParseColumnAsync(parser, context, TEXT,
            "Come  fu da Dio rivelato ad santo Antonio");

        TextPassage passage = context.CurrentItem!.Parts
            .OfType<TextPassagesPart>().Single().Passages.Single();
        Assert.Equal("Vite dei Santi Padri, I,1; paulo primo romito",
            passage.Citation);
        Assert.Equal("Come fu da Dio rivelato ad santo Antonio", passage.Text);
    }

    [Fact]
    public async Task Parse_TextOnly_PassageWithoutCitation()
    {
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(new ColTextPassageEntryRegionParser(),
            context, TEXT, "title");

        TextPassage passage = context.CurrentItem!.Parts
            .OfType<TextPassagesPart>().Single().Passages.Single();
        Assert.Equal("", passage.Citation);
        Assert.Equal("title", passage.Text);
    }

    [Fact]
    public async Task Parse_Empty_NoPart()
    {
        ColTextPassageEntryRegionParser parser = new();
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(parser, context, CITATION, null);
        await TestHelper.ParseColumnAsync(parser, context, TEXT, " ");

        Assert.Empty(context.CurrentItem!.Parts);
    }

    [Fact]
    public async Task Parse_CustomTags_Ok()
    {
        ColTextPassageEntryRegionParser parser = new();
        parser.Configure(new ColTextPassageEntryRegionParserOptions
        {
            CitationTag = "col-a",
            TextTag = "col-b"
        });
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(parser, context, "col-a", "cit");

        Assert.Equal("cit", context.CurrentItem!.Parts
            .OfType<TextPassagesPart>().Single().Passages[0].Citation);
    }
}
