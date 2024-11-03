﻿using System.IO;
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

        // Adding functionality to the Record button
        private void buttonRecord_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecording)
            {
                labelRecordText.Content = "Stop";
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

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            // Create a new LogEntry object
            LogEntry newEntry = new LogEntry(wellnessRating, qualityRating, textNotes.Text, recordingFile);
            UpdateStatus(newEntry.ToString());

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

            // Update the summary display
            UpdateSummary();

            // Update buttons
            buttonRecord.IsEnabled = true;
            buttonPlay.IsEnabled = false;
            buttonDelete.IsEnabled = false;
            buttonRecord.IsEnabled = true;
            textNotes.Text = "";

            // https://stackoverflow.com/questions/47222611/c-sharp-combobox-selected-index-1-not-working
            comboWellness.SelectedIndex = -1;
            comboQuality.SelectedIndex = -1;

            // Mark the entry as saved
            entrySaved = true;
            buttonSave.IsEnabled = false; // Disable the save button after saving
        }

        private void UpdateStatus(string status)
        {
            statusState.Content = status;
        }

        private void UpdateSummary()
        {
            // Update the summary text boxes directly
            firstEntryText.Text = firstEntryTime; // Show first entry time
            newEntryText.Text = latestEntryTime; // Show latest entry time
            entryNumText.Text = entryCount.ToString(); // Show total entries
        }


        private void buttonPlay_Click(object sender, RoutedEventArgs e)
        {
            var player = new SoundPlayer(recordingFile.FullName);
            player.Play();
            UpdateStatus("Playing " + recordingFile.FullName + ".");
        }

        private void TabChanged(object sender,RoutedEventArgs e)
        {
            if (tabController.SelectedItem == tabSummary)
            {
                // Update the summary tab fields/
                UpdateStatus("Viewing Summary");
            }
        }

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
            comboWellness.SelectedIndex = -1;
            comboQuality.SelectedIndex = -1;
        }

        private void comboWellness_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboWellness.SelectedItem is ComboBoxItem selectedItem)
            {
                wellnessRating = int.Parse(selectedItem.Content.ToString());
                UpdateStatus($"Wellness/Mood rating set to {wellnessRating}.");
            }
        }

        private void comboQuality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboQuality.SelectedItem is ComboBoxItem selectedItem)
            {
                qualityRating = int.Parse(selectedItem.Content.ToString());
                UpdateStatus($"Quality rating set to {qualityRating}.");
            }
        }
    }
}