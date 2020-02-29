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
using System.Windows.Media;

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
        private Window _window;

        public MainWindow()
        {
            InitializeComponent();
            BindComponents();
            ProjectFileHandler.ValidateFileStructure();
            ProjectFileHandler.LoadConfigData();
        }

        public void SerializeControls()
        {

        }

        private void BindComponents()
        {
            _window = (Window)FindName("MWindow");
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
                Title = "New Project"
            };

            var res = dialog.ShowDialog();
            if (res == true)
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
                Title = "Open Project"
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

        /***    Settings   ***/


        /***    Edit   ***/
        public void CreateNewMap(object sender, RoutedEventArgs e)
        {
            var tabItem = (TabItem)_leftTabControl.Items.GetItemAt(1);

            var listView = (ListView) tabItem.Content;
            var item = new ListViewItem();
            var stackPanel = new StackPanel()
            {
                Orientation = Orientation.Horizontal
            };

            var viewButton = new Button()
            {
                Content="New Map",
                HorizontalAlignment=HorizontalAlignment.Stretch,
                Width=110,
                MaxWidth=130,
            };

            viewButton.Click += new RoutedEventHandler(SwitchToMap);

            var delButton = new Button()
            {
                Content="-",
                Width=20,
                Background=Brushes.Red,
                Margin = new Thickness(3, 0, 0, 0)
            };

            delButton.Click += new RoutedEventHandler(DeleteCurrentMap);

            stackPanel.Children.Add(viewButton);
            stackPanel.Children.Add(delButton);

            item.Content = stackPanel;
            listView.Items.Add(item);
            //tabItem.Content = listView;

            //_leftTabControl.Items.Add(tabItem);


        }

        public void DeleteCurrentMap(object sender, RoutedEventArgs e)
        {

        }

        public void SwitchToMap(object sender, RoutedEventArgs e)
        {

        }
    }
}
