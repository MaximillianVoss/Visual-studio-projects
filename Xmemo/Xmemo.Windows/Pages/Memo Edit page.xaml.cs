using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Xmemo.Pages;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace Xmemo
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class Memo_Edit_page : Page
    {
        public Memo_Edit_page()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;
            Get_color_collection();
            //Слепок заметки в классе для редактирования
            if (Temp.temp_memo != null)
            {
                name_txtblck.Text = Temp.temp_memo.name;
                theme_textblck.Text = Temp.temp_memo.theme;
                string text = Temp.temp_memo.text;
                memo_text.Document.SetText(Windows.UI.Text.TextSetOptions.None, text);
                SolidColorBrush temp_brush =new SolidColorBrush(GetColorFromHex(Temp.temp_memo.memo_color));
                memo_text.Background =  temp_brush;
                memo_text.BorderBrush = temp_brush;
            }
        }

        

        #region Collections
        ObservableCollection<Memo_groups> Memo_colletion = new ObservableCollection<Memo_groups>();
        Memo_groups memo_group = new Memo_groups();

        #endregion
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (Temp.temp_memo == null)
            {
                Color_picker.SelectedIndex = 10;     //стандарные значения
                Text_color_picker.SelectedIndex = 7; // стандартные значения
            }
            else
            {
                memo_text.Background = new SolidColorBrush(GetColorFromHex(Temp.temp_memo.memo_color));
                memo_text.Foreground = new SolidColorBrush(GetColorFromHex(Temp.temp_memo.text_color));
                name_txtblck.Foreground = new SolidColorBrush(GetColorFromHex(Temp.temp_memo.text_color));
                theme_textblck.Foreground = new SolidColorBrush(GetColorFromHex(Temp.temp_memo.text_color));
            }
            base.OnNavigatedTo(e);
            if (e.Parameter!= null)
            Memo_colletion = e.Parameter as ObservableCollection<Memo_groups>;                       
            #region
            //Memo temp_memo = new Memo
            //{
            //    name = "Новая заметка",
            //    theme = "Без темы",
            //    memo_color = "#FF963CFF",
            //    text_color = "#FF363636",
            //    text = String.Empty
            //};
            //name_txtblck.Foreground = new SolidColorBrush(GetColorFromHex(temp_memo.text_color));
            //theme_textblck.Foreground = new SolidColorBrush(GetColorFromHex(temp_memo.text_color));
            //memo_text.Foreground = new SolidColorBrush(GetColorFromHex(temp_memo.memo_color));
            #endregion
        }
        #region Done
        private string Name_of_Month(string Sample)
        {
            string Result = string.Empty;
            string [] Year = {"January","Febuary","March","April","May","June","July","August","Semptember","October","November","December"};
            return Result = Year[Convert.ToInt32(Sample)-1];
        }
        private  Color_groups Get_color_group (string Sample,ObservableCollection<Color_groups> Sample_group)
        {
            Color_groups Result = null;
             foreach (Color_groups item in Sample_group)
             {
                 if (item.group_name == Sample)
                 {
                     Result = item;
                     break;
                 }
             }
             return Result;
        }
        private void Get_color_collection()
        {
            string colors = Path.Combine(Package.Current.InstalledLocation.Path, "Assets/colors.xml");
            XDocument colors_data = XDocument.Load(colors);
            var data = from query in colors_data.Descendants("color")
                       select new Memo_color
                       {
                           value = (string)query.Value,
                           name = (string)query.FirstAttribute
                       };           
            List<string> temp = new List<string>();
            ObservableCollection<Color_groups> colors_collection = new ObservableCollection<Color_groups>();
            Color_groups group = new Color_groups();
            foreach (Memo_color item in data)
            {
                group = Get_color_group(item.name.Substring(0, 1), colors_collection);
                if (group==null)
                {
                    group = new Color_groups { group_name = item.name.Substring(0, 1) };
                    group.Colors.Add(item);
                    colors_collection.Add(group);
                }
                else
                {
                    group.Colors.Add(item);                    
                }
            }
            IOrderedEnumerable<Color_groups> colors_collection_sorted = colors_collection.OrderBy(x => x.group_name);
            MemoColors.Source = colors_collection_sorted;
            gvZoomedOut.ItemsSource = MemoColors.View.CollectionGroups;
            TextColors.Source = colors_collection_sorted;
            text_gvZoomedOut.ItemsSource = TextColors.View.CollectionGroups;
        }
        public Color GetColorFromHex(string pHex)
        {
            string hex = pHex.Replace("#", string.Empty);

            int offset = 0;
            byte alpha = 255;
            byte r = 0;
            byte g = 0;
            byte b = 0;
            if (hex.Length == 8)
            {
                alpha = (byte)Convert.ToInt32(hex.Substring(0, 2), 16);
                offset += 2;
            }

            if (hex.Length >= 6)
            {
                r = (byte)Convert.ToInt32(hex.Substring(offset, 2), 16);
                offset += 2;

                g = (byte)Convert.ToInt32(hex.Substring(offset, 2), 16);
                offset += 2;

                b = (byte)Convert.ToInt32(hex.Substring(offset, 2), 16);
                offset += 2;
            }
            else if (hex.Length == 3)
            {
                r = (byte)Convert.ToInt32(hex.Substring(offset, 1) + "0", 16);
                offset += 1;

                g = (byte)Convert.ToInt32(hex.Substring(offset, 1) + "0", 16);
                offset += 1;

                b = (byte)Convert.ToInt32(hex.Substring(offset, 1) + "0", 16);
                offset += 1;
            }

            return ColorHelper.FromArgb(alpha, r, g, b);
        }
        Memo temp_memo = new Memo();
        private void Done_btn_Click(object sender, RoutedEventArgs e)
        {            
            temp_memo.name = name_txtblck.Text;
            temp_memo.theme = theme_textblck.Text;
            temp_memo.Hour = DateTimeOffset.Now.Hour.ToString();
            temp_memo.Minutes = DateTimeOffset.Now.Minute.ToString();
            temp_memo.date = DateTimeOffset.Now;
            temp_memo.Day = DateTimeOffset.Now.Day.ToString();
            temp_memo.Day_of_week = DateTimeOffset.Now.DayOfWeek.ToString();
            temp_memo.Month = DateTimeOffset.Now.Month.ToString();
            temp_memo.Name_of_month = Name_of_Month(DateTimeOffset.Now.Month.ToString());
            string text = string.Empty;
            memo_text.Document.GetText(Windows.UI.Text.TextGetOptions.None, out text);
            temp_memo.text = text;

            #region Set color
            if (Color_picker.SelectedIndex != -1)
            {
                SolidColorBrush memo_brush = Brush_picker(Color_picker.SelectedItem as Memo_color);
                temp_memo.memo_color = memo_brush.Color.ToString();
            }
            //else if (memo_text.Background != null) temp_memo.memo_color = memo_text.Background.ToString();
            //else temp_memo.memo_color = "#FF37D0A4";
            if (Text_color_picker.SelectedIndex != -1)
            {
                SolidColorBrush text_brush = Brush_picker(Text_color_picker.SelectedItem as Memo_color);
                temp_memo.text_color = text_brush.Color.ToString();
            }
            //else if (name_txtblck.Foreground != null) temp_memo.text_color = name_txtblck.Foreground.ToString();
            //else temp_memo.text_color = "#FF363636";
            #endregion
            if (Memo_colletion.Count>0)
            {
                List<string> Temp_dates = new List<string>();
                foreach (var item in Memo_colletion)
                {
                    Temp_dates.Add(item.Day);
                }
                foreach (var item in Memo_colletion)
                {
                    if (Temp_dates.Contains(item.Day))
                    {
                        item.Memos.Add(temp_memo);
                    }
                    else
                    {
                        Memo_groups Temp_group = new Memo_groups 
                        { 
                            Day = DateTimeOffset.Now.Day.ToString(),
                            Day_of_week = DateTimeOffset.Now.DayOfWeek.ToString(),
                            Month = DateTimeOffset.Now.Month.ToString(),
                            Name_of_month = Name_of_Month(DateTimeOffset.Now.Month.ToString()),
                            Year = DateTimeOffset.Now.Year.ToString(), 
                            Memos = { temp_memo } 
                        };
                        Memo_colletion.Add(Temp_group);
                    }
                }

            }
            else
            {
                Memo_groups Temp_group = new Memo_groups
                { 
                    Day = DateTimeOffset.Now.Day.ToString(),
                    Day_of_week = DateTimeOffset.Now.DayOfWeek.ToString(),
                    Month = DateTimeOffset.Now.Month.ToString(),
                    Name_of_month = Name_of_Month(DateTimeOffset.Now.Month.ToString()),
                    Year = DateTimeOffset.Now.Year.ToString(), 
                    Memos = { temp_memo }
                };
                Memo_colletion.Add(Temp_group);
            }
            Temp.temp_memo = null;
            this.Frame.Navigate(typeof(Memo_list), Memo_colletion);
        }
        #endregion
        #region App bars
        private SolidColorBrush Brush_picker (Memo_color Sample)
        {
             SolidColorBrush Result =   new SolidColorBrush(GetColorFromHex(Sample.value));
             return Result;
        }
        private void Text_color_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             if (Text_color_picker.SelectedItem!=null)
             {
                  SolidColorBrush temp_brush = Brush_picker(Text_color_picker.SelectedItem as Memo_color);
                  memo_text.Foreground = temp_brush;
                  theme_textblck.Foreground = temp_brush;
                  name_txtblck.Foreground = temp_brush;
             }
        }
        private void Color_picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Color_picker.SelectedItem != null)
            {
                SolidColorBrush temp_brush = Brush_picker(Color_picker.SelectedItem as Memo_color);
                memo_text.Background = temp_brush;
                memo_text.BorderBrush = temp_brush;
            }
        }
        #endregion
    }
}
