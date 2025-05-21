using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Timers;
using Avalonia.Media;
using Avalonia.Threading;
using Newtonsoft.Json.Linq;
using Avalonia;
using Avalonia.Layout;
using Database;
using System.Text.Json;
using JsonException = Newtonsoft.Json.JsonException;

namespace ModelUI.Views
{
    public partial class MainView : UserControl
    {

        private WindowMain mainWindow;
        private List<generalDataClass> dbData = new();
        private Dictionary<int, (TextBox jsonBox, TextBox anyDataBox)> editableFields = new();


        public MainView()
        {
            InitializeComponent();
        }

        public void MainContext(WindowMain MainWindow)
        {
            mainWindow = MainWindow;
            PopulateDatabaseFields();
        }
        /// <summary>
        /// Populates the database fields in the UI.
        /// Is it ugly, yes. But it works.
        /// </summary>
        public void PopulateDatabaseFields()
        {
            // Clear only the grid area, not the Save button
            dbGridPanel.Children.Clear();
            editableFields.Clear();

            dbData = mainWindow.databaseHelper.GetAllDataFromTable("GeneralSettings");

            // Add header row
            var headerGrid = new Grid
            {
                ColumnDefinitions =
        {
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star)
        },
                Background = Brushes.LightGray
            };
            headerGrid.Children.Add(new TextBlock { Text = "Name", FontWeight = FontWeight.Bold });
            headerGrid.Children.Add(new TextBlock { Text = "JSON", FontWeight = FontWeight.Bold, Margin = new Thickness(4, 0, 0, 0) });
            headerGrid.Children.Add(new TextBlock { Text = "AnyData", FontWeight = FontWeight.Bold, Margin = new Thickness(4, 0, 0, 0) });

            Grid.SetColumn(headerGrid.Children[1], 1);
            Grid.SetColumn(headerGrid.Children[2], 2);
            dbGridPanel.Children.Add(headerGrid);

            // Add each row
            foreach (var entry in dbData)
            {
                var grid = new Grid
                {
                    Margin = new Thickness(2),
                    ColumnDefinitions =
            {
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star)
            }
                };

                var nameBlock = new TextBlock { Text = entry.Name, VerticalAlignment = VerticalAlignment.Center };

                var jsonBox = new TextBox
                {
                    Text = entry.JSON,
                    AcceptsReturn = true,
                    TextWrapping = TextWrapping.Wrap,
                    Height = 60
                };

                var anyDataBox = new TextBox
                {
                    Text = entry.AnyData,
                    Height = 30
                };

                grid.Children.Add(nameBlock);
                grid.Children.Add(jsonBox);
                grid.Children.Add(anyDataBox);

                Grid.SetColumn(nameBlock, 0);
                Grid.SetColumn(jsonBox, 1);
                Grid.SetColumn(anyDataBox, 2);

                dbGridPanel.Children.Add(grid);
                editableFields[entry.ID] = (jsonBox, anyDataBox);
            }
        }
        public bool TryParseJson(string jsonInput, out JToken parsedJson)
        {
            parsedJson = null;
            try
            {
                parsedJson = JToken.Parse(jsonInput);
                return true;
            }
            catch (JsonReaderException)
            {
                // Invalid JSON
                return false;
            }
        }

        private void SaveToDB_Button_OnClick(object? sender, RoutedEventArgs e)
        {
            bool hasErrors = false;

            foreach (var kvp in editableFields)
            {
                var id = kvp.Key;
                var jsonBox = kvp.Value.jsonBox;
                var anyDataBox = kvp.Value.anyDataBox;

                string jsonInput = jsonBox.Text;

                if (!TryParseJson(jsonInput, out var _))
                {
                    hasErrors = true;
                    // Show error UI, e.g. red border
                    jsonBox.BorderBrush = Brushes.Red;
                    jsonBox.BorderThickness = new Thickness(2);
                }
                else
                {
                    // Clear error UI if valid
                    jsonBox.ClearValue(TextBox.BorderBrushProperty);
                    jsonBox.ClearValue(TextBox.BorderThicknessProperty);

                    // Save valid data
                    var item = dbData.FirstOrDefault(x => x.ID == id);
                    if (item != null)
                    {
                        item.JSON = jsonInput;
                        item.AnyData = anyDataBox.Text;
                        mainWindow.databaseHelper.InsertUpdateInTable(item, "GeneralSettings");
                    }
                }
            }

            if (hasErrors)
            {
                // Optionally show a message to user about invalid JSON fields
                //MessageBox("Some JSON fields are invalid. Please correct them.");
            }
            else
            {
                // All good, maybe notify success
            }
        }

        private async void MessageBox(string message)
        {
            //Message box is in paid version of Actipro
            //will have to make a workaround
        }


    }
}
