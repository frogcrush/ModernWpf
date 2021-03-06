﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using ModernWpf.Controls;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Windows.Globalization.NumberFormatting;

namespace ModernWpfTestApp
{
    [TopLevelTestPage(Name = "NumberBox")]
    public sealed partial class NumberBoxPage : TestPage
    {
        public NumberBoxPage()
        {
            InitializeComponent();
        }

        private void SpinMode_Changed(object sender, RoutedEventArgs e)
        {
            if (TestNumberBox != null)
            {
                if (SpinModeComboBox.SelectedIndex == 0)
                {
                    TestNumberBox.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Hidden;
                }
                else if (SpinModeComboBox.SelectedIndex == 1)
                {
                    TestNumberBox.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Compact;
                }
                else if (SpinModeComboBox.SelectedIndex == 2)
                {
                    TestNumberBox.SpinButtonPlacementMode = NumberBoxSpinButtonPlacementMode.Inline;
                }
            }
        }

        private void Validation_Changed(object sender, RoutedEventArgs e)
        {
            if (TestNumberBox != null)
            {
                if (ValidationComboBox.SelectedIndex == 0)
                {
                    TestNumberBox.ValidationMode = NumberBoxValidationMode.InvalidInputOverwritten;
                }
                else if (ValidationComboBox.SelectedIndex == 1)
                {
                    TestNumberBox.ValidationMode = NumberBoxValidationMode.Disabled;
                }
            }
        }

        private void MinCheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            MinNumberBox.IsEnabled = (bool)MinCheckBox.IsChecked;
            MinValueChanged(null, null);
        }

        private void MaxCheckBox_CheckChanged(object sender, RoutedEventArgs e)
        {
            MaxNumberBox.IsEnabled = (bool)MaxCheckBox.IsChecked;
            MaxValueChanged(null, null);
        }

        private void MinValueChanged(object sender, object e)
        {
            if (TestNumberBox != null)
            {
                TestNumberBox.Minimum = (bool)MinCheckBox.IsChecked ? MinNumberBox.Value : double.MinValue;
            }
        }

        private void MaxValueChanged(object sender, object e)
        {
            if (TestNumberBox != null)
            {
                TestNumberBox.Maximum = (bool)MaxCheckBox.IsChecked ? MaxNumberBox.Value : double.MaxValue;
            }
        }

        private void NumberBoxValueChanged(object sender, NumberBoxValueChangedEventArgs e)
        {
            if (TestNumberBox != null && NewValueTextBox != null && OldValueTextBox != null)
            {
                NewValueTextBox.Text = e.NewValue.ToString();
                OldValueTextBox.Text = e.OldValue.ToString();
            }
        }

        private void CustomFormatterButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> languages = new List<string>() { "fr-FR" };
            DecimalFormatter formatter = new DecimalFormatter(languages, "FR");
            formatter.IntegerDigits = 1;
            formatter.FractionDigits = 2;
            TestNumberBox.NumberFormatter = new WinRTNumberFormatter(formatter);
        }

        private void SetTextButton_Click(object sender, RoutedEventArgs e)
        {
            TestNumberBox.Text = "15";
        }

        private void SetValueButton_Click(object sender, RoutedEventArgs e)
        {
            TestNumberBox.Value = 42;
        }

        private void SetNaNButton_Click(object sender, RoutedEventArgs e)
        {
            TestNumberBox.Value = double.NaN;
        }

        private void ScrollviewerWithScroll_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.VerticalChange != 0)
            {
                VerticalOffsetDisplayBlock.Text = (sender as ScrollViewer).VerticalOffset.ToString();
            }
        }

        private class WinRTNumberFormatter : INumberBoxNumberFormatter
        {
            private readonly DecimalFormatter _formatter;

            public WinRTNumberFormatter(DecimalFormatter formatter)
            {
                _formatter = formatter;
            }

            public string FormatDouble(double value)
            {
                return _formatter.FormatDouble(value);
            }

            public double? ParseDouble(string text)
            {
                return _formatter.ParseDouble(text);
            }
        }
    }
}