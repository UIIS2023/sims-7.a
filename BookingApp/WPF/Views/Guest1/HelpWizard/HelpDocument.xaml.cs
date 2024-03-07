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
using System.Windows.Xps.Packaging;

namespace BookingApp.WPF.Views.Guest1.HelpWizard
{
    /// <summary>
    /// Interaction logic for HelpDocument.xaml
    /// </summary>
    public partial class HelpDocument : Window
    {
        public HelpDocument()
        {
           
            InitializeComponent();
            this.DataContext = this;
        }

        private void LoadDocument(string documentName)
        {
            string strExeFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string strWorkPath = System.IO.Path.GetDirectoryName(strExeFilePath);
            string path = strWorkPath + "..\\..\\..\\..\\Resources\\XPS\\" + documentName; 

            XpsDocument xps = new XpsDocument(@path, System.IO.FileAccess.Read);
            Document1.Document = xps.GetFixedDocumentSequence();
        }

        private void Radio_Checked(object sender, RoutedEventArgs e)
        {
            switch ((sender as RadioButton).Name)
            {
                case "Radio1":
                    LoadDocument("HelpDoc1.xps");
                    break;
                case "Radio2":
                    LoadDocument("HelpDoc2.xps");
                    break;
                case "Radio3":
                    LoadDocument("HelpDoc3.xps");
                    break;
                case "Radio4":
                    LoadDocument("HelpDoc4.xps");
                    break;
                case "Radio5":
                    LoadDocument("HelpDoc5.xps");
                    break;
                case "Radio6":
                    LoadDocument("HelpDoc6.xps");
                    break;

            }
        }
    }
}
