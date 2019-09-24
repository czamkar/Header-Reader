using System;
using System.Windows.Forms;

namespace Header_Reader
{
    public partial class Form1 : Form
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        public Form1()
        {
            InitializeComponent();
        }

        private void DoWork(string url)
        {
            try
            {
                HtmlAnalyzer analyzer = new HtmlAnalyzer(url);
                analyzer.SetKeywords();
                var results = analyzer.findingWord();
                foreach (var item in results)
                {
                    var listViewItem = new ListViewItem(new string[] { item.Key, item.Value.ToString() });
                    lvResults.Items.Add(listViewItem);
                }
            
                lvResults.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
                lvResults.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
                lvResults.Show();
            }
            catch (UriFormatException ex)
            {
                Logger.Error(ex, "Bad url: ");
                MessageBox.Show(ex.Message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGet_Click(object sender, EventArgs e)
        {
            lvResults.Items.Clear();
            DoWork(txtUrlInput.Text);
        }
    }
}
