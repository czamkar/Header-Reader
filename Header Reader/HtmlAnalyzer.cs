using Header_Reader.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Header_Reader
{
    public class HtmlAnalyzer : Interfaces.HtmlAnalyzer
    {
        private HtmlElement head { get; set; }
        private HtmlElement body { get; set; }
        public Uri uri { get; set; }
        public string[] keyWordsArray { get; set; }

        public HtmlAnalyzer(string url)
        {
            setUri(url);
            SetHeadBody();
        }
        private Uri setUri(string url)
        {
                uri = new Uri(url);
                return uri;
 
        }
        public void SetHeadBody()
        {
            using (WebClient client = new WebClient())
            {
                client.Encoding = Encoding.UTF8;
                string htmlCode = client.DownloadString(uri);
                HtmlDocument html = GetHtmlDocument(htmlCode);
                head = html.GetElementsByTagName("head")[0];
                body = html.GetElementsByTagName("body")[0];
            }
        }

        public void SetKeywords()
        {
            string keywords = String.Empty;
            foreach (HtmlElement item in head.Children)
            {
                if (item.Name.ToLower() == "keywords")
                {
                    keywords = item.GetAttribute("content");
                    break;
                }
            }
            if (String.IsNullOrEmpty(keywords))
                throw new Exception("Na stronie brak tagu keywords");
            keyWordsArray = keywords.Split(',');
        }

        public HtmlDocument GetHtmlDocument(string html)
        {
            WebBrowser browser = new WebBrowser();
            browser.ScriptErrorsSuppressed = true;
            browser.DocumentText = html;
            browser.Document.OpenNew(true);
            browser.Document.Write(html);
            browser.Refresh();
            return browser.Document;
        }
        public Dictionary<string, int> findingWord()
        {
            Dictionary<string, int> res = new Dictionary<string, int>();
            foreach (var item in keyWordsArray)
            {
                string pattern = @"\b" + item.Trim() + @"\b";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                res.Add(item.Trim(), rgx.Matches(body.InnerText).Count);
            }
            return res;
        }
    }
}
