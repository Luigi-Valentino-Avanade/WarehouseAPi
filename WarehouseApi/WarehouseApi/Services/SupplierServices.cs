using WarehouseApi.Model;

namespace WarehouseApi.Services
{
    public class SupplierServices : ISupplierServices
    {
        private List<ProductSet> _supplierProducts = new List<ProductSet>();



        public List<ProductSet> GetSupplierProducts()
        {
            return _supplierProducts;
        }

        public bool AddProductToSupplier(Product product, int quantity)
        {
            ProductSet? existingProductSet = _supplierProducts.FirstOrDefault(p => p.Product.Id == product.Id);
            if (existingProductSet != null)
            {
                existingProductSet.Quantity += quantity;
            }
            else
            {
                _supplierProducts.Add(new ProductSet { Product = product, Quantity = quantity });
            }
            return true;
        }

        public bool RemoveProductFromSupplier(int productId, int quantity)
        {
            ProductSet? existingProductSet = _supplierProducts.FirstOrDefault(p => p.Product.Id == productId);
            if (existingProductSet == null || existingProductSet.Quantity < quantity)
            {
                return false;
            }
            existingProductSet.Quantity -= quantity;
            if (existingProductSet.Quantity == 0)
            {
                _supplierProducts.Remove(existingProductSet);
            }
            return true;
        }


    }
}
