using CrystalDecisions.CrystalReports.Engine;
using photoAlbum.Activities;
using photoAlbum.DB.Dal;
using photoAlbum.Models;
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
using System.IO;
using photoAlbum.Tools;
using Newtonsoft.Json;
using Microsoft.Win32;
using photoAlbum.Pages;

namespace photoAlbum
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string InFileName;
        string FileName;
        public MainWindow(string InFileName)
        {
            InitializeComponent();
            this.InFileName = InFileName;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //InFileName = "c:\\works\\photoalbum\\ssce2020\\abia\\0010002.alb";
            if (!string.IsNullOrWhiteSpace(InFileName))
            {
                var lastFolderName = System.IO.Path.GetDirectoryName(InFileName);
                //System.IO.Path.GetFileName(System.IO.Path.GetDirectoryName(InFileName));
                //MessageBox.Show(lastFolderName);
                //var fullPath = System.IO.Path.GetDirectoryName(InFileName);
                //MessageBox.Show(fullPath);
                //Application.Current.Shutdown(0);
                this.Title += " - " + lastFolderName;
                //PrepFiles(lastFolderName);
            }

        }

        /// <summary>
        /// Collapses all AccordionItems (if allowed).
        /// </summary>
        /// <param name="sender">Sender of the event (a Button).</param>
        /// <param name="e">Event arguments.</param>
        private void CollapseAll()
        {
            foreach (AccordionItem item in this.MainNav.Items)
            {
                if (!item.IsLocked)
                {
                    item.IsSelected = false;
                }
            }
        }

        /// <summary>
        /// Applies the Accordion's AutoExpand behavior.
        /// </summary>
        /// <param name="sender">Sender of the event (an AccordionItem).</param>
        /// <param name="e">Event arguments.</param>
        private void AccordionItem_MouseEnter(object sender, MouseEventArgs e)
        {

            AccordionItem item = sender as AccordionItem;
            item.IsSelected = true;

        }
       

       

        void ShowRPT(ReportDocument report)
        {
            //crv.ViewerCore.ReportSource = report;

        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }

        private void BtnDBFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog()
            {
                Description = "Data Base Folder"
            };
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                EntryPoint.DataBasePath = openFileDlg.SelectedPath;
                EntryPoint.DestinationFilesPath = EntryPoint.DataBasePath;
            }

            //txtsdatadetails.Text += EntryPoint.DataBasePath;
        }

        private void BtnDestinationFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog()
            {
                Description = "Files Destination folder",
                //RootFolder = Environment.SpecialFolder.Recent
            };
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                EntryPoint.DestinationFilesPath = openFileDlg.SelectedPath;
            }

        }

        private void BtnPicturesFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog()
            {
                Description = "Picture Files folder",
                
                //RootFolder = Environment.SpecialFolder.Recent
            };
            var result = openFileDlg.ShowDialog();
            if (result.ToString() != string.Empty)
            {
                EntryPoint.PictureFilesFolder = openFileDlg.SelectedPath;
            }

            //txtimagedetails.Text += EntryPoint.PictureFilesFolder;
        }

        private void BtnGenerateFiles_Click(object sender, RoutedEventArgs e)
        {
            SafeGuiWpf.AddUserControlToGrid<GenerateFilesPage>(content);
        }

        private void BtnGeneratePdf_Click(object sender, RoutedEventArgs e)
        {
            SafeGuiWpf.AddUserControlToGrid<PrintToPdfPage>(content);
        }

        private void BtnSchoolList_Click(object sender, RoutedEventArgs e)
        {
            SafeGuiWpf.AddUserControlToGrid<SchoolListPage>(content);
        }
    }
}
