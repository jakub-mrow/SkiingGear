using System.ComponentModel;
using System.Collections.ObjectModel;
using MularczykMrowczynski.SkiingGear.Interfaces;

namespace MularczykMrowczynski.SkiingGear.SkiingGear.ViewModels
{
    public class SkiisViewModel : INotifyPropertyChanged
    {
       public event PropertyChangedEventHandler? PropertyChanged;
       public ObservableCollection<SkiViewModel> Skiis { get; set; } = new ObservableCollection<SkiViewModel>();

        public void UpdateList(IEnumerable<ISkiis> skiis)
        {
            try
            {
                Skiis.Clear();
                foreach (var ski in skiis)
                {
                    Skiis.Add(new SkiViewModel(ski));
                }

                OnPropertyChanged(nameof(Skiis));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error loading skiis: {ex.Message}");
            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 
