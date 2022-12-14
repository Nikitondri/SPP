using System.Collections.Concurrent;

namespace core.model.node;

public abstract class Node
{
    private string? _route;
    
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Route { get => _route ??= GetRoute(); }
    
    public long? Size { get; set; }
    
    public double Percent { get; set; }
    
    public Node? Parent { get; set; }

    public ConcurrentBag<Node>? Childrens { get; set; }
    
    
    private string GetRoute()
    {
        return Parent is null ? Name : Parent.Route + "/" + Name;
    }

    public abstract NodeType GetType();
}