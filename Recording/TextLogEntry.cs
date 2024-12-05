// Author:        Tahia Hossain
// File:          Recording (version 3)
// Date Created:  29th November 2024
// Date Modified: 05th December 2024
// Description:   An inherited class created to keep track of the entered text logs 


using System;
using System.IO;
using System.Windows;

namespace Recording
    
{
    // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
    [Serializable]
    public class TextLogEntry : LogEntry
    {
        // Properties
        private string textContent;

        // Constructor
        public TextLogEntry()
        {
            count++;
            logID = count;
        }

        // Parameterized Constructor
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

            //// Creates a dictionary and saves the entry to a text entry
            //string directory = "TextEntries";
            //Directory.CreateDirectory(directory);
            //SaveToFile(directory);

            LogEntry.logEntries.Add(this);
            // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
            // Adds the entry to the log list and saves all entries to JSON
            JsonDataHandler.SaveEntries(LogEntry.logEntries);
        }

        // To get the content of the text and store it
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

        //private void SaveToFile(string directory)
        //{
        //    try
        //    {
        //        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmssfff");
        //        string fileName = Path.Combine(directory, $"{logID}_{timestamp}_entry.txt");

        //        File.WriteAllText(fileName, textContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Error saving text entry: {ex.Message}", "Save Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}


        // Returns formatted string
        public override string ToString()
        {
            return $"Text Entry {Id} created at {EntryDate}, Wellness: {Wellness}, Quality: {Quality}, Essay: {Notes}";
        }
    }
}
