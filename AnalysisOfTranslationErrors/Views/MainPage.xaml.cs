using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisOfTranslationErrors.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AnalysisOfTranslationErrors
{
    public sealed partial class MainPage : Page
    {
        Services.FileService fileService = new Services.FileService();

        public MainPage()
        {
            this.InitializeComponent();


            Windows.UI.Core.Preview.SystemNavigationManagerPreview.GetForCurrentView().CloseRequested +=
                async (sender, args) =>
                {
                    args.Handled = true;

                    if (App.Corpus != null)
                    {
                        ContentDialog SaveConfirm = new ContentDialog
                        {
                            Title = "Do you want to save your work?",
                            Content = String.Format("There are unsaved changes to {0}.", App.Corpus.ProjectName),
                            PrimaryButtonText = "Save",
                            SecondaryButtonText = "Don't save",
                            CloseButtonText = "Cancel"
                        };

                        var result = await SaveConfirm.ShowAsync();
                        if (result == ContentDialogResult.Primary)
                        {
                            // Save work
                            await fileService.SaveProjectAndAskAsync();
                            App.Current.Exit();
                        }
                        else if (result == ContentDialogResult.Secondary)
                        {
                            App.Current.Exit();
                        }

                    }
                    else
                    {
                        ContentDialog SaveConfirm = new ContentDialog
                        {
                            Title = "Are you sure you want to exit?",
                            Content = String.Format("There is no data needed to be save."),
                            PrimaryButtonText = "Yes",
                            SecondaryButtonText = "No"
                        };

                        var result = await SaveConfirm.ShowAsync();
                        if (result == ContentDialogResult.Primary)
                        {
                            App.Current.Exit();
                        }
                        else if (result == ContentDialogResult.Secondary)
                        {
                            // nothing to do
                        }
                    }

                };
            MyFrame.Navigate(typeof(HomePage));
        }




        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            SplitView.IsPaneOpen = !SplitView.IsPaneOpen;
        }

        private void NavigationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HomeListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(HomePage)); TitleTextBlock.Text = "Home";
            }
            else if (AnnotationListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(AnnotationPage));
                if (App.Corpus != null) TitleTextBlock.Text = String.Format("Annotation: {0}", App.Corpus.ProjectName);
                else TitleTextBlock.Text = "Annotation";
            }
            else if (TypologyListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(TypologyPage));
                if (App.Corpus != null) TitleTextBlock.Text = String.Format("Quality Metrics: {0}", App.Corpus.ProjectName);
                else TitleTextBlock.Text = "Quality Metrics";
            }
            else if (StatisticsListBoxItem.IsSelected)
            {
                MyFrame.Navigate(typeof(StatisticsPage));
                if (App.Corpus != null) TitleTextBlock.Text = String.Format("Statistics: {0}", App.Corpus.ProjectName);
                else TitleTextBlock.Text = "Statistics";
            }
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string)
            {
                string par = e.Parameter as string;
                if (par == "TypologyPage")
                { 
                    MyFrame.Navigate(typeof(TypologyPage));
                    TitleTextBlock.Text = String.Format("Quality Metrics: {0}", App.Corpus.ProjectName);
                }
                else if (par == "AnnotationPage")
                {
                    MyFrame.Navigate(typeof(AnnotationPage));
                    TitleTextBlock.Text = String.Format("Annotation: {0}", App.Corpus.ProjectName);
                }
            }
        }
    }
}
