namespace Lands.Services
{
    using Models;
    using Plugin.Connectivity;
    using System.Threading.Tasks;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Text;

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor active la red WiFi"
                };
            }

            var IsReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!IsReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor verifique la conexión a internet"
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Todo Bien"
            };
        }

        public async Task<TokenResponse> GetToken(
            string urlBase,
            string userName,
            string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var response = await client.PostAsync("Token"
                    , new StringContent("grant_type=password&username={userName}&password={password}"
                    , Encoding.UTF8
                    , "application/x-www-form-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);
                return result;

            }
            catch
            {
                return null;
            }
        }

        public async Task<Response> GetList<T>(
            string urlBase
            , string servicePrefix
            , string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = $"{servicePrefix}{controller}";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
