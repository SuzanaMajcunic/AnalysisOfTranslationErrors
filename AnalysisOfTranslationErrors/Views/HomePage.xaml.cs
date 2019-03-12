using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AnalysisOfTranslationErrors
{
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
        }


        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateRecentProjectList();

        }

        private void ProjectListBox_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (NewProjectListBoxItem.IsSelected)
            {
                ProjectFilesPanel.Visibility = Visibility.Visible;
                ViewModel.CheckIfProjectIsLoaded();

            }
            else if (OpenProjectListBoxItem.IsSelected)
            {
                ProjectFilesPanel.Visibility = Visibility.Collapsed;
                ViewModel.OpenExistingProject();
            }

        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var recentProject = (Models.RecentProject)e.ClickedItem;

            var mru = Windows.Storage.AccessCache.StorageApplicationPermissions.MostRecentlyUsedList;
            StorageFile recentFile = await mru.GetFileAsync(recentProject.FileToken);

            ViewModel.OpenRecentProject(recentFile);
        }

        public void CreateProjectAndRefresh()
        {
            string proName = ProjectNameTextBox.Text;
            if (proName == "")
            {
                proName = "NewProject"; ViewModel.CreateProjectAsync(proName);
            }
            else ViewModel.CreateProjectAsync(proName);
        }

    }
}
