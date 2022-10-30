using core.model.node;

namespace core.service.calculate;

public class SizeNodeCalculator : INodesCalculator
{
    public void Calculate(Node node)
    {
        // if (!node.Size)
        node.Size = GetChildFileSize(node);
    }
    
    private long GetChildFileSize(Node node)
    {
        long size = 0;
        foreach (var child in node.Childrens)
        {
            Calculate(child);
            size += child.Size;
        }
        return size;
    }
}