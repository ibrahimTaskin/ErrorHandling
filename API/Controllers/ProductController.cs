using API.Models;
using API.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)] // Geri dönüşte beklediğimiz tip
        public async Task<IActionResult> GetProducts() // HandleResultta ActionResult verdiğimiz için herhangi bir değer vermek zorunda değiliz.
        {
            return HandleResult(await _productRepository.GetProducts()); // Geri dönüş zaten Repository'de Result tipinde
        }


        [HttpGet("{id:length(24)}", Name = "GetProduct")] // Custom Name, id must 24 length
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)] // Geri dönüşte beklediğimiz tip
        public async Task<IActionResult> GetProductById(string id)
        {
            return HandleResult(await _productRepository.GetProduct(id)); // HandleResult ile geri dönüşleri kontrol edebiliyoruz.
        }


        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)] // Geri dönüşte beklediğimiz tip
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _productRepository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }


        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)] // Geri dönüşte beklediğimiz tip
        public async Task<ActionResult<Product>> UpdateProduct([FromBody] Product product)
        {
            return Ok(await _productRepository.Update(product));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)] // Geri dönüşte beklediğimiz tip
        public async Task<ActionResult<Product>> DeleteProduct(string id)
        {
            return Ok(await _productRepository.Delete(id));
        }
    }
}
