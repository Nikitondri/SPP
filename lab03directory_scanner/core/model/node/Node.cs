using System.Collections.Concurrent;

namespace core.model.node;

public abstract class Node
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Route { get; set; }
    
    public long Size { get; set; }
    
    public double Percent { get; set; }
    
    public Node? Parent { get; set; }
    
    public ConcurrentBag<Node> Childrens { get; set; }

    protected Node()
    {
    }

    public abstract NodeType GetType();
}