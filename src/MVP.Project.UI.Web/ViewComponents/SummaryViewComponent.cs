using Microsoft.AspNetCore.Mvc;

namespace MVP.Project.UI.Web.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}