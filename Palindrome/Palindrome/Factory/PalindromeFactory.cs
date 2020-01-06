using Newtonsoft.Json;
using Palindrome.Models;
using Palindrome.Models.Core;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Web.Configuration;

namespace Palindrome.Factory
{
    public static class PalindromeFactory
    {

        static PalindromeViewModel palindromeViewResponse = new PalindromeViewModel();
        
        /// <summary>
        /// This is a factory method used to check the palindrome
        /// </summary>
        /// <returns></returns>
        public static List<PalindromeViewModel> Palindrome()
        {
            List<PalindromeViewModel> response = new List<PalindromeViewModel>();
            try
            {
                //Capture the url from webconfig
                string urlLink = WebConfigurationManager.AppSettings[Constants.urlLink];

                //Read the JsonFomrat from remote Url
                var jsonContent = ReadJsonFromUrl(urlLink);

                //Deserialize Json to object
                var resultJson = DeserializeJsonContent(jsonContent);

                //check if the content contain palindrome data or not 
                response = CheckPalindrome(resultJson);
            }
            catch (Exception ex)
            {
                palindromeViewResponse.ErrorMessage = ex.Message;
                palindromeViewResponse.ErrorCount++;
            }

            return response;
        }

        
        /// <summary>
        /// This method is used to Read the Json data from remote URL
        /// </summary>
        /// <param name="urlLink"></param>
        /// <returns></returns>
        private static string ReadJsonFromUrl(string urlLink)
        {
            string jsonString = string.Empty;

            if (!string.IsNullOrEmpty(urlLink))
            {
                try
                {
                    WebClient webClient = new WebClient();
                    jsonString = webClient.DownloadString(urlLink);
                }
                catch (Exception)
                {
                    palindromeViewResponse.ErrorMessage = "Error reading the Json file from remote url provide.";
                    palindromeViewResponse.ErrorCount++;
                }
            }
            else
            {
                palindromeViewResponse.ErrorMessage = "Remote url is null or empty.";
                palindromeViewResponse.ErrorCount++;
            }

            return jsonString;
        }

        /// <summary>
        /// This method is used to deserialize the json string
        /// </summary>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        private static PalidromeModel DeserializeJsonContent(string jsonData)
        {
            PalidromeModel resultJson = new PalidromeModel();

            if (!string.IsNullOrEmpty(jsonData))
            {
                try
                {
                    resultJson = JsonConvert.DeserializeObject<PalidromeModel>(jsonData);
                }
                catch (Exception)
                {
                    palindromeViewResponse.ErrorMessage = "json file provided is not in correct format";
                    palindromeViewResponse.ErrorCount++;
                }

            }
            else
            {
                palindromeViewResponse.ErrorMessage = "Cannot deserizlze the empty or null json string";
                palindromeViewResponse.ErrorCount++;
            }

            return resultJson;
        }


        /// <summary>
        /// This method is used to check the palindrome
        /// </summary>
        /// <param name="palindromModel"></param>
        /// <returns></returns>
        private static List<PalindromeViewModel> CheckPalindrome(PalidromeModel palindromModel)
        {
            List<PalindromeViewModel> responseLst = new List<PalindromeViewModel>();
            

            if (palindromModel == null || palindromModel.strings == null)
            {
                palindromeViewResponse.ErrorMessage = "json file data is empty";
                palindromeViewResponse.ErrorCount++;
            }
            else
            {
                try
                {
                    foreach (var item in palindromModel.strings)
                    {
                        PalindromeViewModel responseModel = new PalindromeViewModel();
                        
                        if (!string.IsNullOrEmpty(item.Str))
                        {
                            var reversedString = ReverseString(item.Str).ToString();
                            if (string.Equals(item.Str.Replace(" ","").ToLowerInvariant(), reversedString.Replace(" ", "").ToLowerInvariant()))
                            {
                                responseModel.GivenString = item.Str;
                                responseModel.FinalResult = true;
                            }
                            else
                            {
                                responseModel.GivenString = item.Str;
                                responseModel.FinalResult = false;
                            }
                        }
                        else
                        {
                            responseModel.WarningMessage = "String is empty";
                            responseModel.WarningCount++;
                        }
                        responseLst.Add(responseModel);
                    }
                }
                catch (Exception)
                {
                    palindromeViewResponse.ErrorMessage = "Error occured while procesing... Please try again";
                    palindromeViewResponse.ErrorCount++;
                }
            }

            return responseLst;
        }

        /// <summary>
        /// This method is used to reverse the string
        /// </summary>
        /// <param name="originalString"></param>
        /// <returns></returns>
        private static StringBuilder ReverseString (string originalString)
        {
            StringBuilder reversedString = new StringBuilder();
            if (!string.IsNullOrEmpty(originalString))
            {
                for (int str = originalString.Length - 1; str >= 0; str--)
                {
                    reversedString.Append(originalString[str]);
                }
            }
            return reversedString;
        }
    }
}