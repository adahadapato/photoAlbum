using Apex.MVVM;
using photoAlbum.Activities;
using photoAlbum.Models;
using photoAlbum.Tools;
using photoAlbum.Utils;
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
    public class CreateFilesPageVM : BaseVM
    {
        public ObservableCollection<FinModel> _finCollection;
        public ObservableCollection<PageSizeModel> _pageSizeCollection;

        private UserControl _page;


        int PageSize = 20;
        int PageNum = 0;
        int PageIndex = 0;
        int TotalRec = 0;
        int TotalPage = 0;
        int Status = 2;
        bool isLastPage = false;

        public CreateFilesPageVM()
        {

        }

        public CreateFilesPageVM(UserControl page)
        {
            this._page = page;
            FetchPageSize();
            SelectedPageSize = new PageSizeModel()
            {
                size = PageSize
            };
            Task.Run(async() =>
            {
                await FetchSchoolToView("");
            });
            
        }

        public ObservableCollection<FinModel> FinCollection
        {
            get { return _finCollection; }
            set
            {
                SetValue(ref _finCollection, value);
                OnPropertyChanged(nameof(FinCollection));
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
                    FetchSchoolToView("");
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
                FetchSchoolToView(SearchText);
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

        private async void FetchPageSize()
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

        public ObservableCollection<PageSizeModel> PageSizeCollection
        {
            get { return _pageSizeCollection; }
            set
            {
                SetValue(ref _pageSizeCollection, value);
                OnPropertyChanged(nameof(PageSizeCollection));
            }
        }

        private async void FetchCustomerCount()
        {
            var results = 0;// await _repository.FetchCustomerCount();
            TotalRec = (int)results;
            PageIndex++;
            Display();
        }
        private void Display()
        {
            LblTotal = StrPaginationUtil.Display(TotalRec, PageSize, ref PageIndex, ref TotalPage, isLastPage);
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
                return new Command(async() =>
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
                return new Command(async() =>
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
                return new Command(async() =>
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
                return new Command(async() =>
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

        private async Task FetchSchoolToView(string v)
        {
            try
            {
                FetchDataClass fetchDataClass = new FetchDataClass();
                var result = await fetchDataClass.FetchSchoolsToPreview(v);
                FinCollection = new ObservableCollection<FinModel>(result);
                Display();
            }
            catch(Exception ex)
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
