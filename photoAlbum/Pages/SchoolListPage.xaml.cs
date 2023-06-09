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
    /// Interaction logic for SchoolListPage.xaml
    /// </summary>
    public partial class SchoolListPage : UserControl
    {
        ClickableHeaderListView clv = new ClickableHeaderListView();

        public SchoolListViewModel ViewModel
        {
            get { return DataContext as SchoolListViewModel; }
            set { DataContext = value; }
        }
        public SchoolListPage()
        {
            InitializeComponent();
            ViewModel = new SchoolListViewModel(this);
            this.DataContext = ViewModel;
        }

        private void lv_Data_Click(object sender, RoutedEventArgs e)
        {
            clv.Click(this, sender, e);
        }
    }
}
