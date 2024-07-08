using System.Text;
using System.Text.Json;

namespace OL_OASP_dev_H_07_23_Shared.Services.Implementation
{
    public abstract class WebApiServiceClientBase
    {
        // Privatno polje za HttpClient
        private readonly HttpClient httpClient;
        // Privatno polje za akciju koja će se izvršiti u slučaju neuspješnog odgovora
        private readonly Action<HttpResponseMessage> unsuccessfulResponseAction;
        // Privatno polje za postavke JSON serijalizatora
        private readonly JsonSerializerOptions jsonSerializerOptions;

        // Konstruktor klase koji inicijalizira HttpClient, akciju za neuspješan odgovor i postavke JSON serijalizatora
        protected WebApiServiceClientBase(HttpClient httpClient, Action<HttpResponseMessage> unsuccessfulResponseAction)
        {
            // Inicijalizira se httpClient s proslijeđenim argumentom
            this.httpClient = httpClient;

            // Inicijalizira se unsuccessfulResponseAction s proslijeđenim argumentom
            this.unsuccessfulResponseAction = unsuccessfulResponseAction;

            // Inicijalizira se jsonSerializerOptions s novim postavkama gdje se koristi CamelCase za nazive svojstava
            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }
        protected WebApiServiceClientBase(HttpClient httpClient, Action<HttpResponseMessage> unsuccessfulResponseAction, JsonSerializerOptions jsonSerializerOptions)
        {
            // Inicijalizira se httpClient s proslijeđenim argumentom
            this.httpClient = httpClient;

            // Inicijalizira se unsuccessfulResponseAction s proslijeđenim argumentom
            this.unsuccessfulResponseAction = unsuccessfulResponseAction;

            // Inicijalizira se jsonSerializerOptions s novim postavkama gdje se koristi CamelCase za nazive svojstava
            this.jsonSerializerOptions = jsonSerializerOptions;
        }

        // Zaštićena metoda koja izvršava HTTP zahtjev i pokušava dobiti sadržaj odgovora
        protected T DoRequestAndTryGetResponseContent<T>(string url, HttpMethod httpMethod, bool readResponseBody,
Action<HttpResponseMessage> unsuccessfulResponseAction, object contentObj = null)
        {
            // Izvršava HTTP zahtjev i dobiva poruku odgovora
            var responseMessage = DoRequest(url, httpMethod, contentObj: contentObj);

            // Pokušava dobiti sadržaj odgovora
            return TryGetResponseContent<T>(responseMessage, readResponseBody, unsuccessfulResponseAction);
        }


        // Privatna metoda koja pokušava dobiti sadržaj odgovora
        private T TryGetResponseContent<T>(HttpResponseMessage responseMessage, bool readResponseBody, Action<HttpResponseMessage> unsuccessfulResponseAction)
        {
            // Provjerava je li statusni kod odgovora uspješan
            if (responseMessage.IsSuccessStatusCode)
            {
                // Ako treba pročitati tijelo odgovora
                if (readResponseBody)
                {
                    // Čita tijelo odgovora kao string
                    var response = responseMessage.Content.ReadAsStringAsync().Result;

                    // Deserijalizira string u objekt tipa T koristeći postavke JSON serijalizatora
                    var responseObj = JsonSerializer.Deserialize<T>(response, jsonSerializerOptions);

                    // Vraća deserijalizirani objekt
                    return responseObj;
                }
            }
            else
            {
                // Ako je odgovor neuspješan, izvršava zadanu akciju za neuspješan odgovor
                var action = unsuccessfulResponseAction ?? this.unsuccessfulResponseAction;

                // Ako akcija nije null, izvršava je
                action?.Invoke(responseMessage);
            }

            // Vraća zadanu vrijednost tipa T
            return default(T);
        }
        // Privatna metoda koja izvršava HTTP zahtjev
        private HttpResponseMessage DoRequest(string url, HttpMethod httpMethod, object contentObj = null)
        {
            // Kreira se novi HttpRequestMessage s proslijeđenom metodom i URL-om
            var request = new HttpRequestMessage(httpMethod, url);

            // Ako je objekt sadržaja proslijeđen, serijalizira ga u JSON i postavlja kao sadržaj zahtjeva
            if (contentObj != null)
            {
                HttpContent content = new StringContent(JsonSerializer.Serialize(contentObj), Encoding.UTF8, "application/json");

                // Postavlja sadržaj zahtjeva
                request.Content = content;
            }

            // Ako je metoda GET, šalje GET zahtjev
            if (httpMethod == HttpMethod.Get)
            {
                return httpClient.SendAsync(request).Result;
            }

            // Ako je metoda DELETE, šalje DELETE zahtjev
            if (httpMethod == HttpMethod.Delete)
            {
                return httpClient.DeleteAsync(url).Result;
            }

            // Ako je metoda PUT, šalje PUT zahtjev
            if (httpMethod == HttpMethod.Put)
            {
                return httpClient.PutAsync(url, request.Content).Result;
            }

            // Ako nije niti jedna od navedenih metoda, pretpostavlja se da je metoda POST i šalje POST zahtjev
            return httpClient.PostAsync(url, request.Content).Result;
        }
    }
}
