using core.model.node;

namespace core.service.calculate.composite.leaf;

public class PercentNodesCalculator : INodesCalculator
{
    public void Calculate(Node node)
    {
        if(node.Size is null) return;
        var size = node.Parent != null ? node.Parent.Size!.Value : node.Size.Value;
        node.Percent = size == 0 ? 0 : (double)node.Size.Value / size;

        if (node.Childrens == null) return;
        foreach (var child in node.Childrens)
            Calculate(child);
    }
}