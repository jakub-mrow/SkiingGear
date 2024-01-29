using BL;
using Core;
using System.Data.Common;
using System.Reflection;
using System.Runtime.Intrinsics.Arm;
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

        //Skiis
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

        private void ConfirmSkiSearch(object sender, RoutedEventArgs e)
        {

            string filterValue = skiSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                SkiList.UpdateList(bl.GetAllSkiis());
                return;
            }

            SkiList.UpdateList(bl.FindSkiisByModel(filterValue));

        }

        private void RefreshFiltersSkiis(object sender, RoutedEventArgs e)
        {
            SkiList.UpdateList(bl.GetAllSkiis());
            skiSearchField.Clear();
        }


        private void ConfirmModelFilter(object sender, RoutedEventArgs e)
        {
            var selectedFilter = filterModels.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                SkiList.UpdateList(bl.GetAllSkiis());
            }

            string filterValue = filterModelsValue.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                SkiList.UpdateList(bl.GetAllSkiis());
            }

            switch (selectedFilter.Content.ToString())
            {

                case "Brand name":
                    if (filterValue == "")
                    {
                        SkiList.UpdateList(bl.GetAllSkiis());
                    }
                    else
                    {

                        SkiList.UpdateList(bl.FilterSkiisByBrandName(filterValue));
                    }
                    break;

                case "Type":
                    if (filterValue == "")
                    {
                        SkiList.UpdateList(bl.GetAllSkiis());
                    }
                    else
                    {
                        SkiType type;
                        Enum.TryParse<SkiType>(filterValue, out type);
                        SkiList.UpdateList(bl.FilterSkiisByType(type));
                    }
                    break;
            }

            if (ModelList.Items.Count > 0)
            {
                ModelList.SelectedItem = ModelList.Items[0];

            }

        }

        private void FilterModel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedFilter = (e.AddedItems[0] as ComboBoxItem)?.Content.ToString();

            switch (selectedFilter)
            {
                case "Brand name":
                    filterModelsValue.ItemsSource = bl.GetAllSkiis().Select(p => p.Brand.Name).Distinct();
                    break;
                case "Type":
                    filterModelsValue.ItemsSource = bl.GetAllSkiis().Select(p => p.Type.ToString()).Distinct();
                    break;
                default:
                    filterModelsValue.ItemsSource = null;
                    break;
            }

            filterModelsValue.SelectedItem = null;
        }


        #endregion

        //Brands
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

        private void ConfirmBrandSearch(object sender, RoutedEventArgs e)
        {

            string filterBrandValue = brandSearchField.Text;

            if (string.IsNullOrWhiteSpace(filterBrandValue))
            {
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                return;
            }

            SkiBrandsList.UpdateList(bl.FindSkiBrandByName(filterBrandValue));

        }

        private void RefreshFiltersBrands(object sender, RoutedEventArgs e)
        {
            SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
            brandSearchField.Clear();
        }

        private void ConfirmBrandFilter(object sender, RoutedEventArgs e)
        {
            var selectedFilter = filterBrands.SelectedItem as ComboBoxItem;

            if (selectedFilter == null)
            {
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
            }

            string filterValue = filterBrandsValue.Text;

            if (string.IsNullOrWhiteSpace(filterValue))
            {
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
            }

            switch (selectedFilter.Content.ToString())
            {

                case "Country":
                    if (filterValue == "")
                    {
                        SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                    }
                    else
                    {
                        SkiBrandsList.UpdateList(bl.FilterBrandsByCountry(filterValue));
                    }
                    break;

                case "Year":
                    if (filterValue == "")
                    {
                        SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                    }
                    else
                    {
                        int intFilterValue = int.Parse(filterValue);
                        SkiBrandsList.UpdateList(bl.FilterBrandsByYear(intFilterValue));
                    }
                    break;
            }

            if (BrandsList.Items.Count > 0)
            {
                BrandsList.SelectedItem = BrandsList.Items[0];

            }

        }

        private void FilterBrands_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            string selectedFilter = (e.AddedItems[0] as ComboBoxItem)?.Content.ToString();

            switch (selectedFilter)
            {
                case "Country":
                    filterBrandsValue.ItemsSource = bl.GetAllSkiBrands().Select(p => p.Country).Distinct();
                    break;
                case "Year":
                    filterBrandsValue.ItemsSource = bl.GetAllSkiBrands().Select(p => p.FoundationYear).Distinct();
                    break;
                default:
                    filterBrandsValue.ItemsSource = null;
                    break;
            }

            filterBrandsValue.SelectedItem = null;
        }

        #endregion

    }

}


