﻿#pragma checksum "E:\Projects\AnalysisOfTranslationErrors\AnalysisOfTranslationErrors\Views\StatisticsPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "CE20999586B2B901170D862BA2462542"
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
    partial class StatisticsPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private static class XamlBindingSetters
        {
            public static void Set_Windows_UI_Xaml_Controls_TextBlock_Text(global::Windows.UI.Xaml.Controls.TextBlock obj, global::System.String value, string targetNullValue)
            {
                if (value == null && targetNullValue != null)
                {
                    value = targetNullValue;
                }
                obj.Text = value ?? global::System.String.Empty;
            }
        };

        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        private class StatisticsPage_obj1_Bindings :
            global::Windows.UI.Xaml.Markup.IComponentConnector,
            IStatisticsPage_Bindings
        {
            private global::AnalysisOfTranslationErrors.StatisticsPage dataRoot;
            private bool initialized = false;
            private const int NOT_PHASED = (1 << 31);
            private const int DATA_CHANGED = (1 << 30);

            // Fields for each control that has bindings.
            private global::Windows.UI.Xaml.Controls.TextBlock obj10;
            private global::Windows.UI.Xaml.Controls.TextBlock obj11;
            private global::Windows.UI.Xaml.Controls.TextBlock obj12;
            private global::Windows.UI.Xaml.Controls.TextBlock obj13;
            private global::Windows.UI.Xaml.Controls.TextBlock obj14;

            public StatisticsPage_obj1_Bindings()
            {
            }

            // IComponentConnector

            public void Connect(int connectionId, global::System.Object target)
            {
                switch(connectionId)
                {
                    case 10: // Views\StatisticsPage.xaml line 101
                        this.obj10 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 11: // Views\StatisticsPage.xaml line 97
                        this.obj11 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 12: // Views\StatisticsPage.xaml line 93
                        this.obj12 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 13: // Views\StatisticsPage.xaml line 89
                        this.obj13 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    case 14: // Views\StatisticsPage.xaml line 85
                        this.obj14 = (global::Windows.UI.Xaml.Controls.TextBlock)target;
                        break;
                    default:
                        break;
                }
            }

            // IStatisticsPage_Bindings

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
                    this.dataRoot = (global::AnalysisOfTranslationErrors.StatisticsPage)newDataRoot;
                    return true;
                }
                return false;
            }

            public void Loading(global::Windows.UI.Xaml.FrameworkElement src, object data)
            {
                this.Initialize();
            }

            // Update methods for each path node used in binding steps.
            private void Update_(global::AnalysisOfTranslationErrors.StatisticsPage obj, int phase)
            {
                this.Update_AnalysisOfTranslationErrors_App_StatisticsData(global::AnalysisOfTranslationErrors.App.StatisticsData, phase);
            }
            private void Update_AnalysisOfTranslationErrors_App_StatisticsData(global::AnalysisOfTranslationErrors.Models.StatisticsData obj, int phase)
            {
                if (obj != null)
                {
                    if ((phase & (NOT_PHASED | (1 << 0))) != 0)
                    {
                        this.Update_AnalysisOfTranslationErrors_App_StatisticsData_AvgTimeEdit(obj.AvgTimeEdit, phase);
                        this.Update_AnalysisOfTranslationErrors_App_StatisticsData_AvgErrorSen(obj.AvgErrorSen, phase);
                        this.Update_AnalysisOfTranslationErrors_App_StatisticsData_NumErrorAll(obj.NumErrorAll, phase);
                        this.Update_AnalysisOfTranslationErrors_App_StatisticsData_NumSentences(obj.NumSentences, phase);
                        this.Update_AnalysisOfTranslationErrors_App_StatisticsData_ProjectName(obj.ProjectName, phase);
                    }
                }
            }
            private void Update_AnalysisOfTranslationErrors_App_StatisticsData_AvgTimeEdit(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\StatisticsPage.xaml line 101
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj10, obj, null);
                }
            }
            private void Update_AnalysisOfTranslationErrors_App_StatisticsData_AvgErrorSen(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\StatisticsPage.xaml line 97
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj11, obj, null);
                }
            }
            private void Update_AnalysisOfTranslationErrors_App_StatisticsData_NumErrorAll(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\StatisticsPage.xaml line 93
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj12, obj, null);
                }
            }
            private void Update_AnalysisOfTranslationErrors_App_StatisticsData_NumSentences(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\StatisticsPage.xaml line 89
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj13, obj, null);
                }
            }
            private void Update_AnalysisOfTranslationErrors_App_StatisticsData_ProjectName(global::System.String obj, int phase)
            {
                if ((phase & ((1 << 0) | NOT_PHASED )) != 0)
                {
                    // Views\StatisticsPage.xaml line 85
                    XamlBindingSetters.Set_Windows_UI_Xaml_Controls_TextBlock_Text(this.obj14, obj, null);
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
            case 1: // Views\StatisticsPage.xaml line 1
                {
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)(target);
                    ((global::Windows.UI.Xaml.Controls.Page)element1).Loaded += this.Page_Loaded;
                }
                break;
            case 2: // Views\StatisticsPage.xaml line 17
                {
                    this.ViewModel = (global::AnalysisOfTranslationErrors.ViewModels.StatisticsPageViewModel)(target);
                }
                break;
            case 3: // Views\StatisticsPage.xaml line 44
                {
                    this.CardTemplateSelector = (global::AnalysisOfTranslationErrors.ViewModels.CardTemplateSelector)(target);
                }
                break;
            case 5: // Views\StatisticsPage.xaml line 116
                {
                    this.FirstChart = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 6: // Views\StatisticsPage.xaml line 158
                {
                    this.SecondChart = (global::Windows.UI.Xaml.Controls.StackPanel)(target);
                }
                break;
            case 7: // Views\StatisticsPage.xaml line 163
                {
                    this.PieChart = (global::WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart)(target);
                }
                break;
            case 8: // Views\StatisticsPage.xaml line 144
                {
                    this.LineChart = (global::WinRTXamlToolkit.Controls.DataVisualization.Charting.Chart)(target);
                }
                break;
            case 9: // Views\StatisticsPage.xaml line 106
                {
                    this.resultTreeView = (global::Windows.UI.Xaml.Controls.TreeView)(target);
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
            case 1: // Views\StatisticsPage.xaml line 1
                {                    
                    global::Windows.UI.Xaml.Controls.Page element1 = (global::Windows.UI.Xaml.Controls.Page)target;
                    StatisticsPage_obj1_Bindings bindings = new StatisticsPage_obj1_Bindings();
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
