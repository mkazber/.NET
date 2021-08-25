using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace pizza
{

    public struct PizzaCosts
    {
        public const double mała = 24.90;
        public const double średnia = 34.90;
        public const double duża = 44.90;
    }

    public struct PizzaSizeNames
    {
        public const string małaName = "mała 30 cm - 24,90 zł";
        public const string średniaName = "średnia 45 cm - 34,90 zł";
        public const string dużaName = "duża 60 cm - 44,90 zł";
    }

    public struct AddonCosts
    {
        public const double Pieczarki = 2.00;
        public const double Papryka = 2.00;
        public const double Szynka = 2.00;
        public const double Ananas = 2.00;
        public const double Pomidor = 2.00;
        public const double Jajko = 3.00;
        public const double Anchois = 3.00;
        public const double SerFeta = 3.00;
        public const double Boczek = 5.00;
        public const double Oliwki = 5.00;
        public const double Kurczak = 5.00;
    }

    public struct AddonCostsNames
    {
        public const string Pieczarki = "2 zł Pieczarki ";
        public const string Papryka = "2 zł - Papryka ";
        public const string Szynka = "2 zł - Szynka ";
        public const string Ananas = "2 zł - Ananas ";
        public const string Pomidor = "2 zł - Pomidor ";
        public const string Jajko = "3 zł - Jajko ";
        public const string Anchois = "3 zł - Anchois ";
        public const string SerFeta = "3 zł - Ser Feta ";
        public const string Boczek = "5 zł - Boczek ";
        public const string Oliwki = "5 zł - Oliwki ";
        public const string Kurczak = "5 zł - Kurczak ";
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetDefaultSauceAndSize();
            Sum.Content = CalculatePizzaCost();
        }

        private void ResetSkladnikiText()
        {
            Skladniki.Content = "Składniki pizzy:";
        }

        private void SetDefaultSauceAndSize()
        {
            RadioButton sauce = SaucePanel.Children[0] as RadioButton;
            RadioButton size = SizePanel.Children[0] as RadioButton;
            ChozenSauce.Content = sauce.Content;
            ChosenSize.Content = size.Content.ToString().Split(' ')[0];
        }

        private string CalculatePizzaCost()
        {
            if (pizzaListBox.SelectedItems.Count == 0)
            {
                return "Łącznie: 29,90 zł (5zł dostawa)";
            }

            double cost = 0.0;

            var sizeChecked = SizePanel.Children.OfType<RadioButton>().FirstOrDefault(r => (bool)r.IsChecked);
            var addonsChecked = AddonsPanel.Children.OfType<CheckBox>().Where(f => (bool) f.IsChecked);
            switch (sizeChecked.Content)
            {
                case PizzaSizeNames.małaName:
                    cost += PizzaCosts.mała;
                    break;
                case PizzaSizeNames.średniaName:
                    cost += PizzaCosts.średnia;
                    break;
                case PizzaSizeNames.dużaName:
                    cost += PizzaCosts.duża;
                    break;
            }

            foreach (var checkBox in addonsChecked)
            {
                switch (checkBox.Content)
                {
                    case AddonCostsNames.Pieczarki:
                        cost += AddonCosts.Pieczarki;
                        break;
                    case AddonCostsNames.Papryka:
                        cost += AddonCosts.Papryka;
                        break;
                    case AddonCostsNames.Szynka:
                        cost += AddonCosts.Szynka;
                        break;
                    case AddonCostsNames.Ananas:
                        cost += AddonCosts.Ananas;
                        break;
                    case AddonCostsNames.Pomidor:
                        cost += AddonCosts.Pomidor;
                        break;
                    case AddonCostsNames.Jajko:
                        cost += AddonCosts.Jajko;
                        break;
                    case AddonCostsNames.Anchois:
                        cost += AddonCosts.Anchois;
                        break;
                    case AddonCostsNames.SerFeta:
                        cost += AddonCosts.SerFeta;
                        break;
                    case AddonCostsNames.Boczek:
                        cost += AddonCosts.Boczek;
                        break;
                    case AddonCostsNames.Oliwki:
                        cost += AddonCosts.Oliwki;
                        break;
                    case AddonCostsNames.Kurczak:
                        cost += AddonCosts.Kurczak;
                        break;
                }
            }
            if (cost <= 30.00)
                return "Łącznie: " + (cost + 5.00) + "0 zł (5zł dostawa)";
            else
                return "Łącznie: " + cost + "0 zł (darmowa dostawa)";
        }

        private void OnListBoxItemClick(object sender, MouseButtonEventArgs e)
        {
            if (ItemsControl.ContainerFromElement(pizzaListBox, e.OriginalSource as DependencyObject) is ListBoxItem item)
            {
                ResetSkladnikiText();
                switch (item.Content)
                {
                    case "Margherita":
                        Skladniki.Content += "\nsos, ser mozarrella, \noregano";
                        break;
                    case "Hawajska":
                        Skladniki.Content += "\nsos, ser mozarella, \nszynka, kukurydza, \nananas, oregano";
                        break;
                    case "Pepperoni":
                        Skladniki.Content += "\nsos, ser mozarella, \nsalami, papryka, \ncebula, oregano";
                        break;
                    case "Mexicana":
                        Skladniki.Content += "\nsos, ser mozarella, \nkukurydza, papryka jalapeno, \ncebula, tabasco";
                        break;
                    case "Spice Chicken":
                        Skladniki.Content += "\nsos, ser mozarella, \nkurczak grillowany, szynka, \ncebula, oregano";
                        break;
                    case "Kebab":
                        Skladniki.Content += "\nsos, ser mozarella, \nkebab z kurczaka, ogórek, \ncebula, gyros, oregano";
                        break;
                    case "Cheese":
                        Skladniki.Content += "\nsos, ser mozarella, \nser parmezan, ser feta, \nser pleśniowy";
                        break;
                    case "Vegetariana":
                        Skladniki.Content += "\nsos, ser mozarella, \noliwki, pieczarki, \npapryka, kukurydza, oregano";
                        break;
                    case "Napoletana":
                        Skladniki.Content += "\nsos, ser mozarella, \noliwki, kapary, \nanchois, bazylia";
                        break;
                }
                ChozenPizza.Content = item.Content;
            }
            Sum.Content = CalculatePizzaCost();
        }

        private void SauceChecked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RadioButton)
            {
                if (ChozenSauce != null)
                {
                    var button = e.OriginalSource as RadioButton;
                    ChozenSauce.Content = button.Content;
                }
            }
        }

        private void SizeChecked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is RadioButton)
            {
                if (ChosenSize != null)
                {
                    var button = e.OriginalSource as RadioButton;
                    ChosenSize.Content = button.Content.ToString().Split(' ')[0];
                    Sum.Content = CalculatePizzaCost();
                }
            }
        }

        private void OnAddonChecked(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox)
            {
                if (ChozenAddons != null)
                {
                    var addon = e.OriginalSource as CheckBox;
                    var addonContent = addon.Content.ToString();
                    int countOfAddons = ChozenAddons.Content.ToString().Count(f => f == '\n');
                    if (countOfAddons >= 4)
                    {
                        MessageBox.Show("Maksymalnie 4 dodatki", "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                        addon.IsChecked = false;
                        return;
                    }
                    if ((string)ChozenAddons.Content == "Brak")
                    {
                        ChozenAddons.Content = addonContent.Remove(addonContent.Length - 1) + "\n";
                    }
                    else
                    {
                        ChozenAddons.Content += addonContent.Remove(addonContent.Length - 1) + "\n";
                    }
                    Sum.Content = CalculatePizzaCost();
                }
            }
        }

        private void OnAddonUncheck(object sender, RoutedEventArgs e)
        {
            if (e.OriginalSource is CheckBox)
            {
                var addonsContainer = ChozenAddons.Content.ToString();
                var addon = e.OriginalSource as CheckBox;
                var addonContent = addon.Content.ToString().Remove(addon.Content.ToString().Length - 1);
                if (addonsContainer.Contains(addonContent))
                {
                    ChozenAddons.Content = addonsContainer.Replace(addonContent + "\n", "");
                }
                Sum.Content = CalculatePizzaCost();
            }
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void OnOrderClick(object sender, RoutedEventArgs e)
        {
            if (pizzaListBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Nie zaznaczyłeś pizzy!", "Alert", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

           

            var sum = Sum.Content.ToString().Replace("Razem: ", "");
            MessageBox.Show("Dziękujemy za złożenie zamówienia! \nKoszt całkowity: " + sum,
                "Alert", MessageBoxButton.OK, MessageBoxImage.Information);
        }

      
    }
}
