namespace core.model.node;

public class Package : Node
{
    public override NodeType GetType()
    {
        return NodeType.Package;
    }
}