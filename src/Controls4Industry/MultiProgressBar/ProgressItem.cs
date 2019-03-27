using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Brush = System.Windows.Media.Brush;
using Brushes = System.Windows.Media.Brushes;

namespace C4I
{
    /// <summary>
    /// Represents one of the processes in a <see cref="MultiProgressBar"/>
    /// </summary>
    public class ProgressItem : Control, INotifyPropertyChanged
    {
        #region ValueChanged-Event

        /// <summary>
        /// Notify interested Parties about a value-change
        /// </summary>
        public event EventHandler ValueChanged;

        private void RaiseValueChanged()
        {
            ValueChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Properties with DependencyProperties

        /// <summary>
        /// The name, describing the corresponding progress. 
        /// </summary>
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set
            {
                SetValue(TextProperty, value);
                RaisePropertyChanged("Text");
            }
        }

        /// <summary>
        /// <see cref="Text"/>
        /// </summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(ProgressItem), new PropertyMetadata(string.Empty));

        private readonly DependencyPropertyDescriptor _textDescriptor = DependencyPropertyDescriptor.FromProperty(TextProperty, typeof(ProgressItem));


        /// <summary>
        /// Brush to paint text inside the ProgressBar segment
        /// </summary>
        public Brush TextColor
        {
            get { return (Brush)GetValue(TextColorProperty); }
            set
            {
                SetValue(TextColorProperty, value);
                RaisePropertyChanged("TextColor");
            }
        }

        /// <summary>
        /// <see cref="TextColor"/>
        /// </summary>
        public static readonly DependencyProperty TextColorProperty =
            DependencyProperty.Register("TextColor", typeof(Brush), typeof(ProgressItem), new PropertyMetadata(Brushes.Transparent));

        /// <summary>
        /// Determines wether the text is visible
        /// </summary>
        public bool TextVisibility
        {
            get
            {
                return (bool)GetValue(TextVisibilityProperty);
            }
            set
            {
                SetValue(TextVisibilityProperty, value);
                RaisePropertyChanged("TextVisibility");
            }
        }

        /// <summary>
        /// <see cref="TextVisibility"/>
        /// </summary>
        public static readonly DependencyProperty TextVisibilityProperty =
            DependencyProperty.Register("TextVisibility", typeof(bool), typeof(ProgressItem), new PropertyMetadata(false));

        /// <summary>
        /// As long as this segment has values, the ProgressBar should display a glow effect, 
        /// even if the ProgreeBar-Value has reached the maximum.
        /// The is usefull e.g. for "Completed", "Failed", "Running" where the glow effect should be active as long as "Running" has values
        /// </summary>
        public bool KeepGlowEffect
        {
            get { return (bool)GetValue(KeepGlowEffectProperty); }
            set
            {
                SetValue(KeepGlowEffectProperty, value);
                RaisePropertyChanged("KeepGlowEffect");
            }
        }

        /// <summary>
        /// <see cref="KeepGlowEffect"/>
        /// </summary>
        public static readonly DependencyProperty KeepGlowEffectProperty =
            DependencyProperty.Register("KeepGlowEffect", typeof(bool), typeof(ProgressItem), new PropertyMetadata(false));

        /// <summary>
        /// The color of the progress item, shown in the MultiProgressBar
        /// </summary>
        public Brush Brush
        {
            get { return (Brush)GetValue(BrushProperty); }
            set
            {
                SetValue(BrushProperty, value);
                RaisePropertyChanged("Brush");
            }
        }

        /// <summary>
        /// <see cref="Brush"/>
        /// </summary>
        public static readonly DependencyProperty BrushProperty =
            DependencyProperty.Register("Brush", typeof(Brush), typeof(ProgressItem), new PropertyMetadata(Brushes.Green));

        /// <summary>
        /// Value to describe the progress of this ProgressItem. Set to zero for negtiv values.
        /// </summary>
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set
            {
                SetValue(ValueProperty, value);
            }
        }

        /// <summary>
        /// <see cref="Value"/>
        /// </summary>
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int), typeof(ProgressItem), new PropertyMetadata(0, OnValuePropertyChanged, OnCoerceValue));

        private static object OnCoerceValue(DependencyObject d, object basevalue)
        {
            var value = (int) basevalue;

            return (value < 0) ? 0 : value;
        }

        private static void OnValuePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var progressItem = d as ProgressItem;
            if (progressItem == null)
                return;

            progressItem.TextVisibility = (!string.IsNullOrWhiteSpace(progressItem.Text) && progressItem.Value > 0);
            progressItem.RaisePropertyChanged("Value");
            progressItem.RaiseValueChanged();
        }

        /// <summary>
        /// If this is the first visible element of the <see cref="MultiProgressBar"/>, it has to change its Borders on the left side. 
        /// </summary>
        public bool FirstVisible
        {
            get { return (bool)GetValue(FirstVisibleProperty); }
            set
            {
                SetValue(FirstVisibleProperty, value);
                RaisePropertyChanged("FirstVisible");
            }
        }

        /// <summary>
        /// <see cref="FirstVisible"/>
        /// </summary>
        public static readonly DependencyProperty FirstVisibleProperty =
            DependencyProperty.Register("FirstVisible", typeof(bool), typeof(ProgressItem), new PropertyMetadata(false));

        /// <summary>
        /// If this is the last visible element of the <see cref="MultiProgressBar"/>, it has to change its Borders on the right side. 
        /// </summary>
        public bool LastVisible
        {
            get { return (bool)GetValue(LastVisibleProperty); }
            set
            {
                SetValue(LastVisibleProperty, value);
                RaisePropertyChanged("LastVisible");
            }
        }

        /// <summary>
        /// <see cref="LastVisible"/>
        /// </summary>
        public static readonly DependencyProperty LastVisibleProperty =
            DependencyProperty.Register("LastVisible", typeof(bool), typeof(ProgressItem), new PropertyMetadata(false));
        #endregion

        /// <summary>
        /// Static constructor
        /// </summary>
        static ProgressItem()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ProgressItem), new FrameworkPropertyMetadata(typeof(ProgressItem)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressItem"/> class.
        /// </summary>
        public ProgressItem()
        {
            _textDescriptor?.AddValueChanged(this, delegate
            {
                TextVisibility = (!string.IsNullOrWhiteSpace(Text) && Value > 0);
            });
        }

        #region INotifyPropertyChanged

        /// <summary>
        /// PropertyChanged event to
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raise the PropertyChanged-Event if someone is interested
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}