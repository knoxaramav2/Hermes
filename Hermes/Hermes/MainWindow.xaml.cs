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
using Hermes.projects;

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
        private Menu _menu;

        //State
        private MapManager _mapManager;
        private ProjectManager _projectManager;

        public MainWindow()
        {
            InitializeComponent();
            BindComponents();

            _mapManager = new MapManager();
            _projectManager = new ProjectManager();

            LockComponents(GuiLockMode.LOCK_NO_PROJECT);

            ProjectFileHandler.ValidateFileStructure();
            ProjectFileHandler.LoadConfigData();
        }

        public void UpdateWindowTitle()
        {
            var map = _mapManager.GetCurrentMap();
            string mapName = "";
            if (map != null) { mapName = " - "+map.Name; }

            _window.Title = $"{_projectManager.GetProjectTitle()} {mapName}";
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
            _menu = (Menu)FindName("TopMenu");
        }

        private void LockComponents(GuiLockMode mode)
        {
            switch (mode)
            {
                case GuiLockMode.LOCK_ALL:

                    break;
                case GuiLockMode.UNLOCK_ALL:

                    break;
                case GuiLockMode.LOCK_NO_PROJECT:

                    break;
            }
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
                if (Directory.Exists(dialog.FileName))
                {
                    var msg = "Project already exists";
                    var caption = "Unable to create projects";
                    var btn = MessageBoxButton.OK;
                    var icon = MessageBoxImage.Error;
                    MessageBox.Show(msg, caption, btn, icon);
                } else
                {
                    _projectManager.NewProject(dialog.FileName);
                    UpdateWindowTitle();
                }
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
                _projectManager.LoadProject(dialog.FileName);
                _mapManager = new MapManager();
                UpdateWindowTitle();
            }
        }

        /*      Save     */
        private void SaveProject(object sender, RoutedEventArgs e)
        {
            _mapManager.SaveAllMapData();
            _projectManager.SaveProject();
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

            //TODO Defaults file
            var id = _mapManager.NewMap("New Map", 50, 50);
            //_projectManager.SetMapCounter(_mapManager.GetCurrentMap().MapId);
            _projectManager.IncrementMapCounter();

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
                CommandParameter=$"{_mapManager.GetCurrentMap().MapId}"
            };

            viewButton.Click += new RoutedEventHandler(SwitchToMap);

            var delButton = new Button()
            {
                Content="-",
                Width=20,
                Background=Brushes.Red,
                Margin = new Thickness(3, 0, 0, 0),
                CommandParameter = $"{_mapManager.GetCurrentMap().MapId}"
            };

            delButton.Click += new RoutedEventHandler(DeleteCurrentMap);

            stackPanel.Children.Add(viewButton);
            stackPanel.Children.Add(delButton);

            item.Content = stackPanel;
            listView.Items.Add(item);

            _mapManager.SelectMap(id);
            LoadMap();
        }

        public void DeleteCurrentMap(object sender, RoutedEventArgs e)
        {
            _mapManager.DeleteMap();
        }

        public void SwitchToMap(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(e);
        }

        public void LoadMap()
        {
            UpdateWindowTitle();
            var map = _mapManager.GetCurrentMap();
        }
    }

    enum GuiLockMode{
        UNLOCK_ALL,         //Unlock all options
        LOCK_ALL,           //Lock all options
        LOCK_NO_PROJECT,    //Lock all options requiring active project
        }
}
