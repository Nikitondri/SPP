using System.Collections.Concurrent;
using core.exception;
using core.model.node;

namespace core.repository;

public class InMemoryNodesRepository : IRepository<Node>
{
    private ConcurrentDictionary<int, Node> Nodes { get; }

    private int _idGenerator;

    public InMemoryNodesRepository()
    {
        Nodes = new();
        _idGenerator = 0;
    }

    public void Add(Node value)
    {
        lock (Nodes)
        {
            var newId = Interlocked.Increment(ref _idGenerator);
            value.Id = newId;
        }		
        Nodes.TryAdd(value.Id, value);
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