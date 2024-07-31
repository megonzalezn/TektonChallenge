using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tekton.Repository.Interfaces;

namespace Tekton.UoW.Interfaces
{
    public interface IProductUnitOfWork
    {
        public IProductRepository Product { get; }

        void InitializeProduct();
    }
}
