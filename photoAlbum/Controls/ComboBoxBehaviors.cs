

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace goTax.Controls
{
    public class ComboBoxBehaviors
    {
        public static readonly DependencyProperty DefaultTextProperty =
           DependencyProperty.RegisterAttached("DefaultText",
                 typeof(String),
                 typeof(ComboBoxBehaviors),
                 new PropertyMetadata(null, HookupBehavior));

        public static String GetDefaultText(DependencyObject obj)
        {
            return (String)obj.GetValue(DefaultTextProperty);
        }

        private static void HookupBehavior(DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            ComboBox combo = d as ComboBox;
            if (combo == null) return;

            SetDefaultText(d, e.NewValue.ToString());
        }

        public static void SetDefaultText(DependencyObject obj, String value)
        {
            var combo = (ComboBox)obj;

            RefreshDefaultText(combo, value);

            combo.SelectionChanged += (sender, _) => RefreshDefaultText((ComboBox)sender, GetDefaultText((ComboBox)sender));

            obj.SetValue(DefaultTextProperty, value);
        }

        static void RefreshDefaultText(ComboBox combo, string text)
        {
            // if item is selected and DefaultText is set
            if (combo.SelectedIndex == -1 && !String.IsNullOrEmpty(text))
            {
                // Show DefaultText
                var visual = new TextBlock()
                {
                    FontStyle = FontStyles.Italic,
                    Text = text,
                    Foreground = Brushes.Gray
                };

                combo.Background = new VisualBrush(visual)
                {
                    Stretch = Stretch.None,
                    AlignmentX = AlignmentX.Left,
                    AlignmentY = AlignmentY.Center,
                    Transform = new TranslateTransform(3, 0)
                };
            }
            else
            {
                // Hide DefaultText
                combo.Background = null;
            }
        }
    }
}
