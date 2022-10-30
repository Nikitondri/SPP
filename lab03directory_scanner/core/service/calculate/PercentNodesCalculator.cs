using core.model.node;

namespace core.service.calculate;

public class PercentNodesCalculator : INodesCalculator
{
    public void Calculate(Node node)
    {
        var size = node.Size;
        if (node.Parent != null)
            size = node.Parent.Size;

        node.Percent = (double)node.Size / size;

        foreach (var child in node.Childrens)
            Calculate(child);
    }
}