﻿#pragma checksum "E:\Projects\AnalysisOfTranslationErrors\AnalysisOfTranslationErrors\Views\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "640803BCD0A97212672843ED8C361D06"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AnalysisOfTranslationErrors
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class MainPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IMainPage_Bindings
        {
            private global::AnalysisOfTranslationErrors.MainPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.MenuFlyoutItem obj15;
            private global::Windows.UI.Xaml.Controls.MenuFlyoutItem obj16;
            private global::Windows.UI.Xaml.Controls.MenuFlyoutItem obj17;
            private global::Windows.UI.Xaml.Controls.MenuFlyoutItem obj18;

            public MainPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 15: // Views\MainPage.xaml line 52
                        this.obj15 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target;
                        ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target).Click += (global::System.Object sender, global::Windows.UI.Xaml.RoutedEventArgs e) =>
                        {
                            this.dataRoot.ViewModel.SaveProject();
                        };
                        break;
                    case 16: // Views\MainPage.xaml line 54
                        this.obj16 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target;
                        ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target).Click += (global::System.Object sender, global::Windows.UI.Xaml.RoutedEventArgs e) =>
                        {
                            this.dataRoot.ViewModel.SaveStatistics();
                        };
                        break;
                    case 17: // Views\MainPage.xaml line 57
                        this.obj17 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target;
                        ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target).Click += (global::System.Object sender, global::Windows.UI.Xaml.RoutedEventArgs e) =>
                        {
                            this.dataRoot.ViewModel.SaveChartOne();
                        };
                        break;
                    case 18: // Views\MainPage.xaml line 59
                        this.obj18 = (global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target;
                        ((global::Windows.UI.Xaml.Controls.MenuFlyoutItem)target).Click += (global::System.Object sender, global::Windows.UI.Xaml.RoutedEventArgs e) =>
                        {
                            this.dataRoot.ViewModel.SaveChartTwo();
                        };
                        break;
                    default:
                        break;
                }
            }

            // IMainPage_Bindings

            public void Initialize()
            {
                if (!this.initialized)
                {
                    this.Update();
                }
            }
            
            public void Update()
            {
                this.Update_(this.dataRoot, NOT_PHASED);
                this.initialized = true;
            }

            public void StopTracking()
            {
            }

            public void DisconnectUnloadedObject(int connectionId)
            {
                throw new global::System.ArgumentException("No unloadable elements to disconnect.");
            }

            public bool SetDataRoot(global::System.Object newDataRoot)
            {
                if (newDataRoot != null)
                {
                    this.dataRoot = (global::AnalysisOfTranslationErrors.MainPage)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::AnalysisOfTranslationErrors.MainPage obj, int phase)
            {
                if (obj != null)
                {
                }
            }
        }
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // Views\MainPage.xaml line 13
                {
                    this.ViewModel = (global::AnalysisOfTranslationErrors.ViewModels.MainPageViewModel)(target);
                }
                break;
            case 3: // Views\MainPage.xaml line 34
                {
                    this.HamburgerButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                    ((global::Windows.UI.Xaml.Controls.AppBarButton)this.HamburgerButton).Click += this.HamburgerButton_Click;
                }
                break;
            case 4: // Views\MainPage.xaml line 71
                {
                    this.SplitView = (global::Windows.UI.Xaml.Controls.SplitView)(target);
                }
                break;
            case 5: // Views\MainPage.xaml line 116
                {
                    this.FooterText = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 6: // Views\MainPage.xaml line 77
                {
                    this.NavigationListBox = (global::Windows.UI.Xaml.Controls.ListBox)(target);
                    ((global::Windows.UI.Xaml.Controls.ListBox)this.NavigationListBox).SelectionChanged += this.NavigationListBox_SelectionChanged;
                }
                break;
            case 7: // Views\MainPage.xaml line 78
                {
                    this.HomeListBoxItem = (global::Windows.UI.Xaml.Controls.ListBoxItem)(target);
                }
                break;
            case 8: // Views\MainPage.xaml line 85
                {
                    this.TypologyListBoxItem = (global::Windows.UI.Xaml.Controls.ListBoxItem)(target);
                }
                break;
            case 9: // Views\MainPage.xaml line 92
                {
                    this.AnnotationListBoxItem = (global::Windows.UI.Xaml.Controls.ListBoxItem)(target);
                }
                break;
            case 10: // Views\MainPage.xaml line 99
                {
                    this.StatisticsListBoxItem = (global::Windows.UI.Xaml.Controls.ListBoxItem)(target);
                }
                break;
            case 11: // Views\MainPage.xaml line 111
                {
                    this.MyFrame = (global::Windows.UI.Xaml.Controls.Frame)(target);
                }
                break;
            case 12: // Views\MainPage.xaml line 42
                {
                    this.TitleTextBlock = (global::Windows.UI.Xaml.Controls.TextBlock)(target);
                }
                break;
            case 13: // Views\MainPage.xaml line 49
                {
                    this.SaveButton = (global::Windows.UI.Xaml.Controls.AppBarButton)(target);
                }
                break;
            case 14: // Views\MainPage.xaml line 64
                {
                    this.ToggleButton = (global::Windows.UI.Xaml.Controls.AppBarToggleButton)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
        }

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            switch(connectionId)
            {
            case 1: // Views\MainPage.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    MainPage_obj1_Bindings bindings = new MainPage_obj1_Bindings();
                    returnValue = bindings;
                    bindings.SetDataRoot(this);
                    this.Bindings = bindings;
                    element1.Loading += bindings.Loading;
                }
                break;
            }
            return returnValue;
        }
    }
}
