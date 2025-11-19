using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Repositories.Interfaces;
using Firmezaa.Web.Services.Interfaces;

namespace Firmezaa.Web.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        // GET ALL
        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                return await _productRepository.GetAllProduct();
            }
            catch (Exception ex)
            {
                throw new Exception("Error obteniendo la lista de productos.", ex);
            }
        }

        // GET BY ID
        public async Task<Product?> GetProductById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id debe ser mayor a cero.");

            var product = await _productRepository.GetProductById(id);

            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado.");

            return product;
        }

        // CREATE
        public async Task AddProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("El nombre es obligatorio.");

            if (product.Price < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            if (product.Quantity < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");

            // Seteamos CreatedAt y UpdatedAt aunque ya venga desde el Controller
            product.UpdatedAt = product.CreatedAt;

            try
            {
                await _productRepository.AddProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creando el producto.", ex);
            }
        }

        // UPDATE
        public async Task UpdateProduct(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            if (product.Id <= 0)
                throw new ArgumentException("El id no es válido.");

            if (string.IsNullOrWhiteSpace(product.Name))
                throw new ArgumentException("El nombre es obligatorio.");

            if (product.Price < 0)
                throw new ArgumentException("El precio no puede ser negativo.");

            if (product.Quantity < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");

            // Actualizamos la fecha
            product.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error actualizando el producto con Id {product.Id}.", ex);
            }
        }

        // DELETE
        public async Task DeleteProduct(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id debe ser mayor a cero.");

            try
            {
                await _productRepository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error eliminando el producto con Id {id}.", ex);
            }
        }
    }
}
