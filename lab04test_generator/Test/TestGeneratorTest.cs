using Core.service.generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Test;

public class TestGeneratorTest
{

    [Test]
    public void GenerateTestsForClassWithMethods()
    {
        var code = @"
           namespace ConsoleApp.resources.input
           {
                public class Test3
                {
                    public void Method1()
                    {
                    }
                    
                    private void Method2()
                    {
                    }
                    
                    public void Method3()
                    {
                    }
                    
                    public void Method4()
                    {
                    }
                }
            }
        ";
        
        var classes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
            .Where(@class => @class.Modifiers.Any(SyntaxKind.PublicKeyword))
            .Where(@class => !@class.Modifiers.Any(SyntaxKind.StaticKeyword)).ToArray();

        var result = TestClassGenerator.CreateTest(classes[0]);
        
        Assert.True(result.Contains("Assert.Fail(\"auto generated test\");"));
        Assert.True(result.Contains("public void Method1Test()"));
        Assert.True(result.Contains("[TestFixture]"));
        Assert.True(result.Contains("namespace GeneratedNamespace.Tests"));
        Assert.True(result.Contains("public class"));
    }

    [Test]
    public void GenerateTestsForClassWithoutMethods()
    {
        var code = @"
           namespace ConsoleApp.resources.input
           {
                public class Test3
                {
                }
            }
        ";

        var classes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
            .Where(@class => @class.Modifiers.Any(SyntaxKind.PublicKeyword))
            .Where(@class => !@class.Modifiers.Any(SyntaxKind.StaticKeyword)).ToArray();

        var result = TestClassGenerator.CreateTest(classes[0]);
        
        Assert.False(result.Contains("Assert.Fail(\"auto generated test\");"));
        Assert.False(result.Contains("public void"));
        Assert.True(result.Contains("[TestFixture]"));
        Assert.True(result.Contains("namespace GeneratedNamespace.Tests"));
        Assert.True(result.Contains("public class"));
    }
}