using Microsoft.Win32;
using System.Windows;
using System.IO;
using EdIt.Properties;
using System.Windows.Media;
using System.Windows.Controls;
using System.Collections.Generic;

using static EdIt.Functions;

namespace EdIt
{
    public partial class MainWindow : Window
    {

        List<MenuItem> HistoryItems;
        
        public MainWindow()
        {
            DarkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b2b2b"));
            DarkForeGround = Brushes.White;

            LightBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            LightForeGround = Brushes.Black;

            if (Settings.Default.FileHistory == null)
            {
                Settings.Default.FileHistory = new System.Collections.Specialized.StringCollection();
                Settings.Default.Save();
            }

            InitializeComponent();
            LoadConfig();
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            var Dialog = new OpenFileDialog();
            if (Dialog.ShowDialog() == true)
            {
                TextEditor.Clear();
                TextEditor.Text = File.ReadAllText(Dialog.FileName);
                this.Title = Dialog.SafeFileName;
                AddToHistory(Dialog.FileName);
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            var SaveDialog = new SaveFileDialog();

            SaveDialog.DefaultExt = "txt";
            SaveDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (SaveDialog.ShowDialog() == true)
            {
                File.WriteAllText(SaveDialog.FileName, TextEditor.Text);
                this.Title = SaveDialog.SafeFileName;
                AddToHistory(SaveDialog.FileName);
            }
        }


        private void NewFile(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ConfirmationResult = MessageBox.Show("Any unsaved text will be lost.", "New File", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (ConfirmationResult == MessageBoxResult.Yes)
            {
                TextEditor.Clear();
                this.Title = "EdIt";
            }
        }

        private void TextWrappingToggle(object sender, RoutedEventArgs e)
        {
            if (TextEditor.TextWrapping == TextWrapping.Wrap)
                TextEditor.TextWrapping = TextWrapping.NoWrap;
            else
                TextEditor.TextWrapping = TextWrapping.Wrap;

            SaveConfig();
        }
        private void SpellcheckToggle(object sender, RoutedEventArgs e)
        {
            if (TextEditor.SpellCheck.IsEnabled)
                TextEditor.SpellCheck.IsEnabled = false;
            else
                TextEditor.SpellCheck.IsEnabled = true;

            SaveConfig();
            SpellcheckToggleButton.IsChecked = Settings.Default.Spellcheck;
            TextWrappingToggleButton.IsChecked = Settings.Default.Wrapping;
        }

        private void DarkModeToggle(object sender, RoutedEventArgs e)
        {
            DarkModeToggleButton.IsChecked = !DarkModeToggleButton.IsChecked;
            SaveConfig();
            ApplyTheme();
        }

        public void SaveConfig()
        {
            Settings.Default.Spellcheck = TextEditor.SpellCheck.IsEnabled;
            
            if (TextEditor.TextWrapping == TextWrapping.Wrap)
                Settings.Default.Wrapping = true;
            else
                Settings.Default.Wrapping = false;

            Settings.Default.DarkMode = DarkModeToggleButton.IsChecked;

            Settings.Default.Save();
            SpellcheckToggleButton.IsChecked = Settings.Default.Spellcheck;
            TextWrappingToggleButton.IsChecked = Settings.Default.Wrapping;
        }

        public void LoadConfig()
        {
            TextEditor.SpellCheck.IsEnabled = Settings.Default.Spellcheck;

            if (Settings.Default.Wrapping)
                TextEditor.TextWrapping = TextWrapping.Wrap;
            else
                TextEditor.TextWrapping = TextWrapping.NoWrap;

            SpellcheckToggleButton.IsChecked = Settings.Default.Spellcheck;
            TextWrappingToggleButton.IsChecked = Settings.Default.Wrapping;

            ApplyTheme();
            DarkModeToggleButton.IsChecked = Settings.Default.DarkMode;
        }

        public void LoadHistory()
        {
            if (Settings.Default.FileHistory.Count >= 6)
            {
                Settings.Default.FileHistory.RemoveAt(0);
                LoadHistory();
                return;
            }

            HistoryItems = new List<MenuItem>();
            
            for (int HistoryIndex = 0; HistoryIndex < Settings.Default.FileHistory.Count; HistoryIndex++)
            {
                MenuItem NewHistoryItem = new MenuItem();
                NewHistoryItem.Header = Path.GetFileName(Settings.Default.FileHistory[HistoryIndex]);
                NewHistoryItem.Tag = Settings.Default.FileHistory[HistoryIndex];
                NewHistoryItem.Click += LoadHistoryFile;
                HistoryHeader.Items.Add(NewHistoryItem);
                HistoryItems.Add(NewHistoryItem);

                if (Settings.Default.DarkMode)
                {
                    NewHistoryItem.Background = DarkBackground;
                    NewHistoryItem.Foreground = DarkForeGround;
                }
                else
                {
                    NewHistoryItem.Background = LightBackground;
                    NewHistoryItem.Foreground = LightForeGround;
                }
            }
        }

        public void AddToHistory(string FilePath)
        {
            Settings.Default.FileHistory.Add(FilePath);
            Settings.Default.Save();

            foreach (MenuItem Item in HistoryItems)
            {
                HistoryHeader.Items.Remove(Item);
            }

            LoadHistory();
        }

        public void LoadHistoryFile(object sender, RoutedEventArgs e)
        {
            MenuItem HistoryItem = sender as MenuItem;
            
            if (!File.Exists(HistoryItem.Tag.ToString()))
                return;

            TextEditor.Clear();
            TextEditor.Text = File.ReadAllText(HistoryItem.Tag.ToString());
            this.Title = Path.GetFileName(HistoryItem.Tag.ToString());
        }

        public void ApplyTheme()
        {
            SetItemTheme(GridSplitter);
            SetItemTheme(StackPanel);
            SetItemTheme(MenuContainer);

            SetItemTheme(TextEditor);
            SetItemTheme(FileHeader);
            SetItemTheme(SettingsHeader);
            SetItemTheme(HistoryHeader);
            SetItemTheme(TextWrappingToggleButton);
            SetItemTheme(SpellcheckToggleButton);

            SetItemTheme(NewFileButton);
            SetItemTheme(OpenFileButton);
            SetItemTheme(SaveFileButton);
            SetItemTheme(DarkModeToggleButton);

            if (HistoryItems == null)
                LoadHistory();
            foreach (MenuItem HistoryItem in HistoryItems)
            {
                SetItemTheme(HistoryItem);
            }
        }
    }
}