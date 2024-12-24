using assessment_api_developer.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace assessment_api_developer.UI.Pages
{
    public class IndexModel : PageModel
    {
        //private readonly ILogger<IndexModel> _logger;

        //public IndexModel(ILogger<IndexModel> logger)
        //{
        //    _logger = logger;
        //}

        private readonly CustomerService _customerService;

        public string Message { get; set; }

        public IndexModel(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                var token = await _customerService.GetTokenAsync();
                _customerService.StoreToken(token);
            }
            catch (HttpRequestException ex)
            {
                Message = $"Error: {ex.Message}";
            }

            return Page();
        }
    }
}
