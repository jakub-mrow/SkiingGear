using System.ComponentModel;
using MularczykMrowczynski.SkiingGear.Interfaces;


namespace MularczykMrowczynski.SkiingGear.SkiingGear.ViewModels
{
    public class SkiBrandViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private ISkiBrand skiBrand;

        public SkiBrandViewModel(ISkiBrand skiBrand) 
        {
            this.skiBrand = skiBrand;
        }

        public int SkiBrandId
        {
            get => skiBrand.BrandId;
            set
            {
                skiBrand.BrandId = value;
                OnPropertyChanged(nameof(SkiBrandId));
            }
        }

        public string SkiBrandName
        {
            get => skiBrand.Name;
            set
            {
                skiBrand.Name = value;
                OnPropertyChanged(nameof(SkiBrandName));
            }
        }

        public string SkiBrandCountry
        {
            get => skiBrand.Country;
            set
            {
                skiBrand.Country = value;
                OnPropertyChanged(nameof(SkiBrandCountry));
            }
        }
        public int SkiBrandFoundationYear
        {
            get => skiBrand.FoundationYear;
            set
            {
                skiBrand.FoundationYear = value;
                OnPropertyChanged(nameof(SkiBrandFoundationYear));
            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
