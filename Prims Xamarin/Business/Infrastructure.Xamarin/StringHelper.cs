//------------------------------------------------------------------------------
// Copyright of Nicholas Andrew Bull 2019
// This code is for portfolio use only.
//------------------------------------------------------------------------------

namespace Infrastructure.Xamarin
{
    using System.Text.RegularExpressions;

    public static class StringHelper
    {
        public static string GetImageSourceFromHtml(string html)
        {
            var output = Regex.Match(html, "<img.+?src=[\"'](.+?)[\"'].*?>", RegexOptions.IgnoreCase).Groups[1].Value;
            return output;
        }

        public static string StripHref(string html)
        {
            var output = html.Split('"')[1];
            return output;
        }

        public static string FindAndReplaceHtmlCodes(string html)
        {
            if (html.Contains("&quot;"))
            {
                html = html.Replace("&quot;", "\"");
            }

            if (html.Contains("&#039;"))
            {
                html = html.Replace("&#039;", "'");
            }


            return html;
        }
    }
}
// Infrastructure.Xamarin namespace 