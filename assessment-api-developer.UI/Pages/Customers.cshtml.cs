using assessment_api_developer.Domain.Models;
using assessment_api_developer.Services.Services;
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
        public Customer customer { get; set; }

        [BindProperty]
        public int SelectedCustomerId { get; set; } 

        public List<SelectListItem> Customers { get; set; }




        public void OnGet()
        {
            Customers = _customerService.GetAllCustomersAsync().Result.Select(c => new SelectListItem { Value = c.ID.ToString(), Text = c.Name }).ToList();
        }
    }
}
