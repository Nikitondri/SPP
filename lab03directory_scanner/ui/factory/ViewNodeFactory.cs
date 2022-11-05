using core.exception;
using core.model.node;
using lab03directory_scanner.view.node;

namespace lab03directory_scanner.factory;

public static class ViewNodeFactory
{
    public static NodeView CreateNode(NodeType nodeType)
    {
        return nodeType switch
        {
            NodeType.File => new FileView(),
            NodeType.Package => new PackageView(),
            _ => throw new UnknownNodeException()
        };
    }
}