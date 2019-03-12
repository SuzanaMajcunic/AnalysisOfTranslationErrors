using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisOfTranslationErrors.Models;
using Windows.UI;
using Windows.UI.Text;
using Windows.UI.Xaml.Markup;

namespace AnalysisOfTranslationErrors.Services
{
    public class SentenceService
    {
        public ActiveSentence GetActiveSentenceByIndex(int index)
        {
            int i = 0;

            if (index < App.Corpus.SourceSentences.Count && index >= 0) i = index;
            else if (index == App.Corpus.SourceSentences.Count) i = 0;
            else if (index < 0) i = App.Corpus.SourceSentences.Count - 1;
            else return null;

            ActiveSentence aSentence = new ActiveSentence();
            aSentence.ActiveIndex = i;
            aSentence.ActiveSrcSentence = App.Corpus.SourceSentences.ElementAt(i);
            aSentence.ActiveSysSentence = App.Corpus.SystemSentences.ElementAt(i);

            aSentence.ActiveModSysSentence = App.Corpus.ModifiedSystemSentences.ElementAt(i);

            if (App.Corpus.ReferenceSentences.Count() > 0)
            {
                aSentence.ActiveRefSentence = App.Corpus.ReferenceSentences.ElementAt(i);
            }
            else aSentence.ActiveRefSentence = new Sentence() { Index = i, Text = "" };

            return aSentence;
        }

        public int CheckIfAnnoExists(ObservableCollection<SysAnnotation> sysAnnotations, int location)
        {
            bool found = false;
            int foundLocation = -1;
            for (int i = 0; i < sysAnnotations.Count(); i++)
            {
                if (location >= sysAnnotations.ElementAt(i).StartPosition && location <= sysAnnotations.ElementAt(i).EndPosition)
                {
                    found = true;
                    foundLocation = i;
                }
            }

            if (found) return foundLocation;
            else return -1;
        }

        public bool CheckIfAnyAnnoInProject()
        {
            bool found = false;
            foreach (var sysSent in App.Corpus.SystemSentences)
            {
                if (sysSent.Annotations.Any()) found = true;
            }

            if (found == false) return false;
            else return true;
        }

        public bool CheckIfAnnoOverlay(ObservableCollection<SysAnnotation> sysAnnotations, ITextSelection sText)
        {
            bool overlay = false;
            int s = sText.StartPosition;
            int e = sText.EndPosition;
            foreach (var item in sysAnnotations)
            {
                if (s == item.StartPosition && (e < item.EndPosition || e > item.EndPosition)) overlay = true;
                if (e == item.EndPosition && (s < item.StartPosition || s > item.StartPosition)) overlay = true;
                if (s > item.StartPosition && e < item.EndPosition) overlay = true;
                if (s < item.StartPosition && e > item.EndPosition) overlay = true;
                if (s >= item.StartPosition && s <= item.EndPosition) overlay = true;
                if (e >= item.StartPosition && e <= item.EndPosition) overlay = true;
            }

            if (overlay) return true;
            else return false;
        }

        public void ApplyVisualChangeAnno(ITextSelection selectedTxt, string color)
        {
            ITextCharacterFormat charFormatting = selectedTxt.CharacterFormat;
            charFormatting.Bold = FormatEffect.On;
            charFormatting.Underline = UnderlineType.Single;
            charFormatting.ForegroundColor = GetColor(color);
            selectedTxt.CharacterFormat = charFormatting;
        }

        public void UndoVisualChangeAnno(ITextSelection selectedTxt)
        {
            ITextCharacterFormat charFormatting = selectedTxt.CharacterFormat;
            charFormatting.Bold = FormatEffect.Off;
            charFormatting.Underline = UnderlineType.None;
            charFormatting.ForegroundColor = GetColor("");
            selectedTxt.CharacterFormat = charFormatting;
        }

        public Color GetColor(string myColor)
        {
            if (myColor == "RED")
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "RED");
            else if (myColor == "GREEN")
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "GREEN");
            else if (myColor == "PURPLE")
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "PURPLE");
            else if (myColor == "OLIVE")
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "OLIVE");
            else if (myColor == "SADDLEBROWN")
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "SADDLEBROWN");
            else if (myColor == "DEEPPINK")
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "DEEPPINK");
            else if (myColor == "BLUE")
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "BLUE");
            else
                return (Color)XamlBindingHelper.ConvertValue(typeof(Color), "BLACK");
        }
    }
}
