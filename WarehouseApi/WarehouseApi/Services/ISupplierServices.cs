using WarehouseApi.Model;

namespace WarehouseApi.Services
{
    public interface ISupplierServices
    {
        bool AddProductToSupplier(Product product, int quantity);
        List<ProductSet> GetSupplierProducts();
        bool RemoveProductFromSupplier(int productId, int quantity);
    }
}