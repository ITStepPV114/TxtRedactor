using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextRedactor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            this.Resources = new ResourceDictionary()
            {
                Source = new Uri("Theme/light.xaml",UriKind.Relative)
            };
            var menuControl = new MenuControl(rtbEditor);
            mainPanel.Children.Add(menuControl);
            rtbEditor.Document.LineHeight = 3;
            //BlockCollection rtbBc = rtbEditor.Document.Blocks;
            //foreach(var block in rtbBc) 
            //{
            //    block.TextAlignment= TextAlignment.Center;
            //}
            //Paragraph p = rtbEditor.Document.Blocks.FirstBlock as Paragraph;
            //p.LineHeight = 3;
        }

        private void rtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            string fileText = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
            int lineCount = 0;
            int charCount = 0;
            for (int i = 0; i < fileText.Length; i++)
            {
                if (fileText[i] == '\n')
                {
                    lineCount++;
                }
            }

            charCount = fileText.Length;

            statusText.Text = $"Lines: {lineCount}, Chars: {charCount}";
        }
    }
}
