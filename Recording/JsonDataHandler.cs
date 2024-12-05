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
        private static string filePath = "logEntries.json";

        public static void SaveEntries(List<LogEntry> entries)
        {
            try
            {
                string json = JsonSerializer.Serialize(entries, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving entries: {ex.Message}");
            }
        }

        public static List<LogEntry> LoadEntries()
        {
            try
            {
                if (File.Exists(filePath))
                {
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

        public static void DeleteEntry(LogEntry entry)
        {
            LogEntry.logEntries.Remove(entry);
            SaveEntries(LogEntry.logEntries);
        }

        public static void EditEntry(LogEntry entry, int wellnessValue, int qualityValue, string notesValue)
        {
            entry.Wellness = wellnessValue;
            entry.Quality = qualityValue;
            entry.Notes = notesValue;

            // Find and replace the entry in the list
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
