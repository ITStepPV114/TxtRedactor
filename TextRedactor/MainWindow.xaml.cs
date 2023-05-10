using Org.BouncyCastle.Asn1.Cmp;
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
            var menuControl = new MenuControl(rtbEditor);
            mainPanel.Children.Add(menuControl); 
        }

        private void rbtEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            int countSymbol = rtbEditor.Selection.Text.Length;
			int countSlach = rtbEditor.Selection.Text.Split(new char[] {'\n'}).Length;
			lblCursorPosition.Text = "Line " + (countSlach) + ", Char " + (countSymbol);
            
		}


       
    }


}
