using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupplyChainX.DTOs.Product;
using SupplyChainX.Services.Interfaces;

namespace SupplyChainX.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        this._productService = productService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll() => 
        Ok(await _productService.GetAllAsync());

    [HttpGet("{id:int}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _productService.GetByIdAsync(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto updateProductDto)
    {
        var product = await _productService.UpdateAsync(id, updateProductDto);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpDelete("{id:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var delete = await _productService.DeleteAsync(id);
        return delete ? NoContent() : NotFound();
    }
    
}