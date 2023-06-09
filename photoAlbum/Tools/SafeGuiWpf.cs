//using goTax.Pages;

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
//using Xceed.Wpf.Toolkit;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Text.RegularExpressions;
using photoAlbum.Pages;
using System.ComponentModel;
using System.Windows.Data;
using System.Reflection;
using photoAlbum.VewModels;
//using System.Windows.Threading;

namespace photoAlbum.Tools
{
    public class SafeGuiWpf
    {
        public static bool emailIsValid(string email)
        {
            string expresion;
            //expresion = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
            expresion = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$";
            if (Regex.IsMatch(email, expresion, RegexOptions.IgnoreCase))
            {
                if (Regex.Replace(email, expresion, string.Empty).Length == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static T FindVisualChild<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        return (T)child;
                    }

                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null) return childItem;
                }
            }
            return null;
        }

        /*public static void RefreshData(Window TB, string _method)
        {
            //FetchRecordCount()
            if (TB.Dispatcher.CheckAccess())
            {
                if (TB != null && TB is MainWindow)
                {
                    //((T)child).GetType().GetMethod("RefreshData").Invoke(_Interface, null);
                    TB.GetType().GetMethod(_method).Invoke(TB,
                                         BindingFlags.DeclaredOnly |
                                         BindingFlags.Public | BindingFlags.NonPublic |
                                         BindingFlags.Instance | BindingFlags.CreateInstance,
                                         null, null, null);
                }
            }
            else
            {
                TB.Dispatcher.Invoke(new Action<Window>(RefreshData), TB);
            }
        }*/
        /*public static void RefreshData(Window TB) 
        {
            if (TB.Dispatcher.CheckAccess())
            {
                if (TB != null && TB is MainWindow)
                {
                    //((T)child).GetType().GetMethod("RefreshData").Invoke(_Interface, null);
                    TB.GetType().GetMethod("FetchTinCount").Invoke(TB,
                                         BindingFlags.DeclaredOnly |
                                         BindingFlags.Public | BindingFlags.NonPublic |
                                         BindingFlags.Instance | BindingFlags.CreateInstance,
                                         null, null, null);
                }
            }
            else
            {
                TB.Dispatcher.Invoke(new Action<Window>(RefreshData), TB);
            }
        }*/

       /* public static void Initialize(Window TB)
        {
            if (TB.Dispatcher.CheckAccess())
            {
                if (TB != null && TB is MainWindow)
                {
                    //((T)child).GetType().GetMethod("RefreshData").Invoke(_Interface, null);
                    TB.GetType().GetMethod("Initialize").Invoke(TB,
                                         BindingFlags.DeclaredOnly |
                                         BindingFlags.Public | BindingFlags.NonPublic |
                                         BindingFlags.Instance | BindingFlags.CreateInstance,
                                         null, null, null);
                }
            }
            else
            {
                TB.Dispatcher.Invoke(new Action<Window>(Initialize), TB);
            }
        }*/

        public static void RefreshData<T>(UserControl TB) where T : UserControl
        {
            if (TB.Dispatcher.CheckAccess())
            {
                if (TB.Parent != null && TB.Parent is Grid)
                {
                    Grid parentControl = TB.Parent as Grid;
                    foreach (UIElement child in parentControl.Children)
                    {
                        if (child is T)
                        {
                            //((T)child).GetType().GetMethod("RefreshData").Invoke(_Interface, null);
                            ((T)child).GetType().GetMethod("RefreshData").Invoke((T)child,
                                                 BindingFlags.DeclaredOnly |
                                                 BindingFlags.Public | BindingFlags.NonPublic |
                                                 BindingFlags.Instance | BindingFlags.CreateInstance,
                                                 null, null, null);
                            //return Convert.ToInt32(result);
                            //((T)child).RefreshData();
                            break;
                        }

                    }
                }
            }
            else
            {
                TB.Dispatcher.Invoke(new Action<UserControl>(RefreshData<T>), TB);
            }
        }
        /*public static void FetchCustomersToView(UserControl TB)
        {
            if (TB.Dispatcher.CheckAccess())
            {
                if (TB.Parent != null && TB.Parent is Grid)
                {
                    Grid parentControl = TB.Parent as Grid;
                    foreach (UIElement child in parentControl.Children)
                    {
                        if (child is CustomersViewPage)
                        {
                            ((CustomersViewPage)child).FetchCustomersToView("");
                            break;
                        }

                    }
                }
            }
            else
            {
                TB.Dispatcher.Invoke(new Action<UserControl>(FetchCustomersToView), TB);
            }
        }*/

        public static void ShowError(string message)//ShowWarning
        {
            ToastViewModel _vm = new ToastViewModel();
            if (Application.Current.Dispatcher.CheckAccess())
            {
                _vm.ShowError(message);
            }
            else Application.Current.Dispatcher.Invoke(new Action<string>(ShowError), message);
        }

        public static void ShowWarning(string message)//ShowWarning
        {
            ToastViewModel _vm = new ToastViewModel();
            if (Application.Current.Dispatcher.CheckAccess())
            {
                _vm.ShowWarning(message);
            }
            else Application.Current.Dispatcher.Invoke(new Action<string>(ShowWarning), message);
        }

        public static void ShowInformation(string message)
        {
            ToastViewModel _vm = new ToastViewModel();
            if (Application.Current.Dispatcher.CheckAccess())
            {
                _vm.ShowInformation(message);
            }
            else Application.Current.Dispatcher.Invoke(new Action<string>(ShowInformation), message);
        }

        public static void ShowSuccess(string message)
        {
            ToastViewModel _vm = new ToastViewModel();
            if (Application.Current.Dispatcher.CheckAccess())
            {
                _vm.ShowSuccess(message);
            }
            else Application.Current.Dispatcher.Invoke(new Action<string>(ShowSuccess), message);
        }
        public static object GetTag(Control C)
        {
            if (C.Dispatcher.CheckAccess()) return C.Tag;
            else return C.Dispatcher.Invoke(new Func<Control, object>(GetTag), C);
        }

        public static string GetText(TextBox TB)
        {
            if (TB.Dispatcher.CheckAccess()) return TB.Text;
            else return (string)TB.Dispatcher.Invoke(new Func<TextBox, string>(GetText), TB);
        }

        /*public static int? GetValue(IntegerUpDown TB)
        {
            if (TB.Dispatcher.CheckAccess()) return TB.Value;
            else return (int)TB.Dispatcher.Invoke(new Func<IntegerUpDown, int?>(GetValue), TB);
        }*/
        public static string GetText(TextBlock TB)
        {
            if (TB.Dispatcher.CheckAccess()) return TB.Text;
            else return (string)TB.Dispatcher.Invoke(new Func<TextBlock, string>(GetText), TB);
        }
        public static string GetText(ComboBox TB)
        {
            if (TB.Dispatcher.CheckAccess()) return TB.Text;
            else return (string)TB.Dispatcher.Invoke(new Func<ComboBox, string>(GetText), TB);
        }

        public static T GetItems<T>(ComboBox TB) where T : new()
        {
            if (TB.Dispatcher.CheckAccess())
            {
                var mo = (T)TB.SelectedItem ;
                return mo;
            }
            else return (T)TB.Dispatcher.Invoke(new Func<ComboBox, T>(GetItems<T>), TB);
        }

       /* public static T GetItems<T>(Button TB) where T : new()
        {
            if (TB.Dispatcher.CheckAccess())
            {
                var mo = (T)TB..SelectedItem;
                return mo;
            }
            else return (T)TB.Dispatcher.Invoke(new Func<Button, T>(GetItems<T>), TB);
        }*/

        /* public static void SetItems<T>(ComboBox TB, List<T> Str, object strDisplayMemberPath, object strSelectedValuePath) where T : new()
         {
             try
             {
                 if (TB.Dispatcher.CheckAccess())
                 {
                     TB.ItemsSource = Str;
                     TB.DisplayMemberPath = (string)strDisplayMemberPath;// "\"" + strDisplayMemberPath + "\"";
                     TB.SelectedValuePath = (string)strSelectedValuePath;// "\"" + strSelectedValuePath + "\"";
                 }
                 else
                 {
                     try
                     {
                         TB.Dispatcher.Invoke(new Action<ComboBox, List<T>, string, string>(SetItems<T>), TB, Str, (string)strDisplayMemberPath, (string)strSelectedValuePath);
                     }
                     catch(Exception ex)
                     {

                     }

                 }
             }
             catch(Exception ex)
             {

             }

         }*/
        public static void SetItems<T>(ListView TB, List<T> Str) where T: new()
        {
            if (TB.Dispatcher.CheckAccess())
            {
                TB.ItemsSource = Str;
                ICollectionView dataView = CollectionViewSource.GetDefaultView(TB.ItemsSource);
                dataView.Refresh();
                //TB.Items.Refresh();
            }
            else TB.Dispatcher.Invoke(new Action<ListView, List<T>>(SetItems<T>), TB, Str);
        }

        public static void SetItems<T>(ComboBox TB, string DisplayMemberPath, string SelectedValuePath, string DefaultSelectedValue, List<T> Str) where T : new()
        {
            if (TB.Dispatcher.CheckAccess())
            {
                TB.ItemsSource = Str;
                TB.DisplayMemberPath = DisplayMemberPath;
                TB.SelectedValuePath = SelectedValuePath;
                if (!string.IsNullOrWhiteSpace(DefaultSelectedValue))
                {
                    TB.SelectedValue = DefaultSelectedValue;
                    TB.Items.Refresh();
                }
            }
            else TB.Dispatcher.Invoke(new Action<ComboBox, string, string, string, List<T>>(SetItems<T>), TB, DisplayMemberPath, SelectedValuePath, DefaultSelectedValue, Str);
        }

        public static void SetCursor(Cursor _previousCursor)
        {
            if (Application.Current.Dispatcher.CheckAccess())
            {
                _previousCursor = Mouse.OverrideCursor;
                Mouse.OverrideCursor = Cursors.Wait;
            }
            else
            {
                Application.Current.Dispatcher.BeginInvoke(
                  DispatcherPriority.Background,
                  new Action(() =>
                  {
                      _previousCursor = Mouse.OverrideCursor;
                      Mouse.OverrideCursor = Cursors.Wait;
                  }));
            }
        }

        public static void ReportProgress(ProgressBar TB, bool value, int percent)
        {
            if (TB.Dispatcher.CheckAccess())
            {
                TB.IsIndeterminate = value;
                TB.Value = percent;
            }
            else TB.Dispatcher.Invoke(new Action<ProgressBar, bool, int>(ReportProgress), TB, value, percent);
        }

        public static string GetText(PasswordBox TB)
        {
            if (TB.Dispatcher.CheckAccess()) return TB.Password;
            else return (string)TB.Dispatcher.Invoke(new Func<PasswordBox, string>(GetText), TB);
        }

        public static void ClearImage(Image TB)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Source = null; 
            else TB.Dispatcher.Invoke(new Action<Image>(ClearImage), TB); 
        }

        public static void ClearDate(DatePicker TB)
        {
            if (TB.Dispatcher.CheckAccess())
            {
                TB.SelectedDate = null;
                TB.DisplayDate = DateTime.Today;
            }
            else TB.Dispatcher.Invoke(new Action<DatePicker>(ClearDate), TB);
        }
        //.MinDate.ClearValue(DatePicker.SelectedDateProperty);
        /*public static void SetImage(Image TB, byte[] bytes)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Source = ProcessImageData.ConvertByteArrayToBitmapImage(bytes); 
            else TB.Dispatcher.Invoke(new Action<Image, byte[]>(SetImage), TB, bytes);
        }*/


        /*public static void SetImageEx(Image TB, byte[] bytes)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Source = ProcessImageData.ConvertByteArrayToBitmapImage(bytes);
            else TB.Dispatcher.Invoke(new Action<Image, byte[]>(SetImage), TB, bytes);
        }*/
        //ImageToBase64Ex

        /*public static string SetImage(UserControl TB, byte[] bytes)
        {
            if (TB.Dispatcher.CheckAccess()) return (string)ProcessImageData.byteToString(bytes);
            else return (string)TB.Dispatcher.Invoke(new Func<UserControl, byte[], string>(SetImage), TB, bytes);
        }*/

        public static void SetImage(Image TB, string Source)
        {
            var imgUri = new Uri("pack://application:,,,/goTax;component/Images/Biometrics/" + Source, UriKind.RelativeOrAbsolute);
            if (TB.Dispatcher.CheckAccess()) TB.Source = new BitmapImage(imgUri);
            else TB.Dispatcher.Invoke(new Action<Image, string>(SetImage), TB, Source);
        }

        /*public static BitmapImage SetImage(byte[] bytes)
        {
            if (Application.Current.Dispatcher.CheckAccess()) return (BitmapImage)ProcessImageData.ConvertByteArrayToBitmapImage(bytes);
            else return (BitmapImage)Application.Current.Dispatcher.Invoke(new Func<byte[], BitmapImage>(SetImage), bytes);
        }*/

        /*public static void SetImage(Image TB, System.Drawing.Bitmap bmpImage)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Source = ProcessImageData.ToBitmapSource(bmpImage);
            else TB.Dispatcher.Invoke(new Action<Image, System.Drawing.Bitmap>(SetImage), TB, bmpImage);
        }*/
        public static ImageSource GetImageSource(Image TB)
        {
            if (TB.Dispatcher.CheckAccess()) return TB.Source;
            else return (ImageSource)TB.Dispatcher.Invoke(new Func<Image, ImageSource>(GetImageSource), TB);
        }

        public static byte[] ByteFromImageSource(ImageSource TB)
        {
            if (TB.Dispatcher.CheckAccess()) return ProcessImageData.ConvertBitmapSourceToByteArray(TB);
            else return (byte[])TB.Dispatcher.Invoke(new Func<ImageSource, byte[]>(ByteFromImageSource), TB);
        }

        /*public static byte[] ByteFromImageSourceBMP(ImageSource TB)
        {
            if (TB.Dispatcher.CheckAccess()) return ProcessImageData.ConvertBitmapSourceToByteArrayBMP(TB);
            else return (byte[])TB.Dispatcher.Invoke(new Func<ImageSource, byte[]>(ByteFromImageSourceBMP), TB);
        }*/

        public static void SetText(TextBlock TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Text = Str;
            else TB.Dispatcher.Invoke(new Action<TextBlock, string>(SetText), TB, Str);
        }

        public static void SetText(Label TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Content = Str;
            else TB.Dispatcher.Invoke(new Action<Label, string>(SetText), TB, Str);
        }
        public static void SetText(PasswordBox TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Password = Str;
            else TB.Dispatcher.Invoke(new Action<PasswordBox, string>(SetText), TB, Str);
        }

        public static void SetText(MenuItem TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Header = Str;
            else TB.Dispatcher.Invoke(new Action<MenuItem, string>(SetText), TB, Str);
        }
       
       /* public static void SetText(WatermarkTextBox TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Text = Str;
            else TB.Dispatcher.Invoke(new Action<WatermarkTextBox, string>(SetText), TB, Str);
        }*/
       
        public static void SetText(TextBox TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Text = Str;
            else TB.Dispatcher.Invoke(new Action<TextBox, string>(SetText), TB, Str);
        }

        public static void SetBackground(TextBox TB, string HexColor)
        {
            var bc = new BrushConverter();
            if (TB.Dispatcher.CheckAccess()) TB.Background = (Brush)bc.ConvertFrom(HexColor);
            else TB.Dispatcher.Invoke(new Action<TextBox, string>(SetBackground), TB, HexColor);
        }

        public static void SetBackground(TextBlock TB, string HexColor)
        {
            var bc = new BrushConverter();
            if (TB.Dispatcher.CheckAccess()) TB.Background = (Brush)bc.ConvertFrom(HexColor);
            else TB.Dispatcher.Invoke(new Action<TextBlock, string>(SetBackground), TB, HexColor);
        }

        public static void SetBackground(Label TB, string HexColor)
        {
            var bc = new BrushConverter();
            if (TB.Dispatcher.CheckAccess()) TB.Background = (Brush)bc.ConvertFrom(HexColor);
            else TB.Dispatcher.Invoke(new Action<Label, string>(SetBackground), TB, HexColor);
        }
        public static void SetText(DatePicker TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Text = Str;
            else TB.Dispatcher.Invoke(new Action<DatePicker, string>(SetText), TB, Str);
        }
        public static void SetText(Button TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess()) TB.Content = Str;
            else TB.Dispatcher.Invoke(new Action<Button, string>(SetText), TB, Str);
        }
        public static void AppendText(TextBox TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess())
            {
                TB.AppendText(Str);
                TB.ScrollToEnd(); // scroll to end?
            }
            else TB.Dispatcher.Invoke(new Action<TextBox, string>(AppendText), TB, Str);
        }

       /* public static void AppendText(WatermarkTextBox TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess())
            {
                TB.AppendText(Str);
                TB.ScrollToEnd(); // scroll to end?
            }
            else TB.Dispatcher.Invoke(new Action<TextBox, string>(AppendText), TB, Str);
        }*/

        public static void AppendText(TextBlock TB, string Str)
        {
            if (TB.Dispatcher.CheckAccess())
            {
                TB.Text += Str;
               // TB.ScrollToEnd(); // scroll to end?
            }
            else TB.Dispatcher.Invoke(new Action<TextBlock, string>(AppendText), TB, Str);
        }
        public static bool? GetChecked(CheckBox Ck)
        {
            if (Ck.Dispatcher.CheckAccess()) return Ck.IsChecked;
            else return (bool?)Ck.Dispatcher.Invoke(new Func<CheckBox, bool?>(GetChecked), Ck);
        }
        public static void SetChecked(CheckBox Ck, bool? V)
        {
            if (Ck.Dispatcher.CheckAccess()) Ck.IsChecked = V;
            else Ck.Dispatcher.Invoke(new Action<CheckBox, bool?>(SetChecked), Ck, V);
        }
        public static bool GetChecked(MenuItem Ck)
        {
            if (Ck.Dispatcher.CheckAccess()) return Ck.IsChecked;
            else return (bool)Ck.Dispatcher.Invoke(new Func<MenuItem, bool>(GetChecked), Ck);
        }
        public static void SetChecked(MenuItem Ck, bool V)
        {
            if (Ck.Dispatcher.CheckAccess()) Ck.IsChecked = V;
            else Ck.Dispatcher.Invoke(new Action<MenuItem, bool>(SetChecked), Ck, V);
        }
        public static bool? GetChecked(RadioButton Ck)
        {
            if (Ck.Dispatcher.CheckAccess()) return Ck.IsChecked;
            else return (bool?)Ck.Dispatcher.Invoke(new Func<RadioButton, bool?>(GetChecked), Ck);
        }
        public static void SetChecked(RadioButton Ck, bool? V)
        {
            if (Ck.Dispatcher.CheckAccess()) Ck.IsChecked = V;
            else Ck.Dispatcher.Invoke(new Action<RadioButton, bool?>(SetChecked), Ck, V);
        }

        public static void SetVisible(UIElement Emt, Visibility V)
        {
            if (Emt.Dispatcher.CheckAccess()) Emt.Visibility = V;
            else Emt.Dispatcher.Invoke(new Action<UIElement, Visibility>(SetVisible), Emt, V);
        }
        public static Visibility GetVisible(UIElement Emt)
        {
            if (Emt.Dispatcher.CheckAccess()) return Emt.Visibility;
            else return (Visibility)Emt.Dispatcher.Invoke(new Func<UIElement, Visibility>(GetVisible), Emt);
        }
        public static bool GetEnabled(UIElement Emt)
        {
            if (Emt.Dispatcher.CheckAccess()) return Emt.IsEnabled;
            else return (bool)Emt.Dispatcher.Invoke(new Func<UIElement, bool>(GetEnabled), Emt);
        }
        public static void SetEnabled(UIElement Emt, bool V)
        {
            if (Emt.Dispatcher.CheckAccess()) Emt.IsEnabled = V;
            else Emt.Dispatcher.Invoke(new Action<UIElement, bool>(SetEnabled), Emt, V);
        }

        public static void SetSelectedItem(Selector Ic, object Selected)
        {
            if (Ic.Dispatcher.CheckAccess())
            {
                Ic.SelectedItem = Selected;
               
            }
            else Ic.Dispatcher.Invoke(new Action<Selector, object>(SetSelectedItem), Ic, Selected);
        }
        public static object GetSelectedItem(Selector Ic)
        {
            if (Ic.Dispatcher.CheckAccess()) return Ic.SelectedItem;
            else return Ic.Dispatcher.Invoke(new Func<Selector, object>(GetSelectedItem), Ic);
        }

        public static int GetSelectedIndex(Selector Ic)
        {
            if (Ic.Dispatcher.CheckAccess()) return Ic.SelectedIndex;
            else return (int)Ic.Dispatcher.Invoke(new Func<Selector, int>(GetSelectedIndex), Ic);
        }

        delegate MessageBoxResult MsgBoxDelegate(Window owner, string text, string caption, MessageBoxButton button, MessageBoxImage icon);
        public static MessageBoxResult MsgBox(Window owner, string text, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            if (owner.Dispatcher.CheckAccess()) return System.Windows.MessageBox.Show(owner, text, caption, button, icon);
            else return (MessageBoxResult)owner.Dispatcher.Invoke(new MsgBoxDelegate(MsgBox), owner, text, caption, button, icon);
        }

        delegate MessageBoxResult WMsgBoxDelegate(Window owner, string caption, string text,  MessageBoxButton button, WpfMessageBox.MessageBoxImage icon);
        public static MessageBoxResult WMsgBox(Window owner, string caption, string text,  MessageBoxButton button, WpfMessageBox.MessageBoxImage icon)
        {
            if (owner.Dispatcher.CheckAccess()) return WpfMessageBox.Show(caption, text,  button, icon);
            else return (MessageBoxResult)owner.Dispatcher.Invoke(new WMsgBoxDelegate(WMsgBox), owner, caption, text,  button, icon);
        }

        delegate MessageBoxResult UMsgBoxDelegate(UserControl owner, string caption, string text,  MessageBoxButton button, WpfMessageBox.MessageBoxImage icon);
        public static MessageBoxResult UMsgBox(UserControl owner, string caption, string text,  MessageBoxButton button, WpfMessageBox.MessageBoxImage icon)
        {
            if (owner.Dispatcher.CheckAccess()) return WpfMessageBox.Show(caption, text,  button, icon);
            else return (MessageBoxResult)owner.Dispatcher.Invoke(new UMsgBoxDelegate(UMsgBox), owner, caption, text,  button, icon);
        }

        #region Toast Notify

        // delegate Toaster UToastDelegate(UserControl owner, string caption, string text, ToastTypes button, bool isTPersistent);
        /* public static Toaster UToast(UserControl owner, string caption, string text, ToastTypes button, bool isTPersistent)
         {
             if (owner.Dispatcher.CheckAccess())
             {
                var _Toaster = new Toaster();
                return (Toaster) _Toaster.Show(caption, text, button, isPersistent: isTPersistent);
             }
             else owner.Dispatcher.Invoke(new Action<UserControl, string, string, ToastTypes, bool>(UToast), owner, caption, text, button, isTPersistent);
         }*/

       /* public static void UToast(Toaster TB, string caption, string text, ToastTypes button, bool isTPersistent)
        {
           /* if (TB.Dispatcher.CheckAccess())
            {
                
            }
            else TB.Dispatcher.Invoke(new Action<UIElement, string, string, ToastTypes, bool>(UToast), TB, caption, text, button, isTPersistent);
        }*/

        /*  public static Toaster UToast()
          {
              if (Application.Current.Dispatcher.CheckAccess())
              {
                  var _Toaster = new Toaster();
                  return (Toaster)_Toaster;
              }
              else return (Toaster)Application.Current.Dispatcher.Invoke(new Func<Toaster>(UToast));
          }*/
        #endregion

        public static double GetRangeValue(RangeBase RngBse)
        {
            if (RngBse.Dispatcher.CheckAccess()) return RngBse.Value;
            else return (double)RngBse.Dispatcher.Invoke(new Func<RangeBase, double>(GetRangeValue), RngBse);
        }
        public static void SetRangeValue(RangeBase RngBse, double V)
        {
            if (RngBse.Dispatcher.CheckAccess()) RngBse.Value = V;
            else RngBse.Dispatcher.Invoke(new Action<RangeBase, double>(SetRangeValue), RngBse, V);
        }

        public static Grid AddUserControlToGrid<T>(Grid grd) 
            where T : UserControl, new()
        {
            if (grd.Dispatcher.CheckAccess())
            {
                var Win = new T(); 
                grd.Children.Clear();
                grd.Children.Add(Win);
                //grd.Background = new ImageBrush(null);
                //grd.Background = new ImageBrush(new BitmapImage(new Uri("")));
                return grd;
            }
            else return (Grid)grd.Dispatcher.Invoke(new Func<Grid, Grid>(AddUserControlToGrid<T>), grd);
        }

        public static void AddDashbordToGrid<T>(Grid grd, UIElement Page)
           where T : UserControl, new()
        {
            if (grd.Dispatcher.CheckAccess())
            {
                var Win = new T();
                grd.Children.Remove(Page);
                if (grd.Children.Count==0)
                {
                    grd.Children.Add(Win);
                    grd.Background = new ImageBrush(new BitmapImage(new Uri(@"pack://application:,,,/idenysisTNA;component/Images/herobg.jpg")));
                    return;
                }
                grd.Background = new ImageBrush(null);
            }
            else grd.Dispatcher.Invoke(new Action<Grid, UIElement>(AddDashbordToGrid<T>), grd, Page);
        }


        public static void AddDashbordToGrid<T>(Grid grd)
          where T : UserControl, new()
        {
            if (grd.Dispatcher.CheckAccess())
            {
                var Win = new T();
                grd.Children.Clear();
                grd.Children.Add(Win);

            }
            else grd.Dispatcher.Invoke(new Action<Grid, UIElement>(AddDashbordToGrid<T>), grd);
        }

        public static void AddUserControlToGrid<T, T2>(Grid grd, T2 model, bool clearGrid)
            where T : UserControl,new()
            where T2: new()
        {
            if (grd.Dispatcher.CheckAccess())
            {
                var Win = Activator.CreateInstance(typeof(T), new object[] { model }) as T;
                if(clearGrid)
                    grd.Children.Clear();

                //grd.Background = new ImageBrush(null);
                grd.Children.Add(Win);
                
            }
            else grd.Dispatcher.Invoke(new Action<Grid, T2, bool>(AddUserControlToGrid<T, T2>), grd, model, clearGrid);
        }


        public static void AddUserControlToGrid<T>(Grid grd, bool clearGrid)
         where T : UserControl, new()
        {
            if (grd.Dispatcher.CheckAccess())
            {
                var Win = new T();
                if(clearGrid)
                   grd.Children.Clear();

                //grd.Background = new ImageBrush(null);
                grd.Children.Add(Win);
                
            }
            else grd.Dispatcher.Invoke(new Action<Grid, bool>(AddUserControlToGrid<T>), grd, clearGrid);
        }

        public static void AddUserControlToGrid<T>(Grid grd, object obj, bool clearGrid)
       where T : UserControl, new()
        {
            if (grd.Dispatcher.CheckAccess())
            {
                if (obj.GetType().Equals(typeof(string)))
                    obj = (string)obj;

                
                //string Obj = (string)obj;
                var Win = Activator.CreateInstance(typeof(T), new object[] { obj }) as T;
                if (clearGrid)
                    grd.Children.Clear();

                //grd.Background = new ImageBrush(null);
                grd.Children.Add(Win);

            }
            else grd.Dispatcher.Invoke(new Action<Grid, object, bool>(AddUserControlToGrid<T>), grd, obj, clearGrid);
        }
        public static void RemoveUserControlFromGrid(Grid grd, UserControl cntrl)
        {
            if (grd.Dispatcher.CheckAccess())
            {
                grd.Children.Remove(cntrl);
            }
            else grd.Dispatcher.Invoke(new Action<Grid, UserControl>(RemoveUserControlFromGrid), grd, cntrl);
        }
        public static void AddUserControlToGridEx<T, T2>(Grid grd, T2 model, bool clearGrid)
             where T : UserControl, new()
             where T2 : new()
          {
              if (grd.Dispatcher.CheckAccess())
              {
                  var Win = Activator.CreateInstance(typeof(T), new object[] { model }) as T;
                  if (clearGrid)
                      grd.Children.Clear();

                //grd.Background = new ImageBrush(null);
                grd.Children.Add(Win);
              }
              else grd.Dispatcher.Invoke(new Action<Grid, T2, bool>(AddUserControlToGridEx<T, T2>), grd, model, clearGrid);
          }

       public static void AddUserControlToGrid<T, T2, T3>(Grid grd, T2 model, T3 model2, bool clearGrid)
           where T : UserControl, new()
           where T2: new()
           where T3:new()
        {
            if (grd.Dispatcher.CheckAccess())
            {
                var Win = Activator.CreateInstance(typeof(T), new object[] { model, model2 }) as T;
                if(clearGrid)
                   grd.Children.Clear();

                //grd.Background = new ImageBrush(null);
                grd.Children.Add(Win);
                
            }
            else grd.Dispatcher.Invoke(new Action<Grid, T2, T3, bool>(AddUserControlToGrid<T, T2, T3>), grd, clearGrid);
        }
        public static T CreateWindow<T>(Window Owner) where T : Window, new()
        {
            if (Owner.Dispatcher.CheckAccess())
            {
                var Win = new T(); // Window created on GUI thread
                Win.Owner = Owner;
                return Win;
            }
            else return (T)Owner.Dispatcher.Invoke(new Func<Window, T>(CreateWindow<T>), Owner);
        }

        public static void CloseWindow(Window Win)
        {
            if (Win.Dispatcher.CheckAccess())
            {
                Win.Close();
            }
            else Win.Dispatcher.Invoke(new Action<Window>(CloseWindow), Win);
        }

        public static bool? ShowDialog(Window Dialog)
        {
            if (Dialog.Dispatcher.CheckAccess()) return Dialog.ShowDialog();
            else return (bool?)Dialog.Dispatcher.Invoke(new Func<Window, bool?>(ShowDialog), Dialog);
        }

       /* public static string ShowDialogue(Window Dialog)
        {
            if (Dialog.Dispatcher.CheckAccess()) return Dialog.ShowDialog();
            else return (string)Dialog.Dispatcher.Invoke(new Func<Window, string?>(ShowDialogue), Dialog);
        }*/

        public static void SetDialogResult(Window Dialog, bool? Result)
        {
            if (Dialog.Dispatcher.CheckAccess()) Dialog.DialogResult = Result;
            else Dialog.Dispatcher.Invoke(new Action<Window, bool?>(SetDialogResult), Dialog, Result);
        }

        public static Window GetWindowOwner(Window window)
        {
            if (window.Dispatcher.CheckAccess()) return window.Owner;
            else return (Window)window.Dispatcher.Invoke(new Func<Window, Window>(GetWindowOwner), window);
        }

    }
}
