using MularczykMrowczynski.SkiingGear.Core;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace MularczykMrowczynski.SkiingGear.SkiingGear
{
    public partial class SkiForm : Window
    {
        public SkiForm(IEnumerable<string> skiBrands)
        {
            InitializeComponent();
            skiBrands.ToList().ForEach(f => skiBrand.Items.Add(f));
            if (skiBrand.Items.Count > 0)
            {
                skiBrand.SelectedIndex = 0;
            }
            skiType.ItemsSource = Enum.GetNames(typeof(SkiType));
            if (skiType.Items.Count > 0)
            {
                skiType.SelectedIndex = 0;
            }
        }

        public SkiForm(IEnumerable<string> skiBrands, Interfaces.ISkiis ski)
        {
            InitializeComponent();
            skiBrands.ToList().ForEach(f => skiBrand.Items.Add(f));

            for (int i = 0; i < skiBrands.Count(); i++)
            {
                if (skiBrands.ElementAt(i).Equals(ski.Brand.Name))
                {
                    skiBrand.SelectedIndex = i;
                    break;
                }
            }
            skiType.ItemsSource = Enum.GetNames(typeof(SkiType));
            if (skiType.Items.Count > 0)
            {
                skiType.SelectedIndex = 0;
            }
            skiModel.Text = ski.Model;
            skiType.SelectedIndex = (int)ski.Type;
            skiLength.Text = ski.Length.ToString();
            skiPrice.Text = ski.Price.ToString();

        }

        private void btnFormOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            skiModel.SelectAll();
            skiModel.Focus();
        }

        public string SkiModel
        {
            get { return skiModel.Text; }
        }

        public SkiType SkiType
        {
            get
            {
                return (SkiType)skiType.SelectedIndex;
            }
        }

        public int SkiLength
        {
            get
            {
                return int.Parse(skiLength.Text);
            }
        }

        public int SkiPrice
        {
            get
            {
                return int.Parse(skiPrice.Text);
            }
        }

        public string SkiBrand
        {
            get
            {
                return skiBrand.Text;
            }
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}


