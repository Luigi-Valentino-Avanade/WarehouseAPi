using WarehouseApi.Model;

namespace WarehouseApi.Services
{
    public class TransferService : ITransferService
    {
        private readonly ISupplierServices _supplierServices;
        private readonly IWarehouseService _warehouseService;

        public TransferService(ISupplierServices supplierServices, IWarehouseService warehouseService)
        {
            _supplierServices = supplierServices;
            _warehouseService = warehouseService;
        }

        public bool TransferProduct(int productId, int quantity)
        {
            List<ProductSet> supplierProducts = _supplierServices.GetSupplierProducts();
            ProductSet productSet = supplierProducts.FirstOrDefault(p => p.Product.Id == productId);
            if (productSet == null || productSet.Quantity < quantity)
            {
                return false;
            }


            if (!_supplierServices.RemoveProductFromSupplier(productId, quantity))
            {
                return false;
            }

            if (!_warehouseService.AddProductToWarehouse(productSet.Product, quantity))
            {
                _supplierServices.AddProductToSupplier(productSet.Product, quantity);
                return false;
            }
            return true;
        }
    }
}
