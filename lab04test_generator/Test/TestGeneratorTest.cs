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
        
        var result = GenerateCode(code);
        
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

        var result = GenerateCode(code);
        
        Assert.False(result.Contains("Assert.Fail(\"auto generated test\");"));
        Assert.False(result.Contains("public void"));
        Assert.True(result.Contains("[TestFixture]"));
        Assert.True(result.Contains("namespace GeneratedNamespace.Tests"));
        Assert.True(result.Contains("public class"));
    }

    [Test]
    public void CountMethodsTest()
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
                }
            }
        ";

        var result = GenerateCode(code);

        var generatedClass = CSharpSyntaxTree.ParseText(result).GetCompilationUnitRoot();
        var generatedMethods = generatedClass.DescendantNodes().OfType<MethodDeclarationSyntax>().ToList();
        Assert.That(generatedMethods, Has.Count.EqualTo(2));
    }

    [Test]
    public void OverloadedMethodTest()
    {
        var code = @"
           namespace ConsoleApp.resources.input
           {
                public class Test3
                {
                    public void Method()
                    {
                    }
                    
                    public void Method()
                    {
                    }
                    
                    public void MethodZ()
                    {
                    }

                    public void MethodZ()
                    {
                    }
                }
            }
        ";

        var result = GenerateCode(code);
        
        Assert.True(result.Contains("public void MethodTest()"));
        Assert.True(result.Contains("public void Method1Test()"));
        Assert.True(result.Contains("public void MethodZTest()"));
        Assert.True(result.Contains("public void MethodZ1Test()"));
    } 

    private string GenerateCode(string code)
    {
        var classes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
            .Where(@class => @class.Modifiers.Any(SyntaxKind.PublicKeyword))
            .Where(@class => !@class.Modifiers.Any(SyntaxKind.StaticKeyword)).ToArray();

        return TestClassGenerator.CreateTest(classes[0]);
    }
}