using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace assessment_api_developer.UI.Pages
{
    public class FilesModel : PageModel
    {
        public string ImageFileName { get; set; } = "20240203.jpg";

        public void OnGet()
        {
        }
    }
}
