using Microsoft.AspNetCore.Mvc;
using mini_store.Models;

namespace mini_store.Controllers
{
    public class WebApiController : Controller
    {
        
       private readonly HttpClient _httpClient;

        public WebApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

public async Task<IActionResult> GetProducts()
{
    var products = await _httpClient.GetFromJsonAsync<List<ApiProduct>>(
        "https://fakestoreapi.com/products"
    );

    return View(products);
}
    }
}
