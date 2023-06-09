using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace photoAlbum.Models
{
    public class FinModel : INotifyPropertyChanged
    {
        public string fileName { get; set; }
        public string schnum { get; set; }
        public string sch_name { get; set; }
        public string state_name { get; set; }
        public string cust_code { get; set; }
        public string custodian { get; set; }
        public string lga { get; set; }
        public int Candidates { get; set; }

        private bool isSelected;

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                this.isSelected = value;
                OnPropertyChanged(nameof(IsSelected));
            }
        }
    }
}
