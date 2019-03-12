using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using AnalysisOfTranslationErrors.Models;
using System.Text.RegularExpressions;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Graphics.Imaging;
using Windows.Graphics.Display;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml.Controls;

namespace AnalysisOfTranslationErrors.Services
{
    public class FileService
    {
        public async Task<StorageFile> OpenFile()
        {
            var openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add(".rtf");
            openPicker.FileTypeFilter.Add(".doc");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                return file;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> LoadSentencesToCorpus(StorageFile file, string key, Corpus corpus)
        {

            IList<string> lines = await FileIO.ReadLinesAsync(file);

            if (lines.Any())
            {
                if (key == "REF")
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        corpus.ReferenceSentences.Add(new Sentence() { Index = i, Text = lines.ElementAt(i) });
                    }
                }
                else if (key == "SYS")
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        corpus.SystemSentences.Add(new SysSentence()
                        {
                            Index = i,
                            Text = lines.ElementAt(i)
                        });
                        corpus.ModifiedSystemSentences.Add(new Sentence() { Index = i, Text = lines.ElementAt(i) });
                        long time = 0;
                        corpus.TimeOfEditSen.Add(time);
                    }
                }
                else if (key == "SRC")
                {
                    for (int i = 0; i < lines.Count(); i++)
                    {
                        corpus.SourceSentences.Add(new Sentence() { Index = i, Text = lines.ElementAt(i) });
                    }
                }
                return true;
            }
            else
            {
                if (key == "REF") { corpus.ReferenceSentences.Clear(); }
                else if (key == "SYS") { corpus.SystemSentences.Clear(); }
                else if (key == "SRC") { corpus.SourceSentences.Clear(); }

                await new Windows.UI.Popups.MessageDialog("Chosen file is empty. Please select another file.").ShowAsync();
                return false;
            }

        }

        public Typology LoadTypologyFromFile(IList<string> lines)
        {
            Typology myTypology = new Typology();

            for (int i = 0; i < lines.Count(); i = i + 4)
            {
                string id = lines[i].Trim().Substring(4);
                string name = lines[i + 1].Trim().Substring(6);
                string parentId = lines[i + 2].Trim().Substring(10);
                string color = lines[i + 3].Trim().Substring(7);

                myTypology.Dimensions.Add(new Dimension() { Id = id, Name = name, ParentId = parentId, Color = color });
            }

            return myTypology;


        }

        public async Task<Typology> LoadTypologyFromCoreLocalFile()
        {
            string path = "ms-appx:///Assets/mqm_core.txt";
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(path));
            Typology coreTypology;
            if (file == null)
            {
                await new Windows.UI.Popups.MessageDialog("There is no MQM CORE file in application library.").ShowAsync();
                return null;
            }
            else
            {
                IList<string> lines = await FileIO.ReadLinesAsync(file);
                coreTypology = LoadTypologyFromFile(lines);
                return coreTypology;
            }
        }

        public Corpus LoadCorpusFromFile(IList<string> lines)
        {
            Corpus myCorpus = new Corpus();

            int index = 0;
            for (int i = 0; i < lines.Count(); i = i + 6)
            {
                string src = lines[i].Trim().Substring(5);
                myCorpus.SourceSentences.Add(new Sentence() { Index = index, Text = src });

                string refLine = "";
                if (lines[i + 1].Length > 5) refLine = lines[i + 1].Trim().Substring(5);
                myCorpus.ReferenceSentences.Add(new Sentence() { Index = index, Text = refLine });

                string sys = lines[i + 2].Trim().Substring(5);
                myCorpus.SystemSentences.Add(new SysSentence() { Index = index, Text = sys });

                string sysAnno = "";
                if (lines[i + 3].Length > 9)
                {
                    sysAnno = lines[i + 3].Trim().Substring(9);
                    string[] listOfAnno = sysAnno.Split(' '); //   XXX*XXX*STRING
                    List<string> rez = new List<string>();
                    Regex regex = new Regex("(?<=#).*?(?=#)");

                    foreach (var item in listOfAnno)
                    {
                        foreach (Match match in regex.Matches(item))
                        {
                            rez.Add(match.Value);
                        }
                    }

                    for (int j = 0; j < rez.Count(); j = j + 3)
                    {
                        Dimension foundDim = new Dimension();
                        var dimList = App.typologyApp.Dimensions.Where(d => d.Id == rez[j + 2]).ToList();
                        if (dimList.Any()) foundDim = dimList.ElementAt(0);
                        else return null;

                        SysAnnotation newAnno = new SysAnnotation()
                        {
                            StartPosition = Int32.Parse(rez[j]),
                            EndPosition = Int32.Parse(rez[j + 1]),
                            AnnoDim = foundDim
                        };

                        myCorpus.SystemSentences.ElementAt(index).Annotations.Add(newAnno);
                    }


                }

                string sysModify = "";
                if (lines[i + 4].Length > 11) sysModify = lines[i + 4].Trim().Substring(11);
                myCorpus.ModifiedSystemSentences.Add(new Sentence() { Index = index, Text = sysModify });

                long time = 0;
                string timeStr = "";
                if (lines[i + 5].Length > 13)
                {
                    timeStr = lines[i + 5].Trim().Substring(13);
                    time = long.Parse(timeStr);
                }
                myCorpus.TimeOfEditSen.Add(time);
                index++;

            }
            // build model
            return myCorpus;
        }

        public async Task SaveProjectAndAskAsync()
        {
            if (App.Corpus != null)
            {
                if (App.typologyApp != null)
                {
                    var savePicker = new FileSavePicker();
                    savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
                    // Dropdown of file types the user can save the file as
                    savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
                    // Default file name if the user does not type one in or select a file to replace
                    savePicker.SuggestedFileName = App.Corpus.ProjectName;
                    var file = await savePicker.PickSaveFileAsync();

                    if (file != null)
                    {
                        string lines = GetTypologyLines();
                        lines += GetCorpusLines();
                        await FileIO.WriteTextAsync(file, lines);
                        lines = "";
                        App.Corpus.ProjectName = file.DisplayName;
                        AddToMRU(file);
                        await new Windows.UI.Popups.MessageDialog("Project has been saved successfully.").ShowAsync();
                    }
                    else
                        await new Windows.UI.Popups.MessageDialog("Project file is not saved.").ShowAsync();

                }
                else
                {
                    await new Windows.UI.Popups.MessageDialog("Please define MQM typology before saving the project.").ShowAsync();

                }
            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("There is no loaded project to save.").ShowAsync();
            }
        }


        public string GetCorpusLines()
        {
            string lines = "";
            lines = "SENTENCES" + Environment.NewLine;
            for (int i = 0; i < App.Corpus.SourceSentences.Count(); i++)
            {
                lines += ("SRC: " + App.Corpus.SourceSentences.ElementAt(i).Text + Environment.NewLine);

                if (App.Corpus.ReferenceSentences.Count() > 0)
                {
                    lines += ("REF: " + App.Corpus.ReferenceSentences.ElementAt(i).Text + Environment.NewLine);
                }
                else
                {
                    lines += ("REF: " + Environment.NewLine);
                }
                lines += ("SYS: " + App.Corpus.SystemSentences.ElementAt(i).Text + Environment.NewLine);

                lines += "SYSANNO: ";
                foreach (var item in App.Corpus.SystemSentences.ElementAt(i).Annotations)
                {
                    lines += "#" + item.StartPosition + "#" + item.EndPosition + "#" + item.AnnoDim.Id + "#" + " ";
                }
                lines += Environment.NewLine;

                lines += ("SYSMODIFY: " + App.Corpus.ModifiedSystemSentences.ElementAt(i).Text + Environment.NewLine);
                lines += ("SYSEDITTIME: " + App.Corpus.TimeOfEditSen.ElementAt(i).ToString() + Environment.NewLine);
            }

            return lines;

        }

        public string GetTypologyLines()
        {
            string lines = "";

            lines = "ANNO_TYPOLOGY" + Environment.NewLine;
            foreach (var item in App.typologyApp.Dimensions)
            {
                lines += ("ID: " + item.Id + Environment.NewLine);
                lines += ("Name: " + item.Name + Environment.NewLine);
                lines += ("ParentID: " + item.ParentId + Environment.NewLine);
                lines += ("Color: " + item.Color + Environment.NewLine);
            }

            return lines;

        }



        public async void LoadExistingProject(StorageFile projectFile)
        {
            if (projectFile == null) projectFile = await OpenFile();

            if (projectFile == null)
            {
                await new Windows.UI.Popups.MessageDialog("Project file was not loaded correctly. Please, try again.").ShowAsync();
            }
            else
            {

                IList<string> lines = await FileIO.ReadLinesAsync(projectFile);

                if (lines.Any())
                {
                    if (lines.ElementAt(0).StartsWith("ANNO_TYPOLOGY"))
                    {
                        IList<string> typologyLines = new List<string>();
                        foreach (var item in lines)
                        {
                            if (item.StartsWith("ID: ") || item.StartsWith("Name: ") || item.StartsWith("ParentID: ") || item.StartsWith("Color: "))
                                typologyLines.Add(item);
                        }
                        if (typologyLines.Count() % 4 == 0)
                        {
                            App.typologyApp = LoadTypologyFromFile(typologyLines);

                            IList<string> corpusLines = new List<string>();
                            foreach (var item in lines)
                            {
                                if (item.StartsWith("SRC: ") || item.StartsWith("REF: ") || item.StartsWith("SYS: ") || item.StartsWith("SYSANNO: ") || item.StartsWith("SYSMODIFY: ") || item.Contains("SYSEDITTIME: "))
                                    corpusLines.Add(item);
                            }
                            if (corpusLines.Count() % 6 == 0)
                            {
                                App.Corpus = LoadCorpusFromFile(corpusLines);
                                if (App.Corpus == null)
                                {
                                    await new Windows.UI.Popups.MessageDialog("Project file is corrupted. There was an unauthorized change of predefined Typology data.").ShowAsync();
                                }
                                else
                                {
                                    App.Corpus.ProjectName = projectFile.DisplayName;
                                    // add to mru
                                    AddToMRU(projectFile);

                                    await new Windows.UI.Popups.MessageDialog("Project file was loaded successfully.").ShowAsync();

                                }

                            }
                            else
                            {
                                await new Windows.UI.Popups.MessageDialog("Project file is corrupted. There is a problem with structure of Corpus data in file.").ShowAsync();
                            }
                        }
                        else
                        {
                            await new Windows.UI.Popups.MessageDialog("Project file is corrupted. There is a problem with structure of Typology data in file.").ShowAsync();
                        }






                    }
                    else await new Windows.UI.Popups.MessageDialog("Project file is corrupted. Please obey the structure of project file and load correct file.").ShowAsync();
                }
                else await new Windows.UI.Popups.MessageDialog("Project file is empty. Please load another one.").ShowAsync();

            }
        }


        public void AddToMRU(StorageFile myFile)
        {
            var mruList = StorageApplicationPermissions.MostRecentlyUsedList;
            while (mruList.Entries.Count() >= mruList.MaximumItemsAllowed)
                mruList.Remove(mruList.Entries.First().Token);
            if (!mruList.Entries.Any(x => x.Metadata == myFile.Path))
                mruList.Add(myFile, myFile.Path);
            var futureAccessList = StorageApplicationPermissions.FutureAccessList;
            futureAccessList.Add(myFile);
        }

        public async Task SaveStatisticsFileAsync()
        {
            if (App.Corpus != null && App.StatisticsData != null && App.typologyApp != null)
            {
                var savePicker = new FileSavePicker();
                savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
                // Dropdown of file types the user can save the file as
                savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt", ".rtf", ".doc" });
                // Default file name if the user does not type one in or select a file to replace
                savePicker.SuggestedFileName = "Statistics_" + App.StatisticsData.ProjectName.Trim();
                var file = await savePicker.PickSaveFileAsync();

                if (file != null)
                {
                    string lines = "";

                    lines += "STATISTIC DATA FOR PROJECT: " + App.StatisticsData.ProjectName + Environment.NewLine + Environment.NewLine;
                    lines += "NUMBER OF SENTENCES: " + App.StatisticsData.NumSentences + Environment.NewLine;
                    lines += "NUMBER OF ERRORS IN PROJECT: " + App.StatisticsData.NumErrorAll + Environment.NewLine;
                    lines += "AVERAGE NUMBER OF ERRORS PER SENTENCE: " + App.StatisticsData.AvgErrorSen + Environment.NewLine;

                    TimeSpan t = TimeSpan.FromMilliseconds(AverageTimeEdit());
                    string avgTime = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                                            t.Hours,
                                            t.Minutes,
                                            t.Seconds,
                                            t.Milliseconds);
                    lines += "AVERAGE TIME OF EDITING SYSTEM SENTENCE: " + avgTime + Environment.NewLine + Environment.NewLine;
                    lines += "NUMBER OF ERRORS FOR EACH NODE IN THE ERROR TYPOLOGY:" + Environment.NewLine;

                    foreach (var dim in App.typologyApp.Dimensions)
                    {
                        lines += dim.Name + ": " + dim.Errors + Environment.NewLine;
                    }

                    lines += Environment.NewLine + "NUMBER OF ERRORS FOR EACH ERROR TYPE BY EXACT SENTENCE:" + Environment.NewLine;


                    int counter = 0;
                    foreach (var sent in App.Corpus.SystemSentences)
                    {
                        lines += Environment.NewLine + "Number of sentence: " + sent.Index + Environment.NewLine;
                        counter = 0;
                        foreach (var type in App.typologyApp.Dimensions)
                        {
                            counter = sent.Annotations.Where(a => a.AnnoDim == type).Count();
                            lines += type.Name + ": " + counter + Environment.NewLine;
                        }
                    }

                    //SaveChartAsPNG(App.StatisticsData.ChartOne, "ChartOne");
                    //lines += "CHART ONE" + Environment.NewLine;
                    //lines += "CHART TWO" + Environment.NewLine;

                    await FileIO.WriteTextAsync(file, lines);
                    lines = "";
                    await new Windows.UI.Popups.MessageDialog("Statistics file has been saved successfully.").ShowAsync();
                }
                else
                    await new Windows.UI.Popups.MessageDialog("Statistics file is not saved.").ShowAsync();

            }
            else
            {
                await new Windows.UI.Popups.MessageDialog("There is no statistic data. Please load/create a project first and then generate statistic data.").ShowAsync();
            }
        }

        public Single AverageTimeEdit()
        {
            Single suma = App.Corpus.TimeOfEditSen.Sum(t => t);
            var avgTimePerSent = suma / App.Corpus.ModifiedSystemSentences.Count();
            return avgTimePerSent;
        }

        public async Task SaveChartAsPNGAsync(StackPanel panel, string panelName)
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(panel);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.Desktop;
            // Dropdown of file types the user can save the file as

            savePicker.FileTypeChoices.Add("JPEG format", new List<string> { ".jpg" });
            savePicker.FileTypeChoices.Add("Portable Network Graphics", new List<String> { ".png" });
            savePicker.DefaultFileExtension = ".png";
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = panelName;
            var saveFile = await savePicker.PickSaveFileAsync();


            // Verify the user selected a file
            if (saveFile == null)
                await new Windows.UI.Popups.MessageDialog("Chart is not saved.").ShowAsync();


            // Encode the image to the selected file on disk
            using (var fileStream = await saveFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);

                encoder.SetPixelData(
                    BitmapPixelFormat.Bgra8,
                    BitmapAlphaMode.Straight,
                    (uint)renderTargetBitmap.PixelWidth,
                    (uint)renderTargetBitmap.PixelHeight,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    DisplayInformation.GetForCurrentView().LogicalDpi,
                    pixelBuffer.ToArray());

                await encoder.FlushAsync();
            }
        }
    }
}
