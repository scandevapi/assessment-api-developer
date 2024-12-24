using assessment_api_developer.UI.Models;
using System.Net.Http.Headers;

namespace assessment_api_developer.UI.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomerService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<string> GetTokenAsync()
        {
            var response = await _httpClient.GetAsync("api/v1/auth/generate-token");
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<TokenResponse>();
            return result.Token;
        }

        public void StoreToken(string token)
        {
            _httpContextAccessor.HttpContext.Session.SetString("JWToken", token);
        }

        private void AddAuthorizationHeader()
        {
            var token = _httpContextAccessor.HttpContext.Session.GetString("JWToken");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }



        public async Task<List<Customer>> GetCustomersAsync()
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<List<Customer>>("api/v1/customers");
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            AddAuthorizationHeader();
            return await _httpClient.GetFromJsonAsync<Customer>($"api/v1/customers/{id}");
        }

        public async Task CreateCustomerAsync(Customer customer)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PostAsJsonAsync("api/v1/customers", customer);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateCustomerAsync(Customer customer)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.PutAsJsonAsync($"api/v1/customers/{customer.ID}", customer);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteCustomerAsync(int id)
        {
            AddAuthorizationHeader();
            var response = await _httpClient.DeleteAsync($"api/v1/customers/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
