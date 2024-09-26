using WarehouseApi.Model;

namespace WarehouseApi.Services
{
    public class WarehouseService : IWarehouseService
    {
        private List<ProductSet> _warehouseProducts = new List<ProductSet>();
        private const double _maxCapacity = 1000;

        public List<ProductSet> GetWarehouseProduct()
        {
            return _warehouseProducts;
        }

        public double GetAvailableSpace()
        {
            double occupiedSpace = _warehouseProducts.Sum(s => s.Quantity * s.Product.Size);
            return _maxCapacity - occupiedSpace;
        }

        public bool AddProductToWarehouse(Product product, int quantity)
        {
            double newProductSize = product.Size * quantity;
            double occupiedSpace = _warehouseProducts.Sum(s => s.Quantity * s.Product.Size);
            if (occupiedSpace + newProductSize <= _maxCapacity)
            {
                ProductSet? existingProductSet = _warehouseProducts.FirstOrDefault(p => p.Product.Id == product.Id);
                if (existingProductSet != null)
                {
                    existingProductSet.Quantity += quantity;
                }
                else
                {
                    _warehouseProducts.Add(new ProductSet { Product = product, Quantity = quantity });
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RemoveProductFromWarehouse(int productId, int quantity)
        {
            ProductSet? existingProductSet = _warehouseProducts.FirstOrDefault(p => p.Product.Id == productId);
            if (existingProductSet == null || existingProductSet.Quantity < quantity)
            {
                return false;
            }
            existingProductSet.Quantity -= quantity;
            if (existingProductSet.Quantity == 0)
            {
                _warehouseProducts.Remove(existingProductSet);
            }
            return true;
        }
    }
}
