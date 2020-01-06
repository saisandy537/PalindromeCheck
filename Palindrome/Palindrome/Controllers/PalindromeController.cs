using System.Web.Mvc;
using Palindrome.Factory;

namespace Palindrome.Controllers
{
    public class PalindromeController : Controller
    {
        /// <summary>
        /// Palindrome Action Method
        /// </summary>
        /// <returns></returns>
        public ActionResult Palindrome()
        {
            var PalindromeResult = PalindromeFactory.Palindrome();
            return View(PalindromeResult);
        }

    }
}
