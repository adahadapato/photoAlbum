using Apex.MVVM;
using photoAlbum.Activities;
using photoAlbum.Common;
using photoAlbum.Models;
using photoAlbum.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace photoAlbum.ViewModels
{
    public class PrintToPdfPageViewModel : BaseVM
    {
        

        private UserControl _page;


        int PageSize = 20;
        int PageNum = 0;
        int PageIndex = 0;
        int TotalRec = 0;
        int TotalPage = 0;
        int Status = 2;
        bool isLastPage = false;
        public PrintToPdfPageViewModel()
        {

        }

        public PrintToPdfPageViewModel(UserControl page)
        {
            _page = page;
           
            Task.Run(async () =>
            {
                await FetchStateToView();
            });
            //Search.ProgressChanged += UpdateValueInProgressBar(i);
        }


        private bool _checkAll;
        public bool CheckAll
        {
            get => _checkAll;
            set
            {
                SetValue(ref _checkAll, value);
                OnPropertyChanged(nameof(CheckAll));
                SelecteAll();
            }
        }


        public void SelecteAll()
        {
            if (SelectedFin == null)
                SelectedFin = new ObservableCollection<FinModel>();

            if (FinCollection == null) return;

            foreach (var f in FinCollection)
            {
                f.IsSelected = CheckAll;
                if (f.IsSelected)
                {
                    SelectedFin.Add(f);
                }
                else
                {
                    SelectedFin.Remove(f);
                }
            }
        }

        public ObservableCollection<FinModel> _finCollection;
        public ObservableCollection<FinModel> FinCollection
        {
            get { return _finCollection; }
            set
            {
                SetValue(ref _finCollection, value);
                OnPropertyChanged(nameof(FinCollection));
            }
        }

        public ObservableCollection<FinModel> _selectedFin;
        public ObservableCollection<FinModel> SelectedFin
        {
            get { return _selectedFin; }
            set
            {
                SetValue(ref _selectedFin, value);
                OnPropertyChanged(nameof(SelectedFin));
            }
        }


        public ObservableCollection<State> _stateCollection;
        public ObservableCollection<State> StateCollection
        {
            get { return _stateCollection; }
            set
            {
                SetValue(ref _stateCollection, value);
                OnPropertyChanged(nameof(StateCollection));
            }
        }

        State _selectedState;
        public State SelectedState
        {
            get { return _selectedState; }
            set
            {
                SetValue(ref _selectedState, value);
                OnPropertyChanged(nameof(_selectedState));
                {
                    LblTotal = SelectedState.Schools;
                    CheckAll = false;
                    Task.Run(async() =>
                    {
                        await FetchSchoolToView(SelectedState.Name);
                    });
                    
                }
            }
        }

        private int _lblSchools;
        public int LblSchools
        {
            get { return _lblSchools; }
            set
            {
                SetValue(ref _lblSchools, value);
                OnPropertyChanged(nameof(LblSchools));
            }
        }


        private int _lblTotal;
        public int LblTotal
        {
            get { return _lblTotal; }
            set
            {
                SetValue(ref _lblTotal, value);
                OnPropertyChanged(nameof(LblTotal));
            }
        }

        private int _Progress;
        public int Progress
        {
            get => _Progress;
            set
            { 
                _Progress = value;
                OnPropertyChanged(nameof(Progress));
            }
        }

        //public async void StartSearch(object obj)
        //{
        //    Search.ProgressChanged += UpdateValueInProgressBar();
        //    await Task.Run(() => search.SearchByName(string a);
        //    Search.ProgressChanged -= UpdateValueInProgressBar();
        //}

        private async Task FetchStateToView()
        {
            using (FetchDataClass fd = new FetchDataClass())
            {
                var s = await fd.ReadStateInfo();
                StateCollection = new ObservableCollection<State>(s);
            }
            
        }

        private async Task FetchSchoolToView(string v)
        {
            var schools = await PrintToPdf.GetTlbFiles(SelectedState.Code);
            if(schools==null || schools.Count==0)
            {
                SafeGuiWpf.ShowError($"No Alb files generated for state : {SelectedState.Name}");
                return;
            }

            LblSchools = schools.Where(x => x.schnum.Substring(0, 3) == SelectedState.Code).Count();

            FinCollection = new ObservableCollection<FinModel>(schools);
        }

        public ICommand PrintCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if (SelectedFin == null)
                    {
                        SafeGuiWpf.ShowError("Please select schools");
                        return;
                    }
                    //PrintToPdf.ProgressChanged += UpdateValueInProgressBar();
                    //await Task.Run(() => search.SearchByName(string a);
                    //PrintToPdf.ProgressChanged -= UpdateValueInProgressBar();
                    var schools = SelectedFin.Select(x => x.schnum).ToList();
                    await PrintToPdf.GeneratePdf(SelectedState.Name, schools);
                    //foreach (var p in SelectedFin)
                    //{
                    //    //await GenerateFiles.GenerateBySchool(p.schnum);
                    //    await PrintToPdf.GeneratePdf(SelectedState.Name, p.schnum);
                    //}

                    SelectedFin.Clear();
                    
                });
            }
        }

        public ICommand PrintAllCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (SelectedState == null)
                    {
                        SafeGuiWpf.ShowError("No State selected");
                        return;
                    }
                    await PrintToPdf.GeneratePdf(SelectedState.Name);
                });
            }
        }

        public ICommand LVPrintCommand
        {
            get
            {
                return new Command(async (object e) =>
                {
                    var model = e as FinModel;
                    await PrintToPdf.GeneratePdf(SelectedState.Name, model.schnum);
                    //await GenerateFiles.GenerateBySchool(model.schnum);
                });
            }
        }

        public ICommand CloseCommand
        {
            get
            {
                return new Command(() =>
                {
                    SafeGuiWpf.RemoveUserControlFromGrid((_page.Parent as Grid), _page);
                });
            }
        }

        public ICommand SelectFinCommand
        {
            get
            {
                return new Command((object e) =>
                {
                    if (e != null)
                    {
                        if (SelectedFin == null)
                            SelectedFin = new ObservableCollection<FinModel>();
                        var item = e as FinModel;
                        if (item != null)
                        {
                            if (item.IsSelected)
                            {
                                SelectedFin.Add(item);
                            }
                            else
                            {
                                SelectedFin.Remove(item);
                            }
                        }
                    }

                });
            }
        }
    }
}
