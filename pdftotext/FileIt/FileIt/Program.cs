using System.Text.Json;
using UglyToad.PdfPig;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var path = @"###";

var pages = new List<PdfPageDto>();

using (var document = PdfDocument.Open(path))
{
    int pageNumber = 1;

    foreach (var page in document.GetPages())
    {
        pages.Add(new PdfPageDto
        {
            PageNumber = pageNumber,
            Text = page.Text
        });

        pageNumber++;
    }
}

var result = new PdfResultDto
{
    Pages = pages
};

var json = JsonSerializer.Serialize(
    result,
    new JsonSerializerOptions
    {
        WriteIndented = true,
        Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    });

Console.WriteLine(json);

// DTOs
public record PdfPageDto
{
    public int PageNumber { get; init; }
    public string Text { get; init; } = "";
}

public record PdfResultDto
{
    public List<PdfPageDto> Pages { get; init; } = new();
}
