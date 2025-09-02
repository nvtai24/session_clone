using Microsoft.AspNetCore.Mvc;

namespace SessionClone.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            var session = HttpContext.GetSession();
            session.SetString("Name", "Devip666");

            session = HttpContext.GetSession();
            var name = session.GetString("Name");
            name = name.StartsWith("Devip") ? "Hello " + name : "Who are you?";

            //session.CommitAsync();

            return Content(name);
        }

        public IActionResult Clear()
        {
            var session = HttpContext.GetSession();
            session.Clear();
            session.CommitAsync();
            return Content("Session Cleared");
        }

        public IActionResult Check()
        {
            return Ok();
        }

        public async Task<IActionResult> SetSessionValue(string key, string value)
        {
            var session = HttpContext.GetSession();
            await session.LoadAsync();

            session.SetString(key, value);
            await session.CommitAsync();
            return Ok();
        }

        public async Task<IActionResult> GetSessionValue(string key)
        {
            var session = HttpContext.GetSession();
            await session.LoadAsync();
            var value = session.GetString(key);
            return Ok(value);
        }
    }
}
