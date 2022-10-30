using core.model.node;

namespace core.service;

public interface IScanner
{
    bool IsFinished();
    void Start();
    void Stop();
    Node GetResult();
}