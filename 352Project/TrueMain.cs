

#pragma checksum "..\..\App.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0BCE2F085C7B433E9260313B7032446A228DD0EE"


using _352Project;
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


namespace _352Project
{


    /// <summary>
    /// App
    /// </summary>
    public partial class TrueMain : System.Windows.Application
    {

        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]


        public void InitializeComponent()
        {

#line 5 "..\..\App.xaml"
            this.StartupUri = new System.Uri("MainMenu.xaml", System.UriKind.Relative);

#line default
#line hidden
        }



        /// Application Entry Point.

        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main()
        {

            _352Project.TrueMain app = new _352Project.TrueMain();
            app.InitializeComponent();


        

            app.Run();
        }
    }
}
