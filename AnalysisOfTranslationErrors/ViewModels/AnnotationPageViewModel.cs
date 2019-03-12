using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Text;
using AnalysisOfTranslationErrors.Models;
using System.ComponentModel;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;

namespace AnalysisOfTranslationErrors.ViewModels
{
    public class AnnotationPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Services.SentenceService sentenceService = new Services.SentenceService();

        private ActiveSentence _CurrentSentence = new ActiveSentence();
        public ActiveSentence CurrentSentence
        {
            get { return _CurrentSentence; }
            set
            {
                _CurrentSentence = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentSentence)));
            }
        }

        private List<ActiveSentence> _ListSentByCat = new List<ActiveSentence>();
        public List<ActiveSentence> ListSentByCat
        {
            get { return _ListSentByCat; }
            set
            {
                _ListSentByCat = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ListSentByCat)));

            }
        }

        private ActiveSentence _SearchedSent = new ActiveSentence();
        public ActiveSentence SearchedSent
        {
            get { return _SearchedSent; }
            set
            {
                _SearchedSent = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchedSent)));
            }
        }

        private int _SearchedIndex = new int();
        public int SearchedIndex
        {
            get { return _SearchedIndex; }
            set
            {
                _SearchedIndex = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SearchedIndex)));
            }
        }

        public async Task GenerateAnnoTreeAsync(TreeView annoTreeView)
        {

            annoTreeView.RootNodes.Clear();

            if (App.typologyApp != null)
            {
                foreach (var dim in App.typologyApp.Dimensions)
                {
                    if (dim.ParentId == "ROOT")
                    {
                        List<Models.Dimension> children = App.typologyApp.Dimensions.Where(p => p.ParentId == dim.Id).ToList();
                        if (children.Any())
                        {
                            TreeViewNode nodeWithChildren = new TreeViewNode() { Content = dim };
                            foreach (var child in children)
                            {
                                DimensionToNodeRecursion(nodeWithChildren, child, App.typologyApp);
                            }
                            annoTreeView.RootNodes.Add(nodeWithChildren);
                        }
                        else
                        {
                            annoTreeView.RootNodes.Add(new TreeViewNode() { Content = dim });
                        }
                    }
                }
            }
            else await new Windows.UI.Popups.MessageDialog("Can not initialize annotation tree view. Model is not loaded!").ShowAsync();
        }

        public void DimensionToNodeRecursion(TreeViewNode root, Dimension childDim, Typology model)
        {
            TreeViewNode tmpNode = new TreeViewNode() { Content = childDim };
            List<Models.Dimension> children = model.Dimensions.Where(p => p.ParentId == childDim.Id).ToList();

            if (children.Any())
            {
                foreach (var c in children)
                {
                    DimensionToNodeRecursion(tmpNode, c, model);
                }
                root.Children.Add(tmpNode);
            }
            else
            {
                root.Children.Add(tmpNode);
            }
        }

        public List<string> GetIssuesNames()
        {
            List<string> names = new List<string>();

            names = App.typologyApp.Dimensions.Select(p => p.Name).ToList();

            return names;
        }

        public async void GetCurrentSentence(int index)
        {
            ActiveSentence localSent = sentenceService.GetActiveSentenceByIndex(index);
            if (localSent != null)
            {
                CurrentSentence = localSent;
            }
            else await new Windows.UI.Popups.MessageDialog("The number of sentence is out of range.").ShowAsync();

        }

        public void GetNextSentence()
        {
            CurrentSentence = sentenceService.GetActiveSentenceByIndex(CurrentSentence.ActiveIndex + 1);
        }

        public void GetPreviousSentence()
        {
            CurrentSentence = sentenceService.GetActiveSentenceByIndex(CurrentSentence.ActiveIndex - 1);
        }

        public void LoadRichEditBox(RichEditBox box)
        {
            box.Document.SetText(TextSetOptions.FormatRtf, CurrentSentence.ActiveSysSentence.Text);
            LoadAnnotationsEditBox(box);
            //Sprjecava selekciju anotacije kod prijelaska na drugu recenicu
            box.Focus(FocusState.Keyboard);
            box.Document.Selection.SetRange(box.Document.Selection.EndPosition, box.Document.Selection.EndPosition);
        }

        public void LoadSearcedRichEditBox(RichEditBox box)
        {
            box.Document.SetText(TextSetOptions.FormatRtf, SearchedSent.ActiveSysSentence.Text);
            LoadAnnotationsSearchedEditBox(box);
        }

        public async void Annotate(ITextSelection selectedTxt, Dimension selectedDim)
        {
            if (selectedTxt != null && selectedDim != null)
            {

                //check if exist in list exact anno
                List<SysAnnotation> currentAnno = CurrentSentence.ActiveSysSentence.Annotations.
                    Where(p => p.StartPosition == selectedTxt.StartPosition && p.EndPosition == selectedTxt.EndPosition).ToList();

                if (currentAnno.Any()) // exatc anno exist - edit it
                {
                    int editIndex = CurrentSentence.ActiveSysSentence.Annotations.IndexOf(currentAnno.ElementAt(0));
                    CurrentSentence.ActiveSysSentence.Annotations.ElementAt(editIndex).AnnoDim = selectedDim;
                    sentenceService.ApplyVisualChangeAnno(selectedTxt, selectedDim.Color);
                }
                else
                {
                    // is it overlay or doesn't exist

                    if (sentenceService.CheckIfAnnoOverlay(CurrentSentence.ActiveSysSentence.Annotations, selectedTxt))
                    {
                        // anno overlays - do not create new anno or edit old
                        await new Windows.UI.Popups.MessageDialog("Can't add an annotation over existing one - overlay.").ShowAsync();
                    }
                    else if (selectedTxt.StartPosition == selectedTxt.EndPosition) // start == end 
                    {
                        await new Windows.UI.Popups.MessageDialog("Can't add an annotation! Please select at least one word.").ShowAsync();

                    }
                    else // anno doesn't exist - add it
                    {
                        SysAnnotation newAnno = new SysAnnotation()
                        {
                            AnnoDim = selectedDim,
                            StartPosition = selectedTxt.StartPosition,
                            EndPosition = selectedTxt.EndPosition
                        };

                        CurrentSentence.ActiveSysSentence.Annotations.Add(newAnno);
                        sentenceService.ApplyVisualChangeAnno(selectedTxt, selectedDim.Color);
                    }

                }
            }
        }

        public void LoadAnnotationsEditBox(RichEditBox box)
        {
            if (App.Corpus.SystemSentences.ElementAt(CurrentSentence.ActiveIndex).Annotations.Any())
            {
                foreach (var item in App.Corpus.SystemSentences.ElementAt(CurrentSentence.ActiveIndex).Annotations)
                {
                    box.Document.Selection.SetRange(item.StartPosition, item.EndPosition);
                    ITextSelection selectedTxt = box.Document.Selection;
                    sentenceService.ApplyVisualChangeAnno(selectedTxt, item.AnnoDim.Color);
                }
            }

        }

        public void LoadAnnotationsSearchedEditBox(RichEditBox box)
        {
            if (SearchedSent.ActiveSysSentence.Annotations != null)
            {
                if (SearchedSent.ActiveSysSentence.Annotations.Any())
                {
                    foreach (var item in SearchedSent.ActiveSysSentence.Annotations)
                    {
                        box.Document.Selection.SetRange(item.StartPosition, item.EndPosition);
                        ITextSelection selectedTxt = box.Document.Selection;
                        sentenceService.ApplyVisualChangeAnno(selectedTxt, item.AnnoDim.Color);
                    }
                }
            }

        }

        public void ClearSentenceFromAnnotation(RichEditBox box)
        {
            if (App.Corpus.SystemSentences.ElementAt(CurrentSentence.ActiveIndex).Annotations.Any())
            {
                foreach (var item in App.Corpus.SystemSentences.ElementAt(CurrentSentence.ActiveIndex).Annotations)
                {
                    box.Document.Selection.SetRange(item.StartPosition, item.EndPosition);
                    ITextSelection selectedTxt = box.Document.Selection;
                    sentenceService.UndoVisualChangeAnno(selectedTxt);
                }
                App.Corpus.SystemSentences.ElementAt(CurrentSentence.ActiveIndex).Annotations.Clear();
                CurrentSentence.ActiveSysSentence.Annotations.Clear();
            }
        }

        public async void ClearOneAnnotation(RichEditBox box)
        {
            if (App.Corpus.SystemSentences.ElementAt(CurrentSentence.ActiveIndex).Annotations.Any())
            {
                ITextSelection selectedTxt = box.Document.Selection;
                if (selectedTxt != null)
                {

                    List<SysAnnotation> delAnno = CurrentSentence.ActiveSysSentence.Annotations.
                    Where(p => p.StartPosition == selectedTxt.StartPosition && p.EndPosition == selectedTxt.EndPosition).ToList();

                    if (delAnno.Any())
                    {
                        int delIndex = CurrentSentence.ActiveSysSentence.Annotations.IndexOf(delAnno.ElementAt(0));
                        CurrentSentence.ActiveSysSentence.Annotations.RemoveAt(delIndex);
                        //App.Corpus.SystemSentences.ElementAt(CurrentSentence.ActiveIndex).Annotations.RemoveAt(delIndex);
                        sentenceService.UndoVisualChangeAnno(selectedTxt);
                    }
                    else await new Windows.UI.Popups.MessageDialog("Please select correctly the annotation - partial selection is not allowed!").ShowAsync();

                }

            }
        }

        public string GetAnnoName(ITextSelection selectedTxt)
        {
            int start = selectedTxt.StartPosition;

            int foundLocation = sentenceService.CheckIfAnnoExists(CurrentSentence.ActiveSysSentence.Annotations, start);

            start = 0;
            if (foundLocation == -1) return "No annotation";
            else return CurrentSentence.ActiveSysSentence.Annotations.ElementAt(foundLocation).AnnoDim.Name;


            //if (CurrentSentence != null)
            //{
            //    List<SysAnnotation> currentAnno = CurrentSentence.ActiveSysSentence.Annotations.
            //    Where(p => p.StartPosition == selectedTxt.StartPosition && p.EndPosition == selectedTxt.EndPosition).ToList();

            //    if (currentAnno.Any())
            //    {
            //        Dimension dim = currentAnno.ElementAt(0).AnnoDim;
            //        return " " + dim.Name;
            //    }
            //    else return " No annotation";
            //}
            //else return " No annotation";

        }


        public void SearchSentByCategory(string cat)
        {
            if (cat != "")
            {
                ListSentByCat.Clear();

                foreach (var sysSen in App.Corpus.SystemSentences)
                {
                    var foundAnno = sysSen.Annotations.Where(x => x.AnnoDim.Name == cat).ToList();

                    if (foundAnno.Any())
                    {
                        ActiveSentence localSent = sentenceService.GetActiveSentenceByIndex(sysSen.Index);
                        ListSentByCat.Add(localSent);
                    }
                }

                if (ListSentByCat.Any())
                {
                    SearchedIndex = 0;
                    SearchedSent = ListSentByCat.ElementAt(SearchedIndex);
                }
                else
                {
                    SearchedIndex = 0;
                    SearchedSent = new ActiveSentence()
                    {
                        ActiveIndex = 0,
                        ActiveModSysSentence = new Sentence() { Index = 0, Text = "" },
                        ActiveRefSentence = new Sentence() { Index = 0, Text = "" },
                        ActiveSrcSentence = new Sentence() { Index = 0, Text = "" },
                        ActiveSysSentence = new SysSentence() { Index = 0, Text = "", Annotations = null }
                    };
                }

            }
            else
            {
                ListSentByCat.Clear();
                SearchedIndex = 0;
                SearchedSent = new ActiveSentence()
                {
                    ActiveIndex = 0,
                    ActiveModSysSentence = new Sentence() { Index = 0, Text = "" },
                    ActiveRefSentence = new Sentence() { Index = 0, Text = "" },
                    ActiveSrcSentence = new Sentence() { Index = 0, Text = "" },
                    ActiveSysSentence = new SysSentence() { Index = 0, Text = "", Annotations = null }
                };
            }

        }

        public void GetNextSearchedSentence()
        {
            if (App.Corpus != null)
            {
                int newIndex = SearchedIndex + 1;

                if (newIndex < ListSentByCat.Count() && newIndex >= 0) SearchedIndex = newIndex;
                else if (newIndex == ListSentByCat.Count) SearchedIndex = 0;
                else if (newIndex < 0) SearchedIndex = ListSentByCat.Count - 1;

                if (ListSentByCat.Any()) SearchedSent = ListSentByCat.ElementAt(SearchedIndex);
            }

        }

        public void GetPreviousSearchedSentence()
        {
            if (App.Corpus != null)
            {
                int newIndex = SearchedIndex - 1;

                if (newIndex < ListSentByCat.Count() && newIndex >= 0) SearchedIndex = newIndex;
                else if (newIndex == ListSentByCat.Count) SearchedIndex = 0;
                else if (newIndex < 0) SearchedIndex = ListSentByCat.Count - 1;

                if (ListSentByCat.Any()) SearchedSent = ListSentByCat.ElementAt(SearchedIndex);

            }
        }
    }
}
