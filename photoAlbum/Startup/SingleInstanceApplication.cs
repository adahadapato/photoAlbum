


using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

namespace photoAlbum
{
    public class EntryPoint
    {
        public static string DataBasePath = @"E:\works\photoAlbum\ssce2022";
        public static string DestinationFilesPath;
        public static string PictureFilesFolder = @"J:\ssce2022";
        public static string PictureFilesFolder1 = @"J:\ssce2022_2";
        public static string PictureFilesFolder2 = @"J:\ssce2022_3";
        public const string ExaminationYear = "2022";
        public const string Examination = "Internal";
        public const string Null_passportPath = @"E:\works\photoAlbum\null_passport.jpg";
        [STAThread]
        public static void Main()
        {
            string[] args = Environment.GetCommandLineArgs();
            SingleInstanceController controller = new SingleInstanceController();
            controller.Run(args);
        }
    }

    public class SingleInstanceApplication : Application
    {
        //public readonly ToastViewModel _vm = new ToastViewModel();
        System.Collections.ObjectModel.ReadOnlyCollection<string> _commandLine;
        public SingleInstanceApplication(System.Collections.ObjectModel.ReadOnlyCollection<string> _commandLine)
        {
            this._commandLine = _commandLine;
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string fileName = "";
            if (_commandLine.Count > 1)
                fileName = _commandLine[1].ToString();
            else
                fileName = "";
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            MainWindow window = new MainWindow(fileName);

            window.Show();

        }
        public void Activate()
        {
            // Reactivate the main window
            MainWindow.Activate();
        }

        /// <summary>
        /// Loads a resource dictionary from a URI and merges it into the application resources.
        /// </summary>
        /// <param name="resourceLocater">URI of resource dictionary</param>
        public static void MergeResourceDictionary(Uri resourceLocater)
        {
            if (Application.Current != null)
            {
                var dictionary = (ResourceDictionary)Application.LoadComponent(resourceLocater);
                Application.Current.Resources.MergedDictionaries.Add(dictionary);
            }
        }
    }
    
}
