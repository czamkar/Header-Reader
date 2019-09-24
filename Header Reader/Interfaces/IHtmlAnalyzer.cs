using System.Collections.Generic;
using System.Windows.Forms;

namespace Header_Reader.Interfaces
{

    public interface HtmlAnalyzer
    {
        void SetHeadBody();
        HtmlDocument GetHtmlDocument(string html);
        Dictionary<string, int> FindingWord();
        void SetKeywords();
    }
}
