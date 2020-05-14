using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
using Tribalwars.UI.DeffRequester.Models;
using Tribalwars.UI.DeffRequester.Util;

namespace Tribalwars.UI.DeffRequester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<DeffRequestVillage> ReadVillages;
        public List<DeffRequestVillage> Villages;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnReadCode_Clicked(object sender, RoutedEventArgs e)
        {
            ReadVillages = ParseInputCode.Parse(TbUtRequestInput.Text);
            Villages = CopyList(ReadVillages);
            DataGridVillages.ItemsSource = Villages;
            int i = 0;
        }

        private void CbAll_OnChecked(object sender, RoutedEventArgs e)
        {
            if (Villages != null)
            {
                foreach (var village in Villages)
                {
                    village.IsSelected = true;
                    DataGridVillages.Items.Refresh();
                }
            }
        }

        private void CbAll_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (Villages != null)
            {
                foreach (var village in Villages)
                {
                    village.IsSelected = false;
                    DataGridVillages.Items.Refresh();
                }
            }
        }

        private void BtnGenerateCode_Clicked(object sender, RoutedEventArgs e)
        {
            List<int> deffRequested = new List<int>();
            if (CheckBoxUseRelativeDeffCount.IsChecked == true)
            {
                deffRequested.AddRange(Enumerable.Range(1 + (int)SliderRelativeDeff.Value, 10 + (int)SliderRelativeDeff.Value));
            }
            else
            {
                deffRequested.Add(int.Parse(Cb1Inc.Text));
                deffRequested.Add(int.Parse(Cb2Incs.Text));
                deffRequested.Add(int.Parse(Cb3Incs.Text));
                deffRequested.Add(int.Parse(Cb4Incs.Text));
                deffRequested.Add(int.Parse(Cb5Incs.Text));
                deffRequested.Add(int.Parse(Cb6Incs.Text));
                deffRequested.Add(int.Parse(Cb7Incs.Text));
                deffRequested.Add(int.Parse(Cb8Incs.Text));
                deffRequested.Add(int.Parse(Cb9Incs.Text));
                deffRequested.Add(int.Parse(Cb10Incs.Text));
            }
            string output = GenerateOutputCode.Generate(TbFormatTemplate.Text, Villages, deffRequested);
            TbOutput.Text = output;
        }

        private void BtnCopyCode_Clicked(object sender, RoutedEventArgs e)
        {
            Clipboard.SetText(TbOutput.Text);
        }

        private void TbOutput_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TbOutput.Text.Count(f => f == '[') > 0)
                TbBracketCount.Text = "Anzahl an \"[\"-Klammern: " + TbOutput.Text.Count(f => f == '[').ToString();
            else TbBracketCount.Text = "";
        }

        private void BtnUpdateFilter_Clicked(object sender, RoutedEventArgs e)
        {
            Villages = CopyList(ReadVillages);
            foreach (DeffRequestVillage village in Villages)
            {
                village.Attacks = village.Attacks.Where(a =>
                    a.Arrival >= DpStart.SelectedDate && a.Arrival.Date <= DpEnd.SelectedDate).ToList();
            }
            Villages = Villages.Where(v => v.Attacks.Count > 0).ToList();
            DataGridVillages.ItemsSource = Villages;
        }

        private List<DeffRequestVillage> CopyList(List<DeffRequestVillage> input)
        {
            List<DeffRequestVillage> temp = new List<DeffRequestVillage>();
            foreach (var village in input)
            {
                temp.Add(new DeffRequestVillage(village));
            }
            return temp;
        }
    }
}
