using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CafeLab.Web.Pages;

public class LoginModel : PageModel
{
    private readonly IConfiguration _config;

    public LoginModel(IConfiguration config) => _config = config;

    public string ApiBaseUrl => _config["CafeApi:BaseUrl"] ?? "";

    public void OnGet() { }
}