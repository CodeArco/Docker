using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

// AppSettings read
var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false);
IConfiguration config = builder.Build();
var apiUrl = config.GetValue<string>("Api");
var appName = config.GetValue<string>("App:Name");

// Http client create
HttpClient client = new HttpClient();
client.BaseAddress = new Uri(apiUrl);
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));

// Start
Console.WriteLine($"Hi from {appName}");

while (true)
{
    try
    {
        Console.WriteLine($"Try to connect to {apiUrl}");

        // GET
        HttpResponseMessage response = await client.GetAsync("api/Employees");
        if (response.IsSuccessStatusCode)
        {
            var employees = await response.Content.ReadAsAsync<IEnumerable<Employee>>();
            Console.WriteLine($"Receiving {employees.Count()} employees");
        }

        // POST
        response = await client.PostAsJsonAsync("api/Employees",new Employee { Name=Guid.NewGuid().ToString()});
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Inserted 1 employee");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine($"Exception {e}");
    }

    Thread.Sleep(2000);
}
class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
}
