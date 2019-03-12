using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AnalysisOfTranslationErrors
{
    public sealed partial class TypologyPage : Page
    {
        public TypologyPage()
        {
            this.InitializeComponent();
            ViewModel.OpenCoreFileAndLoadTreeByDimensions(coreTreeView);
            ViewModel.InvokedNodeLabel = "No selected node";
        }


        private void adaptedTreeView_ItemInvoked(TreeView sender, TreeViewItemInvokedEventArgs args)
        {
            var node = args.InvokedItem as TreeViewNode;
            ViewModel.InvokedTreeNode = node;
            Models.Dimension dim = node.Content as Models.Dimension;
            ViewModel.InvokedNodeLabel = dim.Name;
        }

        private void GenerateTree_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.GenerateAdaptedTreeByCoreAsync(adaptedTreeView, coreTreeView.SelectedNodes);
        }

        private void CancelAddChildButton_Click(object sender, RoutedEventArgs e)
        {
            AddChildFlyout.Hide();
            NewIssueTextBox.Text = "";
            ViewModel.InvokedTreeNode = null;
            ViewModel.InvokedNodeLabel = "No selected node";
        }

        private void CancelDeleteChildButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteChildFlyout.Hide();
            ViewModel.InvokedTreeNode = null;
            ViewModel.InvokedNodeLabel = "No selected node";
        }

        private void AddChildButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AddChildNode(NewIssueTextBox.Text, adaptedTreeView);
            AddChildFlyout.Hide();
            adaptedTreeView.UpdateLayout();
            NewIssueTextBox.Text = "";
            ViewModel.InvokedTreeNode = null;
            ViewModel.InvokedNodeLabel = "No selected node";
        }

        private void DeleteChildButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteChildNode(adaptedTreeView);
            DeleteChildFlyout.Hide();
            adaptedTreeView.UpdateLayout();
            ViewModel.InvokedTreeNode = null;
            ViewModel.InvokedNodeLabel = "No selected node";

        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.typologyApp != null)
            {
                await ViewModel.InitializeTreeViewByDimensions(adaptedTreeView, App.typologyApp);
            }
        }
    }
}
