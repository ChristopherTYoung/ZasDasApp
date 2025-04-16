using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("pay")]
public class PaymentController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        var htmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "square-payment.html");
        var htmlContent = System.IO.File.ReadAllText(htmlPath);
        return Content(htmlContent, "text/html");
    }
}