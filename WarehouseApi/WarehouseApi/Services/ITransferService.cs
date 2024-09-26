namespace WarehouseApi.Services
{
    public interface ITransferService
    {
        bool TransferProduct(int productId, int quantity);
    }
}