using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDb.BLL;
using MongoDb.Common;

namespace CrudMongoDb.Controllers
{
    
    public class ProductsController : GenericController<Product>
    {
        public ProductsController(GenericService<Product> service) : base(service)
        {
        }
    }
}
