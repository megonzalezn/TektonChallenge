using Tekton.Model;

namespace Tekton.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductResponseDTO> GetAll();
        Task<ProductResponseDTO> GetById(int id);
        Task<ProductResponseDTO> Create(ProductRequestDTO product);
        Task<ProductResponseDTO> Update(ProductRequestDTO product);
    }
}
