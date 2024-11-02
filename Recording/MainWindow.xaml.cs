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
        int wellnessRating;
        int qualityRating;


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
                buttonSave.IsEnabled = false;
                buttonPlay.IsEnabled = false;
                UpdateStatus("Recording started at " + DateTime.Now.ToString("yyyy-MM-dd") + ".");
            }
            else
            {
                labelRecordText.Content = "_Record";
                isRecording = false;
                recordingFile = RecordWav.EndRecording();
                buttonSave.IsEnabled = true;
                buttonPlay.IsEnabled = true;
                buttonDelete.IsEnabled = true;
                UpdateStatus("Recording completed and saved to " + recordingFile.FullName);
            }
        }

        private void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            LogEntry newEntry = new LogEntry(wellnessRating,qualityRating,textNotes.Text,recordingFile);
            UpdateStatus(newEntry.ToString());
            buttonRecord.IsEnabled = true;
            buttonPlay.IsEnabled = true;
        }

        private void UpdateStatus(string status)
        {
            statusState.Content = status;
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
            if (recordingFile != null && recordingFile.Exists)
                {

                    // Delete the recording file
                    recordingFile.Delete();
                    UpdateStatus("Recording deleted from " + recordingFile.FullName);

                    // Reset buttons
                    buttonRecord.IsEnabled = true;
                    buttonPlay.IsEnabled = false;
                    buttonDelete.IsEnabled = false;
                    buttonSave.IsEnabled = false;

                    // Clear the recording file reference
                    recordingFile = null;
            }
            else
            {
                MessageBox.Show("No recording to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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