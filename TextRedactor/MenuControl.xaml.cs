using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using iTextSharp.text;
using System.Drawing;
using System.Drawing.Printing;

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
        string temp;
        private void MenuItem_Click_open(object sender, RoutedEventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            Microsoft.Win32.OpenFileDialog op = new Microsoft.Win32.OpenFileDialog();
            if (op.ShowDialog() == true)
            {
                if (System.IO.Path.GetExtension(op.FileName) == ".txt")
                {
                    temp = op.FileName;
                    filePath = op.FileName;
                    //MessageBox.Show(filePath);
                    var fileStream = op.OpenFile();
                    //TextRange textRange = new TextRange();
                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        fileContent = reader.ReadToEnd();

                        RichTextBox.Document.Blocks.Add(new Paragraph(new Run(fileContent)));
                    }
                }else if(System.IO.Path.GetExtension(op.FileName) == ".txt")
                {
                    LoadPdf(op.FileName);
                }
            }       
            
            
        }
        private void LoadPdf(string filename)
        {
            // Clear the existing text from the RichTextBox
            RichTextBox.Document.Blocks.Clear();

            // Load the PDF file
            using (PdfReader reader = new PdfReader(filename))
            {
                // Extract the text from each page of the PDF file
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    string text = PdfTextExtractor.GetTextFromPage(reader, i);
                    RichTextBox.AppendText(text);
                }
            }
        }

        private void MenuItem_Click_save(object sender, RoutedEventArgs e)
        {
            string filePath = temp;            
            string fileText = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text; 
            File.WriteAllText(filePath, fileText);
        }

        private void MenuItem_Click_saveAs(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog op = new Microsoft.Win32.SaveFileDialog();
            op.Filter = "PDF files (*.pdf)|*.pdf|Text files (*.txt)|*.txt|All files (*.*)|*.*";
            
            string fileText = new TextRange(RichTextBox.Document.ContentStart, RichTextBox.Document.ContentEnd).Text;
            if (op.ShowDialog() == true)
            {
             
                File.WriteAllText(op.FileName, fileText);
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MenuItem_Print(object sender, RoutedEventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();

            // Set the PrintPage event handler for the PrintDocument
            printDocument.PrintPage += new PrintPageEventHandler(PrintPageHandler);

            // Print the document
            printDocument.Print();

            // Define the PrintPage event handler
            private void PrintPageHandler(object sender, PrintPageEventArgs e)
            {
                // Get the contents of the RichTextBox
                string contents = richTextBox1.Text;

                // Create a new Font object
                Font font = new Font("Arial", 12);

                // Calculate the size of the text
                SizeF textSize = e.Graphics.MeasureString(contents, font);

                // Calculate the number of lines that will fit on the page
                int linesPerPage = (int)(e.MarginBounds.Height / textSize.Height);

                // Initialize the line count and the offset
                int lineCount = 0;
                int yOffset = 0;

                // Print the text to the page
                while (lineCount < linesPerPage && yOffset < richTextBox1.ClientSize.Height)
                {
                    string line = richTextBox1.Lines[lineCount];
                    yOffset = (int)(lineCount * textSize.Height);
                    e.Graphics.DrawString(line, font, Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top + yOffset, new StringFormat());
                    lineCount++;
                }

                // Check if there are more pages to print
                if (yOffset < richTextBox1.ClientSize.Height)
                {
                    e.HasMorePages = true;
                }
                else
                {
                    e.HasMorePages = false;
                }
            }
        }
    }
}
