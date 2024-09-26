using Microsoft.AspNetCore.Mvc;
using WarehouseApi.Model;
using WarehouseApi.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WarehouseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierServices _supplierServices;

        public SupplierController(ISupplierServices supplierServices)
        {
            _supplierServices = supplierServices;
        }



        
        [HttpGet]
        public ActionResult <List<ProductSet>> GetSupplierProducts()
        {
            List<ProductSet> supplierProducts = _supplierServices.GetSupplierProducts();
            if (supplierProducts.Count == 0)
            {
                return NotFound();
            }
            return Ok(supplierProducts) ;
        }

        
        [HttpGet("{id}")]
        public ActionResult <ProductSet> GetSupplierProduct(int id)
        {
            ProductSet? supplierProduct = _supplierServices.GetSupplierProducts().FirstOrDefault(w => w.Product.Id == id);
            if (supplierProduct != null)
            {
                return Ok(supplierProduct);
            }
            return NotFound($"There isn't any product with this id: {id}!");
        }

        
        [HttpPost]
        public ActionResult AddSupplierProduct(Product product, int quantity)
        {
            if (!_supplierServices.AddProductToSupplier(product, quantity))
            {
                return BadRequest("There must have been an error");
            }
            return Ok("Product added to the supplier.");
        }

        
        

        
        [HttpDelete("{id}")]
        public ActionResult RemoveProductFromSupplier(int id, int quantity)
        {
            if (!_supplierServices.RemoveProductFromSupplier(id, quantity))
            {
                return NotFound();
            }
            return Ok("The product has been removed");
        }
    }
}
