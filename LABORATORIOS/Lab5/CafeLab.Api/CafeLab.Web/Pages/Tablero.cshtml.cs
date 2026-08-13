using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CafeLab.Web.Pages;

public class TableroModel : PageModel
{
    private readonly IConfiguration _config;

    public TableroModel(IConfiguration config) => _config = config;

    public string ApiBaseUrl =>
        _config["CafeApi:BaseUrl"] ?? "";

    public void OnGet() { }
}