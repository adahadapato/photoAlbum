using CrystalDecisions.CrystalReports.Engine;
using photoAlbum.Activities;
using photoAlbum.Common;
using photoAlbum.DB.Dal;
using photoAlbum.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.Tools
{
    public class PrintToPdf
    {
       // public static event Action<string> ProgressChanged;
        public static async Task<List<FinModel>> GetTlbFiles(string state)
        {
            if(string.IsNullOrWhiteSpace(state))
            {
                SafeGuiWpf.ShowError($"Invalid state code {state}");
                return null;
            }
            var s = new State();
            using (FetchDataClass fd = new FetchDataClass())
            {
                s = await fd.ReadStateInfo(state);
                
            }

            if(s == null)
            {
                SafeGuiWpf.ShowError($"Invalid state code {state}");
                return null;
            }
            string _path = EntryPoint.DataBasePath;
            var _state = s.Name;// States.Where(x => x.Name.Equals(stateName))
                                  //.Select(x => x.Name).FirstOrDefault();

            var lastFolderName = $"{_path}\\{_state}";
            //this.Title = "Photo Album - " + state;
            if (!Directory.Exists(lastFolderName)) return null;

            var FileNames = Directory.GetFiles(lastFolderName, "*.alb");
            if (FileNames.Length == 0)
            {
                SafeGuiWpf.ShowError($"Files not found for state {_state}");
                return null;
            }
            var fin = new List<FinModel>();
            var schools = new List<FinModel>();
            using (FetchDataClass fd = new FetchDataClass())
            {
                schools = await fd.FetchSchoolsToPreview(state);
            }

            if(schools==null || schools.Count == 0)
            {
                SafeGuiWpf.ShowError($"No schools found for state {_state}");
                return null;
            }

            foreach (string f in FileNames)
            {
                var stateCode = System.IO.Path.GetFileNameWithoutExtension(f).Substring(0, 3);
                //string FolderName = string.Format($"{EntryPoint.DataBasePath}\\pdf\\{stateCode}_pdf");
                //if (!Directory.Exists(FolderName))
                //    Directory.CreateDirectory(FolderName);

                fin.Add(new FinModel
                {
                    fileName = Path.GetFileName(f),
                    schnum = schools.Where(x => x.schnum.Contains(Path.GetFileNameWithoutExtension(f))).Select(x => x.schnum).FirstOrDefault(),
                    sch_name = schools.Where(x => x.schnum.Contains(Path.GetFileNameWithoutExtension(f))).Select(x => x.sch_name).FirstOrDefault(),
                    Candidates = schools.Where(x => x.schnum.Contains(Path.GetFileNameWithoutExtension(f))).Select(x => x.Candidates).FirstOrDefault()
                });

                //var DestFileName = string.Format($"{FolderName}\\{System.IO.Path.GetFileNameWithoutExtension(f)}.pdf");
                //albumModel data;
                //using (FetchDataClass fd = new FetchDataClass())
                //{
                //    data = await fd.FetchAlbum(f);
                //}
                //ReportData(data, DestFileName);
            }
            return fin;
        }

        public static async Task GeneratePdf(string stateName, IEnumerable<string> Schools)
        {
            string _path = EntryPoint.DataBasePath;

            var state = stateName;

            var lastFolderName = $"{_path}\\{state}";
            //this.Title = "Photo Album - " + state;
            if (!Directory.Exists(lastFolderName)) return;
            foreach (var s in Schools)
            {
                await PrepFiles(lastFolderName, s);
            }
        }

        public static async Task GeneratePdf(string stateName, string SchoolNo)
        {
            await Task.Run(() =>
            {
                string _path = EntryPoint.DataBasePath;

                var state = stateName;// States.Where(x => x.Name.Equals(stateName))
                                      //.Select(x => x.Name).FirstOrDefault();

                var lastFolderName = $"{_path}\\{state}";
                //this.Title = "Photo Album - " + state;
                if (!Directory.Exists(lastFolderName)) return;
                PrepFiles(lastFolderName, SchoolNo);
            });
           
            //await Task.CompletedTask;
        }

        public static async Task GeneratePdf(string stateName)
        {
            string _path = EntryPoint.DataBasePath;
            
            //var path = "c:\\works\\photoalbum\\ssce2022\\";
            
            if (string.IsNullOrWhiteSpace(stateName))
            {
                var States = await GetStates();
                foreach (var s in States)
                {
                    var lastFolderName = $"{_path}\\{s.Name}";
                    //this.Title = "Photo Album - " + s.Name;
                    if (!Directory.Exists(lastFolderName)) continue;
                    PrepFiles(lastFolderName);
                }
            }
            else
            {
                var state = stateName;// States.Where(x => x.Name.Equals(stateName))
                           //.Select(x => x.Name).FirstOrDefault();

                var lastFolderName = $"{_path}\\{state}";
                //this.Title = "Photo Album - " + state;
                if (!Directory.Exists(lastFolderName)) return;
                PrepFiles(lastFolderName);
            }
        }

        private static async Task<List<State>> GetStates()
        {
            using (FetchDataClass fd = new FetchDataClass())
            {
                var s = await fd.ReadStateInfo();
                return s;
            }
        }

        async static Task PrepFiles(string lastFolderName, string SchoolNo=null)
        {
                var FileNames = (string.IsNullOrWhiteSpace(SchoolNo)) ? Directory.GetFiles(lastFolderName, "*.alb") : Directory.GetFiles(lastFolderName, $"{SchoolNo}*.alb");

            foreach (string f in FileNames)
            {
                //MessageBox.Show(f);
                //string FolderName = string.Format($"{System.IO.Path.GetDirectoryName(f)}\\pdf");
                var stateCode = System.IO.Path.GetFileNameWithoutExtension(f).Substring(0, 3);
                string FolderName = string.Format($"{EntryPoint.DataBasePath}\\pdf\\{stateCode}_pdf");
                if (!Directory.Exists(FolderName))
                    Directory.CreateDirectory(FolderName);



                var DestFileName = string.Format($"{FolderName}\\{System.IO.Path.GetFileNameWithoutExtension(f)}.pdf");
                albumModel data;
                using (FetchDataClass fd = new FetchDataClass())
                {
                    data = await fd.FetchAlbum(f);
                }
                ReportData(data, DestFileName);

                if (string.IsNullOrEmpty(SchoolNo))
                {
                    SafeGuiWpf.ShowSuccess(string.Format($"Export Completed for {lastFolderName}"));
                }
                else
                {   
                    SafeGuiWpf.ShowSuccess(string.Format($"Export Completed for {lastFolderName}\\{SchoolNo}"));
                }
                await Task.Delay(500);
            }
        }

        static void ReportData(albumModel model, string fileName)
        {
            try
            {
                CrystalReportDataLayer reportdata = new CrystalReportDataLayer();
                ReportDocument report = null;
                report = reportdata.GeneratePhotoAlbumDocument(model);
                report.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, fileName);
                report.Dispose();
                //ProgressChanged?.Invoke(model.schnum);
            }
            catch(Exception ex)
            {
                WpfMessageBox.Show("", ex.Message);
            }
        }
    }
}
