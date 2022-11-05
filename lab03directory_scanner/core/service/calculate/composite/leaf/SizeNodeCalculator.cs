using core.model.node;

namespace core.service.calculate.composite.leaf;

public class SizeNodeCalculator : INodesCalculator
{
    public void Calculate(Node node)
    {
        node.Size ??= GetChildFileSize(node);
    }
    
    private long GetChildFileSize(Node node)
    {
        long size = 0;
        if (node.Childrens == null) return size;
        foreach (var child in node.Childrens)
        {
            Calculate(child);
            size += child.Size!.Value;
        }
        return size;
    }
}