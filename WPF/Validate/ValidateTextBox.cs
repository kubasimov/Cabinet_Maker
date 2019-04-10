﻿using System;
using System.Globalization;
using System.Windows.Controls;

namespace WPF.Validate
{
    public class ValidateTextBox : ValidationRule
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int intValue;

            string text = String.Format("Must be between {0} and {1}",
                MinValue, MaxValue);

            if (!int.TryParse(value.ToString(), out intValue))
                return new ValidationResult(false, "Not an integer");
            if (intValue < MinValue)
                return new ValidationResult(false, "To small. " + text);
            if (intValue > MaxValue)
                return new ValidationResult(false, "To large. " + text);
            return ValidationResult.ValidResult;
        }
    }
    }
