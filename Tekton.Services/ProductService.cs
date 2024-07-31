using LazyCache;
using Newtonsoft.Json;
using Tekton.Entity;
using Tekton.Model;
using Tekton.Repository.Interfaces;
using Tekton.Services.Interfaces;


namespace Tekton.Services
{
    public class ProductService: IProductService
    {
        
        private IProductRepository repository;
        private IAppCache cache;
        private Dictionary<int, string> statusList;
        public ProductService(IProductRepository repository, IAppCache cache)
        {
            this.repository = repository;
            this.cache = cache;
            this.statusList = cache.GetOrAdd("status", () =>
            {
                return new Dictionary<int, string>() {
                    {0, "Inactivo"},
                    {1, "Activo"}
                };
            }, TimeSpan.FromMinutes(5));

        }

        public List<ProductResponseDTO> GetAll()
        {
            return repository.GetAll().Select(s =>populate(s).Result).ToList();
        }

        public async Task<ProductResponseDTO> GetById(int id)
        {
            return await populate(repository.GetById(id)) ;
        }

        public async Task<ProductResponseDTO> Create(ProductRequestDTO product)
        {
            Product newProduct = cast(product);
            newProduct.Creation = DateTime.Now;
            newProduct.LastUpdated = DateTime.Now;
            return await populate(repository.Create(newProduct));
        }

        public async Task<ProductResponseDTO> Update(ProductRequestDTO product)
        {
            Product updatedProduct = cast(product);
            updatedProduct.LastUpdated = DateTime.Now;
            return await populate(repository.Update(updatedProduct));
        }
                
        private async Task<ProductResponseDTO> populate(Product product) 
        {
            return new ProductResponseDTO { 
                Id = product.Id,
                Name = product.Name,    
                Description = product.Description,
                Price = product.Price,
                Creation = product.Creation,
                LastUpdated = product.LastUpdated,
                Discount = await GetDiscount(product.Id),
                Stock = product.Stock,
                Status = statusList[product.Status]
            };
        
        }

        private async Task<int> GetDiscount(int id)
        {
            using var client = new HttpClient();
            HttpResponseMessage webResponse = await client.GetAsync($"https://66a8509453c13f22a3d251bb.mockapi.io/api/discount/{id}");
            if (webResponse.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<Discount>(webResponse.Content.ReadAsStringAsync().Result).Value;
            }
            else
            {
                return 0;
            }
        }

        public Product cast(ProductRequestDTO product)
        {
            return new Product
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Creation = product.Creation,
                LastUpdated = product.LastUpdated,
                Stock = product.Stock,
                Status = product.Status
            };
        }
    }
}
