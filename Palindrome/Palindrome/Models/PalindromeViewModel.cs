using Palindrome.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Palindrome.Models
{
    public class PalindromeViewModel : ErrorModel
    {
        //model prperties required for view
        public string GivenString { get; set; }
        public bool FinalResult { get; set; }

        
    }
}