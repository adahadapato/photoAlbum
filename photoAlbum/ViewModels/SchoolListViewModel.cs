using photoAlbum.Activities;
using photoAlbum.Models;
using photoAlbum.Utils;
using Apex.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using photoAlbum.Tools;
using photoAlbum.Common;

namespace photoAlbum.ViewModels
{
    public class SchoolListViewModel : BaseVM
    {
        
       

        private UserControl _page;


        int PageSize = 20;
        int PageNum = 0;
        int PageIndex = 1;
        int TotalRec = 0;
        int TotalPage = 0;
        int Status = 2;
        bool isLastPage = false;
        public SchoolListViewModel()
        {
            //FetchPageSize();
            //SelectedPageSize = new PageSizeModel()
            //{
            //    size = PageSize
            //};
            ////Task.Run(async () =>
            ////{
            ////    await FetchSchoolToView("");
            ////});
        }

        public SchoolListViewModel(UserControl page)
        {
            this._page = page;
            Task.Run(async () =>
            { 
                await FetchPageSize();
                //SelectedPageSize = new PageSizeModel { size = PageSize };
            });
            Task.Run(async () =>
            {
                await FetchStateToView();
            });
            Task.Run(async () =>
            {
                
                if (FinCollection == null)
                    await FetchSchoolToView("");
            });
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


        PageSizeModel _selectedPageSize;
        public PageSizeModel SelectedPageSize
        {
            get { return _selectedPageSize; }
            set
            {
                SetValue(ref _selectedPageSize, value);
                OnPropertyChanged(nameof(SelectedPageSize));
                {
                    PageSize = SelectedPageSize.size;
                    Task.Run(async() =>
                    {
                        await FetchSchoolToView("");
                    });
                    
                    Display();
                }
            }
        }

        private string _searchText;
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                SetValue(ref _searchText, value);
                OnPropertyChanged(nameof(SearchText));

                Task.Run(async () =>
                {
                    var stateCode = (SelectedState == null) ? "" : SelectedState?.Code;
                    await FetchSchoolToView(stateCode, SearchText);
                });
            }
        }

        private string _lblTotal;
        public string LblTotal
        {
            get { return _lblTotal; }
            set
            {
                SetValue(ref _lblTotal, value);
                OnPropertyChanged(nameof(LblTotal));
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
                    Task.Run(async () =>
                    {
                        CheckAll = false;
                        LblSchools = SelectedState.Schools;
                        await FetchSchoolToView(SelectedState.Code);
                    });
                    
                }
            }
        }

        private async Task FetchStateToView()
        {
            using (FetchDataClass fd = new FetchDataClass())
            {
                var s = await fd.ReadStateInfo();
                StateCollection = new ObservableCollection<State>(s);
            }

        }

        private async Task FetchPageSize()
        {
            using (FetchDataClass fd = new FetchDataClass())
            {
                var result = await fd.FetchPageSize();
                if (result != null)
                {
                    PageSizeCollection = new ObservableCollection<PageSizeModel>(result);
                }
            }
        }

        public ObservableCollection<PageSizeModel> _pageSizeCollection;
        public ObservableCollection<PageSizeModel> PageSizeCollection
        {
            get { return _pageSizeCollection; }
            set
            {
                SetValue(ref _pageSizeCollection, value);
                OnPropertyChanged(nameof(PageSizeCollection));
               
            }
        }

        private void Display()
        {
            LblTotal = StrPaginationUtil.Display(TotalRec, PageSize, ref PageIndex, ref TotalPage, isLastPage);
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async() =>
                {
                    await FetchSchoolToView("");
                });
            }
        }

        /// <summary>
        /// Selects the schools to be generated for
        /// </summary>
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


        /// <summary>
        /// Genertes adb files for selecte schools
        /// </summary>
        /// <returns></returns>
        private async Task GenerateSelected()
        {
            if(SelectedFin == null)
            {
                SafeGuiWpf.ShowError("Please select schools");
                return;
            }

            if (!SelectedFin.Any())
            {
                SafeGuiWpf.ShowError("Please select schools");
                return;
            }

            LongActionDialog.ShowDialog("Generating ... ", Task.Run(async () =>
            {

                foreach (var p in SelectedFin)
                {
                    await GenerateFiles.GenerateBySchool(p.schnum);
                   
                }
            }));
            //RefreshAsync();
            SelectedFin.Clear();
            await Task.CompletedTask;
        }

        /// <summary>
        /// Generates alb files by states
        /// </summary>
        public ICommand GenerateCommand
        {
            get
            {
                return new Command(async() =>
                {
                    if (SelectedState == null)
                    {
                        SafeGuiWpf.ShowError("Please select state");
                        return;
                    }
                    await GenerateSelected();
                });
            }
        }

        /// <summary>
        /// Generates file for single school
        /// </summary>
        public ICommand LVGenerateCommand
        {
            get
            {
                return new Command(async(object e) =>
                {
                    var model = e as FinModel;
                    await GenerateBySchool(model.schnum);
                });
            }
        }


        private async Task GenerateBySchool(string schnum)
        {
            if (SelectedFin == null)
            {
                SafeGuiWpf.ShowError("Please select schools");
                return;
            }

            if (!SelectedFin.Any())
            {
                SafeGuiWpf.ShowError("Please select schools");
                return;
            }

            LongActionDialog.ShowDialog("Generating ... ", Task.Run(async () =>
            {

                foreach (var p in SelectedFin)
                {
                    await GenerateFiles.GenerateBySchool(schnum);

                }
            }));
            //RefreshAsync();
            SelectedFin.Clear();
            await Task.CompletedTask;
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

        #region Page Navigation Commands
        public ICommand FirstCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (PageNum == 0)
                    {
                        SafeGuiWpf.ShowWarning("Begining of Page Encountered!");
                        return;
                    }
                    PageNum = 0;
                    isLastPage = false;
                    PageIndex = 1;
                    await FetchSchoolToView("");
                    Display();
                });
            }
        }

        public ICommand PreviousCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (PageNum == 0)
                    {
                        SafeGuiWpf.ShowWarning("Begining of Page Encountered!");
                        return;
                    }
                    PageNum -= PageSize;
                    isLastPage = false;
                    PageIndex--;
                    await FetchSchoolToView("");
                    Display();
                });
            }
        }

        public ICommand NextCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (PageIndex == TotalPage)
                    {
                        SafeGuiWpf.ShowWarning("End of Page Encountered!");
                        return;
                    }
                    PageNum += PageSize;
                    isLastPage = false;
                    PageIndex++;
                    await FetchSchoolToView("");
                    Display();
                });
            }
        }

        public ICommand LastCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (PageIndex == TotalPage)
                    {
                        SafeGuiWpf.ShowWarning("End of Page Encountered!");
                        return;
                    }
                    PageNum = PageSize * (TotalPage - 1);
                    PageIndex = TotalPage;
                    isLastPage = true;
                    await FetchSchoolToView("");
                    Display();
                });
            }
        }

        private async Task FetchRecordCount()
        {
            var result = FinCollection.Count;
            TotalRec = result;
            //PageIndex++;
            Display();
        }

        private async Task FetchSchoolToView(string StateCode, string Search)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(EntryPoint.DataBasePath))
                {
                    SafeGuiWpf.ShowWarning("Please select data location");
                    return;
                }
                if (string.IsNullOrWhiteSpace(EntryPoint.DestinationFilesPath))
                    EntryPoint.DestinationFilesPath = EntryPoint.DataBasePath;

                FetchDataClass fetchDataClass = new FetchDataClass();
                var result = await fetchDataClass.FetchSchoolsToPreview(StateCode, Search);

                //Display();
                FinCollection = new ObservableCollection<FinModel>(result);
                await FetchRecordCount();
            }
            catch (Exception ex)
            {
                SafeGuiWpf.ShowError(ex.Message);
            }

            //SafeGuiWpf.SetItems<>
            //var results = 0;// await _repository.FetchCustomerCount();
            //TotalRec = (int)results;
            //PageIndex++;

            await Task.CompletedTask;
        }

        private async Task FetchSchoolToView(string v)
        {
            try
            {
                if(string.IsNullOrWhiteSpace(EntryPoint.DataBasePath))
                {
                    SafeGuiWpf.ShowWarning("Please select data location");
                    return;
                }
                if (string.IsNullOrWhiteSpace(EntryPoint.DestinationFilesPath))
                    EntryPoint.DestinationFilesPath = EntryPoint.DataBasePath;

                FetchDataClass fetchDataClass = new FetchDataClass();
                var result = await fetchDataClass.FetchSchoolsToPreview(v);

                //Display();
                FinCollection = new ObservableCollection<FinModel>(result);
                await FetchRecordCount();
            }
            catch (Exception ex)
            {
                SafeGuiWpf.ShowError(ex.Message);
            }

            //SafeGuiWpf.SetItems<>
            //var results = 0;// await _repository.FetchCustomerCount();
            //TotalRec = (int)results;
            //PageIndex++;

            await Task.CompletedTask;
        }
        #endregion
    }
}
