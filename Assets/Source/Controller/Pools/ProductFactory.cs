using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

public class ProductFactory : Singleton<ProductFactory>
{
    [SerializeField] private ProductModelContainerSO modelContainer;

    public T SpawnObject<T>(ProductTypes type)
    {
        Product product = PoolFactory.Instance.GetDeactiveItem<Product>(PoolEnum.Product);
        ProductModel visualModel = Instantiate(GetPrefabByType(type));
        product.OnInitialize(visualModel);
        product.SetActiveGameObject(true);
        return (T)((object)product);
    }

    private ProductModel GetPrefabByType(ProductTypes type)
    {
        return modelContainer.products.FirstOrDefault(product => product.type == type)?.prefab;
    }
}