// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  15th November 2024
// Date Modified: 16th November 2024
// Description:   An inherited class created to keep track of the entered text logs 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recording
{
    internal class TextLogEntry : LogEntry
    {
        // Static variables are inherited from LogEntry

        // Instance variables
        private string textContent;

        // Default constructor
        public TextLogEntry()
        {
            count++;
            logID = count; // Assign unique ID
        }

        // Constructor with parameters
        public TextLogEntry(int wellnessValue, int qualityValue, string notesValue)
        {
            count++; // Increment the count
            if (count == 1)
            {
                firstEntry = DateTime.Now; // Set first entry time if it's the first entry
            }
            newestEntry = DateTime.Now; // Update latest entry time

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
                textContent = value;
            }
        }

        // Override ToString for display in the ListView
        public override string ToString()
        {
            return "Text " + base.ToString();
        }
    }
}

