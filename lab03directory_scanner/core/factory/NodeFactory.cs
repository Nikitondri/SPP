using core.exception;
using core.model.node;

namespace core.factory;

public static class NodeFactory
{
    public static Node CreateNode(NodeType nodeType)
    {
        return nodeType switch
        {
            NodeType.File => new FileNode(),
            NodeType.Package => new Package(),
            _ => throw new UnknownNodeException()
        };
    }
}