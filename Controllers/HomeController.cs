namespace SessionClone.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var session = HttpContext.GetSession();
        session.SetString("Test", "Hello Session");
        await HttpContext.GetSession().CommitAsync();

        return View();
    }

    public async Task<IActionResult> Privacy()
    {
        var session = HttpContext.GetSession();
        await session.LoadAsync();
        var sessionValue = session.GetString("Test");

        return View("Privacy", sessionValue ?? "NULL");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
