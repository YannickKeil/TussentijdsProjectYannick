﻿#pragma checksum "..\..\MainMenu.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "C4BB3D9DBB13B07A5063250ACA622F599C8F600373C718EB10B953DD3195CE6F"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using TussentijdsProjectYannick;


namespace TussentijdsProjectYannick {
    
    
    /// <summary>
    /// MainMenu
    /// </summary>
    public partial class MainMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDatabeheer;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOverzicht;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBestelling;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\MainMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLogOut;
        
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
            System.Uri resourceLocater = new System.Uri("/TussentijdsProjectYannick;component/mainmenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainMenu.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
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
            this.btnDatabeheer = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\MainMenu.xaml"
            this.btnDatabeheer.Click += new System.Windows.RoutedEventHandler(this.btnDatabeheer_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnOverzicht = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\MainMenu.xaml"
            this.btnOverzicht.Click += new System.Windows.RoutedEventHandler(this.btnOverzicht_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnBestelling = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\MainMenu.xaml"
            this.btnBestelling.Click += new System.Windows.RoutedEventHandler(this.btnBestelling_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnLogOut = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\MainMenu.xaml"
            this.btnLogOut.Click += new System.Windows.RoutedEventHandler(this.btnLogOut_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

