using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hermes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ProjectFileHandler.ValidateFileStructure();
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
    }
}
