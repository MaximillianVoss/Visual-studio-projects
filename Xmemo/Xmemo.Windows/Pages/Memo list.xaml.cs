using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Memo_list : Page
    {
        public Memo_list()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
        }


        ObservableCollection<Memo_groups> memo_groups;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            memo_groups = e.Parameter as ObservableCollection<Memo_groups>;
            if (memo_groups != null)
            {
                cvsMain.Source = memo_groups;
                gvZoomedOut.ItemsSource = cvsMain.View.CollectionGroups;
            }
        }

        #region Appbar
        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Memo_Edit_page), memo_groups);
        }
        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            foreach (Memo_groups item in memo_groups)
            {
                item.Memos.Remove(Mainlist.SelectedItem as Memo);
            }
        }
        private void Edit_btn_click(object sender, RoutedEventArgs e)
        {
            Temp.temp_memo = Mainlist.SelectedItem as Memo;
            foreach (Memo_groups item in memo_groups)
            {
                item.Memos.Remove(Mainlist.SelectedItem as Memo);
            }
            this.Frame.Navigate(typeof(Memo_Edit_page), memo_groups);
        }
        #endregion

        #region Memo list  events
        private void Mainlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }
        private void AppBar_Opened(object sender, object e)
        {
            if (Mainlist.SelectedIndex == -1)
            {
                Edit_btn.IsEnabled = false;
                Delete_btn.IsEnabled = false;
            }
            else
            {
                Edit_btn.IsEnabled = true;
                Delete_btn.IsEnabled = true;
            }
        }
        private void Mainlist_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        #endregion
    }
}
