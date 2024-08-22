using App.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

namespace App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _client;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;

            // AppSettings read
            var builder = new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            var apiUrl = config.GetValue<string>("Api");

            // Http client create
            _client = new HttpClient();
            _client.BaseAddress = new Uri(apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public IActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            try
            {
                var task = Task.Run(() => _client.GetAsync("api/Employees"));
                task.Wait();
                var response = task.Result;

                if (response.IsSuccessStatusCode)
                {
                    var task2 = Task.Run(() => response.Content.ReadAsAsync<List<Employee>>());
                    task2.Wait();
                    employees = task2.Result;
                }
            }
            catch (Exception ex) {
                employees.Add(new Employee()
                {
                    Name= ex.Message
                });
            }

            
            return View(employees);
        }

        public IActionResult Create()
        {
            try
            {
                var task = Task.Run(() => _client.PostAsJsonAsync("api/Employees", new Employee { Name = Guid.NewGuid().ToString() }));
                task.Wait();                
            }
            catch (Exception ex)
            {
                
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var task = Task.Run(() => _client.DeleteAsync("api/Employees/"+id));
                task.Wait();
            }
            catch (Exception ex)
            {

            }
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
