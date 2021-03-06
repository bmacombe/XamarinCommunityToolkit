﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XamarinCommunityToolkit.Behaviors
{
    /// <summary>
    /// Validates the length of the text value in the Entry
    /// </summary>
    public class LengthValidationBehavior : BaseBehavior<Entry>
    {
        Color currentTextColor;

        /// <summary>
        /// Bindable maximum length of the Entry text
        /// </summary>
        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create(nameof(MaxLength), typeof(int), typeof(LengthValidationBehavior), int.MaxValue);

        /// <summary>
        /// Maximum length of the Entry text to validate against
        /// </summary>
        public int MaxLength
        {
            get => (int)GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        /// <summary>
        /// Bindable minimum length of the Entry text
        /// </summary>
        public static readonly BindableProperty MinLengthProperty =
            BindableProperty.Create(nameof(MinLength), typeof(int), typeof(LengthValidationBehavior), 0);

        /// <summary>
        /// Minimum length of the Entry text to validate against
        /// </summary>
        public int MinLength
        {
            get => (int)GetValue(MinLengthProperty);
            set => SetValue(MinLengthProperty, value);
        }

        /// <summary>
        /// Bindable text color to apply when validation fails
        /// </summary>
        public static readonly BindableProperty TextColorProperty =
            BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(NumericValidationBehavior));

        /// <summary>
        /// Text color to apply when validation fails
        /// </summary>
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        /// <summary>
        /// Attaches the OnEntryTextChanged handler to the TextChanged event
        /// </summary>
        /// <param name="bindable">Entry control to which the handler is attached</param>
        protected override void OnAttachedTo(Entry entry)
        {
            currentTextColor = entry.TextColor;
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        /// <summary>
        /// Detaches the OnEntryTextChanged handler from the TextChanged event
        /// </summary>
        /// <param name="bindable">Entry control from which the handler is detached</param>
        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        /// <summary>
        /// Handles the event when the text is changed on the Entry, 
        /// performs the validation if the Entry text length is between the MinLength and MaxLength, 
        /// and sets the text color
        /// </summary>
        /// <param name="sender">Entry control</param>
        /// <param name="e">Text changed event arguments</param>
        void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var isValid = (e.NewTextValue.Length >= MinLength && e.NewTextValue.Length <= MaxLength);

            if (sender is Entry entry)
                entry.TextColor = isValid ? currentTextColor : (TextColor != null ? TextColor : Color.Red);
        }
    }
}
