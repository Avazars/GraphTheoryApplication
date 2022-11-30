﻿#pragma checksum "..\..\..\MainWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E53AF536473D0DCC574E8FF918D852F252A889AF"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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
using WpfApp1;


namespace WpfApp1 {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid navPanelLeft;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton MoveToolButton;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton VertexToolButton;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton EdgeToolButton;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton RemoveToolButton;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton ColorToolButton;
        
        #line default
        #line hidden
        
        
        #line 99 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid navPanelTop;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas DrawingGrid;
        
        #line default
        #line hidden
        
        
        #line 130 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid infoPanel;
        
        #line default
        #line hidden
        
        
        #line 153 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NumVerticesTextBlock;
        
        #line default
        #line hidden
        
        
        #line 162 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NumEdgesTextBlock;
        
        #line default
        #line hidden
        
        
        #line 171 "..\..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NumConnectivityTextBlock;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/WpfApp1;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "7.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.navPanelLeft = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.MoveToolButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 31 "..\..\..\MainWindow.xaml"
            this.MoveToolButton.Checked += new System.Windows.RoutedEventHandler(this.MoveToolButton_OnChecked);
            
            #line default
            #line hidden
            
            #line 32 "..\..\..\MainWindow.xaml"
            this.MoveToolButton.Unchecked += new System.Windows.RoutedEventHandler(this.MoveToolButton_OnUnChecked);
            
            #line default
            #line hidden
            return;
            case 3:
            this.VertexToolButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 45 "..\..\..\MainWindow.xaml"
            this.VertexToolButton.Checked += new System.Windows.RoutedEventHandler(this.VertexToolButton_OnChecked);
            
            #line default
            #line hidden
            
            #line 46 "..\..\..\MainWindow.xaml"
            this.VertexToolButton.Unchecked += new System.Windows.RoutedEventHandler(this.VertexToolButton_OnUnChecked);
            
            #line default
            #line hidden
            return;
            case 4:
            this.EdgeToolButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 59 "..\..\..\MainWindow.xaml"
            this.EdgeToolButton.Checked += new System.Windows.RoutedEventHandler(this.EdgeToolButton_OnChecked);
            
            #line default
            #line hidden
            
            #line 60 "..\..\..\MainWindow.xaml"
            this.EdgeToolButton.Unchecked += new System.Windows.RoutedEventHandler(this.EdgeToolButton_OnUnChecked);
            
            #line default
            #line hidden
            return;
            case 5:
            this.RemoveToolButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 74 "..\..\..\MainWindow.xaml"
            this.RemoveToolButton.Checked += new System.Windows.RoutedEventHandler(this.RemoveToolButton_OnChecked);
            
            #line default
            #line hidden
            
            #line 75 "..\..\..\MainWindow.xaml"
            this.RemoveToolButton.Unchecked += new System.Windows.RoutedEventHandler(this.RemoveToolButton_OnUnChecked);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ColorToolButton = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 88 "..\..\..\MainWindow.xaml"
            this.ColorToolButton.Checked += new System.Windows.RoutedEventHandler(this.ColorToolButton_OnChecked);
            
            #line default
            #line hidden
            
            #line 89 "..\..\..\MainWindow.xaml"
            this.ColorToolButton.Unchecked += new System.Windows.RoutedEventHandler(this.ColorToolButton_OnUnChecked);
            
            #line default
            #line hidden
            return;
            case 7:
            this.navPanelTop = ((System.Windows.Controls.Grid)(target));
            
            #line 100 "..\..\..\MainWindow.xaml"
            this.navPanelTop.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.topPanelMouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 111 "..\..\..\MainWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CloseButtonOnClick);
            
            #line default
            #line hidden
            return;
            case 9:
            this.DrawingGrid = ((System.Windows.Controls.Canvas)(target));
            
            #line 125 "..\..\..\MainWindow.xaml"
            this.DrawingGrid.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.DrawingGrid_OnMouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            case 10:
            this.infoPanel = ((System.Windows.Controls.Grid)(target));
            return;
            case 11:
            this.NumVerticesTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 12:
            this.NumEdgesTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 13:
            this.NumConnectivityTextBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

