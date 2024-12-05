// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  15th November 2024
// Date Modified: 16th November 2024
// Description:   An inherited class created to keep track of the entered text logs 
using System;
using System.IO;
using System.Windows;

namespace Recording
{
    [Serializable]
    public class TextLogEntry : LogEntry
    {
        private string textContent;

        public TextLogEntry()
        {
            count++;
            logID = count;
        }

        public TextLogEntry(int wellnessValue, int qualityValue, string notesValue)
        {
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
            TextContent = notesValue;

            string directory = "TextEntries";
            Directory.CreateDirectory(directory);
            SaveToFile(directory);

            logEntries.Add(this);
            JsonDataHandler.SaveEntries(logEntries);
        }

        public string TextContent
        {
            get => textContent;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Text content cannot be empty or whitespace.");
                }
                textContent = value;
            }
        }

        private void SaveToFile(string directory)
        {
            try
            {
                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
                string fileName = Path.Combine(directory, $"{logID}_{timestamp}_entry.txt");

                File.WriteAllText(fileName, textContent);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving text entry: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public override string ToString()
        {
            return $"Text Entry {Id} created at {EntryDate}, Wellness: {Wellness}, Quality: {Quality}, Essay: {Notes}";
        }
    }
}
