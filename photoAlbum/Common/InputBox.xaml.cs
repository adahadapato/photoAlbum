using photoAlbum.Tools;
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

namespace photoAlbum
{
    /// <summary>
    /// Interaction logic for InputBox.xaml
    /// </summary>
    public partial class InputBox : Window
    {
        private string Caption;
        private string _waterMark;
        private string result;
        public InputBox(string Caption, string waterMark = "")
        {
            InitializeComponent();
            this.Caption = Caption;
            _waterMark = waterMark;
            //this.message = message;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SafeGuiWpf.SetText(MessageTitle, this.Caption);
            //txtAnswer.Watermark = _waterMark;
            //SafeGuiWpf.SetText(txtMsg, message);
        }

        public string Result
        {
            get
            {
                return result;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = string.Empty;
            SafeGuiWpf.CloseWindow(this);
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            result = SafeGuiWpf.GetText(txtAnswer);
            SafeGuiWpf.CloseWindow(this);
        }
    }
}
