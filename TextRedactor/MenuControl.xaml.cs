using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;



namespace TextRedactor
{
    /// <summary>
    /// Interaction logic for MenuControl.xaml
    /// </summary>
    public partial class MenuControl : UserControl
    {
        public RichTextBox RichTextBox { get; set; }
        public MenuControl(RichTextBox richTextBox)
        {
            InitializeComponent();
            RichTextBox = richTextBox;
        }
        
        private void MenuItem_Click_open(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                filePath = op.FileName;
                //MessageBox.Show(filePath);
                var fileStream = op.OpenFile();
                //TextRange textRange = new TextRange();
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    fileContent = reader.ReadToEnd();
                    
                    RichTextBox.Document.Blocks.Add(new Paragraph(new Run(fileContent)));
                }
            }       
            
            
        }

        private void MenuItem_Click_save(object sender, RoutedEventArgs e)
        {
            string filePath = "C:\\Users\\dkhpr\\Desktop\\1\\first.txt";            
            string fileText = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text; 
            File.WriteAllText(filePath, fileText);
        }

        private void MenuItem_Click_saveAs(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog op = new Microsoft.Win32.SaveFileDialog();
            op.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            string fileText = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text;
            if (op.ShowDialog() == true)
            {

                File.WriteAllText(op.FileName, fileText);
            }
        }

        private void MenuItem_Click_Font(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();

            TextRange allTextRange = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd);
            
            
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, new FontFamily(fontDialog.Font.Name));
                RichTextBox.Selection.ApplyPropertyValue(RichTextBox.FontSizeProperty, (double)fontDialog.Font.Size);
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontStyleProperty, (fontDialog.Font.Style & System.Drawing.FontStyle.Italic) == System.Drawing.FontStyle.Italic ? FontStyles.Italic : FontStyles.Normal);
                RichTextBox.Selection.ApplyPropertyValue(TextElement.FontWeightProperty, (fontDialog.Font.Style & System.Drawing.FontStyle.Bold) == System.Drawing.FontStyle.Bold ? FontWeights.Bold : FontWeights.Normal);
                RichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, (fontDialog.Font.Underline) ? TextDecorations.Underline : null);
            }
        }
    }
}
