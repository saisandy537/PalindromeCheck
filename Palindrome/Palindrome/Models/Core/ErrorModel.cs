using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Palindrome.Models.Core
{
    public class ErrorModel
    {
        // Error handeling
        public  string ErrorMessage { get; set; }
        public  int ErrorCount { get; set; }

        //warning

        public string WarningMessage { get; set; }
        public int WarningCount { get; set; }
    }
}