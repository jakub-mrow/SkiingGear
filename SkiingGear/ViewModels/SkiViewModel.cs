using System.ComponentModel;
using Interfaces;

namespace SkiingGear.ViewModels
{
    public class SkiViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private ISkiis ski;
        
        public SkiViewModel(ISkiis ski)
        { 
            this.ski = ski; 
        }

        public int SkiId
        {
            get => ski.Id;
            set
            {
                ski.Id = value;
                OnPropertyChanged(nameof(SkiId));
            }
        }

        public string SkiModel
        {
            get => ski.Model;
            set
            {
                ski.Model = value;
                OnPropertyChanged(nameof(SkiModel));
                
            }
        }

        public string SkiBrand
        {
            get => ski.Brand.Name;
            set
            {
                ski.Brand.Name = value;
                OnPropertyChanged(nameof(SkiBrand));
            }
        }

        public Core.SkiType SkiType
        {
            get => ski.Type;
            set
            {
                ski.Type = value;
                OnPropertyChanged(nameof(SkiType));
            }
        }

        public int SkiLength
        {
            get => ski.Length;
            set
            {
                ski.Length = value;
                OnPropertyChanged(nameof(SkiLength));
            }
        }

        public int SkiPrice
        {
            get => ski.Price;
            set
            {
                ski.Price = value;
                OnPropertyChanged(nameof(SkiPrice));
            }
        }


        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
