using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace ConsoleApp.service.writer;

public class FileAsyncWriter : IAsyncWriter<string, string>
{
    public Task Write(string dest, string value)
    {
        var path = DefineOutputPath(dest, value);
        return WriteAsync(path, value);
    }

    private string DefineOutputPath(string dest, string value)
    {
        var tree = CSharpSyntaxTree.ParseText(value);
        return Path.Combine(dest, GetFileName(tree) + ".cs");
    }

    private async Task WriteAsync(string path, string text)
    {
        await using var outputFile = new StreamWriter(path);
        await outputFile.WriteAsync(text);
    }

    private string GetFileName(SyntaxTree tree)
    {
        return tree
            .GetRoot()
            .DescendantNodes()
            .OfType<ClassDeclarationSyntax>()
            .First()
            .Identifier
            .Text;
    }
}