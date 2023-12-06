using BusTicket.Business;
using BusTicket.Business.Dtos;
using BusTicket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusTicket.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusLocationStore busLocationStore;
        private readonly IBusJourneysStore busJourneysStore;

        public HomeController(IBusLocationStore busLocationStore, IBusJourneysStore busJourneysStore)
        {
            this.busLocationStore = busLocationStore;
            this.busJourneysStore = busJourneysStore;
        }
        [Route("Home/SearchBusLocationAsync/{term}")]
        public async Task<IActionResult> SearchBusLocationAsync(string term)
        {
            var resp = await this.busLocationStore.SearchAsync(term);
            if (resp.IsSuccessful == false)
            {

            }
            return Ok(resp.Data);
        }
        public IActionResult Index()
        {
            var getAll = this.busLocationStore.GetAllWithCloneAsync(0, 100);
            getAll.Wait();
            var result = getAll.Result;
            if (result.IsSuccessful == false)
            {
                ViewBag.Error = result.Message;
                return View();
            }

            return View(result.Data);
        }
        public IActionResult Journeys()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Journeys(BusJourneysGetInput input)
        {
            var getJourneys = this.busJourneysStore.Get(input);
            getJourneys.Wait();
            var result = getJourneys.Result;
            if (result.IsSuccessful == false)
            {

                TempData["Error"] = result.Message;
                return RedirectToAction("Index");
            }
            if (result.Data.Items.Count<1)
            {
                TempData["Error"] = "Bu bilgilerle sefer bulunamadı";
                return RedirectToAction("Index");
            }
            return View(result.Data);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
    public class HomeIndexDto
    {
        public int StartLocationId { get; set; }
        public int EndLocationId { get; set; }
    }
}