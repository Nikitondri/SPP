using core.model.node;

namespace core.service.calculate.composite.leaf;

public class PercentNodesCalculator : INodesCalculator
{
    public void Calculate(Node node)
    {
        var size = node.Size!.Value;
        if (node.Parent != null)
            size = node.Parent.Size!.Value;

        node.Percent = (double)node.Size.Value / size;

        if (node.Childrens == null) return;
        foreach (var child in node.Childrens)
            Calculate(child);
    }
}