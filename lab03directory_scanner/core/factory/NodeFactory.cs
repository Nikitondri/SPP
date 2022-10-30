using core.model;
using core.model.node;

namespace core.factory;

public class NodeFactory
{
    public static Node CreateNode(NodeType nodeType)
    {
        return nodeType switch
        {
            NodeType.File => new FileNode(),
            NodeType.Package => new Package(),
            _ => throw new Exception("Unknown node type")
        };
    }
}