using MularczykMrowczynski.SkiingGear.Interfaces;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MularczykMrowczynski.SkiingGear.SkiingGear
{
    public partial class BrandForm : Window
    {
        public BrandForm()
        {
            InitializeComponent();
        }

        public BrandForm(ISkiBrand brand)
        {
            InitializeComponent();
            brandName.Text = brand.Name;
            brandCountry.Text = brand.Country;
            brandYear.Text = brand.FoundationYear.ToString();
        }

        private void btnFormOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            brandName.SelectAll();
            brandName.Focus();
        }

        public string BrandName
        {
            get { return brandName.Text; }
        }
        public string BrandCountry
        {
            get { return brandCountry.Text; }
        }

        public int BrandFoundationYear
        {
            get { return int.Parse(brandYear.Text); }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

