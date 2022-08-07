using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Roblox_Legacy_Place_Convertor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string fileToConvertPath;
        private string newFilePath;
        private bool isConverting;
        private static readonly Dictionary<string, string> color3uint8ToBrickColor = new Dictionary<string, string> // Yes, i wrote this all manually. Took me about 2 hours.
        {
            {"4294112243", "1"},
            {"4294901760", "1004"},
            {"4288986439", "119"},
            {"4294298928", "24"},
            {"4292511041", "106"},
            {"4291045404", "21"},
            {"4285215356", "104"},
            {"4279069100", "23"},
            {"4278226844", "107"},
            {"4283144011", "37"},
            {"4294506744", "1001"},
            {"4293256415", "208"},
            {"4291677645", "1002"},
            {"4288914085", "194"},
            {"4284702562", "199"},
            {"4279970357", "26"},
            {"4279308561", "1003"},
            {"4286549604", "1022"},
            {"4293040960", "105"},
            {"4293572754", "125"},
            {"4287986039", "153"},
            {"4287388575", "1023"},
            {"4285826717", "135"},
            {"4285438410", "102"},
            {"4286091394", "151"},
            {"4292330906", "5"},
            {"4294830733", "226"},
            {"4294946560", "1017"},
            {"4292511354", "101"},
            {"4293442248", "9"},
            {"4286626779", "11"},
            {"4279430868", "1018"},
            {"4288791692", "29"},
            {"4294954137", "1030"},
            {"4294967244", "1029"},
            {"4294953417", "1025"},
            {"4294928076", "1016"},
            {"4289832959", "1026"},
            {"4289715711", "1024"},
            {"4288672745", "1027"},
            {"4291624908", "1028"},
            {"4290887234", "1008"},
            {"4294967040", "1009"},
            {"4294901951", "1032"},
            {"4278190335", "1010"},
            {"4278255615", "1019"},
            {"4278255360", "1020"},
            {"4286340166", "217"},
            {"4291595881", "18"},
            {"4288700213", "38"},
            {"4284622289", "1031"},
            {"4290019583", "1006"},
            {"4278497260", "1013"},
            {"4290040548", "45"},
            {"4282023189", "1021"},
            {"4285087784", "192"},
            {"4289352960", "1014"},
            {"4288891723", "1007"},
            {"4289331370", "1015"},
            {"4280374457", "1012"},
            {"4278198368", "1011"},
            {"4280844103", "28"},
            {"4280763949", "141"}
        };
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GithubHyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            // You can remove this if you want, this is just me self promoting lol
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            if (isConverting == true) // So you can't browse for a place while it's converting
            {
                return;
            }
            // Opens a file dialog asking you which file do you want to convert
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Roblox XML Place Files (*.rbxlx)|*.rbxlx|Roblox XML Model Files (*.rbxmx)|*.rbxmx|Roblox XML Place Files (*.rbxl)|*.rbxl|Roblox XML Model Files (*.rbxm)|*.rbxm";
            if (fileDialog.ShowDialog() == true)
            {
                fileToConvertPath = fileDialog.FileName;
                PlaceSelectedLabel.Content = "File selected: " + fileToConvertPath;
            }
        }

        private void ConvertButton_Click(object sender, RoutedEventArgs e)
        {
            if (isConverting == true) // If you somehow managed to click the Convert button twice
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(fileToConvertPath)) // When you haven't selected a place file
            {
                MessageBox.Show("Please select a model or place you'd like to convert by clicking on 'Browse'", "Cannot convert place", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string[] file = File.ReadAllLines(fileToConvertPath);

            foreach (var line in file)
            {
                if (line.Contains("<roblox!"))
                {
                    MessageBox.Show("Please select a model or place in Roblox XML format.", "Cannot convert place", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            isConverting = true;
            ConvertButton.IsEnabled = false;
            ProgressBar.Value = 0;
            ProgressLabel.Content = "";

            // Ask user where to save the copy of the file
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Roblox XML Place Files (*.rbxl)|*.rbxl|Roblox XML Model Files (*.rbxm)|*.rbxm";
            if (fileDialog.ShowDialog() != true)
            {
                isConverting = false;
                ConvertButton.IsEnabled = true;
                return;
            }
            newFilePath = fileDialog.FileName;
            File.Copy(fileToConvertPath, newFilePath, true);
            // Read file contents
            string fileContents = File.ReadAllText(newFilePath);

            // Search for terrain in place file, necessary for place to even open on old Roblox versions. When it finds its start and end, it removes the terrain part completely.

            int terrainIndex = fileContents.IndexOf("<Item class=\"Terrain\"", StringComparison.Ordinal);
            if (terrainIndex != -1)
            {
                int terrainEndIndex = fileContents.IndexOf("</Item>", terrainIndex, StringComparison.Ordinal);
                if (terrainEndIndex != -1)
                {
                    fileContents = fileContents.Remove(terrainIndex, terrainEndIndex - terrainIndex + 7);

                }
            }

            //Convert TextSize to Legacy FontSize
            if (ChangeTextSizeToFontSizeCheckbox.IsChecked == true)
            {
                int[] FontSizes = new int[10] { 8, 9, 10, 11, 12, 14, 18, 24, 36, 48 };
                Dictionary<int, int> converted = new Dictionary<int, int>();
                Regex rg = new Regex(@"<float name=""TextSize"">(\d{1,3})</float>");
                MatchCollection matchedfloats = rg.Matches(fileContents);

                foreach (Match ItemMatch in matchedfloats)
                {
                    var nearest = FontSizes.OrderBy(x => Math.Abs((long)x - Int32.Parse(ItemMatch.Groups[1].Value))).First();
                    converted[Int32.Parse(ItemMatch.Groups[1].Value)] = nearest;
                }

                foreach (KeyValuePair<int, int> entry in converted)
                {
                    // do something with entry.Value or entry.Key
                    fileContents = fileContents.Replace("<float name=\"TextSize\">" + entry.Key.ToString() + "</float>", "<token name=\"FontSize\">" + Array.IndexOf(FontSizes, entry.Value).ToString() + "</token>");
                }
            }

            // Convert colors from Color3uint8 to BrickColor
            if (ColorCheckbox.IsChecked == true)
            {
                foreach (KeyValuePair<string, string> entry in color3uint8ToBrickColor)
                {
                    fileContents = fileContents.Replace("<Color3uint8 name=\"Color3uint8\">" + entry.Key + "</Color3uint8>", "<int name=\"BrickColor\">" + entry.Value + "</int>");
                }
            }

            //If Union data is turned off, removes union data
            if (UnionCheckbox.IsChecked == false)
            {
                int unionIndex = fileContents.IndexOf("<Item class=\"NonReplicatedCSGDictionaryService\"", StringComparison.Ordinal);
                if (unionIndex != -1)
                {
                    int binaryStringIndex = fileContents.IndexOf("<Item class=\"BinaryStringValue\"", unionIndex, StringComparison.Ordinal);
                    while (binaryStringIndex != -1)
                    {
                        int binaryStringEndIndex = fileContents.IndexOf("</Item>", binaryStringIndex, StringComparison.Ordinal);
                        if (binaryStringEndIndex != -1)
                        {
                            fileContents = fileContents.Remove(binaryStringIndex, binaryStringEndIndex - binaryStringIndex + 7);
                        }
                        binaryStringIndex = fileContents.IndexOf("<Item class=\"BinaryStringValue\"", binaryStringIndex, StringComparison.Ordinal);
                    }
                }
            }

            //Script conversion, removes the weird CDATA stuff if your script is multiline.
            if (ScriptConvertCheckbox.IsChecked == true)
            {
                fileContents = fileContents.Replace("<ProtectedString name=\"Source\"><![CDATA[", "<ProtectedString name=\"Source\">");
                fileContents = fileContents.Replace("]]></ProtectedString>", "</ProtectedString>");
                //Change stuff like quotes to a format old roblox places support
                int scriptStartIndex = fileContents.IndexOf("<ProtectedString name=\"Source\">", StringComparison.Ordinal);
                while (scriptStartIndex != -1)
                {
                    int scriptEndIndex = fileContents.IndexOf("</ProtectedString>", scriptStartIndex, StringComparison.Ordinal);
                    if (scriptEndIndex != -1)
                    {
                        string scriptBeforeContents = fileContents.Substring(scriptStartIndex + 31, scriptEndIndex - scriptStartIndex - 31);
                        if (scriptBeforeContents.Length > 0)
                        {
                            string scriptAfterContents = scriptBeforeContents;
                            scriptAfterContents = scriptAfterContents.Replace("\"", "&quot;");
                            scriptAfterContents = scriptAfterContents.Replace("\'", "&apos;");
                            scriptAfterContents = scriptAfterContents.Replace("<", "&lt;");
                            scriptAfterContents = scriptAfterContents.Replace(">", "&gt;");
                            fileContents = fileContents.Replace(scriptBeforeContents, scriptAfterContents);
                        }
                    }
                    scriptStartIndex = fileContents.IndexOf("<ProtectedString name=\"Source\">", scriptEndIndex, StringComparison.Ordinal);
                }
            }

            //Changes rbxassetid to longer link variant, fixes assets not loading in older clients
            if (ChangeRbxassetidCheckbox.IsChecked == true)
            {
                fileContents = fileContents.Replace("rbxassetid://", "http://www.roblox.com/asset/?id=");
            }

            //Convert folders to models, since old Roblox clients don't support folders and therefore everything inside of folders isn't shown
            if (ConvertFoldersCheckbox.IsChecked == true)
            {
                fileContents = fileContents.Replace("<Item class=\"Folder\"", "<Item class=\"Model\"");
            }

            

            // Write fileContents string to the copy file
            File.WriteAllText(newFilePath, fileContents);
            // Done :D
            ProgressBar.Value = 100;
            ProgressLabel.Content = "Done!";
            MessageBox.Show("Conversion done!", "Conversion status", MessageBoxButton.OK, MessageBoxImage.Information);
            isConverting = false;
            ConvertButton.IsEnabled = true;
        }
    }
}
