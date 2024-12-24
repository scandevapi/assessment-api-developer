using assessment_api_developer.UI.Models;
using assessment_api_developer.UI.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace assessment_api_developer.UI.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly CustomerService _customerService;

        public CustomersModel(CustomerService customerService)
        {
            _customerService = customerService;
        }


        [BindProperty]
        public Customer Customer { get; set; }

        public List<SelectListItem> Customers { get; set; }

        public string Message { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                await LoadCustomersAsync();
            }
            catch (HttpRequestException ex)
            {
                Message = $"Error: {ex.Message}";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCustomersAsync();
                return Page();
            }

            try
            {
                await _customerService.CreateCustomerAsync(Customer);
            }
            catch (HttpRequestException ex)
            {
                Message = $"Error: {ex.Message}";
                await LoadCustomersAsync();
                return Page();
            }

            return RedirectToPage();

            //var response = await _httpClient.PostAsJsonAsync("https://localhost:7015/api/V1/Customers", Customer);
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorContent = await response.Content.ReadAsStringAsync();
            //    Message = $"Error: {response.StatusCode} - {errorContent}";
            //    await LoadCustomersAsync();
            //    return Page();
            //}
            //return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEditAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadCustomersAsync();
                return Page();
            }

            try
            {
                await _customerService.UpdateCustomerAsync(Customer);
            }
            catch (HttpRequestException ex)
            {
                Message = $"Error: {ex.Message}";
                await LoadCustomersAsync();
                return Page();
            }

            return RedirectToPage();

            //var response = await _httpClient.PutAsJsonAsync($"https://localhost:7015/api/V1/Customers/{Customer.ID}", Customer); ;
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorContent = await response.Content.ReadAsStringAsync();
            //    Message = $"Error: {response.StatusCode} - {errorContent}";
            //    await LoadCustomersAsync();
            //    return Page();
            //}
            //return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            try
            {
                await _customerService.DeleteCustomerAsync(Customer.ID);
            }
            catch (HttpRequestException ex)
            {
                Message = $"Error: {ex.Message}";
                await LoadCustomersAsync();
                return Page();
            }

            return RedirectToPage();

            //var response = await _httpClient.DeleteAsync($"https://localhost:7015/api/V1/Customers/{Customer.ID}");
            //if (!response.IsSuccessStatusCode)
            //{
            //    var errorContent = await response.Content.ReadAsStringAsync();
            //    Message = $"Error: {response.StatusCode} - {errorContent}";
            //    await LoadCustomersAsync();
            //    return Page();
            //}
            //return RedirectToPage();
        }


        private async Task LoadCustomersAsync()
        {
            var customers = await _customerService.GetCustomersAsync();
            Customers = customers.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();

            //var response = await _httpClient.GetAsync("https://localhost:7015/api/V1/Customers");
            //response.EnsureSuccessStatusCode();
            //var customers = await response.Content.ReadFromJsonAsync<List<Customer>>();
            //Customers = customers.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();
        }
    }
}
