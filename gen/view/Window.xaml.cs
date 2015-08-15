using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace gen.view
{
    /// <summary>
    /// Interaction logic for Window.xaml
    /// </summary>
    public partial class MainView
    {
        private int iteration = 0;
        private Color[] _colorsToGenerateFrom;
        private GenerationManager manager;
        public MainView()
        {
            InitializeComponent();
            manager= new GenerationManager();

            _colorsToGenerateFrom = Helper.GenetareColors(9);
        }

        private new void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private bool IsTextAllowed(string text)
        {
            {
                var regex = new Regex("[^0-9]+");
                return !regex.IsMatch(text);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetOriginColor(sender, e);
            _colorsToGenerateFrom = manager.StartNewGenetics(Helper.GenetareColors(9));
            LowestFitness.Content = manager.fitness;
            SetColors();
            iteration = 1;
            Iteration.Content = iteration;
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            SetOriginColor(sender, e);
            _colorsToGenerateFrom = manager.ContinueGen(_colorsToGenerateFrom);
            LowestFitness.Content = manager.fitness;
            SetColors();
            iteration++;
            Iteration.Content = iteration;
        }
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            SetOriginColor(sender, e);
            for (int i = 0; i < 10000; i++)
            {
                _colorsToGenerateFrom = manager.ContinueGen(_colorsToGenerateFrom);
                iteration++;
                if (manager.fitness.StartsWith("0 -"))
                {
                    MessageBox.Show("znaleziono odpowiedni kolor");
                    break;
                }
            }
            LowestFitness.Content = manager.fitness;
            SetColors();
            Iteration.Content = iteration;
        }
        private void SetColors()
        {
            c1.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[0].R, _colorsToGenerateFrom[0].G,
                    _colorsToGenerateFrom[0].B));
            c2.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[1].R, _colorsToGenerateFrom[1].G,
                    _colorsToGenerateFrom[1].B));
            c3.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[2].R, _colorsToGenerateFrom[2].G,
                    _colorsToGenerateFrom[2].B));
            c4.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[3].R, _colorsToGenerateFrom[3].G,
                    _colorsToGenerateFrom[3].B));
            c5.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[4].R, _colorsToGenerateFrom[4].G,
                    _colorsToGenerateFrom[4].B));
            c6.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[5].R, _colorsToGenerateFrom[5].G,
                    _colorsToGenerateFrom[5].B));
            c7.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[6].R, _colorsToGenerateFrom[6].G,
                    _colorsToGenerateFrom[6].B));
            c8.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[7].R, _colorsToGenerateFrom[7].G,
                    _colorsToGenerateFrom[7].B));
            c9.Background =
                new SolidColorBrush(Color.FromRgb(_colorsToGenerateFrom[8].R, _colorsToGenerateFrom[8].G,
                    _colorsToGenerateFrom[8].B));
            l1.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[0]);
            l2.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[1]);
            l3.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[2]);
            l4.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[3]);
            l5.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[4]);
            l6.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[5]);
            l7.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[6]);
            l8.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[7]);
            l9.Content = Helper.ColorToRGBString(_colorsToGenerateFrom[8]);
        }

        private void SetOriginColor(object sender, RoutedEventArgs e)
        {
            if (manager!=null && oR != null && oG != null && oB != null)
            {
                manager.Target = Color.FromRgb(Helper.TextBoxToByte(oR), Helper.TextBoxToByte(oG),
                    Helper.TextBoxToByte(oB));
                OriginCanvas.Background = new SolidColorBrush(manager.Target);
            }
        }

    }
}
