using Infrastructure.MathStructures;
using MaxFlow.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MaxFlow.Implementations
{
    public class SequantionalEdmondKarpMaxFlow : IMaxFlowFinder
    {
        public int GetMaxFlow(NetworkGraph graph)
        {
            var answer = 0;
            List<NetworkEdge> path;
            while ((path = graph.GetAugmentedPathBFS()) != null)
            {
                var currentFlow = path.Min(x => x.Capacity);
                answer += currentFlow;
                for (var i = 0; i < path.Count; ++i)
                {
                    path[i].Flow += currentFlow;
                }
            }
            return answer;
        }
    }
}
