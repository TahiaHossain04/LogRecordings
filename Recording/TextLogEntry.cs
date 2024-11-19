// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  15th November 2024
// Date Modified: 16th November 2024
// Description:   An inherited class created to keep track of the entered text logs 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Recording
{
    class TextLogEntry : LogEntry
    {
        // Static variables are inherited from LogEntry

        // Instance variables
        private string textContent;

        //Constructor
        public TextLogEntry()
        {
            count++;
            logID = count; // Assign unique ID
        }

        // Accepts the four parameters
        public TextLogEntry(int wellnessValue, int qualityValue, string notesValue)
        {
                        // Update the static variables
            // Updates the count
            count++;
            if (count == 1) // Checks if its the first entry
            {
                // If yes, then sets firstEntry to the current date and time
                firstEntry = DateTime.Now;
            }
            // If not new entry, updated the latest entry to be the current date and time
            newestEntry = DateTime.Now;


            // Assign values to properties
            logID = count;
            Wellness = wellnessValue;
            Quality = qualityValue;
            Notes = notesValue;
            TextContent = notesValue; // Store notes as the main content
        }

        // Property for text content
        public string TextContent
        {
            get => textContent; set
            {
                // Validate if text content is empty or null
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Text content cannot be empty or whitespace.", nameof(TextContent));
                }
                textContent = value;
            }
        }

        // Override ToString for display in the ListView
        public override string ToString()
        {
            return $"Text Entry {Id} created at {EntryDate}, Wellness: {Wellness}, Quality: {Quality}, Essay: {Notes}";
        }
    }
}

