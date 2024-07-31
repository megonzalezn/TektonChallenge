using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tekton.Model;
using Tekton.Services.Interface;
using Tekton.Services.Interfaces;

namespace TektonChallenge.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class ProductController : Controller
    {
        private readonly IProductService service;
        private readonly ILogService log;
        public ProductController(IProductService service, ILogService log) {
            this.service = service;
            this.log = log;
        }

        [HttpGet]
        public IActionResult Get()
        {
            Stopwatch sw = Stopwatch.StartNew();
            List<ProductResponseDTO> products = service.GetAll();
            sw.Stop();
            log.Write($"{DateTime.Now} - {nameof(Get)}: {sw.ElapsedMilliseconds} ms");
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id) 
        {
            Stopwatch sw = Stopwatch.StartNew();
            ProductResponseDTO product = await service.GetById(id);
            sw.Stop();
            log.Write($"{DateTime.Now} - {nameof(GetById)} {id}: {sw.ElapsedMilliseconds} ms");
            return Ok(product);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductRequestDTO product)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (!ModelState.IsValid)
            {
                return BadRequest("some required fields are empty or invalid");
            }
            ProductResponseDTO response = await service.Create(product);
            sw.Stop();

            log.Write($"{DateTime.Now} - {nameof(Post)} {response.Id}: {sw.ElapsedMilliseconds} ms");
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] ProductRequestDTO product)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (!ModelState.IsValid)
            {
                return BadRequest("some required fields are empty or invalid");
            }

            if (id != product.Id)
            {
                return BadRequest("id in path doesn't correspond to id in object");
            }

            ProductResponseDTO response = await service.Update(product);
            sw.Stop();
            log.Write($"{DateTime.Now} - {nameof(Put)} {response.Id}: {sw.ElapsedMilliseconds} ms");
            return Ok(response);
        }

    }
}
