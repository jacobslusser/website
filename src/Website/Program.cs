using Markdig;
using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using Markdig.Syntax;
using System.Text;
using System.Text.RegularExpressions;

namespace Website;

public class Program
{
    private const string _htmlStart = """
        <!doctype html>
        <html lang="en">
        <head>
            <meta charset="utf-8">
            <meta name="viewport" content="width=device-width, initial-scale=1">
            <meta name="description" content="The website of Jacob Slusser.">
            <title>jacobsluss.me</title>
        </head>
        <body>
        """;

    private const string _htmlEnd = """
        </body>
        """;

    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        var pipeline = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Build();

        app.MapGet("/", async (IHostEnvironment env) =>
        {
            // Write start of HTML
            using var writer = new StringWriter();
            writer.WriteLine(_htmlStart);
            writer.Flush();

            var renderer = new HtmlRenderer(writer);
            pipeline.Setup(renderer);

            // Get Markdown files
            var files = env.ContentRootFileProvider.GetDirectoryContents("/Entries")
                .Where(f => f.Exists && f.Name.EndsWith(".md", StringComparison.OrdinalIgnoreCase))
                .Reverse();

            // Write each Markdown file as HTML
            foreach (var f in files)
            {
                using var stream = f.CreateReadStream();
                using var reader = new StreamReader(stream);

                var markdown = await reader.ReadToEndAsync();
                MarkdownDocument document = Markdown.Parse(markdown, pipeline);

                var yamlBlock = document.Descendants<YamlFrontMatterBlock>().First();
                var yaml = markdown.Substring(yamlBlock.Span.Start, yamlBlock.Span.Length);

                // Write entry header
                // TODO Properly parse YAML
                var title = Regex.Match(yaml, @"title:\s+(.*)").Groups[1].Value.Trim();
                var date = DateTimeOffset.Parse(Regex.Match(yaml, @"publication_date:\s+(.*)").Groups[1].Value.Trim());
                writer.WriteLine($"<h1>{title} - {date.LocalDateTime.ToShortDateString()}</h1>");
                writer.Flush();

                // Write entry HTML
                renderer.Render(document);
                writer.Flush();
            }

            // Write the end of HTML
            writer.WriteLine(_htmlEnd);

            return Results.Content(writer.ToString(), "text/html", Encoding.UTF8);
        });

        app.Run();
    }
}
