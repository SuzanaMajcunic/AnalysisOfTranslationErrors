using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnalysisOfTranslationErrors.Models;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AnalysisOfTranslationErrors.ViewModels
{
    public class TypologyPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Services.FileService _FileService = new Services.FileService();
        Services.SentenceService _SentenceService = new Services.SentenceService();


        private Models.Typology _MyTypology = new Typology(); // Core tree view
        public Models.Typology MyTypology
        {
            get { return _MyTypology; }
            set
            {
                _MyTypology = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(MyTypology)));
            }
        }


        private TreeViewNode _InvokedTreeNode;
        public TreeViewNode InvokedTreeNode
        {
            get { return _InvokedTreeNode; }
            set
            {
                _InvokedTreeNode = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InvokedTreeNode)));
            }
        }

        private string _InvokedNodeLabel;
        public string InvokedNodeLabel
        {
            get { return _InvokedNodeLabel; }
            set
            {
                _InvokedNodeLabel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InvokedNodeLabel)));
            }
        }


        public async void OpenCoreFileAndLoadTreeByDimensions(TreeView tree)
        {

            MyTypology = await _FileService.LoadTypologyFromCoreLocalFile();

            if (MyTypology == null) await new Windows.UI.Popups.MessageDialog("File is open and model is failed.").ShowAsync();
            else
            {
                //await new Windows.UI.Popups.MessageDialog("File is open and model is loaded.").ShowAsync();
                await InitializeTreeViewByDimensions(tree, MyTypology);
            }
        }

        public async Task InitializeTreeViewByDimensions(TreeView treeView, Typology typology)
        {
            treeView.RootNodes.Clear();

            if (typology != null)
            {
                foreach (var dim in typology.Dimensions)
                {
                    if (dim.ParentId == "ROOT")
                    {
                        List<Models.Dimension> children = typology.Dimensions.Where(p => p.ParentId == dim.Id).ToList();
                        if (children.Any())
                        {
                            TreeViewNode nodeWithChildren = new TreeViewNode() { Content = dim };

                            foreach (var child in children)
                            {
                                DimensionToNodeRecursion(nodeWithChildren, child, typology);
                            }
                            treeView.RootNodes.Add(nodeWithChildren);
                        }
                        else
                        {
                            treeView.RootNodes.Add(new TreeViewNode() { Content = dim });
                        }
                    }
                }
            }
            else await new Windows.UI.Popups.MessageDialog("Can not initialize tree view. Model is not loaded!").ShowAsync();
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

        public async void GenerateAdaptedTreeByCoreAsync(TreeView adaptedTree, IList<TreeViewNode> selectedNodes)
        {
            if (App.Corpus != null)
            {
                if (_SentenceService.CheckIfAnyAnnoInProject())
                {
                    await new Windows.UI.Popups.MessageDialog("Can not modify typology - defined issue types are already used in annotation.").ShowAsync();
                }
                else
                {
                    adaptedTree.RootNodes.Clear();
                    App.typologyApp = LoadModelByCoreTree(selectedNodes);


                    await InitializeTreeViewByDimensions(adaptedTree, App.typologyApp);
                }

            }
            else await new Windows.UI.Popups.MessageDialog("Please load project first.").ShowAsync();

        }

        public Typology LoadModelByCoreTree(IList<TreeViewNode> selectedNodes)
        {
            Typology typology = new Typology();

            foreach (var sNode in selectedNodes)
            {
                var dim = sNode.Content as Dimension;

                if (dim.ParentId == "ROOT") typology.Dimensions.Add(dim);
                else
                {
                    typology.Dimensions.Add(dim);
                    var dimParent = MyTypology.Dimensions.Where(p => p.Id == dim.ParentId);

                    if (typology.Dimensions.Contains(dimParent.ElementAt(0)))
                    {
                        continue;
                    }
                    else
                    {
                        typology.Dimensions.Add(dimParent.ElementAt(0));
                    }

                }
            }

            return typology;
        }

        public async void AddChildNode(string nodeName, TreeView adaptedTree)
        {
            if (_SentenceService.CheckIfAnyAnnoInProject())
            {
                await new Windows.UI.Popups.MessageDialog("Can not modify typology - defined issue types are already used in annotation.").ShowAsync();

            }
            else
            {
                if (!string.IsNullOrEmpty(nodeName))
                {
                    if (InvokedTreeNode == null)
                    {
                        await new Windows.UI.Popups.MessageDialog("Please select parent issue type.").ShowAsync();
                    }
                    else
                    {
                        Dimension invokedDim = InvokedTreeNode.Content as Dimension;
                        if (InvokedTreeNode != null)
                        {
                            Dimension dim = new Dimension() { Name = nodeName, Id = ChangeToId(nodeName), ParentId = invokedDim.Id, Color = invokedDim.Color };
                            App.typologyApp.Dimensions.Add(dim);
                            InvokedTreeNode.Children.Add(new TreeViewNode() { Content = dim });
                        }
                    }
                }
                else
                {
                    await new Windows.UI.Popups.MessageDialog("Can not add issue type without name!").ShowAsync();
                }
            }

        }

        public async void DeleteChildNode(TreeView adaptedTree)
        {
            if (_SentenceService.CheckIfAnyAnnoInProject())
            {
                await new Windows.UI.Popups.MessageDialog("Can not modify typology - defined issue types are already used in annotation.").ShowAsync();

            }
            else
            {
                if (InvokedTreeNode == null)
                {
                    await new Windows.UI.Popups.MessageDialog("Please select issue type to delete.").ShowAsync();
                }
                else
                {
                    Dimension invokedDim = InvokedTreeNode.Content as Dimension;
                    App.typologyApp.Dimensions.Remove(invokedDim);
                    InvokedTreeNode.Parent.Children.Remove(InvokedTreeNode);

                }
            }

        }

        public string ChangeToId(string a)
        {
            string b = a.Trim().ToLower().Replace(" ", "-");
            return b;
        }


    }


    public class CardTemplateSelector : DataTemplateSelector
    {
        public DataTemplate TreeViewItemDataTemplate { get; set; }

        protected override DataTemplate SelectTemplateCore(object item)
        {
            TreeViewNode treeViewNode = item as TreeViewNode;
            if (treeViewNode.Content is Models.Dimension)
            {
                return TreeViewItemDataTemplate;
            }

            return base.SelectTemplateCore(item);
        }
    }
}
