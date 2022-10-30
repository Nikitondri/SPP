using System.Collections.Concurrent;
using core.exception;
using core.model.node;

namespace core.repository;

public class InMemoryNodesRepository : IRepository<Node>
{
    // public Node Root { get; set; }
    
    public ConcurrentDictionary<int, Node> Nodes { get; set; }

    private int _idGenerator;

    public InMemoryNodesRepository()
    {
        Nodes = new();
        _idGenerator = 0;
    }

    public void Add(Node value)
    {
        Nodes.TryAdd(Interlocked.Increment(ref _idGenerator), value);
    }

    public Node FindById(int id)
    {
        return Nodes.TryGetValue(id, out var result) ? result : throw new NotFoundException();
    }

    public int GetSize()
    {
        return Nodes.Count;
    }
}