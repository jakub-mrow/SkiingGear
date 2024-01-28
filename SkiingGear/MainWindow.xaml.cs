using BL;
using System.Data.Common;
using System.Windows;
using System.Windows.Controls;

namespace SkiingGear
{
    public partial class MainWindow : Window
    {
        public ViewModels.SkiBrandsViewModel SkiBrandsList { get; } = new ViewModels.SkiBrandsViewModel();
        private ViewModels.SkiBrandViewModel selectedSkiBrand = null;

        public ViewModels.SkiisViewModel SkiList { get; } = new ViewModels.SkiisViewModel();
        private ViewModels.SkiViewModel selectedSki = null;

        private readonly BL.BL bl;

        private string selectedDataSource = "DBMock.dll";
        public MainWindow()
        {
            bl = new BL.BL(selectedDataSource);

            SkiBrandsList.UpdateList(bl.GetAllSkiBrands().Distinct());
            SkiList.UpdateList(bl.GetAllSkiis());

            InitializeComponent();


        }

        private void ChangeDataSource(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.LoadLibrary("DBMock.dll");
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                SkiList.UpdateList(bl.GetAllSkiis());
                selectedDataSource = "DBMock.dll";
            }
            catch
            {
                MessageBox.Show("Error occurred with input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                bl.LoadLibrary(selectedDataSource);
            }
        }

        private void SkiList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedSki((ViewModels.SkiViewModel)e.AddedItems[0]);
            }
        }

        private void ChangeSelectedSki(ViewModels.SkiViewModel skiModelViewModel)
        {
            selectedSki = skiModelViewModel;
            DataContext = selectedSki;
        }

        private void RemoveSki(object sender, RoutedEventArgs e)
        {
            if (selectedSki != null)
            {
                bl.RemoveSkiis(selectedSki.SkiId);
                SkiList.UpdateList(bl.GetAllSkiis());
                selectedSki = null;
            }
            else
            {
                MessageBox.Show("Aircraft is not selected!");
            }
        }
    }



}


