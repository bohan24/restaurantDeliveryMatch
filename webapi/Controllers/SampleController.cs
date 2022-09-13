using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    public class SampleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public class TestRequest
        {
            public string account { get; set; }
            public string userName { get; set; }
            public string date { get; set; }
            public string status { get; set; }
            public string coinTypeStatus { get; set; }
        }

        [HttpGet]
        public async Task<ActionResult> GetDataSample([FromQuery] TestRequest request)
        {
            return Ok("Success");
        }
    }
}
