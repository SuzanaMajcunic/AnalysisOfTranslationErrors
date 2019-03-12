using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using AnalysisOfTranslationErrors.Models;
using System.Diagnostics;
using Windows.UI;

namespace AnalysisOfTranslationErrors
{
    public sealed partial class AnnotationPage : Page
    {
        Stopwatch stopwatch = new Stopwatch();
        Services.SentenceService sentenceService = new Services.SentenceService();

        public AnnotationPage()
        {
            this.InitializeComponent();
        }


        private async void Page_LoadedAsync(object sender, RoutedEventArgs e)
        {

            if (App.Corpus != null)
            {
                ViewModel.GetCurrentSentence(0);
                ViewModel.LoadRichEditBox(SysEditBox);

                ViewModel.SearchSentByCategory("");
                ViewModel.LoadSearcedRichEditBox(SearcedSysEditBox);

                if (App.typologyApp != null) await ViewModel.GenerateAnnoTreeAsync(annoTreeView);
                else if (App.typologyApp == null)
                {
                    await new Windows.UI.Popups.MessageDialog("Please define MQM typology before annotation.").ShowAsync();
                }
            }
            else await new Windows.UI.Popups.MessageDialog("Please load a project.").ShowAsync();
        }




        private void SearchByCategoryBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (App.Corpus != null)
            {
                List<string> suggestions = ViewModel.GetIssuesNames();
                if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                {
                    var items = suggestions.Where(x => x.Contains(sender.Text));
                    if (!items.Any())
                        items = new List<string> { "No Results" };

                    sender.ItemsSource = items;
                }
            }

        }

        private void SearchByCategoryBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            if (args.SelectedItem.ToString() == "No Results")
            {
                sender.Text = string.Empty;

            }

        }

        private void SearchByCategoryBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                string category = args.ChosenSuggestion.ToString();
                if (category != "No Results")
                {
                    ViewModel.SearchSentByCategory(category);
                    ViewModel.LoadSearcedRichEditBox(SearcedSysEditBox);
                    FilterTextBlock.Text = " " + category;

                    var color = from dim in App.typologyApp.Dimensions
                                where dim.Name == category
                                select dim.Color;

                    Color textColor = sentenceService.GetColor(color.ElementAt(0));
                    FilterTextBlock.Foreground = new SolidColorBrush(textColor);
                    // User selected an item from the suggestion list, take an action on it here.
                }

            }
            else
            {

                // Use args.QueryText to determine what to do.
            }
        }

        public void ClearRichEditBox()
        {
            SysEditBox.Focus(FocusState.Keyboard);
            SysEditBox.Document.Selection.SetRange(0, SysEditBox.Document.Selection.EndPosition);
            ITextSelection sve = SysEditBox.Document.Selection;
            sentenceService.UndoVisualChangeAnno(sve);
            SysEditBox.Focus(FocusState.Keyboard);
            SysEditBox.Document.Selection.SetRange(0, 0);
        }

        private async void GetNext()
        {
            if (App.Corpus != null)
            {
                ClearRichEditBox();
                ViewModel.GetNextSentence();
                ViewModel.LoadRichEditBox(SysEditBox);
            }
            else await new Windows.UI.Popups.MessageDialog("Please load a project.").ShowAsync();

        }

        private async void GetPrevious()
        {
            if (App.Corpus != null)
            {
                ClearRichEditBox();
                ViewModel.GetPreviousSentence();
                ViewModel.LoadRichEditBox(SysEditBox);
            }
            else await new Windows.UI.Popups.MessageDialog("Please load a project.").ShowAsync();

        }

        private async void GetSearchedPrevious()
        {
            if (App.Corpus != null)
            {
                ViewModel.GetPreviousSearchedSentence();
                ViewModel.LoadSearcedRichEditBox(SearcedSysEditBox);
            }
            else await new Windows.UI.Popups.MessageDialog("Please load a project.").ShowAsync();

        }

        private async void GetSearchedNext()
        {
            if (App.Corpus != null)
            {
                ViewModel.GetNextSearchedSentence();
                ViewModel.LoadSearcedRichEditBox(SearcedSysEditBox);
            }
            else await new Windows.UI.Popups.MessageDialog("Please load a project.").ShowAsync();

        }

        private void SearchSentenceSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

            if (args.QueryText != "")
            {
                try
                {
                    int index = Int32.Parse(args.QueryText);
                    ViewModel.GetCurrentSentence(index);
                    SysEditBox.Document.SetText(Windows.UI.Text.TextSetOptions.FormatRtf, ViewModel.CurrentSentence.ActiveSysSentence.Text);

                }
                catch (FormatException)
                {
                    sender.Text = "";
                }

                // User selected an item from the suggestion list, take an action on it here.
            }
            else
            {

                // Use args.QueryText to determine what to do.
            }
        }


        private void ModSysTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (App.Corpus != null)
            {
                stopwatch.Reset();
                stopwatch.Start();
            }
        }


        private async void SaveModSentence_Click(object sender, RoutedEventArgs e)
        {
            if (App.Corpus != null)
            {
                int i = ViewModel.CurrentSentence.ActiveIndex;
                App.Corpus.ModifiedSystemSentences.ElementAt(i).Text = ViewModel.CurrentSentence.ActiveModSysSentence.Text;
                stopwatch.Stop();
                long elapsed_time = stopwatch.ElapsedMilliseconds;

                //String vrijeme = String.Format("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed);
                //RefTextBox.Text = vrijeme;
                App.Corpus.TimeOfEditSen[i] += elapsed_time;
                elapsed_time = 0;
            }
            else await new Windows.UI.Popups.MessageDialog("Please load a project.").ShowAsync();


        }

        private void ClearSentenceButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearSentenceFromAnnotation(SysEditBox);
            AnnoNameTextBlock.Text = "No annotation";
        }

        private void ClearOneAnnoButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.ClearOneAnnotation(SysEditBox);
            AnnoNameTextBlock.Text = "No annotation";
        }

        private void SysEditBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedTxt = SysEditBox.Document.Selection;
            if (selectedTxt != null && ViewModel.CurrentSentence != null && App.Corpus != null)
            {
                string anno = ViewModel.GetAnnoName(selectedTxt);
                AnnoNameTextBlock.Text = anno;
            }

        }

        private void annoTreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var node = args.InvokedItem as TreeViewNode;
            Dimension selDim = node.Content as Dimension;

            ITextSelection selectedText = SysEditBox.Document.Selection;


            // selectedText.SetRange(selectedText.StartPosition, selectedText.EndPosition - 1);

            ViewModel.Annotate(selectedText, selDim);
            AnnoNameTextBlock.Text = selDim.Name;

            string str;
            SysEditBox.Document.Selection.GetText(TextGetOptions.FormatRtf, out str);
            var end = str.Length - 1;
            SysEditBox.Document.Selection.SetRange(end, end); // !!!!!!!!!!!!!!!
            SysEditBox.Focus(FocusState.Keyboard);


        }

        private void SrcTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            SrcTextBox.Select(0, 0);
        }


    }
}
