using Microsoft.AspNetCore.Mvc;
using estudiante_curso.Domain;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MiProyectoWeb.Controllers
{
    public class CourseController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "http://localhost:5000/api/course"; 

        public CourseController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _httpClient.GetStringAsync(_apiBaseUrl);
            var courses = JsonConvert.DeserializeObject<List<Course>>(response);

            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _httpClient.GetStringAsync($"{_apiBaseUrl}/{id}");
            var course = JsonConvert.DeserializeObject<Course>(response);

            return View(course); 
        }
    }
}
