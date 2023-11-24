using System.Collections.Generic;

public static class ProductTypeToPoolMapper
{
    private static readonly Dictionary<ProductTypes, PoolEnum> TypeToPoolMap = new Dictionary<ProductTypes, PoolEnum>
    {
        // { ProductTypes.Sock, PoolEnum.Sock },
        // { ProductTypes.Bra, PoolEnum.Bra },
        // { ProductTypes.Boxer, PoolEnum.Boxer },
        // { ProductTypes.Pant, PoolEnum.Pant }
    };

    // public static PoolEnum MapProductTypeToPool(ProductTypes productType)
    // {
    //     // return TypeToPoolMap.TryGetValue(productType, out PoolEnum mappedPool) ? mappedPool : PoolEnum.Sock;
    // }
}