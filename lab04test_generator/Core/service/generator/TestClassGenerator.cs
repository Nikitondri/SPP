using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.service.generator;

public static class TestClassGenerator
{
    private static readonly List<UsingDirectiveSyntax> DefaultLoadDirectiveList = new()
    {
        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System")),
        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Collections.Generic")),
        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Linq")),
        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("System.Text")),
        SyntaxFactory.UsingDirective(SyntaxFactory.ParseName("NUnit.Framework"))
    };

    private const string Tab = "\t";

    public static string CreateTest(TypeDeclarationSyntax classDeclaration)
    {
        var unit = SyntaxFactory.CompilationUnit();
        unit = DefaultLoadDirectiveList.Aggregate(unit,
            (current, loadDirective) => current.AddUsings(loadDirective));

        var @namespace = SyntaxFactory
            .NamespaceDeclaration(SyntaxFactory
                .QualifiedName(SyntaxFactory
                    .IdentifierName("GeneratedNamespace"), SyntaxFactory
                    .IdentifierName(SyntaxFactory
                        .Identifier(SyntaxFactory
                                .TriviaList(),
                            "Tests", SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .CarriageReturnLineFeed)))))
            .WithNamespaceKeyword(SyntaxFactory
                .Token(SyntaxFactory
                        .TriviaList(SyntaxFactory
                            .CarriageReturnLineFeed, SyntaxFactory
                            .CarriageReturnLineFeed),
                    SyntaxKind.NamespaceKeyword, SyntaxFactory
                        .TriviaList(SyntaxFactory
                            .Space)))
            .WithOpenBraceToken(SyntaxFactory
                .Token(SyntaxFactory
                        .TriviaList(),
                    SyntaxKind.OpenBraceToken, SyntaxFactory
                        .TriviaList(SyntaxFactory
                            .CarriageReturnLineFeed)))
            .WithCloseBraceToken(SyntaxFactory
                .Token(SyntaxFactory
                        .TriviaList(SyntaxFactory
                            .CarriageReturnLineFeed),
                    SyntaxKind.CloseBraceToken, SyntaxFactory
                        .TriviaList()))
            .AddMembers(SyntaxFactory
                .ClassDeclaration(SyntaxFactory
                    .Identifier(SyntaxFactory
                            .TriviaList(),
                        classDeclaration.Identifier.Text + "Test", SyntaxFactory
                            .TriviaList(SyntaxFactory
                                .CarriageReturnLineFeed)))
                .WithAttributeLists(SyntaxFactory
                    .SingletonList(SyntaxFactory
                        .AttributeList(SyntaxFactory
                            .SingletonSeparatedList(SyntaxFactory
                                .Attribute(SyntaxFactory
                                    .IdentifierName("TestFixture"))))
                        .WithOpenBracketToken(SyntaxFactory
                            .Token(SyntaxFactory
                                    .TriviaList(SyntaxFactory
                                        .CarriageReturnLineFeed, SyntaxFactory
                                        .Whitespace(Tab)),
                                SyntaxKind.OpenBracketToken, SyntaxFactory
                                    .TriviaList()))
                        .WithCloseBracketToken(SyntaxFactory
                            .Token(SyntaxFactory
                                    .TriviaList(),
                                SyntaxKind.CloseBracketToken, SyntaxFactory
                                    .TriviaList(SyntaxFactory
                                        .CarriageReturnLineFeed)))))
                .WithModifiers(SyntaxFactory
                    .TokenList(SyntaxFactory
                        .Token(SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .Whitespace(Tab)),
                            SyntaxKind.PublicKeyword, SyntaxFactory
                                .TriviaList(SyntaxFactory.Space))))
                .WithKeyword(SyntaxFactory
                    .Token(SyntaxFactory
                            .TriviaList(),
                        SyntaxKind.ClassKeyword, SyntaxFactory
                            .TriviaList(SyntaxFactory
                                .Space)))
                .WithOpenBraceToken(SyntaxFactory
                    .Token(SyntaxFactory
                            .TriviaList(SyntaxFactory
                                .Whitespace(Tab)),
                        SyntaxKind.OpenBraceToken, SyntaxFactory
                            .TriviaList(SyntaxFactory
                                .CarriageReturnLineFeed)))
                .WithCloseBraceToken(SyntaxFactory
                    .Token(SyntaxFactory
                            .TriviaList(SyntaxFactory
                                .Whitespace(Tab)),
                        SyntaxKind.CloseBraceToken, SyntaxFactory
                            .TriviaList()))
                .AddMembers(AddTestMethods(classDeclaration)));

        return unit.NormalizeWhitespace().AddMembers(@namespace).ToFullString();
    }

    private static MemberDeclarationSyntax[] AddTestMethods(SyntaxNode classDeclaration)
    {
        var methods = classDeclaration.DescendantNodes().OfType<MethodDeclarationSyntax>()
            .Where(method => method.Modifiers.Any(SyntaxKind.PublicKeyword));

        return methods.Select(method => SyntaxFactory
                .MethodDeclaration(SyntaxFactory
                    .PredefinedType(SyntaxFactory
                        .Token(SyntaxFactory
                                .TriviaList(),
                            SyntaxKind.VoidKeyword, SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .Space))), SyntaxFactory
                    .Identifier(method.Identifier.Text + "Test"))
                .WithAttributeLists(SyntaxFactory
                    .SingletonList(SyntaxFactory
                        .AttributeList(SyntaxFactory
                            .SingletonSeparatedList(SyntaxFactory
                                .Attribute(SyntaxFactory
                                    .IdentifierName("Test"))))
                        .WithOpenBracketToken(SyntaxFactory
                            .Token(SyntaxFactory
                                    .TriviaList(SyntaxFactory
                                        .Whitespace(Tab + Tab)),
                                SyntaxKind.OpenBracketToken, SyntaxFactory
                                    .TriviaList()))
                        .WithCloseBracketToken(SyntaxFactory
                            .Token(SyntaxFactory
                                    .TriviaList(),
                                SyntaxKind.CloseBracketToken, SyntaxFactory
                                    .TriviaList(SyntaxFactory
                                        .LineFeed)))))
                .WithModifiers(SyntaxFactory
                    .TokenList(SyntaxFactory
                        .Token(SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .Whitespace(Tab + Tab)),
                            SyntaxKind.PublicKeyword, SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .Space))))
                .WithParameterList(SyntaxFactory
                    .ParameterList()
                    .WithCloseParenToken(SyntaxFactory
                        .Token(SyntaxFactory
                                .TriviaList(),
                            SyntaxKind.CloseParenToken, SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .LineFeed))))
                .WithBody(SyntaxFactory
                    .Block()
                    .WithOpenBraceToken(SyntaxFactory
                        .Token(SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .Whitespace(Tab + Tab)),
                            SyntaxKind.OpenBraceToken, SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .LineFeed)))
                    .WithCloseBraceToken(SyntaxFactory
                        .Token(SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .Whitespace(Tab + Tab)),
                            SyntaxKind.CloseBraceToken, SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .LineFeed, SyntaxFactory
                                    .LineFeed))))
                .AddBodyStatements(SyntaxFactory
                    .ExpressionStatement(SyntaxFactory
                        .InvocationExpression(SyntaxFactory
                            .MemberAccessExpression(
                                SyntaxKind.SimpleMemberAccessExpression, SyntaxFactory
                                    .IdentifierName(SyntaxFactory
                                        .Identifier(SyntaxFactory
                                                .TriviaList(SyntaxFactory
                                                    .Whitespace(Tab + Tab + Tab)),
                                            "Assert", SyntaxFactory
                                                .TriviaList())), SyntaxFactory
                                    .IdentifierName("Fail")))
                        .WithArgumentList(SyntaxFactory
                            .ArgumentList(SyntaxFactory
                                .SingletonSeparatedList(SyntaxFactory
                                    .Argument(SyntaxFactory
                                        .LiteralExpression(
                                            SyntaxKind.StringLiteralExpression, SyntaxFactory
                                                .Literal("auto generated test")))))))
                    .WithSemicolonToken(SyntaxFactory
                        .Token(SyntaxFactory
                                .TriviaList(),
                            SyntaxKind.SemicolonToken, SyntaxFactory
                                .TriviaList(SyntaxFactory
                                    .LineFeed)))))
            .Cast<MemberDeclarationSyntax>().ToArray();
    }
}