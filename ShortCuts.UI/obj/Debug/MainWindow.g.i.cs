﻿#pragma checksum "..\..\MainWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "56A16CB88EB5EAC92B4D45D6E7F7BCB84B2ECD343A9D04DF67457004DAF6F5C2"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AutoCompleteTextBox.Editors;
using ShortCuts.DAL.Models;
using ShortCuts.UI;
using ShortCuts.UI.Animation;
using ShortCuts.UI.AttachedProperties;
using ShortCuts.UI.Converters;
using ShortCuts.UI.Helpers.ControlExtensions;
using ShortCuts.UI.Pages;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ShortCuts.UI {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 16 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ShortCuts.UI.MainWindow WindowApp;
        
        #line default
        #line hidden
        
        
        #line 366 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid VideoGrid;
        
        #line default
        #line hidden
        
        
        #line 367 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MediaElement mediaElement;
        
        #line default
        #line hidden
        
        
        #line 390 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid ShortCutGrid;
        
        #line default
        #line hidden
        
        
        #line 413 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox SoftwareTypeComboBox;
        
        #line default
        #line hidden
        
        
        #line 479 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal AutoCompleteTextBox.Editors.AutoCompleteTextBox AutoCompleteText;
        
        #line default
        #line hidden
        
        
        #line 509 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView ShortCutListView;
        
        #line default
        #line hidden
        
        
        #line 615 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button EditButton;
        
        #line default
        #line hidden
        
        
        #line 628 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Shapes.Path EditButtonPath;
        
        #line default
        #line hidden
        
        
        #line 636 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock EditButtonText;
        
        #line default
        #line hidden
        
        
        #line 692 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ContentControl Dialog;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ShortCuts.UI;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.WindowApp = ((ShortCuts.UI.MainWindow)(target));
            
            #line 19 "..\..\MainWindow.xaml"
            this.WindowApp.Activated += new System.EventHandler(this.OnActivated);
            
            #line default
            #line hidden
            
            #line 20 "..\..\MainWindow.xaml"
            this.WindowApp.Deactivated += new System.EventHandler(this.OnDeactivated);
            
            #line default
            #line hidden
            
            #line 21 "..\..\MainWindow.xaml"
            this.WindowApp.Loaded += new System.Windows.RoutedEventHandler(this.OnViewLoaded);
            
            #line default
            #line hidden
            return;
            case 6:
            this.VideoGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 7:
            this.mediaElement = ((System.Windows.Controls.MediaElement)(target));
            
            #line 369 "..\..\MainWindow.xaml"
            this.mediaElement.MediaEnded += new System.Windows.RoutedEventHandler(this.MediaElement_MediaEnded);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 376 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnStopMediaClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ShortCutGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 10:
            this.SoftwareTypeComboBox = ((System.Windows.Controls.ComboBox)(target));
            
            #line 417 "..\..\MainWindow.xaml"
            this.SoftwareTypeComboBox.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.SoftwareTypeComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 11:
            this.AutoCompleteText = ((AutoCompleteTextBox.Editors.AutoCompleteTextBox)(target));
            return;
            case 12:
            this.ShortCutListView = ((System.Windows.Controls.ListView)(target));
            
            #line 518 "..\..\MainWindow.xaml"
            this.ShortCutListView.AddHandler(System.Windows.Controls.ScrollViewer.ScrollChangedEvent, new System.Windows.Controls.ScrollChangedEventHandler(this.ShortCutListView_ScrollChanged));
            
            #line default
            #line hidden
            return;
            case 14:
            this.EditButton = ((System.Windows.Controls.Button)(target));
            
            #line 626 "..\..\MainWindow.xaml"
            this.EditButton.Click += new System.Windows.RoutedEventHandler(this.UpdateShortCutClick);
            
            #line default
            #line hidden
            return;
            case 15:
            this.EditButtonPath = ((System.Windows.Shapes.Path)(target));
            return;
            case 16:
            this.EditButtonText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 17:
            this.Dialog = ((System.Windows.Controls.ContentControl)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 2:
            
            #line 155 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.ToggleButton)(target)).Checked += new System.Windows.RoutedEventHandler(this.ToggleButton_Checked);
            
            #line default
            #line hidden
            
            #line 156 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Primitives.ToggleButton)(target)).Unchecked += new System.Windows.RoutedEventHandler(this.ToggleButton_Unchecked);
            
            #line default
            #line hidden
            break;
            case 3:
            
            #line 211 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.ClickMinimize);
            
            #line default
            #line hidden
            break;
            case 4:
            
            #line 242 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.MaximizeClick);
            
            #line default
            #line hidden
            break;
            case 5:
            
            #line 275 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseClick);
            
            #line default
            #line hidden
            break;
            case 13:
            
            #line 576 "..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.OnRemoveClick);
            
            #line default
            #line hidden
            break;
            }
        }
    }
}
