using assessment_api_developer.UI.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace assessment_api_developer.UI.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly HttpClient _httpClient;

        public CustomersModel(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        [BindProperty]
        public Customer Customer { get; set; }

        [BindProperty]
        public int SelectedCustomerId { get; set; }

        public List<SelectListItem> Customers { get; set; }


        public async Task OnGetAsync()
        {
           await LoadCustomersAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCustomersAsync();
                return Page();
            }

            var response = await _httpClient.PostAsJsonAsync("https://localhost:7015/api/V1/Customers", Customer);
            response.EnsureSuccessStatusCode();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCustomersAsync();
                return Page();
            }

            var response = await _httpClient.PutAsJsonAsync($"https://localhost:7015/api/V1/Customers/{Customer.ID}", Customer); ;
            response.EnsureSuccessStatusCode();
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7015/api/V1/Customers/{Customer.ID}");
            response.EnsureSuccessStatusCode();
            return RedirectToPage();
        }


        private async Task LoadCustomersAsync()
        {
            var response = await _httpClient.GetAsync("https://localhost:7015/api/V1/Customers");
            response.EnsureSuccessStatusCode();
            var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
            Customers = customers.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();
        }
    }
}
