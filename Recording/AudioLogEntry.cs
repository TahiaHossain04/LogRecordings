// Author:        Tahia Hossain
// File:          Recording (version 2)
// Date Created:  13th November 2024
// Date Modified: 16th November 2024
// Description:   An inherited class created to keep track of the entered audio logs 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Recording
{
    class AudioLogEntry : LogEntry
    {
        // Static variables 
        // All inherited

        // Instance variable
        private FileInfo logFile;

        // Constructors

        // Default sets the count and id values
        // Increments the count each time a LogEntry is created and assigns a unique ID
        public AudioLogEntry()
        {
            count++;
            logID = count;
        }

        // Accepts the four parameters
        public AudioLogEntry(int wellnessValue, int qualityValue, string notesValue, FileInfo filevalue)
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

            // Assigns values to these five parameters usign properties
            logID = count;
            Wellness = wellnessValue;
            Quality = qualityValue;
            Notes = notesValue;
            RecordingFile = filevalue;
        }

        // Properties
        // Everything is inherited

        public FileInfo RecordingFile
        {
            get => logFile; set
            {
                logFile = value;
            }
        }


        // Displays a log entry as a string
        // Logs entry as a string
        // Has to be public, why?
        public override string ToString()
        {
            return base.ToString() + " -Audio";
        }
    }
}