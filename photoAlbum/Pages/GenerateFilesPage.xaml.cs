using photoAlbum.Tools;
using photoAlbum.ViewModels;
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

namespace photoAlbum.Pages
{
    /// <summary>
    /// Interaction logic for GenerateFilesPage.xaml
    /// </summary>
    public partial class GenerateFilesPage : UserControl
    {
        ClickableHeaderListView clv = new ClickableHeaderListView();

        public CreateFilesPageVM ViewModel
        {
            get { return DataContext as CreateFilesPageVM; }
            set { DataContext = value; }
        }
        public GenerateFilesPage()
        {
            InitializeComponent();
            ViewModel = new CreateFilesPageVM(this);
            this.DataContext = ViewModel;
        }

        private void lv_Data_Click(object sender, RoutedEventArgs e)
        {
            clv.Click(this, sender, e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
