using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace WebCliente.Controllers
{
    public class PersonaController : Controller
    {
        private string urlbase;
        private string cadena;
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonaController(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            urlbase = configuration["baseurl"];
            _httpClientFactory = httpClientFactory;

            cadena = "Holaaaa";

        }

        public IActionResult Index()
        {

            return View();
        }

        //Traer los datos como un string
        public async Task<string> listarPersona()
        {
            var cliente = _httpClientFactory.CreateClient();
            cliente.BaseAddress = new Uri(urlbase);
            string cadena = await cliente.GetStringAsync("api/Persona");

            return cadena;

        }
    }
}