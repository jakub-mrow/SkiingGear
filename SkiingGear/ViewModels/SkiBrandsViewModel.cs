using System.Collections.ObjectModel;
using System.ComponentModel;
using Interfaces;

namespace SkiingGear.ViewModels
{
    public class SkiBrandsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public ObservableCollection<SkiBrandViewModel> SkiBrands { get; set;} = new ObservableCollection<SkiBrandViewModel>();

        public void UpdateList(IEnumerable<ISkiBrand> skiBrands)
        {
            try
            {
                SkiBrands.Clear();
                foreach (var brand in skiBrands)
                {
                    SkiBrands.Add(new SkiBrandViewModel(brand));
                }

                OnPropertyChanged(nameof(SkiBrands));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was an error loading ski brands: {ex.Message}");
            }
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
