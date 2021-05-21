using Microsoft.AspNetCore.Mvc;

namespace ToSoftware.Shop.SignalR.Api.Controllers.Default
{
    [ApiController, Route("")]
    public class MeController : Controller
    {
        [HttpGet, Route("")]
        public IActionResult Get() => Ok(new { name = "shop hub", version = "1.0" });
    }
}