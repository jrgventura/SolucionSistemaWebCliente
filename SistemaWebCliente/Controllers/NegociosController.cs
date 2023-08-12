using Microsoft.AspNetCore.Mvc;
using SistemaWebCliente.Models;
using Newtonsoft.Json;

namespace SistemaWebCliente.Controllers
{
    public class NegociosController : Controller
    {
        public async Task<IActionResult> Index()
        {
            List<ClienteModel> clienteList = new List<ClienteModel>();
            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress = 
                    new Uri("https://localhost:7274/api/NegocioApi/");
                HttpResponseMessage response = await 
                    httpClient.GetAsync("GetClientes");
                string apiResponse = await response.Content.ReadAsStringAsync();
                clienteList = JsonConvert.DeserializeObject<List<ClienteModel>>
                    (apiResponse).Select(
                    s => new ClienteModel { 
                        idcliente = s.idcliente,
                        nombre = s.nombre,
                        direccion = s.direccion,
                        idpais = s.idpais,
                        telefono = s.telefono
                    }
                    ).ToList();
            }
            return View(clienteList);
        }
    }
}
