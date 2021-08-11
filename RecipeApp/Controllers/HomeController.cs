using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecipeApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace RecipeApp.Controllers
{
    public class HomeController : Controller
    {
        public static string ApiURL = "https://localhost:44348/api/";
        public static string coneectionString = "";
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<RecipeModel> RecipeList = new List<RecipeModel>();

            string path = ApiURL + "Recipe/GetAllRecipe";

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                RecipeList = JsonConvert.DeserializeObject<List<RecipeModel>>(result);
            }
            int i = 1;
            foreach (var item in RecipeList)
            {
                item.Index = i;
                i++;
            }
            return View(RecipeList);
        }

        public IActionResult CreateRecipe()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateRecipe(RecipeModel createRecipe, IFormFile Image)
        {
            if (Image != null)
            {
                if (Image.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    Image.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    string base64FileString = Convert.ToBase64String(fileBytes);
                    createRecipe.Image = "data:" + Image.ContentType + ";base64," + base64FileString;
                }
            }
            else
            {
                createRecipe.Image = string.Empty;
            }

            string path = ApiURL + "Recipe/CreateRecipe";
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(createRecipe);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(
                path, stringContent).Result;
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public IActionResult UpdateRecipe(int RecipeId = 0)
        {
            RecipeModel recipeData = new RecipeModel();

            string path = ApiURL + "Recipe/GetRecipe?recipeId=" + RecipeId;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.GetAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                recipeData = JsonConvert.DeserializeObject<RecipeModel>(result);
            }

            return View(recipeData);
        }
        [HttpPost]
        public IActionResult UpdateRecipe(RecipeModel updateRecipe, string oldImage, IFormFile Image)
        {
            string base64FileString;
            if (Image != null)
            {
                if (Image.Length > 0)
                {
                    var memoryStream = new MemoryStream();
                    Image.CopyTo(memoryStream);
                    byte[] fileBytes = memoryStream.ToArray();
                    base64FileString = Convert.ToBase64String(fileBytes);
                    updateRecipe.Image = "data:" + Image.ContentType + ";base64," + base64FileString;
                }
            }
            else
            {
                updateRecipe.Image = oldImage;
            }

            string path = ApiURL + "Recipe/UpdateRecipe";
            HttpClient client = new HttpClient();

            var json = JsonConvert.SerializeObject(updateRecipe);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(
                path, stringContent).Result;
            response.EnsureSuccessStatusCode();

            return RedirectToAction("Index");
        }

        public IActionResult DeleteRecipe(int RecipeId = 0)
        {
            string path = ApiURL + "Recipe/DeleteRecipe?recipeId=" + RecipeId;

            HttpClient client = new HttpClient();
            HttpResponseMessage response = client.DeleteAsync(path).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("Index");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
