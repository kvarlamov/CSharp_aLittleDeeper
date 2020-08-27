using System;
using System.Net;
using System.Text.RegularExpressions;

namespace p05_Strings
{
    public static class UrlParser
    {
        public static void ParseSite(string url)
        {
            string pattern = @"^https?:\/\/\S*";
            string siteHtml = null;
            Match m;
            using (WebClient client = new WebClient())
            {
                siteHtml = client.DownloadString("url");
            }

            try
            {
                m = Regex.Match(siteHtml, pattern,
                                RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1));
                while (m.Success)
                {
                    Console.WriteLine($"Found url {m.Groups[1]} at {m.Groups[1].Index}");
                    m = m.NextMatch();
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
}