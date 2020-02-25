using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Reflection;
using System.Xml;

namespace Hermes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Form controls
        private ProgressBar _progressBar;
        private TabControl _leftTabControl;
        private TabControl _rightTabControl;
        private Canvas _canvas;

        public MainWindow()
        {
            InitializeComponent();
            BindComponents();
            ProjectFileHandler.ValidateFileStructure();
            ProjectFileHandler.LoadConfigData();
        }

        

        private void BindComponents()
        {
            _progressBar = (ProgressBar)FindName("ProgressBar");
            _leftTabControl = (TabControl)FindName("LeftTabControl");
            _rightTabControl = (TabControl)FindName("RightTabControl");
            _canvas = (Canvas)FindName("GameCanvasEditor");

            
        }

        /* Menu handlers */

        /***    File     ***/
        /*      New      */
        private void NewProject(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog
            {
                InitialDirectory = ProjectFileHandler.GetFormattedUserSpacePath("projects"),
                Title="New Project"
            };

            var res = dialog.ShowDialog();
            if (res==true)
            {

            }
        }

        /*      Open     */
        private void OpenProject(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = ProjectFileHandler.GetFormattedUserSpacePath("projects"),
                Filter = "Hermes Projects (*.hrms)|*.hrms",
                Title="Open Project"
            };
            var res = dialog.ShowDialog();
            if (res == true)
            {

            }
        }

        /*      Save     */
        private void SaveProject(object sender, RoutedEventArgs e)
        {

        }

        /*      Exit     */
        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        /***    File     ***/
        public void ToggleTabView(object sender, RoutedEventArgs e)
        {

        }
    }
}
