using Firmezaa.Web.Models.Entities;
using Firmezaa.Web.Repositories.Interfaces;
using Firmezaa.Web.Services.Interfaces;

namespace Firmezaa.Web.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public async Task<List<Product>> GetAllProducts()
    {
        try
        {
            return await _productRepository.GetAllProduct();
        }
        catch (Exception ex)
        {
            // Aquí podrías loguear el error
            throw new Exception("Error obteniendo los productos.", ex);
        }
    }

    public async Task<Product?> GetProductById(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El id del producto debe ser mayor a cero.", nameof(id));

        try
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
                throw new KeyNotFoundException("Producto no encontrado.");
            return product;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error obteniendo el producto con Id {id}.", ex);
        }
    }

    public async Task AddProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product), "El producto no puede ser nulo.");

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("El nombre del producto es obligatorio.", nameof(product.Name));

        if (product.Price < 0)
            throw new ArgumentException("El precio del producto no puede ser negativo.", nameof(product.Price));

        if (product.Stock < 0)
            throw new ArgumentException("El stock del producto no puede ser negativo.", nameof(product.Stock));

        try
        {
            await _productRepository.AddProduct(product);
        }
        catch (Exception ex)
        {
            throw new Exception("Error agregando el producto.", ex);
        }
    }

    public async Task UpdateProduct(Product product)
    {
        if (product == null)
            throw new ArgumentNullException(nameof(product), "El producto no puede ser nulo.");

        if (product.Id <= 0)
            throw new ArgumentException("El id del producto no es válido.", nameof(product.Id));

        if (string.IsNullOrWhiteSpace(product.Name))
            throw new ArgumentException("El nombre del producto es obligatorio.", nameof(product.Name));

        if (product.Price < 0)
            throw new ArgumentException("El precio del producto no puede ser negativo.", nameof(product.Price));

        if (product.Stock < 0)
            throw new ArgumentException("El stock del producto no puede ser negativo.", nameof(product.Stock));

        try
        {
            await _productRepository.UpdateProduct(product);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error actualizando el producto con Id {product.Id}.", ex);
        }
    }

    public async Task DeleteProduct(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El id del producto debe ser mayor a cero.", nameof(id));

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
