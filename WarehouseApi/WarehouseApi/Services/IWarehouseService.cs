using WarehouseApi.Model;

namespace WarehouseApi.Services
{
    public interface IWarehouseService
    {
        bool AddProductToWarehouse(Product product, int quantity);
        double GetAvailableSpace();
        List<ProductSet> GetWarehouseProduct();
        bool RemoveProductFromWarehouse(int productId, int quantity);
    }
}