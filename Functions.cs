using EdIt.Properties;
using System.Windows.Controls;
using System.Windows.Media;

using static EdIt.MainWindow;

namespace EdIt
{
    internal class Functions
    {
        public static SolidColorBrush DarkBackground;
        public static SolidColorBrush DarkForeGround;

        public static SolidColorBrush LightBackground;
        public static SolidColorBrush LightForeGround;

        public static void SetItemTheme(MenuItem Item)
        {
            if (Settings.Default.DarkMode)
            {
                Item.Background = Functions.DarkBackground;
                Item.Foreground = DarkForeGround;
                Item.BorderBrush = DarkBackground;
            }
            else
            {
                Item.Background = LightBackground;
                Item.Foreground = LightForeGround;
                Item.BorderBrush = LightBackground;
            }
        }

        public static void SetItemTheme(GridSplitter Item) // Override for GridSplitter type
        {
            if (Settings.Default.DarkMode)
            {
                Item.Background = DarkBackground;
                Item.Foreground = DarkForeGround;
                Item.BorderBrush = DarkBackground;
            }
            else
            {
                Item.Background = LightBackground;
                Item.Foreground = LightForeGround;
                Item.BorderBrush = LightBackground;
            }
        }
        public static void SetItemTheme(StackPanel Item) // Override for StackPanel type
        {
            if (Settings.Default.DarkMode)
            {
                Item.Background = DarkBackground;
            }
            else
            {
                Item.Background = LightBackground;
            }
        }
        public static void SetItemTheme(Menu Item) // Override for Menu type
        {
            if (Settings.Default.DarkMode)
            {
                Item.Background = DarkBackground;
                Item.BorderBrush = DarkBackground;
            }
            else
            {
                Item.Background = LightBackground;
                Item.BorderBrush = LightBackground;
                Item.Foreground = LightForeGround;
            }
        }

        public static void SetItemTheme(TextBox Item) // Override for TextBox type
        {
            if (Settings.Default.DarkMode)
            {
                Item.Background = DarkBackground;
                Item.Foreground = DarkForeGround;
                Item.BorderBrush = DarkBackground;
            }
            else
            {
                Item.Background = LightBackground;
                Item.Foreground = LightForeGround;
                Item.BorderBrush = LightBackground;
            }
        }
    }
}
