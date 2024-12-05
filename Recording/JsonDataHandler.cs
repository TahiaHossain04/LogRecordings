// Author:        Tahia Hossain
// File:          Recording (version 3)
// Date Created:  2nd December 2024
// Date Modified: 5th December 2024
// Description:   JSON Data Class to store and persist data

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace Recording
{
    public static class JsonDataHandler
    {
        // Stores the path to the JSON file in the computer
        private static string filePath = "logEntries.json";

        // Serializes the list into a JSON string 
        // Saves the string to filePath (by writing all text)
        public static void SaveEntries(List<LogEntry> entries)
        {
            try
            {
                // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
                string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving entries: {ex.Message}");
            }
        }

        // To load previous entries from JSON
        public static List<LogEntry> LoadEntries()
        {
            try
            {
                // Checks if the JSON file exists in the path
                if (File.Exists(filePath))
                {
                    // If yes, then reads the file and deserializes it into a list of entries
                    // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<List<LogEntry>>(json);
                }
                return new List<LogEntry>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading entries: {ex.Message}");
                return new List<LogEntry>();
            }
        }

        // To delete a selected entry from the JSON
        // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
        public static void DeleteEntry(LogEntry entry)
        {
            LogEntry.logEntries.Remove(entry);
            // Calls SaveEntries to save the updated list again
            SaveEntries(LogEntry.logEntries);
        }

        // To Edit an existing entry in the list
        public static void EditEntry(LogEntry entry, int wellnessValue, int qualityValue, string notesValue)
        {
            entry.Wellness = wellnessValue;
            entry.Quality = qualityValue;
            entry.Notes = notesValue;

            // Find and replace the entry in the list
            // OpenAI. (2024). ChatGPT [Large language model]. https://chatgpt.com
            var entryIndex = LogEntry.logEntries.FindIndex(e => e.Id == entry.Id);
            if (entryIndex >= 0)
            {
                LogEntry.logEntries[entryIndex] = entry;  // Replace the entry with updated values
            }

            // Save the updated list back to JSON
            SaveEntries(LogEntry.logEntries);
        }

    }
}
