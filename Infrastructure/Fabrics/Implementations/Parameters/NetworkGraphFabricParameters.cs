using Infrastructure.Fabrics.Abstractions.Parameters;

namespace Infrastructure.Fabrics.Implementations.Parameters
{
    public class NetworkGraphFabricParameters: FabricParameters
    {
        public int MaxEdgeCapacity { get; set; }
        public int NodesAmount { get; set; }
        public int EdgesAmount { get; set; }
    }
}
