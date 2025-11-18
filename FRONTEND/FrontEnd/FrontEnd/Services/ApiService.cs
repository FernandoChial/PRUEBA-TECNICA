using Newtonsoft.Json;

namespace FrontEnd.Services
{
    // Servicio genérico para hacer llamadas HTTP a la API
    public class ApiService
    {

        private readonly HttpClient _http;


        public ApiService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient"); 
        }

        // Método genérico para hacer GET a una URL y deserializar la respuesta en tipo T
        public async Task<T?> GetAsync<T>(string url)
        {
            var response = await _http.GetAsync(url); 
            var json = await response.Content.ReadAsStringAsync(); 
            return JsonConvert.DeserializeObject<T>(json); 
        }

        // Método genérico para hacer POST 
        public async Task<T?> PostAsync<T>(string url, object data)
        {
            var response = await _http.PostAsJsonAsync(url, data); 
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
