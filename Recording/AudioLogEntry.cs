// Author:        Tahia Hossain
// File:          Recording (version 3)
// Date Created:  29th November 2024
// Date Modified: 05th December 2024
// Description:   An inherited class created to keep track of the entered audio logs 


using System;
using System.IO;
using System.Windows;

namespace Recording
{
    // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
    [Serializable]
    public class AudioLogEntry : LogEntry
    {
        // Properties
        public FileInfo logFile;

        // Constructors
        public AudioLogEntry()
        {
            count++;
            logID = count;
        }


        // Parameterized Constructor
        public AudioLogEntry(int wellnessValue, int qualityValue, string notesValue, FileInfo filevalue)
        {
            if (string.IsNullOrWhiteSpace(notesValue))
            {
                throw new ArgumentException("Notes cannot be empty or contain only spaces.");
            }

            count++;
            if (count == 1)
            {
                firstEntry = DateTime.Now;
            }
            newestEntry = DateTime.Now;

            logID = count;
            Wellness = wellnessValue;
            Quality = qualityValue;
            Notes = notesValue;
            RecordingFile = filevalue;

            //SaveToFile("AudioEntries");

            // Saves entries to JSON file
            LogEntry.logEntries.Add(this);
            JsonDataHandler.SaveEntries(LogEntry.logEntries);
        }

        public FileInfo RecordingFile
        {
            get => logFile;
            set => logFile = value;
        }

        //private void SaveToFile(string directory)
        //{
        //    try
        //    {
        //        Directory.CreateDirectory(directory);
        //        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
        //        string fileName = Path.Combine(directory, $"{logID}_{timestamp}_entry.wav");


        //        if (logFile != null && logFile.Exists)
        //        {
        //            File.Copy(logFile.FullName, fileName);
        //        }
        //        else
        //        {
        //            throw new ArgumentException("The specified audio file does not exist.");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error saving audio entry: {ex.Message}");
        //        MessageBox.Show($"Error saving audio entry: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        // Returns string
        public override string ToString()
        {
            return $"Audio Entry {Id} created at {EntryDate}, Wellness: {Wellness}, Quality: {Quality}, Essay: {Notes}";
        }
    }
}
