using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SupplyChainX.Data;
using SupplyChainX.DTOs.Product;
using SupplyChainX.Models;
using SupplyChainX.Services.Interfaces;

namespace SupplyChainX.Services.Implementations;

public class ProductService : IProductService
{
    private readonly SupplyChainDbContext _supplyChainDbContext;
    private readonly IMapper _mapper;

    public ProductService(SupplyChainDbContext supplyChainDbContext, IMapper mapper)
    {
        _supplyChainDbContext = supplyChainDbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
    {
        var products = await _supplyChainDbContext.Products.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<ProductResponseDto>>(products);
    }

    public async Task<ProductResponseDto?> GetByIdAsync(int id)
    {
       var product = await _supplyChainDbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
       return product is null ? null : _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<ProductResponseDto> CreateAsync(CreateProductDto createProductDto)
    {
       var  product = _mapper.Map<Product>(createProductDto);
       _supplyChainDbContext.Products.Add(product);
       await _supplyChainDbContext.SaveChangesAsync();
       return _mapper.Map<ProductResponseDto>(product);
       
    }

    public async Task<ProductResponseDto?> UpdateAsync(int id, UpdateProductDto updateProductDto)
    {
       var product = await _supplyChainDbContext.Products.FindAsync(id);
       if (product is null) return null;
       
       _mapper.Map(updateProductDto, product);
       await _supplyChainDbContext.SaveChangesAsync();
       return _mapper.Map<ProductResponseDto>(product);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _supplyChainDbContext.Products.FindAsync(id);
        if (product is null) return false;
       
        _supplyChainDbContext.Products.Remove(product);
        await _supplyChainDbContext.SaveChangesAsync();
        return true;
    }
}