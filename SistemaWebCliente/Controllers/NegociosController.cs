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

        public async Task<IActionResult> Create() { 
            return View(await Task.Run(() => new ClienteModel()));
        }

        [HttpPost]
        public async Task<IActionResult> Create(ClienteModel cliente) {
            string mensaje = "";

            using (var httpClient = new HttpClient()) {
                httpClient.BaseAddress =
                    new Uri("https://localhost:7274/api/NegocioApi/");

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(cliente),
                    System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await
                    httpClient.PostAsync("AddCliente", content);

                mensaje = await response.Content.ReadAsStringAsync();
            }
            ViewBag.mensaje = mensaje;
            return View(cliente);
        }


        public async Task<ClienteModel> Buscar(int id)
        {
            ClienteModel cliente = new ClienteModel();
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress =
                    new Uri("https://localhost:7274/api/NegocioApi/");
                HttpResponseMessage response = await
                    httpClient.GetAsync("GetClientes/"+id);
                string apiResponse = await response.Content.ReadAsStringAsync();
                cliente = JsonConvert.DeserializeObject<ClienteModel>(apiResponse);


            }
            return cliente;
        }

        public async Task<IActionResult> Edit(int id) {
            return View(await Task.Run(() => Buscar(id)));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ClienteModel cliente)
        {
            string mensaje = "";

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress =
                    new Uri("https://localhost:7274/api/NegocioApi/");

                StringContent content = new StringContent(
                    JsonConvert.SerializeObject(cliente),
                    System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = await
                    httpClient.PutAsync("UpdateCliente", content);

                mensaje = await response.Content.ReadAsStringAsync();
            }
            ViewBag.mensaje = mensaje;
            return View(cliente);
        }

    }
}
