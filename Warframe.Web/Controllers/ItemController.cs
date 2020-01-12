// ###############################################################
// Thomas Heise
// Warframe.Web
// ItemController.cs
// 2019/12/29/12:59
// ###############################################################

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Warframe.Web.Models;

namespace Warframe.Web.Controllers
{
    public class ItemController : Controller
    {
        public IActionResult Index() => PartialView("Index");

        [BindProperty]
        public string SelectedTag { get; set; }

        [HttpGet]
        public async Task<IActionResult> GetAll(string parameter)
        {
            var http = new HttpClient();
            http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            
            var data = await http.GetAsync(
                "https://api.warframe.market/v1/items").Result.Content.ReadAsStringAsync();

            var test = JsonConvert.DeserializeObject(data) as JObject;
            var t = test["payload"]["items"];

            var result = CreateItems(t);

            return View("Index", result);
        }

        public async Task<IActionResult> SingleItem(ItemViewModel collection)
        {
            var http = new HttpClient();
            var data = await http.GetAsync(
                $"https://api.warframe.market/v1/items/{collection.Value}").Result.Content.ReadAsStringAsync();
            var statistikData = await http.GetAsync(
                $"https://api.warframe.market/v1/items/{collection.Value}/statistics").Result.Content.ReadAsStringAsync();

            return Ok(JsonConvert.DeserializeObject(data));
        }

        private ItemViewModel CreateItems(JToken items)
        {
            var result = new List<SelectListItem>();

            foreach (var item in items)
            {
                result.Add(new SelectListItem
                {
                    Text = item["item_name"].ToString(),
                    Value = item["url_name"].ToString()
                });
            }

            return new ItemViewModel{Items = result};
        }
    }
}