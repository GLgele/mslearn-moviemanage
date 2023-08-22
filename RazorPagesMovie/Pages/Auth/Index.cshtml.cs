using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesMovie.Pages.Auth
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            if(/*anonymous*/true)
            {
                Response.Redirect("Auth/Login/");
            }
            else
            {
                
            }
        }
    }
}
