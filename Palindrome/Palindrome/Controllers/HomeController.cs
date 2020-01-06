using System.Web.Mvc;

namespace Palindrome.Controllers
{
    public class HomeController : Controller
    {

        /// <summary>
        /// GET: Home page inital page
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}