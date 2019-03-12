using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisOfTranslationErrors.Models;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AnalysisOfTranslationErrors.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        Services.FileService fileService = new Services.FileService();
        public event PropertyChangedEventHandler PropertyChanged;

        private Models.Corpus _LocalCorpus = new Models.Corpus();
        public Models.Corpus LocalCorpus
        {
            get { return _LocalCorpus; }
            set
            {
                _LocalCorpus = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(LocalCorpus)));
            }
        }

        private string _RefPath = "";
        public string RefPath
        {
            get { return _RefPath; }
            set
            {
                _RefPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(RefPath)));

            }
        }

        private string _SrcPath = "";
        public string SrcPath
        {
            get { return _SrcPath; }
            set
            {
                _SrcPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SrcPath)));

            }
        }


        private string _SysPath = "";
        public string SysPath
        {
            get { return _SysPath; }
            set
            {
                _SysPath = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SysPath)));

            }
        }

        private ObservableCollection<RecentProject> _ProjectsList = new ObservableCollection<RecentProject>();
        public ObservableCollection<RecentProject> ProjectsList
        {
            get { return _ProjectsList; }
            set
            {
                _ProjectsList = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProjectsList)));

            }
        }

        public async void OpenRefFile()
        {
            StorageFile file = await fileService.OpenFile();
            if (file != null)
            {
                LocalCorpus.ReferenceSentences.Clear();
                bool full = await fileService.LoadSentencesToCorpus(file, "REF", LocalCorpus);
                if (full) RefPath = file.Path;
                else RefPath = "";
            }
            else await new Windows.UI.Popups.MessageDialog("Reference file was not open successfully. Please try again.").ShowAsync();

            file = null;
        }

        public async void OpenSysFile()
        {
            StorageFile file = await fileService.OpenFile();
            if (file != null)
            {
                LocalCorpus.SystemSentences.Clear();
                bool full = await fileService.LoadSentencesToCorpus(file, "SYS", LocalCorpus);
                if (full) SysPath = file.Path;
                else SysPath = "";
            }
            else await new Windows.UI.Popups.MessageDialog("System file was not open successfully. Please try again.").ShowAsync();

            file = null;
        }
        public async void OpenSrcFile()
        {
            StorageFile file = await fileService.OpenFile();
            if (file != null)
            {
                LocalCorpus.SourceSentences.Clear();
                bool full = await fileService.LoadSentencesToCorpus(file, "SRC", LocalCorpus);
                if (full) SrcPath = file.Path;
                else SrcPath = "";
            }
            else await new Windows.UI.Popups.MessageDialog("Source file was not open successfully. Please try again.").ShowAsync();

            file = null;
        }

        public void CancelNewProject()
        {
            LocalCorpus.SourceSentences.Clear();
            LocalCorpus.ProjectName = "";
            LocalCorpus.SystemSentences.Clear();
            LocalCorpus.ReferenceSentences.Clear();
            LocalCorpus.ModifiedSystemSentences.Clear();
            LocalCorpus.TimeOfEditSen.Clear();
        }

        public async void CreateProjectAsync(string projectName)
        {
            if (LocalCorpus.SourceSentences == null || LocalCorpus.SystemSentences == null)
            {
                await new Windows.UI.Popups.MessageDialog("The Source file and System file are obligatory! Please select both files.").ShowAsync();
            }
            else
            {
                if (LocalCorpus.SourceSentences.Count() == LocalCorpus.SystemSentences.Count())
                {
                    App.typologyApp = null;
                    App.StatisticsData = null;
                    LocalCorpus.ProjectName = projectName;
                    App.Corpus = LocalCorpus;
                    await new Windows.UI.Popups.MessageDialog("Project is created successfully. You will now be redirected to typology page...").ShowAsync();
                    Frame rootFrame = Window.Current.Content as Frame;
                    rootFrame.Navigate(typeof(MainPage), "TypologyPage");
                }
                else await new Windows.UI.Popups.MessageDialog("Chosen files do not respect the structure of parallel corpus. Please select correct ones.").ShowAsync();


            }
        }

        public void OpenExistingProject()
        {
            if (App.Corpus == null)
            {
                fileService.LoadExistingProject(null);

            }
            else
            {
                DisplayNewProjectPromptDialog();
            }
        }

        public async void OpenRecentProject(StorageFile recentFile)
        {
            if (App.Corpus == null)
            {
                ContentDialog locationPromptDialog = new ContentDialog
                {
                    Title = "Load an recent project",
                    Content = String.Format("Are you sure you want to load {0}?", recentFile.Name),
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes"
                };

                ContentDialogResult result = await locationPromptDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    App.Corpus = null;
                    App.typologyApp = null;
                    App.StatisticsData = null;
                    fileService.LoadExistingProject(recentFile);

                }

                // Close = 0, Primary = 1, Secondary = 2

            }
            else
            {
                ContentDialog locationPromptDialog = new ContentDialog
                {
                    Title = "Load an recent project",
                    Content = String.Format("The project {0} is currently loaded. Do you want to load a new project?", App.Corpus.ProjectName),
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes"
                };

                ContentDialogResult result = await locationPromptDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    App.Corpus = null;
                    App.typologyApp = null;
                    App.StatisticsData = null;
                    fileService.LoadExistingProject(recentFile);
                }

                // Close = 0, Primary = 1, Secondary = 2
            }
        }

        private async void DisplayNewProjectPromptDialog()
        {
            ContentDialog locationPromptDialog = new ContentDialog
            {
                Title = "Load a new project",
                Content = String.Format("The project {0} is currently loaded. Do you want to load a new project?", App.Corpus.ProjectName),
                CloseButtonText = "No",
                PrimaryButtonText = "Yes"
            };

            ContentDialogResult result = await locationPromptDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                App.Corpus = null;
                App.typologyApp = null;
                App.StatisticsData = null;
                fileService.LoadExistingProject(null);
            }

            // Close = 0, Primary = 1, Secondary = 2
        }


        public async void UpdateRecentProjectList()
        {
            StorageFile myFile;
            string token = "";
            var mruList = StorageApplicationPermissions.MostRecentlyUsedList;
            //mruList.Clear();

            AccessListEntryView entries = mruList.Entries;

            if (entries.Count() > 0)
            {
                foreach (AccessListEntry entry in entries)
                {
                    token = entry.Token;
                    try
                    {
                        myFile = await mruList.GetFileAsync(token);
                        ProjectsList.Add(new RecentProject()
                        { FileName = myFile.Name, FileToken = token });
                    }
                    catch (FileNotFoundException)
                    {
                        mruList.Remove(token);
                    }



                }
            }
        }

        public void ClearRecentProjectsList()
        {
            var mruList = StorageApplicationPermissions.MostRecentlyUsedList;
            mruList.Clear();
            ProjectsList.Clear();
        }

        public async void CheckIfProjectIsLoaded()
        {
            if (App.Corpus != null)
            {
                ContentDialog locationPromptDialog = new ContentDialog
                {
                    Title = "Create a new project",
                    Content = String.Format("The project {0} is currently loaded. Do you want to save this project first?", App.Corpus.ProjectName),
                    CloseButtonText = "No",
                    PrimaryButtonText = "Yes"
                };

                ContentDialogResult result = await locationPromptDialog.ShowAsync();

                if (result == ContentDialogResult.Primary)
                {
                    await fileService.SaveProjectAndAskAsync();
                    App.Corpus = null;
                    App.typologyApp = null;
                    App.StatisticsData = null;
                }
                else
                {
                    //App.Corpus = null;
                    //App.typologyApp = null;
                    //App.StatisticsData = null;

                }

                // Close = 0, Primary = 1, Secondary = 2
            }
        }
    }
}
