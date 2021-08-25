using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;

namespace edytor_tekstowy
{
 
    public partial class MainWindow : Window
    {
        private bool hasTextChanged;
        private int newCounter = 1;
        private string tytul = "";

        public MainWindow()
        {
            InitializeComponent();
            cmbFontFamily.ItemsSource = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            cmbFontSize.ItemsSource = new List<double>() { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            
        }

        private void ShowToSaveMessage(string operation)
        {
            if (MessageBox.Show("Do you want to Save?", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";
                if (dlg.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Text);
                }
            }

            switch (operation)
            {
                case "New":
                    rtbEditor.SelectAll();
                    rtbEditor.Selection.Text = "";
                    Title = "Notatnik " + ++newCounter;
                    break;
                case "Exit":
                    Close();
                    break;
            }
        }

        private void rtbEditor_SelectionChanged(object sender, RoutedEventArgs e)
        {
            object temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontWeightProperty);
            btnBold.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontWeights.Bold));
            temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontStyleProperty);
            btnItalic.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(FontStyles.Italic));
            temp = rtbEditor.Selection.GetPropertyValue(Inline.TextDecorationsProperty);
            btnUnderline.IsChecked = (temp != DependencyProperty.UnsetValue) && (temp.Equals(TextDecorations.Underline));
            temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontFamilyProperty);
            cmbFontFamily.SelectedItem = temp;
            temp = rtbEditor.Selection.GetPropertyValue(TextElement.FontSizeProperty);
            cmbFontSize.Text = temp.ToString();
        }

        private void Open_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
            {
                ShowToSaveMessage("");
            }
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";
            if (dlg.ShowDialog() == true)
            {
                using FileStream fileStream = new FileStream(dlg.FileName, FileMode.Open);
                TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                range.Load(fileStream, DataFormats.Text);
                Title = dlg.FileName;
                hasTextChanged = false;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            /* if (hasTextChanged)
             {
                 SaveFileDialog dlg = new SaveFileDialog();
                 dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";

                 if (tytul != "")
                 {
                     using FileStream fileStream = new FileStream(tytul, FileMode.Create);
                     TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                     range.Save(fileStream, DataFormats.Text);
                     Title = dlg.FileName;
                     hasTextChanged = false;
                     return;
                 }
                 if (dlg.ShowDialog() == true)
                 {
                     using FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                     TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                     range.Save(fileStream, DataFormats.Text);
                     Title = dlg.FileName;
                     hasTextChanged = false;
                     tytul = dlg.FileName;
                 }
             }
             else
             {
                 using FileStream fileStream = new FileStream(Title, FileMode.Create);
                 TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                 range.Save(fileStream, DataFormats.Text);
             } */


            if (MessageBox.Show("Do you want to Save?", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";
                if (dlg.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Text);
                }
            }

        }

        private void Saveas_Executed(object sender, ExecutedRoutedEventArgs e)
        {
           if (MessageBox.Show("Do you want to Save?", "Confirm",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Text Document (*.txt)|*.txt|All files (*.*)|*.*";
                if (dlg.ShowDialog() == true)
                {
                    FileStream fileStream = new FileStream(dlg.FileName, FileMode.Create);
                    TextRange range = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd);
                    range.Save(fileStream, DataFormats.Text);
                }
            }

        }


        private void cmbFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string text = new TextRange(rtbEditor.Document.ContentStart, rtbEditor.Document.ContentEnd).Text;
            if (string.IsNullOrWhiteSpace(text))
            {
                rtbEditor.FontFamily = new FontFamily(cmbFontFamily.SelectedItem.ToString());
            }

            if (cmbFontFamily.SelectedItem != null)
            {
                rtbEditor.Selection.ApplyPropertyValue(TextElement.FontFamilyProperty, cmbFontFamily.SelectedItem);
            }
        }

        private void cmbFontSize_TextChanged(object sender, TextChangedEventArgs e)
        {
            rtbEditor.Selection.ApplyPropertyValue(TextElement.FontSizeProperty, cmbFontSize.Text);
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
                ShowToSaveMessage("Exit");
            else
                Close();
        }

        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (hasTextChanged)
                ShowToSaveMessage("New");
            else
            {
                rtbEditor.SelectAll();
                rtbEditor.Selection.Text = "";
                Title = "Notatnik " + ++newCounter;
            }

            hasTextChanged = false;
        }

        private void rtbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            hasTextChanged = true;
            if (!Title.EndsWith('*'))
                Title = Title.Insert(Title.Length, "*");
        }
    }
}