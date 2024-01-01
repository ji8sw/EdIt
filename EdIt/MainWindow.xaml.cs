using Microsoft.Win32;
using System.Windows;
using System.IO;
using EdIt.Properties;
using System.Windows.Media;

namespace EdIt
{
    public partial class MainWindow : Window
    {
        SolidColorBrush DarkBackground;
        SolidColorBrush DarkForeGround;

        SolidColorBrush LightBackground;
        SolidColorBrush LightForeGround;
        
        public MainWindow()
        {
            DarkBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#2b2b2b"));
            DarkForeGround = Brushes.White;

            LightBackground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            LightForeGround = Brushes.Black;

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
            }
        }


        private void NewFile(object sender, RoutedEventArgs e)
        {
            TextEditor.Clear();
            this.Title = "EdIt";
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
            if (Settings.Default.DarkMode)
                SwitchToLightMode();
            else
                SwitchToDarkMode();

            DarkModeToggleButton.IsChecked = !DarkModeToggleButton.IsChecked;
            SaveConfig();
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

            if (Settings.Default.DarkMode)
                SwitchToDarkMode();
            else
                SwitchToLightMode();

            DarkModeToggleButton.IsChecked = Settings.Default.DarkMode;
        }

        public void SwitchToDarkMode()
        {
            GridSplitter.Background = DarkBackground;
            StackPanel.Background = DarkBackground;
            MenuContainer.Background = DarkBackground;

            TextEditor.Background = DarkBackground;
            TextEditor.Foreground = DarkForeGround;

            FileHeader.Background = DarkBackground;
            FileHeader.Foreground = DarkForeGround;

            SettingsHeader.Background = DarkBackground;
            SettingsHeader.Foreground = DarkForeGround;

            TextWrappingToggleButton.Background = DarkBackground;
            TextWrappingToggleButton.Foreground = DarkForeGround;

            SpellcheckToggleButton.Background = DarkBackground;
            SpellcheckToggleButton.Foreground = DarkForeGround;

            NewFileButton.Background = DarkBackground;
            NewFileButton.Foreground = DarkForeGround;

            OpenFileButton.Background = DarkBackground;
            OpenFileButton.Foreground = DarkForeGround;

            SaveFileButton.Background = DarkBackground;
            SaveFileButton.Foreground = DarkForeGround;

            DarkModeToggleButton.Background = DarkBackground;
            DarkModeToggleButton.Foreground = DarkForeGround;
        }

        public void SwitchToLightMode()
        {
            GridSplitter.Background = LightBackground;
            StackPanel.Background = LightBackground;
            MenuContainer.Background = LightBackground;

            TextEditor.Background = LightBackground;
            TextEditor.Foreground = LightForeGround;

            FileHeader.Background = LightBackground;
            FileHeader.Foreground = LightForeGround;

            SettingsHeader.Background = LightBackground;
            SettingsHeader.Foreground = LightForeGround;

            TextWrappingToggleButton.Background = LightBackground;
            TextWrappingToggleButton.Foreground = LightForeGround;

            SpellcheckToggleButton.Background = LightBackground;
            SpellcheckToggleButton.Foreground = LightForeGround;

            NewFileButton.Background = LightBackground;
            NewFileButton.Foreground = LightForeGround;

            OpenFileButton.Background = LightBackground;
            OpenFileButton.Foreground = LightForeGround;

            SaveFileButton.Background = LightBackground;
            SaveFileButton.Foreground = LightForeGround;

            DarkModeToggleButton.Background = LightBackground;
            DarkModeToggleButton.Foreground = LightForeGround;
        }

    }
}