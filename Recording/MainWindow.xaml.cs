// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  13th November 2024
// Date Modified: 16th November 2024
// Description:   The main wind of the XAML file to store the event handlers for all the
//                buttons and fields on the XAML file.

using System.IO;
using System.Media;
using System.Windows;
using System.Windows.Controls;

namespace Recording
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool isRecording = false;
        FileInfo recordingFile;
        // Variables to store the combo box value selected by user
        int wellnessRating;
        int qualityRating;
        // Track whether an entry has been saved
        private bool entrySaved = false;
        // Variables to track the summary information
        private int entryCount = 0; // Total number of entries
        private string firstEntryTime = ""; // Time of the first entry
        private string latestEntryTime = ""; // Time of the latest entry

        public MainWindow()
        {
            InitializeComponent();
            // Bind the ListView to the static List of LogEntries
            foreach (var item in LogEntry.logEntries)
            {
                listViewEntries.Items.Add(item);

            }
            //listViewEntries.ItemsSource = LogEntry.List;
            listViewEntries.Items.Add("Great");

            LogEntry.logEntries = JsonDataHandler.LoadEntries();

        }

        // Text Log Tab Event Handlers

        // Only for the Audio Log Entry Tab
        // Adding functionality to the Record button
        private void buttonRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecording)
            {
                labelRecordText.Content = "_Stop";
                isRecording = true;
                RecordWav.StartRecording();
                buttonSave.IsEnabled = false; // Disable save button while recording
                buttonPlay.IsEnabled = false;
                comboWellness.SelectedIndex = 0;
                comboQuality.SelectedIndex = 0;
                UpdateStatus("Recording started at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ".");
            }
            else
            {
                labelRecordText.Content = "_Record";
                isRecording = false;
                recordingFile = RecordWav.EndRecording();
                buttonSave.IsEnabled = true; // Enable save button after recording
                buttonPlay.IsEnabled = true;
                buttonDelete.IsEnabled = true;
                UpdateStatus("Recording completed and saved to " + recordingFile.FullName);
                entrySaved = false; // Reset the entry saved state when recording a new entry
            }
        }

        // Assigning the selected combo box item to a variable to be called later
        private void comboWellness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboWellness.SelectedItem is ComboBoxItem selectedItem)
            {
                // Gets the content (text) of the selected combo box item
                // Converts the content to a string, just in case it’s not already a string.
                // Parses the string content into an integer, which is then assigned to qualityRating
                wellnessRating = int.Parse(selectedItem.Content.ToString());
            }
        }

        // Assigning the selected combo box item to a variable to be called later
        private void comboQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboQuality.SelectedItem is ComboBoxItem selectedItem)
            {
                qualityRating = int.Parse(selectedItem.Content.ToString());
            }
        }

        // For the Audio Log Entry Tab
        // Adding functionality to the Save button
        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Validate the notes field to ensure it's not empty or whitespace
                if (string.IsNullOrWhiteSpace(textNotes.Text))
                {
                    throw new ArgumentException("Notes have been left empty or contain only whitespace.");
                }

                // Create a new AudioLogEntry object
                LogEntry audioLogEntry = new AudioLogEntry(wellnessRating, qualityRating, textNotes.Text, recordingFile);

                // 
                JsonDataHandler.SaveEntries(LogEntry.logEntries);

                // Clear the ListView and add all entries
                UpdateListView();

                // Increment the count of entries
                entryCount++;

                // Get the current date and time
                string currentTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Check if it's the first entry
                if (entryCount == 1)
                {
                    firstEntryTime = currentTime; // Set the first entry time
                }

                latestEntryTime = currentTime; // Update the latest entry time

                MessageBox.Show("Audio Entry saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Update status to reflect the successful save
                UpdateStatus($"{audioLogEntry}");

                // Update the summary display
                UpdateSummary();

                // Update buttons
                buttonRecord.IsEnabled = true;
                buttonPlay.IsEnabled = false;
                buttonDelete.IsEnabled = false;
                textNotes.Text = "";

                comboWellness.SelectedIndex = 0;
                comboQuality.SelectedIndex = 0;

                // Mark the entry as saved
                entrySaved = true;
                buttonSave.IsEnabled = false; // Disable the save button after saving
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Warning: {ex.Message}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        // Updating status bar of the program
        private void UpdateStatus(string status)
        {
            statusState.Content = status;
        }

        // For the Audio Log Entries only
        // Updating summary tab's textboxes
        private void UpdateSummary()
        {
            // Update the summary text boxes directly
            firstEntryText.Text = LogEntry.FirstEntry.ToString("yyy-MM-dd HH:mm:ss"); // Show first entry time
            newEntryText.Text = LogEntry.NewestEntry.ToString("yyy-MM-dd HH:mm:ss"); // Show latest entry time
            entryNumText.Text = LogEntry.Count.ToString(); // Show total entries
        }

        // Only for the Audio Log Entry Tab
        // Adding functionality to the Play button
        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            var player = new SoundPlayer(recordingFile.FullName);
            player.Play();
            UpdateStatus("Playing " + recordingFile.FullName + ".");
        }


        // For the Audio Log Entry Tab
        // Adding functionality to the Delete button
        private void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            // Delete the recording file
            recordingFile.Delete();
            UpdateStatus("Recording deleted from " + recordingFile.FullName);

            // Reset buttons
            buttonRecord.IsEnabled = true;
            buttonPlay.IsEnabled = false;
            buttonDelete.IsEnabled = false;
            buttonSave.IsEnabled = false;
            textNotes.Text = "";
            comboWellness.SelectedIndex = 0;
            comboQuality.SelectedIndex = 0;
        }

        // For the Audio Log Entry Tab
        // Exit Button
        private void buttonExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Changing of tabs
        private void TabChanged(object sender, RoutedEventArgs e)
        {
            //if (tabController.SelectedItem == tabAudioEntry)
            //{
            //    //Update the summary tab fields
            //    UpdateStatus("Viewing Entry tab");
            //}
            //else if (tabController.SelectedItem == tabSummary)
            //{

            //    UpdateStatus("Viewing Summary tab");

            //}
            //else if (tabController.SelectedItem == tabTextEntry)
            //{
            //    UpdateStatus("Viewing File log Entry tab");
            //}

            //else if (tabController.SelectedItem == tabList)
            //{
            //    //listViewEntries.ItemsSource = LogEntry.Entries; 
            //    UpdateStatus("Viewing List tab");
            //}
            if (tabController.SelectedItem == tabSummary)
            {
                UpdateSummary();
            }
        }


        // For the Text Log Entry Tab
        // Adding functionality to the Save button on the Text Tab to save texts
        private void buttonTextSave_Click(object sender, RoutedEventArgs e)
        {
            string notes = textEssay.Text;

            try
            {
                // Check if the notes field is empty or contains only white spaces
                if (string.IsNullOrWhiteSpace(notes))
                {
                    textEssay.Clear();
                    throw new ArgumentException("Failed Saving. The text entry field is empty or only contains spaces.");
                }

                // Save as a TextLogEntry if validation passes
                LogEntry newEntry = new TextLogEntry(wellnessRating, qualityRating, notes);

                //
                JsonDataHandler.SaveEntries(LogEntry.logEntries);

                // Clear the ListView and add all entries
                UpdateListView();

                // Increment the count of entries
                entryCount++;

                // Reset fields and update UI
                textEssay.Clear();
                comboWellness2.SelectedIndex = 0;
                comboQuality2.SelectedIndex = 0;

                entrySaved = true;
                MessageBox.Show("Text Entry saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Update status to reflect the successful save
                UpdateStatus($"{newEntry}");
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateListView()
        {
            listViewEntries.Items.Clear();
            foreach (var item in LogEntry.logEntries)
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = new { Id = item.Id, EntryDate = item.EntryDate, Notes = item.Notes, Wellness = item.Wellness, Quality = item.Quality };
                listViewEntries.Items.Add(listViewItem);
            }
        }

        // For the Text Log Entry Tab
        // Adding functionality to the Delete button on the Text Tab to delete texts
        private void buttonTextDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string notes = textEssay.Text;

                // Validate text field before deleting
                if (string.IsNullOrWhiteSpace(notes))
                {
                    // Reset the field
                    textEssay.Clear();

                    // Throw the Exception (error message box)
                    //throw new ArgumentException("Failed Deletion. The text entry field is empty.");
                }

                // Clear the fields and notify the user
                textEssay.Clear();
                comboWellness2.SelectedIndex = 0;
                comboQuality2.SelectedIndex = 0;

                //UpdateStatus("Text entry deleted.");
                MessageBox.Show("Text entry deleted successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException ex)
            {
                // Catch any ArgumentException related to text entry being empty or whitespace only
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //UpdateStatus($"Deletion failed: {ex.Message}");
            }
        }

        // For the Text Log Entry Tab
        // Assigning the selected combo box item to a variable to be called later
        private void comboWellness2_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            // comboWellness.SelectedItem --> Accesses the currently selected item in the combo box
            // ensures the selected item is of type ComboBoxItem.
            // If it is, it assigns it to the variable selectedItem.
            if (comboWellness2.SelectedItem is ComboBoxItem selectedItem)
            {
                // Gets the content (text) of the selected combo box item
                // Converts the content to a string, just in case it’s not already a string.
                // Parses the string content into an integer, which is then assigned to qualityRating
                wellnessRating = int.Parse(selectedItem.Content.ToString());
            }
        }

        // For the Text Log Entry Tab
        // Assigning the selected combo box item to a variable to be called later
        private void comboQuality2_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            if (comboQuality2.SelectedItem is ComboBoxItem selectedItem)
            {
                qualityRating = int.Parse(selectedItem.Content.ToString());
            }
        }

        private void buttonTextEntryDelete_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonSummaryExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buttonListExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //------------------------------------------------

        private LogEntry selectedEntry;

        private void ListViewEntries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listViewEntries.SelectedItem != null)
            {
                var selectedItem = listViewEntries.SelectedItem as ListViewItem;
                if (selectedItem != null)
                {
                    selectedEntry = selectedItem.Content as LogEntry;
                }
            }
            else
            {
                selectedEntry = null; // In case nothing is selected
            }
        }



        private void buttonListEdit_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEntry != null)
            {
                try
                {
                    // Check if the selected entry is an AudioLogEntry or TextLogEntry
                    if (selectedEntry is AudioLogEntry audioEntry)
                    {
                        // Navigate to the Audio Entry tab
                        tabController.SelectedItem = tabAudioEntry;

                        // Populate the fields with the selected entry's data
                        comboWellness.SelectedItem = audioEntry.Wellness.ToString();
                        comboQuality.SelectedItem = audioEntry.Quality.ToString();
                        textNotes.Text = audioEntry.Notes;

                        // Save the changes when the save button is clicked
                        buttonSave.Click += (s, args) =>
                        {
                            int newWellnessValue = int.Parse(((ComboBoxItem)comboWellness.SelectedItem).Content.ToString());
                            int newQualityValue = int.Parse(((ComboBoxItem)comboQuality.SelectedItem).Content.ToString());
                            string newNotesValue = textNotes.Text;

                            JsonDataHandler.EditEntry(audioEntry, newWellnessValue, newQualityValue, newNotesValue);
                            UpdateListView();
                            MessageBox.Show("Audio Entry edited successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        };
                    }
                    else if (selectedEntry is TextLogEntry textEntry)
                    {
                        // Navigate to the Text Entry tab
                        tabController.SelectedItem = tabTextEntry;

                        // Populate the fields with the selected entry's data
                        comboWellness2.SelectedItem = textEntry.Wellness.ToString();
                        comboQuality2.SelectedItem = textEntry.Quality.ToString();
                        textEssay.Text = textEntry.Notes;

                        // Save the changes when the save button is clicked
                        buttonTextSave.Click += (s, args) =>
                        {
                            int newWellnessValue = int.Parse(((ComboBoxItem)comboWellness2.SelectedItem).Content.ToString());
                            int newQualityValue = int.Parse(((ComboBoxItem)comboQuality2.SelectedItem).Content.ToString());
                            string newNotesValue = textEssay.Text;

                            JsonDataHandler.EditEntry(textEntry, newWellnessValue, newQualityValue, newNotesValue);
                            UpdateListView();
                            MessageBox.Show("Text Entry edited successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        };
                    }
                }
                catch (FormatException ex)
                {
                    MessageBox.Show($"Error parsing input: {ex.Message}", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("No entry selected for editing.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }





        private void buttonListDelete_Click(object sender, RoutedEventArgs e)
        {
            if (selectedEntry != null)
            {
                JsonDataHandler.DeleteEntry(selectedEntry);
                UpdateListView();
                MessageBox.Show("Entry deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No entry selected for deletion.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void DeleteAudioEntryFromFile(AudioLogEntry audioEntry)
        {

        }

        private void DeleteTextEntryFromFile(TextLogEntry textEntry)
        {

        }
    }
}