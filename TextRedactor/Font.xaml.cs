using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TextRedactor
{
    /// <summary>
    /// Interaction logic for Font.xaml
    /// </summary>
    public partial class Font : Window
    {
        private string v1;
        private int v2;

        public string selectedText { get; set; }
        public Font(string v, RichTextBox richTextBox)
        {
            InitializeComponent();
            selectedText = richTextBox.Selection.Text;
        }

        public Font(string v1, int v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }

        private void fontComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string fontName = ((ComboBoxItem)fontComboBox.SelectedItem).Content.ToString();
            richTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(fontName));
        }

        private void fontSizeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string fontSizeString = fontSizeTextBox.Text;
            double fontSize;
            if (Double.TryParse(fontSizeString, out fontSize))
            {
                richTextBox.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }
        }
    }
}
