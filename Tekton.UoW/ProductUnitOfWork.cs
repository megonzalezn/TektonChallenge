using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Tekton.Repository;
using Tekton.Repository.Interfaces;
using Tekton.UoW.Interfaces;

namespace Tekton.UoW
{
    public class ProductUnitOfWork: IProductUnitOfWork
    {
        private readonly IDbConnection connection;
        private readonly IContext context;
        private IDbContextTransaction transaction;

        public ProductUnitOfWork(IDbConnection connection, IContext context)
        {
            this.connection = connection;
            this.context = context;
        }
        
        public IProductRepository Product { get; set; }

        public void InitializeProduct()
        {
            Product ??= new ProductRepository(context);
        }

        public virtual void BeginTransaction()
        {
            if (connection.State == ConnectionState.Open)
                transaction = context.Database.BeginTransaction();
        }
        public virtual void Complete()
        {
            if (transaction != null)
                transaction.Commit();
        }
        public virtual void Dispose()
        {
            if (connection?.State == ConnectionState.Open)
                connection.Close();
            transaction?.Dispose();
        }
        public virtual void Rollback()
        {
            transaction?.Rollback();
        }
        public virtual void Close()
        {
            connection.Close();
        }
    }
}
