using MularczykMrowczynski.SkiingGear.Core;
using MularczykMrowczynski.SkiingGear.DBMock;
using System.Windows;
using System.Windows.Controls;

namespace MularczykMrowczynski.SkiingGear.SkiingGear
{
    public partial class MainWindow : Window
    {
        public ViewModels.SkiBrandsViewModel SkiBrandsList { get; } = new ViewModels.SkiBrandsViewModel();
        private ViewModels.SkiBrandViewModel selectedSkiBrand = null;

        public ViewModels.SkiisViewModel SkiList { get; } = new ViewModels.SkiisViewModel();
        private ViewModels.SkiViewModel selectedSki = null;

        private readonly BL.BL bl;

        private string selectedDataSource = "SkiingGear.DBSQL.dll";
        //private string selectedDataSource = "DBMock.dll";
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
                bl.LoadLibrary("SkiingGear.DBSQL.dll");
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                SkiList.UpdateList(bl.GetAllSkiis());
                selectedDataSource = "SkiingGear.DBSQL.dll";
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

        private void UpdateSki(object sender, RoutedEventArgs e)
        {
            if (selectedSki != null)
            {
                SkiForm skiForm = new(
                    bl.GetAllBrandNames(),
                    bl.GetSkiis(selectedSki.SkiId).First()
                );

                if (skiForm.ShowDialog() == true)
                {
                    bl.UpdateSkiis(new SkiisDBMock()
                    {
                        Id = selectedSki.SkiId,
                        Model = skiForm.SkiModel,
                        Brand = bl.GetSkiBrandByName(skiForm.SkiBrand),
                        Type = skiForm.SkiType,
                        Length = skiForm.SkiLength,
                        Price = skiForm.SkiPrice,
                    });

                    SkiList.UpdateList(bl.GetAllSkiis());
                    ChangeSelectedSki(null);
                }
            }
            else
            {
                MessageBox.Show("Aircraft is not selected!");
            }
        }

        private void AddSki(object sender, RoutedEventArgs e)
        {
            var allBrandsNames = bl.GetAllBrandNames();
            SkiForm skiForm = new(allBrandsNames);

            if (skiForm.ShowDialog() == true)
            {
                SkiisDBMock ski;
                try
                {
                    ski = new SkiisDBMock()
                    {
                       Model = skiForm.SkiModel,
                       Brand = bl.GetSkiBrandByName(skiForm.SkiBrand), 
                       Type = skiForm.SkiType,
                       Length = skiForm.SkiLength,
                       Price = skiForm.SkiPrice,
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                bl.AddSkiis(ski);
                SkiList.UpdateList(bl.GetAllSkiis());
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
                    filterModelsValue.ItemsSource = bl.GetAllBrandNames();
                    break;
                case "Type":
                    filterModelsValue.ItemsSource = bl.GetAllSkiTypes();
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

        private void AddBrand(object sender, RoutedEventArgs e)
        {
            
            BrandForm brandForm = new();

            if (brandForm.ShowDialog() == true)
            {
                SkiBrandDBMock brand;
                try
                {
                    brand = new SkiBrandDBMock()
                    {
                        Name = brandForm.BrandName,
                        Country = brandForm.BrandCountry,
                        FoundationYear = brandForm.BrandFoundationYear
                    };
                }
                catch
                {
                    MessageBox.Show("Error occurred, check your input values!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                bl.AddSkiBrand(brand);
                SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
            }
        }

        private void UpdateBrand(object sender, RoutedEventArgs e)
        {
            if (selectedSkiBrand != null)
            {
                BrandForm brandForm = new(
                    bl.GetSkiBrand(selectedSkiBrand.SkiBrandId).First()
                );

                if (brandForm.ShowDialog() == true)
                {
                    bl.UpdateSkiBrand(new SkiBrandDBMock()
                    {
                        BrandId = selectedSkiBrand.SkiBrandId,
                        Name = brandForm.BrandName,
                        Country = brandForm.BrandCountry,
                        FoundationYear = brandForm.BrandFoundationYear
                    });

                    SkiBrandsList.UpdateList(bl.GetAllSkiBrands());
                    ChangeSelectedBrand(null);
                }
            }
            else
            {
                MessageBox.Show("Airbase is not selected!");
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
                    filterBrandsValue.ItemsSource = bl.GetAllCountries();  
                    break;
                case "Year":
                    filterBrandsValue.ItemsSource = bl.GetAllFoundationYears();
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


