using System.Threading.Tasks.Dataflow;
using Core.service.generator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.service.pipeline.item;

public class PropagatorPipelineItem : IPropagatorPipelineItem<string, string>
{
    private readonly int _maxGenerateThreads;

    public PropagatorPipelineItem(int maxGenerateThreads)
    {
        _maxGenerateThreads = maxGenerateThreads;
    }

    public IPropagatorBlock<string, string> InitAndGetItem()
    {
        var opt = new ExecutionDataflowBlockOptions { MaxDegreeOfParallelism = _maxGenerateThreads };
        return new TransformManyBlock<string, string>(CreateTests, opt);
    }

    private string[] CreateTests(string code)
    {
        var classes = CSharpSyntaxTree.ParseText(code).GetRoot().DescendantNodes().OfType<ClassDeclarationSyntax>()
            .Where(@class => @class.Modifiers.Any(SyntaxKind.PublicKeyword))
            .Where(@class => !@class.Modifiers.Any(SyntaxKind.StaticKeyword)).ToArray();

        return classes.Select(TestClassGenerator.CreateTest).ToArray();
    }
}