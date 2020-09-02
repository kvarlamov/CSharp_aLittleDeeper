using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace p05_Strings
{
    public static class UrlParser
    {
        static List<string> _links = new List<string>();
        public static void ParseSite(string url)
        {
            string pattern = "\"https?://\\S*\"";
            string siteHtml = null;
            using (WebClient client = new WebClient())
            {
                siteHtml = client.DownloadString(url);
            }

            try
            {
                Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection matchCollection = regex.Matches(siteHtml);
                foreach (Match match in matchCollection)
                {
                    Console.WriteLine(match.Value.Replace("\"",""));
                }
            }
            catch (RegexMatchTimeoutException e)
            {
                Console.WriteLine("The matching operation timed out");
            }
        }
    }

    public static class StringExtension
    {
        public static void Parse(this string url)
        {
            UrlParser.ParseSite(url);
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = false, AllowMultiple = true)]
    sealed class MyAttribute : Attribute
    {
        // See the attribute guidelines at 
        //  http://go.microsoft.com/fwlink/?LinkId=85236
        public MyAttribute()
        {
            // TODO: Implement code here
            throw new NotImplementedException();
        }
    }
}