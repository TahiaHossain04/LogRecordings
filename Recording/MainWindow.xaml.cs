// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  13th November 2024
// Date Modified: 16th November 2024
// Description:   The main wind of the XAML file to store the event handlers for all the
//                buttons and fields on the XAML file.


using System.IO;
using System.Media;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                // Checks if the field has white space or if its null
                if (string.IsNullOrWhiteSpace(textNotes.Text))
                {
                    // If yes then initiates an Argument Exception and executes the catch block by stopping the try block
                    throw new ArgumentException("Notes have been left empty or contain only whitespace.");
                }


                // Create a new LogEntry object
                LogEntry newEntry = new AudioLogEntry(wellnessRating, qualityRating, textNotes.Text, recordingFile);

                //// Update status with the full log entry details
                //UpdateStatus(newEntry.ToString());

                // Add the new entry to the static logEntries list
                LogEntry.logEntries.Add(newEntry);

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
                UpdateStatus($"Text Entry Saved: {newEntry}");

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
                // Catch the ArgumentException thrown when notes are empty or whitespace
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
            firstEntryText.Text = firstEntryTime; // Show first entry time
            newEntryText.Text = latestEntryTime; // Show latest entry time
            entryNumText.Text = entryCount.ToString(); // Show total entries
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
            //if (tabController.SelectedItem == tabSummary)
            //{
            //    UpdateStatus("Viewing Summary");
            //}
            //else if (tabController.SelectedItem == tabList)
            //{
            //    UpdateListDisplay();  // This will update the list of entries on the List tab
            //}
            // WHen tab changed to List Tab, the created log entries are displayed in a list format
            if (tabController.SelectedItem == tabList)  // When the List tab is selected
            {
                // Populates the TextBox with the list of saved entries
                ListLogEntries();
            }
        }

        // Text Log Tab Event Handlers

        // Only for the Text Log Entry Tab
        // Textbox displaying the list of all the created entries in the program
        private void ListLogEntries()
        {
            // Clear the TextBox before adding new entries
            listEntriesTextBox.Clear();

            // Loop through all the saved LogEntry objects and display their IDs and dates
            // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
            foreach (var entry in LogEntry.logEntries)
            {
                // Check if the entry is of type AudioLogEntry or TextLogEntry
                if (entry is AudioLogEntry)
                {
                    // Append as "Audio Log Entry"
                    listEntriesTextBox.AppendText($"Audio Log Entry ID: {entry.Id}, Date: {entry.EntryDate}\n");
                }
                else if (entry is TextLogEntry)
                {
                    // Append as "Text Log Entry"
                    listEntriesTextBox.AppendText($"Text Log Entry ID: {entry.Id}, Date: {entry.EntryDate}\n");
                }
            }
        }

        // For the Text Log Entry Tab
        // Adding functionality to the Save button on the Text Tab to save texts
        private void buttonTextSave_Click(object sender, RoutedEventArgs e)
        {
            string notes = textEssay.Text;

            //// Validate notes before saving
            //if (string.IsNullOrWhiteSpace(notes))
            //{
            //    MessageBox.Show("Cannot save. The text entry field is empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    UpdateStatus("Save failed: Text entry is empty.");
            //    return; // Exit without saving
            //}

            try
            {
                // Check if the notes field is empty or contains only white spaces
                if (string.IsNullOrWhiteSpace(notes))
                {
                    // Reset the field
                    textEssay.Clear();

                    // Throw the Exception (error message box)
                    throw new ArgumentException("Failed Saving. The text entry field is empty or only contains spaces.");
                }


                // Save as a TextLogEntry if validation passes
                LogEntry newEntry = new TextLogEntry(wellnessRating, qualityRating, notes);

                // Add the new entry to the static logEntries list
                LogEntry.logEntries.Add(newEntry);

                // Increment the count of entries
                entryCount++;

                // Reset fields and update UI
                textEssay.Clear();
                comboWellness2.SelectedIndex = 0;
                comboQuality2.SelectedIndex = 0;

                entrySaved = true;
                MessageBox.Show("Text Entry saved successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Update status to reflect the successful save
                UpdateStatus($"Text Entry Saved: {newEntry}");
            }
            catch (ArgumentException ex)
            {
                // Catch any ArgumentException related to text entry being empty or whitespace only
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    throw new ArgumentException("Failed Deletion. The text entry field is empty.");
                }

                // Clear the fields and notify the user
                textEssay.Clear();
                comboWellness2.SelectedIndex = 0;
                comboQuality2.SelectedIndex = 0;

                UpdateStatus("Text entry deleted.");
                MessageBox.Show("Text entry deleted successfully.", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ArgumentException ex)
            {
                // Catch any ArgumentException related to text entry being empty or whitespace only
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                UpdateStatus($"Deletion failed: {ex.Message}");
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
    }
}