using Tekton.Entity;

namespace Tekton.Repository.Interfaces
{
    public interface IProductRepository
    {
        public List<Product> GetAll();
        public Product GetById(int id);
        public Product Create(Product product);
        public Product Update(Product product);
    }
}
