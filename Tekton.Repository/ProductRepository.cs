using Microsoft.EntityFrameworkCore;
using System.Data;
using Tekton.Entity;
using Tekton.Repository.Interfaces;

namespace Tekton.Repository
{
    public class ProductRepository: IProductRepository
    {
        protected readonly Context context;
        public ProductRepository(Context context)
        {
            this.context = context;
        }

        public List<Product> GetAll()
        {
            
            return context.Product.ToList();
        }

        public Product GetById(int id)
        {
            return context.Product.Where(s => s.Id == id).FirstOrDefault();
        }

        public Product Create(Product product)
        {
            context.Product.Add(product);
            context.SaveChanges();
            return product;
        }

        public Product Update(Product product)
        {
            context.Entry(product).State = EntityState.Modified;
            context.SaveChanges(); 
            return product;
        }
    }
}
