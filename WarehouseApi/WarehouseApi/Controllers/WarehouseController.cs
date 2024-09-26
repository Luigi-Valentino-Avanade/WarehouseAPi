using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Model;
using WarehouseApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        
        
        private readonly ITransferService _transferService;
        private readonly IWarehouseService _warehouseService;
        

        public WarehouseController(ITransferService transferService, IWarehouseService warehouseService)
        {
            _transferService = transferService;
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public ActionResult <List<ProductSet>> GetWarehouseProducts()
        {
            List<ProductSet> warehouseProducts = _warehouseService.GetWarehouseProduct();
            if (warehouseProducts.Count == 0)
            {
                return NotFound();
            }
            return Ok(warehouseProducts);
        }

        
        [HttpGet("{id}")]
        public ActionResult <ProductSet> GetWarehouseProduct(int id)
        {
            ProductSet? warehouseProduct = _warehouseService.GetWarehouseProduct().FirstOrDefault(p => p.Product.Id == id);
            if (warehouseProduct == null)
            {
                return NotFound($"There isn't any product with this id: {id}!");
            }
            return Ok(warehouseProduct);
        }

        
        [HttpPost]
        public ActionResult AddProductFromSupplierToWarehouse([FromBody] Product product, int quantity)
        {
            if (_transferService.TransferProduct(product.Id, quantity))
            {
                return Ok();
            }
            return BadRequest();
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
