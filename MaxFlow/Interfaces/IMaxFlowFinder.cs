using Infrastructure.MathStructures;

namespace MaxFlow.Interfaces
{
    interface IMaxFlowFinder
    {
        int GetMaxFlow(NetworkGraph graph);
    }
}
