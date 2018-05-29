using Infrastructure.Fabrics.Abstractions.Parameters;

namespace Infrastructure.Fabrics.Abstractions
{
    public interface IFabric<T, P> where P : FabricParameters
    {
        T Create(P parameters);
    }
}
