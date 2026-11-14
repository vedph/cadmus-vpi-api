using Cadmus.General.Parts;
using Cadmus.Import.Proteus;
using System.Linq;
using System.Threading.Tasks;

namespace Cadmus.Vpi.Import.Test;

public sealed class ColMonogramEntryRegionParserTest
{
    [Theory]
    [InlineData("monogram (b)", "b")]
    [InlineData("Monogram ( i )", "i")]
    [InlineData("monogram(s)", "s")]
    public async Task Parse_Monogram_AddsMetadata(string value, string type)
    {
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(new ColMonogramEntryRegionParser(),
            context, "col-notes", value);

        Metadatum[] metadata = [.. context.CurrentItem!.Parts
            .OfType<MetadataPart>().Single().Metadata];
        Assert.Equal(2, metadata.Length);
        Assert.Equal(("boolean", "monogram", "1"),
            (metadata[0].Type, metadata[0].Name, metadata[0].Value));
        Assert.Equal(("string", "monogram-type", type),
            (metadata[1].Type, metadata[1].Name, metadata[1].Value));
        Assert.DoesNotContain(context.CurrentItem.Parts, p => p is NotePart);
    }

    [Fact]
    public async Task Parse_MonogramWithoutType_AddsFlagOnly()
    {
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(new ColMonogramEntryRegionParser(),
            context, "col-notes", "monogram");

        Metadatum m = context.CurrentItem!.Parts.OfType<MetadataPart>()
            .Single().Metadata.Single();
        Assert.Equal("monogram", m.Name);
    }

    [Fact]
    public async Task Parse_OtherText_AddsNote()
    {
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(new ColMonogramEntryRegionParser(),
            context, "col-notes", " Cropped at the\nbottom ");

        NotePart note = context.CurrentItem!.Parts.OfType<NotePart>().Single();
        Assert.Null(note.RoleId);
        Assert.Equal("Cropped at the\nbottom", note.Text);
        Assert.DoesNotContain(context.CurrentItem.Parts, p => p is MetadataPart);
    }

    [Fact]
    public async Task Parse_NoteRole_AddsNoteWithRole()
    {
        ColMonogramEntryRegionParser parser = new();
        parser.Configure(new ColMonogramEntryRegionParserOptions
        {
            NoteRoleId = "x"
        });
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(parser, context, "col-notes", "note");

        Assert.Equal("x", context.CurrentItem!.Parts.OfType<NotePart>()
            .Single().RoleId);
    }

    [Fact]
    public async Task Parse_Empty_NoPart()
    {
        CadmusEntrySetContext context = TestHelper.CreateContext();

        await TestHelper.ParseColumnAsync(new ColMonogramEntryRegionParser(),
            context, "col-notes", "  ");

        Assert.Empty(context.CurrentItem!.Parts);
    }
}
