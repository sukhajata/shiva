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

namespace ShivaWPF3
{
    /// <summary>
    /// Interaction logic for XMLViewer.xaml
    /// </summary>
    public partial class XMLViewer : Window
    {
        public XMLViewer()
        {
            InitializeComponent();
        }

        public void SetText(string txt)
        {
            txtXML.Text = txt;
        }
    }
}
