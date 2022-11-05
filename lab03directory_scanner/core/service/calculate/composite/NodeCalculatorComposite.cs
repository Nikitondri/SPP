using core.model.node;
using core.service.calculate.composite.leaf;

namespace core.service.calculate.composite;

public class NodeCalculatorComposite : INodesCalculator
{
    private readonly List<INodesCalculator> _calculators;

    public NodeCalculatorComposite()
    {
        _calculators = new List<INodesCalculator>
        {
            new SizeNodeCalculator(), 
            new PercentNodesCalculator()
        };
    }

    public void Calculate(Node node)
    {
        foreach (var nodesCalculator in _calculators)
        {
            nodesCalculator.Calculate(node);
        }
    }
}