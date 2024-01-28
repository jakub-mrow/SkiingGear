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

        #region Skiis
        private void SkiList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedSki((ViewModels.SkiViewModel)e.AddedItems[0]);
            }
        }

        private void ChangeSelectedSki(ViewModels.SkiViewModel skiViewModel)
        {
            selectedSki = skiViewModel;
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
                MessageBox.Show("Ski model is not selected!");
            }
        }

        #endregion

        #region Brands
        private void SkiBrandsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count != 0)
            {
                ChangeSelectedBrand((ViewModels.SkiBrandViewModel)e.AddedItems[0]);
            }
        }

        private void ChangeSelectedBrand(ViewModels.SkiBrandViewModel skiBrandViewModel)
        {
            selectedSkiBrand = skiBrandViewModel;
            DataContext = selectedSkiBrand;
        }

        private void RemoveBrand(object sender, RoutedEventArgs e)
        {
            if (selectedSkiBrand != null)
            {
                bl.RemoveSkiBrand(selectedSkiBrand.SkiBrandId);
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                SkiList.UpdateList(bl.GetAllSkiis());
                selectedSkiBrand = null;
            }
            else
            {
                MessageBox.Show("Ski brand is not selected!");
            }
        }


        #endregion

        #region search and filter 

        private void RefreshFiltersBrands(object sender, RoutedEventArgs e)
        {
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                brandSearchField.txtInput.Clear();
        }

        private void RefreshFiltersSkiis(object sender, RoutedEventArgs e)
        {
            SkiList.UpdateList(bl.GetAllSkiis());
            skiSearchField.txtInput.Clear();
        }

        private void ConfirmSkiSearch(object sender, RoutedEventArgs e)
        {

            string filterValue = skiSearchField.txtInput.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                SkiList.UpdateList(bl.GetAllSkiis());
                return;
            }

            SkiList.UpdateList(bl.FindSkiisByModel(filterValue));

        }

        private void ConfirmBrandSearch(object sender, RoutedEventArgs e)
        {

            string filterBrandValue = brandSearchField.txtInput.Text;

            if (string.IsNullOrWhiteSpace(filterBrandValue))
            {
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                return;
            }

            SkiBrandsList.UpdateList(bl.FindSkiBrandByName(filterBrandValue));

        }
        #endregion
    }





}


